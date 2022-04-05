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
            if(!string.IsNullOrEmpty(zip))
                Zip = zip;

            if (!string.IsNullOrEmpty(country))
                Country = country;
        }

        [JsonProperty("country")]
        public string Country
        {
            get { return this.GetLocationData("country"); }
            set { this.SetLocationData("country", value); }
        }

        [JsonProperty("zip")]
        public string Zip
        {
            get { return this.GetLocationData("zip"); }
            set { this.SetLocationData("zip", value); }
        }

        /// <summary>
        /// <inheritdoc/>
        /// Return Country Code
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetLocationTaxRateParameter()
        {
            Dictionary<string, string> taxRateData = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Country))
                taxRateData.Add("country", Country);

            if (!string.IsNullOrEmpty(Zip))
                taxRateData.Add("zip", Zip);

            return taxRateData;
        }

        /// <summary>
        /// <inheritdoc/>
        /// Return Country Code
        /// </summary>
        /// <returns></returns>
        public string GetLocationData(string key)
        {
            if (LocationData.ContainsKey(key))
                return LocationData[key];
            else
                return "";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns></returns>
        public void SetLocationData(string key, string value)
        {
            LocationData[key] = value;
        }
    }
}