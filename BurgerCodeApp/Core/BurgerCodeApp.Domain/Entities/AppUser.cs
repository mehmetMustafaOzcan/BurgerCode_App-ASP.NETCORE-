using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BurgerCodeApp.Domain.Entities
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
