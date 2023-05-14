using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class MenuDetail
    {
        public int MenuId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
