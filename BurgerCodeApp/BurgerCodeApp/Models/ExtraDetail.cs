using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class ExtraDetail
    {
        public int BasketDetailId { get; set; }
        public int ExtraId { get; set; }
        public int Quantity { get; set; }

        public virtual BasketDetail BasketDetail { get; set; } = null!;
        public virtual Extra Extra { get; set; } = null!;
    }
}
