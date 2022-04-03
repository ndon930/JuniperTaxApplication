using NUnit.Framework;
using TaxCalculator;
namespace TaxCalculatorTest
{
    public class Tests
    {
        public TaxCalculator.TaxCalculator? TaxCalculator { get; set; }

        [SetUp]
        public void Setup()
        {
            TaxCalculator = new TaxCalculator.TaxCalculator();
        }

        [Test]
        public void Test1()
        {
            Assert.IsNotNull(TaxCalculator);
            TaxCalculator = null;
            Assert.IsNull(TaxCalculator);
            Assert.Pass();
        }
    }
}