using BusinessObjects.Location;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObjectsTest
{
    public class USLocationTest
    {

        string zip = "123456";
        string state = "Illinois";
        string city = "Chicago";
        string street = "Plum";
        [Test]
        public void TestNewLocation()
        { 
            USLocation testLocation = new USLocation(zip, state, city, street);
            Assert.AreEqual(5, testLocation.LocationData.Count);
            Assert.AreEqual(zip, testLocation.Zip);
            Assert.AreEqual(city, testLocation.City);
            Assert.AreEqual(street, testLocation.Street);
            Assert.AreEqual(state, testLocation.State);
            Assert.AreEqual("US", testLocation.Country);
        }

        [Test]
        public void TestGetTaxRateParameters()
        {
            USLocation testLocation = new USLocation(zip, state, city, street);
            Dictionary<string, string> taxRateData = testLocation.GetLocationTaxRateParameter();

            Assert.AreEqual(5, taxRateData.Count);
            Assert.IsTrue(taxRateData.ContainsKey("zip"));
            Assert.IsTrue(taxRateData.ContainsKey("city"));
            Assert.IsTrue(taxRateData.ContainsKey("street"));
            Assert.IsTrue(taxRateData.ContainsKey("state"));
            Assert.IsTrue(taxRateData.ContainsKey("country"));
            Assert.AreEqual(zip, taxRateData["zip"]);
            Assert.AreEqual(city, taxRateData["city"]);
            Assert.AreEqual(street, taxRateData["street"]);
            Assert.AreEqual(state, taxRateData["state"]);
            Assert.AreEqual("US", taxRateData["country"]);
        }
    }
}