namespace HamburgerMVC.Models
{
    public class ExtraIngredientOrder
    {
        public int ExtraIngredientId { get; set; }
        public ExtraIngredient ExtraIngredient { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
