using System.Configuration;
using System.Collections.Specialized;
using TaxCalculator;

public class TaxService
{
    public ITaxCalculator TaxCalculator { get; set; }

    public TaxService(ITaxCalculator taxCalulator)
    {
        TaxCalculator = taxCalulator;
    }
}