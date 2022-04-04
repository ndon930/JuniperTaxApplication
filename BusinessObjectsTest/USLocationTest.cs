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
            USLocation testLocation = new USLocation(zip, state, city, street) ;
            Assert.AreEqual(testLocation.LocationData.Count, 5);
            Assert.AreEqual(testLocation.Zip, zip);
            Assert.AreEqual(testLocation.City, city);
            Assert.AreEqual(testLocation.Street, street);
            Assert.AreEqual(testLocation.State, state);
            Assert.AreEqual(testLocation.Country, "US");
        }

        [Test]
        public void TestGetTaxRateParameters()
        {
            USLocation testLocation = new USLocation(zip, state, city, street);
            Dictionary<string, string> taxRateData = testLocation.GetTaxRateParameter();

            Assert.AreEqual(taxRateData.Count, 5);
            Assert.IsTrue(taxRateData.ContainsKey("Zip"));
            Assert.AreEqual(taxRateData["Zip"], zip);
            Assert.IsTrue(taxRateData.ContainsKey("City"));
            Assert.AreEqual(taxRateData["City"], city);
            Assert.IsTrue(taxRateData.ContainsKey("Street"));
            Assert.AreEqual(taxRateData["Street"], street);
            Assert.IsTrue(taxRateData.ContainsKey("State"));
            Assert.AreEqual(taxRateData["State"], state);
            Assert.IsTrue(taxRateData.ContainsKey("Country"));
            Assert.AreEqual(taxRateData["Country"], "US");
        }
    }
}