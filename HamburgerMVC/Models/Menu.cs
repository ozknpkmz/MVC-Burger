namespace HamburgerMVC.Models
{
    public class Menu
    {
        public Menu()
        {
            Orders = new HashSet<MenuOrder>();
        }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        //public string Image { get; set; }
        public ICollection<MenuOrder> Orders { get; set; }
    }
}
