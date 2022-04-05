using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Location
{
    public class Jurisdiction : BaseLocation
    {
        #region Properties
        [JsonProperty("state")]
        public string State
        {
            get { return this.GetLocationData("state"); }
            set { this.SetLocationData("state", value); }
        }

        [JsonProperty("city")]
        public string City
        {
            get { return this.GetLocationData("city"); }
            set { this.SetLocationData("city", value); }
        }

        [JsonProperty("county")]
        public string County
        {
            get { return this.GetLocationData("county"); }
            set { this.SetLocationData("county", value); }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetLocationTaxRateParameter()
        {
            Dictionary<string, string> taxRateData = base.GetLocationTaxRateParameter();

            if (!string.IsNullOrEmpty(State))
                taxRateData.Add("state", State);

            if (!string.IsNullOrEmpty(City))
                taxRateData.Add("city", City);

            if (!string.IsNullOrEmpty(County))
                taxRateData.Add("street", County);

            return taxRateData;
        }
        #endregion
        public Jurisdiction(string country, string zip = "", string state = "", string city = "", string county = "") : base(zip, country)
        {
            if (!string.IsNullOrEmpty(state))
                State = state;
            if (!string.IsNullOrEmpty(city))
                City = city;
            if (!string.IsNullOrEmpty(county))
                County = county;
        }

    }
}
