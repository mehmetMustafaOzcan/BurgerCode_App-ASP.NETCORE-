﻿using BurgerCodeApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCodeApp.Areas.Admin.Models
{
    [NotMapped]
    public class MenuVm
        
    {

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuCategoryId { get; set; }
        public decimal MenuPrice { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
