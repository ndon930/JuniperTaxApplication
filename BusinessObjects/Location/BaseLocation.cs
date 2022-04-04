using Newtonsoft.Json;

namespace BusinessObjects.Location
{
    public abstract class BaseLocation : ILocation
    {
        /// <summary>
        /// Dicionary object to hold location information.
        /// Key: string value for data accessing.
        /// Value: string value to convert as needed.
        /// </summary>
        public Dictionary<string, string> LocationData { get; set; } = new Dictionary<string, string>();

        public BaseLocation(string zip, string country)
        {
            if(string.IsNullOrEmpty(zip))
                throw new Exception("Zip is Required!");
            else
                Zip = zip;

            if (!string.IsNullOrEmpty(country))
            {
                if (country.Length != 2)
                {
                    throw new Exception("Country must be a 2 character length!");
                }
                Country = country;
            }
        }

        [JsonProperty("country")]
        public string Country
        {
            get
            {
                if (LocationData.ContainsKey("Country"))
                    return this.LocationData["Country"];
                else
                    return "";
            }

            set
            {
                this.LocationData["Country"] = value;
            }
        }

        [JsonProperty("zip")]
        public string Zip
        {
            get
            {
                if (LocationData.ContainsKey("Zip"))
                    return this.LocationData["Zip"];
                else
                    return "";
            }

            set
            {
                this.LocationData["Zip"] = value;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// Return Country Code
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetTaxRateParameter()
        {
            Dictionary<string, string> taxRateData = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Country))
                taxRateData.Add("country", Country);

            if (!string.IsNullOrEmpty(Zip))
                taxRateData.Add("zip", Zip);

            return taxRateData;
        }
    }
}