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
using BusinessObjects.APIClients;

namespace BusinessObjects.TaxCalculator
{
    public class TaxJarCalculator : ITaxCalculator
    {
        public ApiClient Client { get; set; }

        ILog? Log { get; set; }

        /// <summary>
        /// Instantiate client for tax jar calculator. 
        /// </summary>
        /// <param name="log">logger object</param>
        /// <param name="baseURL">Base url for the client</param>
        /// <param name="version">Version the api client will use</param>
        /// <param name="apiToken">bearer token needed for the client</param>
        public TaxJarCalculator(ILog? log = null, string baseURL = "", string version = "", string apiToken = "")
        {
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : $"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}";
            try
            {
                Log = log;
                Client = new ApiClient(log, baseURL, version, apiToken);
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
            string methodName = MethodBase.GetCurrentMethod() == null ? "Unknown" : $"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}";
            try
            {
                Dictionary<string, string> parameters = location.GetTaxRateParameter();
                if (!parameters.ContainsKey("zip"))
                {
                    throw new Exception("Missing Zip in dictionary");
                }
                string uri = $"rates/{parameters["zip"]}?";
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    if (pair.Key == "zip")
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
