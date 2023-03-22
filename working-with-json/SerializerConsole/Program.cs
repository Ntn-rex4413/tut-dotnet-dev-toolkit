
using SerializerConsole.Models;
using System.Text.Json;

var person = new Person
{
    Id = 1,
    FirstName = "Sean",
    LastName = "Connery",
    Age = 90,
    IsAlive = false
};

var opt = new JsonSerializerOptions
{
    WriteIndented = true,
};

string jsonString = JsonSerializer.Serialize(person, opt);

string fileName = "person.json";

File.WriteAllText(fileName, jsonString);

Console.WriteLine(File.ReadAllText(fileName));
