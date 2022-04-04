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
        public USLocation(string zip, string state = "", string city = "", string street = "") : base(zip, "US")
        {
            if (!string.IsNullOrEmpty(state))
                State = state;
            if (!string.IsNullOrEmpty(city))
                City = city;
            if (!string.IsNullOrEmpty(street))
                Street = street;
        }

        [JsonProperty("state")]
        public string State
        {
            get
            {
                if (LocationData.ContainsKey("State"))
                    return this.LocationData["State"];
                else
                    return "";
            }

            set
            {
                this.LocationData["State"] = value;
            }
        }

        [JsonProperty("city")]
        public string City
        {
            get
            {
                if (LocationData.ContainsKey("City"))
                    return this.LocationData["City"];
                else
                    return "";
            }

            set
            {
                this.LocationData["City"] = value;
            }
        }

        [JsonProperty("street")]
        public string Street
        {
            get
            {
                if (LocationData.ContainsKey("Street"))
                    return this.LocationData["Street"];
                else
                    return "";
            }

            set
            {
                this.LocationData["Street"] = value;
            }
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetTaxRateParameter()
        {
            Dictionary<string, string> taxRateData = base.GetTaxRateParameter();

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
