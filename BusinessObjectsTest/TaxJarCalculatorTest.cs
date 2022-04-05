using BusinessObjects.Location;
using BusinessObjects.TaxCalculator;
using BusinessObjects.APIResponseMessage;
using BusinessObjects.APIClients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using BusinessObjects.Order;

namespace BusinessObjectsTest
{
    public class TaxJarCalculatorTest
    { 
        /// <summary>
        /// Test based on api call as of April 4, 2022
        /// </summary>
        [Test]
        public void TestUSLocationTaxJarCalculator()
        {
            string zip = "05495-2086";
            string state = "VT";
            string city = "Williston";
            string street = "312 Hurricane Lane";
            try
            {
                string APIBaseUrl = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPIBaseURLKey) ?? ApiClient.TaxJarApiClientConstant.DefaultBaseUrl;
                string APIVersion = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarApiVersionKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiVersion;
                string APIToken = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPITokenKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiToken;

                USLocation testLocation = new USLocation(zip, state, city, street);
                TaxJarCalculator taxCalculator = new TaxJarCalculator(null, APIBaseUrl, APIVersion, APIToken);
                APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation).LocationRates;
                Assert.IsNotNull(rsp);
                Assert.AreEqual(zip, rsp.Zip);
                Assert.AreEqual("US", rsp.Country);
                Assert.AreEqual(0.0, rsp.CountyRate);
                Assert.AreEqual(state, rsp.State);
                Assert.AreEqual(0.06, rsp.StateRate);
                Assert.AreEqual("CHITTENDEN", rsp.County);
                Assert.AreEqual(0.0, rsp.CountyRate);
                Assert.AreEqual("WILLISTON", rsp.City);
                Assert.AreEqual(0.01, rsp.CityRate);
                Assert.AreEqual(0.0, rsp.CombinedDistrictRate);
                Assert.AreEqual(0.07, rsp.CombinedRate);
                Assert.IsTrue(rsp.FreightTaxable);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        /// <summary>
        /// Test based on api call as of April 4, 2022
        /// </summary>
        [Test]
        public void TestEULocationTaxJarCalculator()
        {
            string zip = "00150";
            string country = "FI";
            string city = "Helsinki";
            try
            {
                string APIBaseUrl = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPIBaseURLKey) ?? ApiClient.TaxJarApiClientConstant.DefaultBaseUrl;
                string APIVersion = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarApiVersionKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiVersion;
                string APIToken = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPITokenKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiToken;

                EULocation testLocation = new EULocation(zip, country, city);
                TaxJarCalculator taxCalculator = new TaxJarCalculator(null, APIBaseUrl, APIVersion, APIToken);
                APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation).LocationRates;
                Assert.IsNotNull(rsp);
                Assert.AreEqual("FI", rsp.Country);
                Assert.AreEqual(0.0, rsp.DistanceSaleThreshold);
                Assert.IsTrue(rsp.FreightTaxable);
                Assert.AreEqual("Finland", rsp.Name);
                Assert.AreEqual(0.0, rsp.ParkingRate);
                Assert.AreEqual(0.14, rsp.ReducedRate);
                Assert.AreEqual(0.24, rsp.StandardRate);
                Assert.AreEqual(0.1, rsp.SuperReducedRate);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        /// <summary>
        /// Test based on api call as of April 4, 2022
        /// </summary>
        [Test]
        public void TestOrderTaxJarCalculator()
        {
            
            try
            {
                string APIBaseUrl = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPIBaseURLKey) ?? ApiClient.TaxJarApiClientConstant.DefaultBaseUrl;
                string APIVersion = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarApiVersionKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiVersion;
                string APIToken = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPITokenKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiToken;

                OrderLineItem testLineItem = new OrderLineItem() {
                    Id = "1",
                    Quantity = 1,
                    TaxCode = "20010",
                    SalesTax = 15,
                    Discount = 0
                };

                USLocation fromLocation = new USLocation("92093", "CA", "La Jolla", "9500 Gilman Drive");
                USLocation toLocation = new USLocation("90002", "CA", "Los Angeles", "1335 E 103rd St");
                NexusAddress testNexusAddress = new NexusAddress("92093", "CA", "Main Location", "US", "La Jolla", "9500 Gilman Drive");
                Order testOrder = new Order();
                testOrder.ToLocation = toLocation;
                testOrder.FromLocation = fromLocation;
                testOrder.Amount = 15;
                testOrder.Shipping = (decimal)1.5;

                TaxJarCalculator taxCalculator = new TaxJarCalculator(null, APIBaseUrl, APIVersion, APIToken);
                APIOrderTaxResponseMessage rsp = taxCalculator.GetTaxesForOrder(testOrder).OrderRates;

                Assert.IsNotNull(rsp);
                Assert.AreEqual(1.43, rsp.AmountToCollect);
                Assert.IsFalse(rsp.FreightTaxable);
                Assert.IsTrue(rsp.HasNexus);
                Assert.AreEqual("LOS ANGELES", rsp.Jurisdiction.City);
                Assert.AreEqual("US", rsp.Jurisdiction.Country);
                Assert.AreEqual("LOS ANGELES COUNTY", rsp.Jurisdiction.County);
                Assert.AreEqual("CA", rsp.Jurisdiction.State);
                Assert.AreEqual(16.5, rsp.OrderTotalAmount);
                Assert.AreEqual(0.095, rsp.Rate);
                Assert.AreEqual(1.5, rsp.Shipping);
                Assert.AreEqual("destination", rsp.TaxSource);
                Assert.AreEqual(15.0, rsp.TaxableAmount);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}