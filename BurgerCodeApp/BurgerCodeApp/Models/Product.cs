﻿using System;
using System.Collections.Generic;
using BurgerCodeApp.Models.Abstracts;

namespace BurgerCodeApp.Models
{
    public partial class Product: BaseProduct
    {
        public Product()
        {
            MenuDetails = new HashSet<MenuDetail>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; } = null!;
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
