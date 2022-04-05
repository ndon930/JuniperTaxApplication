using BusinessObjects.Location;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObjectsTest
{
    public class BaseLocationTest
    {
        public class TestBaseLocation : BaseLocation
        {
            public TestBaseLocation(string zip, string country) : base(zip, country)
            {
            }
        }

        string zip = "123456";
        string country = "US";
        [Test]
        public void TestNewLocation()
        {
            TestBaseLocation testLocation = new TestBaseLocation(zip, country);
            Assert.AreEqual(2, testLocation.LocationData.Count);
            Assert.AreEqual(zip, testLocation.Zip);
            Assert.AreEqual("US", testLocation.Country);

            testLocation.SetLocationData("NewDataKey", "TestData");
            Assert.AreEqual(3, testLocation.LocationData.Count);
            Assert.AreEqual("TestData", testLocation.GetLocationData("NewDataKey"));

            testLocation.SetLocationData("NewDataKey", "TestData123");
            Assert.AreEqual(3, testLocation.LocationData.Count);
            Assert.AreEqual("TestData123", testLocation.GetLocationData("NewDataKey"));
        }
    }
}