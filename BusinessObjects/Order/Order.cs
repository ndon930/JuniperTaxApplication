using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.APIPostObject;
using BusinessObjects.Location;
using Newtonsoft.Json;

namespace BusinessObjects.Order
{
    /// <summary>
    /// Class representation of an order class
    /// </summary>
    public class Order : IOrder
    {
        #region Properties
        #region To location
        public ILocation? ToLocation { get; set; }

        [JsonProperty("To_country")]
        public string ToCountry
        {
            get
            {
                return this.GetPropertyValuefromLocation("country", ToLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("country", value);
            }
        }

        [JsonProperty("to_zip")]
        public string ToZip
        {
            get
            {
                return this.GetPropertyValuefromLocation("zip", ToLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("zip", value);
            }
        }

        [JsonProperty("to_state")]
        public string ToState
        {
            get
            {
                return this.GetPropertyValuefromLocation("state", ToLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("state", value);
            }
        }

        [JsonProperty("to_street")]
        public string ToStreet
        {
            get
            {
                return this.GetPropertyValuefromLocation("street", ToLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("street", value);
            }
        }

        [JsonProperty("to_city")]
        public string ToCity
        {
            get
            {
                return this.GetPropertyValuefromLocation("city", ToLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("city", value);
            }
        }

        #endregion

        #region from location
        public ILocation? FromLocation { get; set; }

        [JsonProperty("from_country")]
        public string FromCountry
        {
            get
            {
                return this.GetPropertyValuefromLocation("country", FromLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("country", value);
            }
        }

        [JsonProperty("from_zip")]
        public string FromZip
        {
            get
            {
                return this.GetPropertyValuefromLocation("zip", FromLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("zip", value);
            }
        }

        [JsonProperty("from_state")]
        public string FromState
        {
            get
            {
                return this.GetPropertyValuefromLocation("state", FromLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("state", value);
            }
        }

        [JsonProperty("from_street")]
        public string FromStreet
        {
            get
            {
                return this.GetPropertyValuefromLocation("street", FromLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("street", value);
            }
        }

        [JsonProperty("from_city")]
        public string FromCity
        {
            get
            {
                return this.GetPropertyValuefromLocation("city", FromLocation);
            }
            set
            {
                if (FromLocation != null)
                    FromLocation.SetLocationData("city", value);
            }
        }

        #endregion


        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("shipping")]
        public decimal Shipping { get; set; } = 0;

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty("nexus_addresses")]
        public List<NexusAddress> NexusAddresses { get; set; } = new List<NexusAddress>();


        [JsonProperty("line_items")]
        public List<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
        #endregion

        public Order()
        {

        }

        /// <summary>
        /// Null check before getting property;
        /// </summary>
        /// <param name="property"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private string GetPropertyValuefromLocation(string property, ILocation? location)
        {
            if (location != null)
                return location.GetLocationData(property);
            else
                return string.Empty;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public object GetJsonObjectForOrderTaxAPICall()
        {
            TaxJarOrderPostData data = new TaxJarOrderPostData()
            {
                ToCountry = this.ToCountry,
                ToZip = this.ToZip,
                ToState = this.ToState,
                ToCity = this.ToCity,
                ToStreet = this.ToStreet,
                FromCountry = this.FromCountry,
                FromZip = this.FromZip,
                FromState = this.FromState,
                FromCity = this.FromCity,
                FromStreet = this.FromStreet,
                Amount = this.Amount,
                Shipping = this.Shipping,
                CustomerId = this.CustomerId,
                ExemptionType = this.ExemptionType,
                NexusAddresses = this.NexusAddresses,
                LineItems = this.LineItems
            };

            return data;
        }
    }
}
