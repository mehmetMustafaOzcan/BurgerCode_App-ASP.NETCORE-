using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCodeApp.Areas.Identity.Models
{
    [NotMapped]
    public class UserVm
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumberHash { get; set;
        }
    }
}
