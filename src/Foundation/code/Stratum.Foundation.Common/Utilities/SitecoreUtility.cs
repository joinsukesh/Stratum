
namespace Stratum.Foundation.Common.Utilities
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Data.Templates;
    using Sitecore.Mvc.Presentation;
    using Sitecore.SecurityModel;
    using Sitecore.Xml;
    using Stratum.Foundation.Common.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Xml;

    public class SitecoreUtility
    {
        /// <summary>
        /// returns true if the page is being viewed in experience editor mode
        /// </summary>
        /// <returns></returns>
        public static bool IsExperienceEditorMode()
        {
            return Context.PageMode.IsExperienceEditor || Context.PageMode.IsExperienceEditorEditing;
        }

        /// <summary>
        /// returns true if the page is being viewed in preview mode
        /// </summary>
        /// <returns></returns>
        public static bool IsPreviewMode()
        {
            return Context.PageMode.IsPreview;
        }

        public static Item GetItem(ID id, string database = "")
        {
            return (id.IsNull || id.IsGlobalNullId) ? null : GetItem(id.ToString(), database);
        }

        public static Item GetItem(string itemPathOrId, string database = "")
        {
            Item item = null;

            if (!string.IsNullOrWhiteSpace(itemPathOrId))
            {
                Database db = string.IsNullOrWhiteSpace(database) ? Sitecore.Context.Database : Factory.GetDatabase(database);

                if (db != null)
                {
                    using (new SecurityDisabler())
                    {
                        item = db.GetItem(itemPathOrId);
                    }
                }
            }

            return item;
        }

        public static Database GetDatabase(string databaseName)
        {
            return Factory.GetDatabase(databaseName);
        }

        /// <summary>
        /// gets the datasource of the current rendering
        /// </summary>
        /// <returns></returns>
        public static Item GetRenderingDatasourceItem()
        {
            if (RenderingContext.CurrentOrNull != null)
            {
                ///get the datasource assigned to the rendering in presentation details
                string datasourceItemPathOrId = RenderingContext.CurrentOrNull.Rendering.DataSource;

                ///get the datasource set as default in the rendering itself in its 'Data source' field.
                if (string.IsNullOrWhiteSpace(datasourceItemPathOrId))
                {
                    datasourceItemPathOrId = RenderingContext.CurrentOrNull.Rendering.RenderingItem.DataSource;
                }

                return GetItem(datasourceItemPathOrId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// get the active items from a multilist field and return as list of specified class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contextItem"></param>
        /// <param name="multilistFieldID"></param>
        /// <param name="isActiveFieldID">This is the ID of the 'Is Active' field of the featured item</param>
        /// <returns></returns>
        public static List<T> GetActiveItemsFromMultilistField<T>(Item contextItem, ID multilistFieldId, ID isActiveFieldId) where T : class
        {
            List<T> activeItems = new List<T>();
            List<Item> selectedItems = null;

            ///get the selected items selected in the multilist field
            selectedItems = contextItem.GetMultilistFieldItems(multilistFieldId)
                    .Where(x => isActiveFieldId.IsNull || isActiveFieldId.IsGlobalNullId || x.Fields[isActiveFieldId].Value == CommonConstants.One).ToList();

            if (selectedItems != null && selectedItems.Count > 0)
            {
                foreach (Item item in selectedItems)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T), args: item);

                    if (instance != null)
                    {
                        activeItems.Add(instance);
                    }
                }
            }

            return activeItems;
        }

        public static string GetContextDomainName()
        {
            return Sitecore.Context.Domain.Name;
        }

        /// <summary>
        /// get the name value collection of config factory defined in the patch config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentNodeName"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static List<T> GetSitecoreConfigFactoryCollectionProperties<T>(string parentNodeName, string childNodeName) where T : new()
        {
            /// define return list
            List<T> lst = new List<T>();

            ///get member names
            T elem = new T();
            List<string> lstMemberNames = HelperUtility.GetClassPropertyNames(elem);

            if (lstMemberNames != null && lstMemberNames.Count > 0)
            {
                ///Read the configuration nodes
                foreach (XmlNode node in Factory.GetConfigNodes(parentNodeName + "/" + childNodeName))
                {
                    elem = new T();

                    ///Create a element of this type
                    foreach (string memberName in lstMemberNames)
                    {
                        elem.GetType().GetProperty(memberName).SetValue(elem, XmlUtil.GetAttribute(memberName, node), null);
                    }

                    lst.Add(elem);
                }
            }

            /// return the list
            return lst;
        }

        public static List<T> GetClassObjectsFromItems<T>(List<Item> lstItems) where T : class
        {
            List<T> activeItems = new List<T>();

            if (lstItems != null && lstItems.Count > 0)
            {
                foreach (Item item in lstItems)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T), args: item);

                    if (instance != null)
                    {
                        activeItems.Add(instance);
                    }
                }
            }

            return activeItems;
        }

        public static string GetRenderingParameter(string rederingParameterName)
        {
            string renderingParamResult = string.Empty;
            if (RenderingContext.CurrentOrNull != null && RenderingContext.CurrentOrNull.Rendering.Parameters.Contains(rederingParameterName))
            {
                renderingParamResult = RenderingContext.CurrentOrNull.Rendering.Parameters[rederingParameterName];
            }
            return renderingParamResult;
        }

        public static string GetRenderingParameter(ID renderingParametersTemplateID, ID renderingParametersTemplateFieldID)
        {
            string parametersTemplateFieldNameInRendering = CommonConstants.ParametersTemplate;
            string renderingParameters = string.Empty;
            Rendering currentRendering = RenderingContext.Current.Rendering;

            if (currentRendering != null)
            {
                Item currentRenderingItem = GetItem(System.Convert.ToString(currentRendering.RenderingItem.ID));

                if (currentRenderingItem != null && currentRenderingItem.Fields[parametersTemplateFieldNameInRendering] != null &&
                    currentRenderingItem.Fields[parametersTemplateFieldNameInRendering].Value == System.Convert.ToString(renderingParametersTemplateID))
                {
                    Template template = TemplateManager.GetTemplate(renderingParametersTemplateID, Context.Database);
                    TemplateField templateField = template.GetFields(true).Where(x => x.ID == renderingParametersTemplateFieldID).FirstOrDefault();
                    renderingParameters = templateField != null ? currentRendering.Parameters[templateField.Name] : string.Empty;
                }
            }

            return renderingParameters;
        }

        /// <summary>
        /// gets the key value of a dictionary item
        /// </summary>
        /// <param name="dictionaryEntryItemPathOrId"></param>
        /// <returns></returns>
        public static string GetDictionaryKeyValue(string dictionaryEntryItemPathOrId)
        {
            string dictionaryValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(dictionaryEntryItemPathOrId))
            {
                Item dictionaryItem = GetItem(dictionaryEntryItemPathOrId);

                if (dictionaryItem != null)
                {
                    dictionaryValue = dictionaryItem.GetDictionaryKey();
                }
            }

            return dictionaryValue;
        }

        /// <summary>
        /// gets the key value of a dictionary item
        /// </summary>
        /// <param name="dictionaryEntryItemPathOrId"></param>
        /// <returns></returns>
        public static string GetDictionaryPhraseValue(string dictionaryEntryItemPathOrId)
        {
            string dictionaryValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(dictionaryEntryItemPathOrId))
            {
                Item dictionaryItem = GetItem(dictionaryEntryItemPathOrId);

                if (dictionaryItem != null)
                {
                    dictionaryValue = dictionaryItem.GetDictionaryPhrase();
                }
            }

            return dictionaryValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromEmail"></param>
        /// <param name="toEmails">Comma separated To Emails</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="ccEmails">Comma separated CC Emails</param>
        /// <param name="bccEmails">Comma separated BCC Emails</param>
        public static void SendEmail(string fromEmail, string toEmails, string subject, string body,
            bool isBodyHtml, string ccEmails, string bccEmails)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;

            string[] toEmailIds = toEmails.Split(CommonConstants.Characters.Comma);

            foreach (string email in toEmailIds)
            {
                mailMessage.To.Add(new MailAddress(email));
            }

            string[] ccEmailIds = ccEmails.Split(CommonConstants.Characters.Comma);

            foreach (string email in ccEmailIds)
            {
                mailMessage.CC.Add(new MailAddress(email));
            }

            string[] bccEmailIds = bccEmails.Split(CommonConstants.Characters.Comma);

            foreach (string email in bccEmailIds)
            {
                mailMessage.Bcc.Add(new MailAddress(email));
            }

            MainUtil.SendMail(mailMessage);
        }        
    }
}
