namespace HamburgerMVC.Models.DTOs
{
    public class ExtraIngredientDTO
    {
        public int ExtraIngredientId { get; set; }
        public string ExtraIngredientName { get; set; }
        public decimal ExtraIngredientPrice { get; set; }
        public ExtraIngredient ExtraIngredient { get; set; }

    }
}
