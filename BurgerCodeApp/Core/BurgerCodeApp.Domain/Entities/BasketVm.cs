using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCodeApp.Models
{
    [NotMapped]
    public class BasketVm
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public decimal MenuPrice { get; set; }
        public string PicturePath { get; set; }
        public List<int> Extras { get; set; }
    }
}
