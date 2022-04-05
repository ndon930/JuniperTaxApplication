using BusinessObjects.Location;

namespace BusinessObjects.Order
{
    public interface IOrder
    {
        ILocation? ToLocation { get; set; }
        ILocation? FromLocation { get; set; }
        List<OrderLineItem> LineItems { get; set; }

        /// <summary>
        /// This method will return object needed for the API Call
        /// </summary>
        /// <returns></returns>
        object GetJsonObjectForOrderTaxAPICall();
    }
}