using BurgerCodeApp.Domain.Entities.Enums;

namespace BurgerCodeApp.Domain.Entities.Abstracts
{
    public abstract class BaseProduct
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? PicturePath { get; set; }
        public int? Stock { get; set; }
        public Status SaleStatus { get; set; } = Status.Onsale;
    }
}
