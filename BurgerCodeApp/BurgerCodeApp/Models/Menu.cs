using BurgerCodeApp.Models.Abstracts;
using BurgerCodeApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Menu: BaseProduct
    {
        public Menu()
        {
            BasketDetails = new HashSet<BasketDetail>();
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int MenuId { get; set; }
        public int? MenuCategoryId { get; set; }
        public string? Description { get; set; }

        public virtual MenuCategory? MenuCategory { get; set; }
        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
