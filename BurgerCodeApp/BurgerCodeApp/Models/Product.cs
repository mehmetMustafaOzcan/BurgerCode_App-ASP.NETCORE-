using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Product
    {
        public Product()
        {
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public string? PicturePath { get; set; }

        public virtual Category? Category { get; set; } = null!;
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
