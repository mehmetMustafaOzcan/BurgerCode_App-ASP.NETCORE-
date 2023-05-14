using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class ExtraDetail
    {
        public int BasketId { get; set; }
        public int ExtraId { get; set; }
        public string Quantity { get; set; } = null!;

        public virtual Basket Basket { get; set; } = null!;
        public virtual Extra Extra { get; set; } = null!;
    }
}
