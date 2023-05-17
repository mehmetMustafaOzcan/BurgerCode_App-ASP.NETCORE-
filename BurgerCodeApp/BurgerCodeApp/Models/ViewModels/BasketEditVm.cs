namespace BurgerCodeApp.Models.ViewModels
{
    public class BasketEditVm
    {
        public BasketEditVm()
        {
            Extras = new();
            Sizes= new() {1,2,3};
        }
        public string MenuName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal MenüPrice { get; set; }
        public string Photopath { get; set; }
        public int Quantity { get; set; }
        public List<int> Extras { get; set; }
        public int Size { get; set; }
        public List<int> Sizes { get; set; }

    }
}
