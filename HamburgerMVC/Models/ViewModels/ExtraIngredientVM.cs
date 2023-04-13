using HamburgerMVC.Models.DTOs;

namespace HamburgerMVC.Models.ViewModels
{
    public class ExtraIngredientVM
    {
        public ExtraIngredient ExtraIngredient { get; set; }
        public List<ExtraIngredientDTO> eList { get; set; }
        public int ExtraIngredientId { get; set; }
        public string ExtraIngredientName { get; set; }
        public decimal ExtraIngredientPrice { get; set; }
        public bool Selected { get; set; }
    }
}
