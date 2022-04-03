using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Location;

namespace BusinessObjects.Order
{
    /// <summary>
    /// Class representation of an order class
    /// </summary>
    public class Order : IOrder
    {
        #region Properties
        public ILocation? Location { get; set; }
        public ILocation? ToLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ILocation? FromLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, int>? LineItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        public Order(ILocation location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Return the Calcuated Taxes of the Order
        /// </summary>
        /// <returns></returns>
        public double CalculateTaxes()
        {
            double retValue = 0;
            return retValue;
        }
    }
}
