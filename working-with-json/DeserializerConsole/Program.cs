using System.Net.Http.Json;
using System.Text.Json;
using DeserializerConsole.Models;

var opt = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

// normally use HttpClientFactory for thread safety etc.
using HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5138")
};

var temperatures = await client.GetFromJsonAsync<Temperature[]>("weatherforecast", opt);

if (temperatures != null)
{
    foreach (var temperature in temperatures)
    {
        Console.WriteLine($"Summary: {temperature.Summary}");
    }
}

