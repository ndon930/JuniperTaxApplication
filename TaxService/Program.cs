using BusinessObjects.APIResponseMessage;
using BusinessObjects.Location;
using BusinessObjects.Log;
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
APILocationRatesResponseMessage rsp = taxCalculator.GetLocationTaxRate(testLocation);
