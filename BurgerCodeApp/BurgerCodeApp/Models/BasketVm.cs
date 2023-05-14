namespace BurgerCodeApp.Models
{
    public class BasketVm
    {
        public int Size { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int Quantity { get; set; }
        public List<string> Extras { get; set; }
    }
}
