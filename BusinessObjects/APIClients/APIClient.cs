using BusinessObjects.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.APIClients
{
    public class ApiClient
    {
        public static class TaxJarApiClientConstant
        {
            public const string DefaultBaseUrl = "https://api.taxjar.com";
            public const string DefaultApiVersion = "v2";
            public const string DefaultApiToken = "5da2f821eee4035db4771edab942a4cc";

            public const string TaxJarAPIBaseURLKey = "TaxJarAPIBaseURL";
            public const string TaxJarApiVersionKey = "TaxJarApiVersion";
            public const string TaxJarAPITokenKey = "TaxJarAPIToken";
        }

        public string APIBaseUrl { get; set; }
        public string APIToken { get; set; }
        public string APIURL { get; set; }
        public string APIVersion { get; set; }

        private static HttpClient? Client { get; set; }
        public ILog? Log { get; set; }
        public ApiClient(ILog? log = null, string baseURL = "", string version = "", string apiToken = "")
        {
            APIBaseUrl = string.IsNullOrEmpty(baseURL) ? TaxJarApiClientConstant.DefaultBaseUrl : baseURL;
            APIVersion = string.IsNullOrEmpty(version) ? TaxJarApiClientConstant.DefaultApiVersion : version;
            APIToken = string.IsNullOrEmpty(apiToken) ? TaxJarApiClientConstant.DefaultApiToken : apiToken;
            APIURL = APIBaseUrl + "/" + APIVersion;

            if (log != null)
                Log = log;

            Client = new HttpClient();
        }

        public virtual T SendGet<T>(string url)
        {
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : $"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{APIURL}/{url}"),
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIToken);
            if (Log != null)
                Log.LogInfo($"Calling: { APIURL}/{ url}");
            try
            {
                using (HttpResponseMessage response = Client.SendAsync(request).Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (Log != null)
                            Log.LogInfo($"Received: {response.Content.ReadAsStringAsync().Result}");

                        return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new Exception($"Response Code Returned :{response.StatusCode}, Error: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Log != null)
                    Log.LogException(methodName, ex.ToString());

                throw new Exception($"{ex.ToString}: {ex.StackTrace}");
            }
        }

        public virtual T SendPost<T>(string url, object body = null)
        {
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : $"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{APIURL}/{url}"),
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIToken);
            if (Log != null)
                Log.LogInfo($"Calling: {APIURL}/{ url}");
            try
            {
                if (Log != null)
                {
                    Log.LogInfo($"json data: { JsonConvert.SerializeObject(body) }");
                }
                using (HttpResponseMessage response = Client.SendAsync(request).Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (Log != null)
                            Log.LogInfo($"Received: {response.Content.ReadAsStringAsync().Result}");

                        return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new Exception($"Response Code Returned :{response.StatusCode}, Error: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Log != null)
                    Log.LogException(methodName, ex.ToString());

                throw new Exception($"{ex.ToString}: {ex.StackTrace}");
            }
        }
    }
}
