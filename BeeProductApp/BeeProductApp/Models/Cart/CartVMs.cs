namespace BeeProductApp.Models.Cart
{
    public class CartItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartIndexVM
    {
        public List<CartItemVM> Items { get; set; } = new();
        public decimal GrandTotal { get; set; }
    }
}
