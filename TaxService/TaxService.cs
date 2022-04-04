using System.Configuration;
using System.Collections.Specialized;
using BusinessObjects.TaxCalculator;
using BusinessObjects.Log;

public class TaxService
{
    public ITaxCalculator TaxCalculator { get; set; }

    public TaxService(ITaxCalculator taxCalulator)
    {
        TaxCalculator = taxCalulator;
    }
}