using BusinessObjects.Location;
using BusinessObjects.TaxCalculator;
using BusinessObjects.APIResponseMessage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObjectsTest
{
    public class TaxJarCalculatorTest
    {

        string zip = "45011";
        string state = "Oh";
        string city = "Hamilton";
        string street = "test";
        [Test]
        public void TestTaxJarCalculator()
        { 
            USLocation testLocation = new USLocation(zip, state, city, street) ;
            TaxJarCalculator taxCalculator = new TaxJarCalculator();
            APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation);
        }
    }
}