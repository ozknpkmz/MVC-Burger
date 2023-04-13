namespace HamburgerMVC.Models
{
    public class MenuOrder
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
