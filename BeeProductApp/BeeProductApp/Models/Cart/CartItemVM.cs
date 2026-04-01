namespace BeeProductApp.Models.Cart
{
    internal class CartItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}