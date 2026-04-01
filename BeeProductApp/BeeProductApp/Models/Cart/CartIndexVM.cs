namespace BeeProductApp.Models.Cart
{
    internal class CartIndexVM
    {
        public List<CartItemVM> Items { get; set; }
        public decimal GrandTotal { get; set; }
    }

}