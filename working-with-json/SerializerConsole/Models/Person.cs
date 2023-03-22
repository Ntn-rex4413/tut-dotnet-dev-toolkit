using System.Text.Json.Serialization;

namespace SerializerConsole.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        // value specified in decorator overrides JsonSerializerOptions' NamingPolicy (e.g. casing);
        [JsonPropertyName("surname")]
        public string? LastName { get; set; }

        public int Age { get; set; }

        public bool IsAlive { get; set; }

        public Address? Address { get; set; }

        public IList<Phone>? Phones { get; set; }
    }
}
