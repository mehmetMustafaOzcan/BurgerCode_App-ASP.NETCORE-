using System;
using System.Collections.Generic;

namespace BurgerCodeApp.Models
{
    public partial class Extra
    {
        public Extra()
        {
            ExtraDetails = new HashSet<ExtraDetail>();
        }

        public int ExtraId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ExtraDetail> ExtraDetails { get; set; }
    }
}
