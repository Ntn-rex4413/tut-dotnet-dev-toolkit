using System.Text.Json;

// normally use HttpClientFactory for thread safety etc.
using HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5138")
};

var response = await client.GetAsync("weatherforecast");

if (response.IsSuccessStatusCode)
{
    var jsonString = await response.Content.ReadAsStringAsync();

    using (JsonDocument jsonDocument = JsonDocument.Parse(jsonString))
    {
        JsonElement root = jsonDocument.RootElement;

        Console.WriteLine(root.ValueKind);

        foreach (var temp in root.EnumerateArray())
        {
            Console.WriteLine(temp.GetProperty("summary").ToString());
        }
    }
}
else
{
    Console.WriteLine($"Whoops! Error: {response.StatusCode}");
}
