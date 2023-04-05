
namespace Stratum.Foundation.Common.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.CES.GeoIp.Core;
    using Sitecore.CES.GeoIp.Core.Model;
    using Sitecore.Data;
    using Sitecore.DependencyInjection;
    using Stratum.Foundation.Common.Models;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.IO;
    using System.Net;

    public class CommonService
    {
        /// <summary>
        /// First try to fetch the IP using Sitecore GeoIp service
        /// If it doesn't return any value, then try to fetch using user IP
        /// </summary>
        /// <returns></returns>
        public string GetUserPostalCode()
        {
            string zip = string.Empty;
            ContactLocation contactLocation = GetUserContactLocation();
            zip = contactLocation != null ? contactLocation.PostalCode : string.Empty;

            if (string.IsNullOrWhiteSpace(zip))
            {
                zip = GetZipByUserIp();
            }

            return zip;
        }

        public ContactLocation GetUserContactLocation()
        {
            return Sitecore.Analytics.Tracker.Current?.Interaction?.GeoData;
        }

        public string GetZipByUserIp()
        {
            string zip = string.Empty;
            try
            {
                byte[] ipArray = Sitecore.Analytics.Tracker.Current?.Interaction?.Ip;

                if (ipArray != null && ipArray.Length > 0)
                {
                    string ipAddress = string.Join(CommonConstants.Characters.Dot, ipArray);
                    ipAddress = ipAddress.Trim('.');

                    if (!string.IsNullOrWhiteSpace(ipAddress))
                    {
                        IGeoIpManager geoIpManager = ServiceLocator.ServiceProvider.GetRequiredService<IGeoIpManager>();
                        TimeSpan waitTime = new TimeSpan(0, 0, 0, CommonDictionaryValues.GeoIp.GetGeoIpDataWaitMilliseconds);
                        GeoIpFetchedData geoIpFetchedData = geoIpManager.GetGeoIpData(ipAddress, waitTime);

                        if (geoIpFetchedData != null && geoIpFetchedData.Status == GeoIpFetchDataStatus.Fetched && geoIpFetchedData.WhoIsInformation != null)
                        {
                            zip = geoIpFetchedData.WhoIsInformation.PostalCode;
                        }

                        ///if the above service fails to find the zip, use maxmind api as a fallback
                        if (string.IsNullOrWhiteSpace(zip))
                        {
                            string error = string.Empty;
                            dynamic geoData = new System.Dynamic.ExpandoObject();
                            geoData = CommonService.GetMaxMindGeoData(out error);

                            if (geoData != null && geoData.postal != null && geoData.postal.code != null)
                            {
                                zip = geoData.postal.code;
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return zip;
        }

        public Geolocation GetUserGeolocation()
        {
            ContactLocation contactLocation = GetUserContactLocation();
            double latitude = 0;
            double longitude = 0;

            if (contactLocation != null)
            {
                ///setting invalid lat-lng as defaults in case of null
                latitude = contactLocation.Latitude.GetValueOrDefault(91);
                longitude = contactLocation.Longitude.GetValueOrDefault(181);
            }

            return new Geolocation(latitude, longitude);
        }

        public static dynamic GetMaxMindGeoData(out string error)
        {
            error = string.Empty;
            string userIp = string.Empty;
            dynamic geoData = null;

            if (Tracker.Current != null && Tracker.Current.Interaction != null)
            {
                userIp = string.Join(CommonConstants.Characters.Dot, Tracker.Current.Interaction.Ip);

                if (!string.IsNullOrWhiteSpace(userIp))
                {
                    string apiUrl = string.Format(CommonDictionaryValues.MaxMind.GeoipApiUrl, userIp);

                    if (!string.IsNullOrWhiteSpace(apiUrl))
                    {
                        HttpWebRequest requestObj = (HttpWebRequest)WebRequest.Create(apiUrl);
                        requestObj.Method = "Get";
                        requestObj.PreAuthenticate = true;
                        requestObj.Credentials = new NetworkCredential(CommonDictionaryValues.MaxMind.GeoipApiUserId, CommonDictionaryValues.MaxMind.GeoIpApiPassword);

                        try
                        {
                            HttpWebResponse responseObj = (HttpWebResponse)requestObj.GetResponse();
                            string apiResponse = null;

                            using (Stream stream = responseObj.GetResponseStream())
                            {
                                StreamReader sr = new StreamReader(stream);
                                apiResponse = sr.ReadToEnd();
                                sr.Close();
                            }

                            if (!string.IsNullOrWhiteSpace(apiResponse))
                            {
                                geoData = new System.Dynamic.ExpandoObject();
                                geoData = JObject.Parse(apiResponse);
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.ToString();
                        }
                    }
                }
            }

            return geoData;
        }

        public BaseRenderingParams GetBaseRenderingParams(ID renderingParameterId = null)
        {
            if(ID.IsNullOrEmpty(renderingParameterId) || renderingParameterId.IsGlobalNullId || renderingParameterId.IsNull)
            {
                renderingParameterId = CommonTemplates.BaseRenderingParameters.ID;
            }
            
            BaseRenderingParams brParams = new BaseRenderingParams
            {
                SectionId = GetSectionId(renderingParameterId),
                AddDefaultBackgroundColor = SitecoreUtility.GetRenderingParameter(renderingParameterId, CommonTemplates.BaseRenderingParameters.Fields.AddDefaultBackgroundColor) == CommonConstants.One,
                BackgroundColor = SitecoreUtility.GetRenderingParameter(renderingParameterId, CommonTemplates.BaseRenderingParameters.Fields.BackgroundColor)
            };

            return brParams;
        }

        public string GetSectionId(ID renderingParameterId)
        {
            string sectionId = SitecoreUtility.GetRenderingParameter(renderingParameterId, CommonTemplates.BaseRenderingParameters.Fields.SectionId);

            if (string.IsNullOrWhiteSpace(sectionId))
            {
                Random random = new Random();
                sectionId = "section-" + random.Next();
            }

            return sectionId;
        }
    }
}
