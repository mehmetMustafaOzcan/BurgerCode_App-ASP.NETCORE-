using Microsoft.AspNetCore.Identity;

namespace BurgerCodeApp.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
         Baskets = new HashSet<Basket>();
        }
        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
