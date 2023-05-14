using Microsoft.AspNetCore.Identity;

namespace BurgerCodeApp.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
         Baskets = new HashSet<Basket>();
        }
        public int BasketId { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
