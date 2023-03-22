using System.Text.Json.Serialization;

namespace SerializerConsole.Models
{
    public class Person
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }

        // value specified in decorator overrides JsonSerializerOptions' NamingPolicy (e.g. casing);
        [JsonPropertyName("surname")]
        public string? LastName { get; set; }

        public int Age { get; set; }

        [JsonIgnore]
        public bool IsAlive { get; set; }

        public Address? Address { get; set; }

        public IList<Phone>? Phones { get; set; }

        [JsonInclude]
        public string? EyeColor;
        // also - private props/fields are not exposed to serializer by default
    }
}
