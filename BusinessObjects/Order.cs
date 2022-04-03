using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    /// <summary>
    /// Class representation of an order class
    /// </summary>
    public class Order : IOrder
    {
        #region Properties
        public ILocation? Location { get; set; }
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
