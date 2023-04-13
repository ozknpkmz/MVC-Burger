using HamburgerMVC.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamburgerMVC.Models.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            ExtraIngredients= new List<ExtraIngredient>();
            oList = new List<OrderDTO>();
        }
        public List<OrderDTO> oList { get; set; }
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }   
        public Menu Menu { get; set; }
        public List<SelectListItem> DropDownForSize { get; set; }
        public ICollection<ExtraIngredient> ExtraIngredients { get; set; }
        public List<string> ExtraIngredientsNames { get; set; }
        public List<Menu> Menus { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public List<int> ExtraIds { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public List<OrderVM> OrderList { get; set; }
        public int OrderId { get; set; }
    }
}
