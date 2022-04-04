using BusinessObjects.Location;
using BusinessObjects.TaxCalculator;
using BusinessObjects.APIResponseMessage;
using BusinessObjects.APIClients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

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
                APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation);
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
                APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation);
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
    }
}