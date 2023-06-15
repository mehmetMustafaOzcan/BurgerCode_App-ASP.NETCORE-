using BurgerCodeApp.Domain.Entities.Abstracts;
using BurgerCodeApp.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Domain.Entities
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
