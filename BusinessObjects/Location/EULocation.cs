using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Location
{
    public class EULocation : BaseLocation
    {
        public EULocation(string zip, string country, string city = "") : base(zip, country)
        {
            if (!string.IsNullOrEmpty(city))
                City = city;
        }

        [JsonProperty("city")]
        public string City
        {
            get { return this.GetLocationData("city"); }
            set { this.SetLocationData("city", value); }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetLocationTaxRateParameter()
        {
            Dictionary<string, string> taxRateData = base.GetLocationTaxRateParameter();

            if (!string.IsNullOrEmpty(City))
                taxRateData.Add("city", City);

            return taxRateData;
        }
    }
}
