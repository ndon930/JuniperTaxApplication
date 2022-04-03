using BusinessObjects.Location;
using BusinessObjects.Order;

namespace TaxCalculator
{
    public interface ITaxCalculator
    {
        Dictionary<string, string> CalculateOrderTaxes(IOrder Order);
        Dictionary<string, string> GetLocationTaxRate(ILocation Order);
    }
}