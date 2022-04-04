namespace BusinessObjects.Location
{
    public interface ILocation
    {
        /// <summary>
        /// Retrieve the information needed for tax rate retrieval. 
        /// </summary>
        /// <returns></returns>
        Dictionary<string,string> GetTaxRateParameter();
    }
}