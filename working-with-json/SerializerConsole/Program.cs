﻿
using SerializerConsole.Helpers;
using SerializerConsole.Models;
using System.Text.Json;

var person = new Person
{
    Id = 1,
    FirstName = "Sean",
    LastName = "Connery",
    Age = 90,
    IsAlive = false,
    Address = new Address
    {
        StreetName = "1 main street",
        City = "New York",
        ZipCode = "12345"
    },
    Phones = new List<Phone>()
    {
        new Phone{PhoneType = "Home", PhoneNumber = "03452131258"},
        new Phone{PhoneType = "Mobile", PhoneNumber = "03333337258"},
    },
    EyeColor = "Blue"
};

var opt = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = new LowerCaseNamingPolicy(),
    // can use this instead of decorating fields if there's no need to pick and choose
    //IncludeFields = true
};

string jsonString = JsonSerializer.Serialize(person, opt);

string fileName = "person.json";

File.WriteAllText(fileName, jsonString);

Console.WriteLine(File.ReadAllText(fileName));
