using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Dtos
{
    public class PersonUpdateDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? DoB { get; set; }
    }
}
