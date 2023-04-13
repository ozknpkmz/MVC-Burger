namespace HamburgerMVC.Models.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        //public string Image { get; set; }
        public Menu Menu { get; set; }
    }
}
