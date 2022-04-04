using BusinessObjects.Location;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObjectsTest
{
    public class EULocationTest
    {

        string zip = "00150";
        string country = "FI";
        string city = "Helsinki";
        string street = "FI";
        [Test]
        public void TestNewLocation()
        {
            EULocation testLocation = new EULocation(zip, country, city) ;
            Assert.AreEqual(3, testLocation.LocationData.Count);
            Assert.AreEqual(zip, testLocation.Zip);
            Assert.AreEqual(city, testLocation.City);
            Assert.AreEqual(country, testLocation.Country);
        }

        [Test]
        public void TestGetTaxRateParameters()
        {
            EULocation testLocation = new EULocation(zip, country, city);
            Dictionary<string, string> taxRateData = testLocation.GetTaxRateParameter();

            Assert.AreEqual(taxRateData.Count, 3);
            Assert.IsTrue(taxRateData.ContainsKey("zip"));
            Assert.IsTrue(taxRateData.ContainsKey("city"));
            Assert.IsTrue(taxRateData.ContainsKey("country"));
            Assert.AreEqual(zip, taxRateData["zip"]);
            Assert.AreEqual(city, taxRateData["city"]);
            Assert.AreEqual(country, taxRateData["country"]);
        }
    }
}