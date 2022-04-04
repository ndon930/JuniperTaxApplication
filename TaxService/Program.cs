using Newtonsoft.Json;
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
