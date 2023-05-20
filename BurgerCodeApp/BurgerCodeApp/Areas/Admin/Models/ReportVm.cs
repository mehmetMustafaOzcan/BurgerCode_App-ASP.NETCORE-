namespace BurgerCodeApp.Areas.Admin.Models
{
    public class ReportVm
    {
        public decimal TotalEarning { get; set; }
        public decimal ExtraEarning { get; set; }
        public decimal MenuEarning { get; set; }
        public string FirstMenu { get; set; }
        public int FirstMenuQuantity { get; set; }
    }
}
