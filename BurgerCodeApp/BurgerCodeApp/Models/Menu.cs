using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Menu
    {
        public Menu()
        {
            BasketDetails = new HashSet<BasketDetail>();
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public int? MenuCategoryId { get; set; }
        public string? MenüPrice { get; set; }

        public virtual MenuCategory? MenuCategory { get; set; }
        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
