using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Infrastructure.Data.Domain
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        [Range(1, 5000)]
        public int Quantity { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.Now;

        public decimal TotalPrice => Quantity * Product.Price
                                     - Quantity * Product.Price * Product.Discount / 100;
    }
}
