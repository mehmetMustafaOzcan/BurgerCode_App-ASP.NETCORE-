using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class BasketDetail
    {
        public BasketDetail()
        {
            ExtraDetails = new HashSet<ExtraDetail>();
        }

        public int BasketId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public int MenuSize { get; set; }
        public int BasketDetailId { get; set; }

        public virtual Basket Basket { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
        public virtual ICollection<ExtraDetail> ExtraDetails { get; set; }
    }
}
