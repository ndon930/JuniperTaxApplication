namespace BusinessObjects.Location
{
    public interface ILocation
    {
        /// <summary>
        /// Retrieve the information needed for tax rate retrieval. 
        /// </summary>
        /// <returns></returns>
        Dictionary<string,string> GetLocationTaxRateParameter();

        /// <summary>
        /// Get the value of the location data by key
        /// </summary>
        /// <param name="key"> the key to search by</param>
        /// <returns>return the value of the key</returns>
        public string GetLocationData(string key);


        /// <summary>
        /// update the location data for the key.
        /// </summary>
        /// <param name="key"> the key to search by</param>
        /// <param name="value"> the value of the dictionary</param>
        /// <returns>return the value of the key</returns>
        public void SetLocationData(string key, string value);
    }
}