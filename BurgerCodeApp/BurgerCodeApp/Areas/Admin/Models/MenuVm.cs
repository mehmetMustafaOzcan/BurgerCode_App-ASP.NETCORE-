namespace BurgerCodeApp.Areas.Admin.Models
{
    public class MenuVm
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuCategoryId { get; set; }
        public decimal MenüPrice { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
