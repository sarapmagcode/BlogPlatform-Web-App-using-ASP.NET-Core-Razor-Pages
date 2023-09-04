using System.ComponentModel.DataAnnotations;

namespace BlogPlatformWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime AccountCreated { get; set; }
    }
}
