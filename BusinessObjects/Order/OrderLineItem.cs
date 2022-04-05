using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Order
{
    public class OrderLineItem
    {
        #region properties
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_identifier")]
        public string ProductIdentifier { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("product_tax_code ")]
        public string TaxCode { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("sales_tax")]
        public decimal SalesTax { get; set; }
        #endregion
    }
}
