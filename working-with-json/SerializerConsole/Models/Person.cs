namespace SerializerConsole.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; }

        public bool IsAlive { get; set; }

        public Address? Address { get; set; }

        public IList<Phone>? Phones { get; set; }
    }
}
