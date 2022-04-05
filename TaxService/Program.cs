using BusinessObjects.APIClients;
using BusinessObjects.APIResponseMessage;
using BusinessObjects.Location;
using BusinessObjects.Log;
using BusinessObjects.Order;
using BusinessObjects.TaxCalculator;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

Console.WriteLine("Hello, World!");

Dictionary<string,string> test = new Dictionary<string, string>();
test.Add("zip", "45011");

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.taxjar.com/v2/rates/90404?country=US&zip=45011"),
};
request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");

WinHttpHandler handler = new WinHttpHandler();
HttpClient Client = new HttpClient(handler);
var response = await Client.SendAsync(request).ConfigureAwait(false);
var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
Console.WriteLine(responseContent);




string zip = "45011";
string state = "Oh";
string city = "Hamilton";
string street = "test";

FileLog testLog = new FileLog(ConfigurationManager.AppSettings.Get("LogFilePath"), ConfigurationManager.AppSettings.Get("FileName"));
USLocation testLocation = new USLocation(zip, state, city, street);
TaxJarCalculator taxCalculator = new TaxJarCalculator(testLog);
APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation).LocationRates;


string APIBaseUrl = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPIBaseURLKey) ?? ApiClient.TaxJarApiClientConstant.DefaultBaseUrl;
string APIVersion = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarApiVersionKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiVersion;
string APIToken = ConfigurationManager.AppSettings.Get(ApiClient.TaxJarApiClientConstant.TaxJarAPITokenKey) ?? ApiClient.TaxJarApiClientConstant.DefaultApiToken;

OrderLineItem testLineItem = new OrderLineItem()
{
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

testLog.LogInfo(testOrder.ToString());
taxCalculator = new TaxJarCalculator(testLog, APIBaseUrl, APIVersion, APIToken);
APIOrderTaxResponseMessage rspNew = taxCalculator.GetTaxesForOrder(testOrder).OrderRates;