using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Domain.Entities
{
    public partial class MenuCategory
    {
        public MenuCategory()
        {
            Menus = new HashSet<Menu>();
        }

        public int MenuCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
