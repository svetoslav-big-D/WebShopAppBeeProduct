
namespace BeeProductApp.Models.Favourite
{
    public class FavouriteIndexVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime AddedOn { get; set; }

        public decimal DiscountedPrice =>
            Price - Price * Discount / 100;
    }
}
