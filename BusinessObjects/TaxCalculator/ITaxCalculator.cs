using BusinessObjects.APIResponseMessage;
using BusinessObjects.Location;
using BusinessObjects.Order;

namespace BusinessObjects.TaxCalculator
{
    public interface ITaxCalculator
    {
        /// <summary>
        /// Return the taxes being applied to teh order.
        /// </summary>
        /// <param name="order">The order to proccess taxes for.</param>
        /// <returns>A dictionary list of all the taxes</returns>
        OrderTaxResponse GetTaxesForOrder(IOrder order);

        /// <summary>
        /// Get the tax rate based on a location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>A dictionary list of tax data</returns>
        RateResponse GetLocationTaxRate(ILocation location);
    }
}