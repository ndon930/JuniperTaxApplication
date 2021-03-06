using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Location
{
    /// <summary>
    /// Instance of a US based location;
    /// </summary>
    public class USLocation : BaseLocation
    {
        #region Propeties
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

        [JsonProperty("street")]
        public string Street
        {
            get { return this.GetLocationData("street"); }
            set { this.SetLocationData("street", value); }
        }
        #endregion

        public USLocation(string zip, string state = "", string city = "", string street = "") : base(zip, "US")
        {
            if (!string.IsNullOrEmpty(state))
                State = state;
            if (!string.IsNullOrEmpty(city))
                City = city;
            if (!string.IsNullOrEmpty(street))
                Street = street;
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

            if (!string.IsNullOrEmpty(Street))
                taxRateData.Add("street", Street);

            return taxRateData;
        }
    }
}
