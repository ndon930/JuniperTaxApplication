using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Location;
using BusinessObjects.Order;

namespace TaxCalculator
{
    public class TaxCalculator : ITaxCalculator
    {
        public string LocationTaxRateAPIKey { get; set; } = String.Empty;
        public string OrderTaxRateAPIKey { get; set; } = String.Empty;
        public string SalesAPIKey { get; set; } = String.Empty;

        public TaxCalculator(NameValueCollection configuration)
        {

        }

        public Dictionary<string, string> CalculateOrderTaxes(IOrder Order)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            return result;
        }

        public Dictionary<string, string> GetLocationTaxRate(ILocation Order)
        {
            throw new NotImplementedException();
        }
    }
}
