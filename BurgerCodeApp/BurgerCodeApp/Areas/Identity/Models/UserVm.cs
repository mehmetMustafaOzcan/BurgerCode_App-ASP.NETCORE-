using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCodeApp.Areas.Identity.Models
{
    [NotMapped]
    public class UserVm
    {

        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Kullanıcı adı 5 ila 20 karakter arasında olmalıdır.")]
        [RegularExpression(@"^[^\s!@#$%^&*(),.?:{}|<>]+$", ErrorMessage = "Kullanıcı adı özel karakter içermemelidir.")]
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "Pasword!")]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string EmailAddress { get; set; }
    }
}

