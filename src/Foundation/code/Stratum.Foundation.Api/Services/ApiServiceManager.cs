
namespace Stratum.Foundation.Api.Services
{
    using Stratum.Foundation.Api.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Utilities;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Sitecore.Diagnostics;

    public class ApiServiceManager
    {
        public struct Properties
        {
            public static string Divider = "===========================================================================";
            public static string Time = string.Format("TIME              : {0}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));

        }

        public ApiResponse GetAsyncResponse(string endPointUrl, List<KeyValuePair<string, string>> requestHeaderKeyValues)
        {
            ApiResponse apiResponse = new ApiResponse();
            string requestJson = string.Empty;
            HttpResponseMessage res = null;

            using (HttpClient client = new HttpClient())
            {
                if (requestHeaderKeyValues != null && requestHeaderKeyValues.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in requestHeaderKeyValues)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                res = client.GetAsync(new Uri(endPointUrl)).Result;

                if (res != null)
                {
                    apiResponse.StatusCode = res.StatusCode.ToString();
                    apiResponse.ResponseMessage = res.Content != null ? res.Content.ReadAsStringAsync().Result : string.Empty;
                }
                else
                {
                    apiResponse = null;
                }
            }

            LogApiInfo(endPointUrl, requestJson, apiResponse?.ResponseMessage, apiResponse?.StatusCode, requestHeaderKeyValues);
            return apiResponse;
        }

        public ApiResponse GetPostAsyncResponse<T>(string endPointUrl, List<KeyValuePair<string, string>> requestHeaderKeyValues, T requestObject) where T : class
        {
            string requestJson = requestObject != null ? JsonConvert.SerializeObject(requestObject) : string.Empty;
            ApiResponse apiResponse = GetPostAsyncResponse(endPointUrl, requestHeaderKeyValues, requestJson);
            return apiResponse;
        }

        public ApiResponse GetPostAsyncResponse(string endPointUrl, List<KeyValuePair<string, string>> requestHeaderKeyValues, string requestJson)
        {
            ApiResponse apiResponse = new ApiResponse();
            HttpResponseMessage res = null;

            using (HttpClient client = new HttpClient())
            {
                if (requestHeaderKeyValues != null && requestHeaderKeyValues.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in requestHeaderKeyValues)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                StringContent stringContent = new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json");
                res = client.PostAsync(new Uri(endPointUrl), stringContent).Result;

                if (res != null)
                {
                    apiResponse.StatusCode = res.StatusCode.ToString();
                    apiResponse.ResponseMessage = res.Content != null ? res.Content.ReadAsStringAsync().Result : string.Empty;
                }
                else
                {
                    apiResponse = null;
                }
            }
            LogApiInfo(endPointUrl, requestJson, apiResponse?.ResponseMessage, apiResponse?.StatusCode, requestHeaderKeyValues);
            return apiResponse;
        }

        public ApiResponse PatchAsyncResponse(string endPointUrl, List<KeyValuePair<string, string>> requestHeaderKeyValues, string requestJson)
        {
            ApiResponse apiResponse = new ApiResponse();
            HttpResponseMessage res = null;

            using (HttpClient client = new HttpClient())
            {
                if (requestHeaderKeyValues != null && requestHeaderKeyValues.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in requestHeaderKeyValues)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                StringContent stringContent = !string.IsNullOrWhiteSpace(requestJson) ?
                                              new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json") :
                                              new StringContent(requestJson, UnicodeEncoding.UTF8);
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), endPointUrl);
                request.Content = stringContent;
                res = client.SendAsync(request).Result;

                if (res != null)
                {
                    apiResponse.StatusCode = res.StatusCode.ToString();
                    apiResponse.ResponseMessage = res.Content != null ? res.Content.ReadAsStringAsync().Result : string.Empty;
                }
                else
                {
                    apiResponse = null;
                }
            }

            LogApiInfo(endPointUrl, requestJson, apiResponse?.ResponseMessage, apiResponse?.StatusCode, requestHeaderKeyValues);
            return apiResponse;
        }

        public async Task LogApiInfo(string apiUrl, string requestJson, string responseJson, string responseStatusCode, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("===========================================================================");
                sb.AppendLine("API LOG DETAILS");
                sb.AppendLine(string.Format("TIME             : {0}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")));
                sb.AppendLine(string.Format("EVENTS           :"));
                sb.AppendLine(string.Join(" < ", GetStackTraceMethods()));
                sb.AppendLine(string.Format("END POINT        : {0}", apiUrl));
                sb.AppendLine(string.Format("REQUEST JSON     :"));
                sb.AppendLine(requestJson);
                sb.AppendLine(string.Format("STATUS CODE      : {0}", responseStatusCode));
                sb.AppendLine("RESPONSE JSON    :");
                sb.AppendLine(responseJson);
                sb.AppendLine("REQUEST HEADERS  :");

                if (requestHeaders != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requestHeaders)
                    {
                        sb.AppendLine(kvp.Key + " : " + kvp.Value);
                    }
                }

                sb.AppendLine("===========================================================================");
                Sitecore.Diagnostics.Log.Info(sb.ToString(), "ApiLogger");
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                ApiResponse = null;
            }
        }

        public List<string> GetStackTraceMethods()
        {
            List<string> stackTraceMethods = new List<string>();
            StackTrace stackTrace = new StackTrace();
            string className = string.Empty;
            string methodName = string.Empty;
            List<string> methodNamesToIgnore = new List<string> {
            "GetStackTraceMethods", "LogApiInfo", "MoveNext", "Start", "lambda_method",
            "BuildProduct", "BuildProducts", "b__2", ".ctor", "ToList"
            };

            foreach (StackFrame frame in stackTrace.GetFrames())
            {
                className = string.Empty;
                methodName = frame.GetMethod().Name;

                if (methodName == "InvokeActionMethod")
                {
                    break;
                }
                else if (methodNamesToIgnore.Any(x => x == methodName))
                {
                    continue;
                }
                else
                {
                    className = frame.GetMethod().DeclaringType.Name;
                    stackTraceMethods.Add(className + CommonConstants.Characters.Dot + methodName);
                }
            }

            return stackTraceMethods;
        }
    }
}
