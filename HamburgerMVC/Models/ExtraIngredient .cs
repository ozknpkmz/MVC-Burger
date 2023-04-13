namespace HamburgerMVC.Models
{
    public class ExtraIngredient
    {
        public ExtraIngredient()
        {
            Orders = new HashSet<ExtraIngredientOrder>();
        }
        public int ExtraIngredientId { get; set; }
        public string ExtraIngredientName { get; set; }
        public decimal ExtraIngredientPrice { get; set; }
        public ICollection<ExtraIngredientOrder> Orders { get; set; }
        public bool Selected { get; set; }
    }
}
