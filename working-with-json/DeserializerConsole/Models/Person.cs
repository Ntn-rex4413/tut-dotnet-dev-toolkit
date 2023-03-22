using System.Text.Json.Serialization;

namespace DeserializerConsole.Models
{
    public class Person
    {
        public int Id { get; set; }

        [JsonPropertyName("firstname")]
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
    }
}
