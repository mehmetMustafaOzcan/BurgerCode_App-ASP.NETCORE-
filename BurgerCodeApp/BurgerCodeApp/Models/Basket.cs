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
        }

        public int BasketId { get; set; }
        public string AppUserId { get; set; } = null!;
        public BasketStage Stage { get; set; }
        public AppUser AppUser { get; set; }

        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
    }
}
