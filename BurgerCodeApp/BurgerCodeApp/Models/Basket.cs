using BurgerCodeApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Basket
    {
        public Basket()
        {
            BasketDetails = new HashSet<BasketDetail>();
            ExtraDetails = new HashSet<ExtraDetail>();
        }

        public int BasketId { get; set; }
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public BasketStage Stage { get; set; } = BasketStage.Active;

        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
        public virtual ICollection<ExtraDetail> ExtraDetails { get; set; }
    }
}
