
namespace Stratum.Foundation.Forms.Repositories
{
    using Sitecore.Data.Items;
    using Sitecore.DependencyInjection;
    using Sitecore.ExperienceForms.Data;
    using Sitecore.ExperienceForms.Data.Entities;
    using Stratum.Foundation.Common.Utilities;
    using Stratum.Foundation.Forms;
    using Stratum.Foundation.Forms.Models;
    using System;
    using System.Collections.Generic;

    public class SitecoreFormsRepository : ISitecoreFormsRepository
    {
        private IFormDataProvider _dataProvider = (IFormDataProvider)ServiceLocator.ServiceProvider.GetService(typeof(IFormDataProvider));
        public SitecoreFormsRepository(IFormDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SaveDataInSitecoreFormsDb(Guid formId, List<KeyValuePair<string, string>> formFieldNameValues, Guid? userId)
        {
            FormEntry formEntry = new FormEntry()
            {
                Created = DateTime.Now,
                FormItemId = formId,
                FormEntryId = Guid.NewGuid(),
                Fields = new List<FieldData>(),
                ContactId = userId
            };

            if (formFieldNameValues != null && formFieldNameValues.Count > 0)
            {
                List<SitecoreFormFieldDataModel> lstSitecoreFormFieldsData = new List<SitecoreFormFieldDataModel>();
                SitecoreFormFieldDataModel sffdm = null;
                foreach (KeyValuePair<string, string> kvp in formFieldNameValues)
                {
                    sffdm = GetFormFieldDataModel(kvp.Key, kvp.Value);

                    if (sffdm != null)
                    {
                        lstSitecoreFormFieldsData.Add(sffdm);
                    }
                }

                if (lstSitecoreFormFieldsData.Count > 0)
                {
                    foreach (SitecoreFormFieldDataModel fieldData in lstSitecoreFormFieldsData)
                    {
                        AddFormFieldData(formEntry, fieldData);
                    }
                }
            }

            _dataProvider.CreateEntry(formEntry);
        }

        protected static void AddFormFieldData(FormEntry formEntry, SitecoreFormFieldDataModel postedField)
        {
            FieldData fieldData = new FieldData()
            {
                FieldDataId = Guid.NewGuid(),
                FieldItemId = postedField.FormFieldId,
                FormEntryId = formEntry.FormEntryId,
                FieldName = postedField.FormFieldName,
                Value = postedField.FormFieldValue,
                ValueType = Constants.FieldValueType
            };

            formEntry.Fields.Add(fieldData);
        }

        protected SitecoreFormFieldDataModel GetFormFieldDataModel(string fieldId, string fieldValue)
        {
            SitecoreFormFieldDataModel formFieldData = new SitecoreFormFieldDataModel();

            if (!string.IsNullOrWhiteSpace(fieldId))
            {
                Item fieldItem = SitecoreUtility.GetItem(fieldId);

                if (fieldItem != null)
                {
                    formFieldData = new SitecoreFormFieldDataModel()
                    {
                        FormFieldId = fieldItem.ID.ToGuid(),
                        FormFieldName = fieldItem.Name,
                        FormFieldValue = fieldValue
                    };
                }
            }
            return formFieldData;
        }
    }
}