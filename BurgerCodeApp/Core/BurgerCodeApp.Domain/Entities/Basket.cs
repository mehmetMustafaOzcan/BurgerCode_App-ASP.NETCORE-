using BurgerCodeApp.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Domain.Entities
{
    public partial class Basket
    {
        public Basket()
        {
            BasketDetails = new HashSet<BasketDetail>();
        }

        public int BasketId { get; set; }
        public string AppUserId { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public BasketStage Stage { get; set; }
        public DateTime? ComplateDate { get; set; }
        public AppUser AppUser { get; set; }

        public virtual ICollection<BasketDetail> BasketDetails { get; set; }
    }
}
