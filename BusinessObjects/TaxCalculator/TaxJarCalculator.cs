using BusinessObjects.Location;
using BusinessObjects.Log;
using BusinessObjects.Order;
using BusinessObjects.APIResponseMessage;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessObjects.TaxCalculator
{
    public class TaxJarCalculator : ITaxCalculator
    {
        public class ApiClient
        {
            public static class ApiClientConstant
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
            public ApiClient(ILog? log = null)
            {
                APIBaseUrl = ConfigurationManager.AppSettings.Get(ApiClientConstant.TaxJarAPIBaseURLKey) ?? ApiClientConstant.DefaultBaseUrl;
                APIVersion = ConfigurationManager.AppSettings.Get(ApiClientConstant.TaxJarApiVersionKey) ??  ApiClientConstant.DefaultApiVersion;
                APIToken = ConfigurationManager.AppSettings.Get(ApiClientConstant.TaxJarAPITokenKey) ?? ApiClientConstant.DefaultApiToken;
                APIURL = APIBaseUrl + "/" + APIVersion;

                if (log != null)
                    Log = log;

                WinHttpHandler handler = new WinHttpHandler();
                Client = new HttpClient(handler);
            }

            public virtual T SendGet<T>(string url)
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{APIURL}/{url}"),
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIToken);

                using (HttpResponseMessage response = Client.SendAsync(request).Result)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public ApiClient Client { get; set; }

        ILog? Log { get; set; }

        public TaxJarCalculator(ILog? log = null)
        {
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : MethodBase.GetCurrentMethod().Name;
            try
            {
                Log = log;
                Client = new ApiClient(log);
            }
            catch (Exception e) 
            { 
                if(log != null)
                    log.LogException(methodName, e.Message); 
            };
        }

        public Dictionary<string, string> CalculateOrderTaxes(IOrder order)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            return result;
        }

        /// <summary>
        /// <inheritdoc>/>
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Missing Zip</exception>
        public APILocationRatesResponseMessage GetLocationTaxRate(ILocation location)
        {
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : MethodBase.GetCurrentMethod().Name;
            try
            {
                Dictionary<string, string> parameters = location.GetTaxRateParameter();
                if (!parameters.ContainsKey("Zip"))
                {
                    throw new Exception("Missing Zip in dictionary");
                }
                string uri = $"rates/{parameters["Zip"]}?";
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    if (pair.Key == "Zip")
                        continue;
                    else
                        uri += $"{pair.Key}={pair.Value}&";
                }
                if (Log != null)
                {
                    Log.LogInfo($"Called: GetLocationTaxRate, Data:{uri.Remove(uri.Length - 1, 1)}");
                }

                APILocationRatesResponseMessage ret = this.Client.SendGet<RateResponse>(uri.Remove(uri.Length - 1, 1)).LocationRates;
                return ret;
            }
            catch (Exception e)
            {
                if(Log != null)
                {
                    Log.LogException(methodName, e.Message);
                }
                return new APILocationRatesResponseMessage();
            }
           
        }
    }
}
