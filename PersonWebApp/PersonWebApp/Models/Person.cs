using System.ComponentModel.DataAnnotations;

namespace PersonWebApp.Models
{
    public class Person
    {

        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public required string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public required string LastName { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
        }
    }
}