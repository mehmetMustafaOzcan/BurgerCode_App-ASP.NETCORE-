using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCodeApp.Areas.Identity.Models
{
    [NotMapped]
    public class UserVm
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        [RegularExpression(@"^[^\s!@#$%^&*(),.?:{}|<>]+$", ErrorMessage = "Username must not contain special characters.")]
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address.")]
        public string EmailAddress { get; set; }
    }
}

