using BurgerCodeApp.Models.Abstracts;
using BurgerCodeApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Extra: BaseProduct
    {
        public Extra()
        {
            ExtraDetails = new HashSet<ExtraDetail>();
        }

        public int ExtraId { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ExtraDetail> ExtraDetails { get; set; }
    }
}
