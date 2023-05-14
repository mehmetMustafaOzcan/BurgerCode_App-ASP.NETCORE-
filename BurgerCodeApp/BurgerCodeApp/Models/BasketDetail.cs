using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class BasketDetail
    {
        public int BasketId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public int MenuSize { get; set; }

        public virtual Basket Basket { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
    }
}
