using HamburgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HamburgerMVC.Models.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public Menu Menu { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
