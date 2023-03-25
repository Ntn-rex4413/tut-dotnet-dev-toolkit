using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? DoB { get; set; }

        public int YearsAlive
        {
            get
            {
                var today = DateTime.Today;

                var splitDob = DoB!.Split("-");

                // doesn't work correctly for leap years etc.
                return today.Year - int.Parse(splitDob[0]);
            }
        }

        public string? House { get; set; }

        public int Points { get; set; }
    }
}