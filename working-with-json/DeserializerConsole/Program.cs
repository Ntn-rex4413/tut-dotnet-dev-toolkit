using System.Text.Json;
using DeserializerConsole.Models;

string fileName = "person.json";

string jsonString = File.ReadAllText(fileName);

var opt = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Person? person = JsonSerializer.Deserialize<Person>(jsonString);

// ! is not a best practice, normally check if it isn't null
Console.WriteLine($"The first name is: {person!.FirstName}");
