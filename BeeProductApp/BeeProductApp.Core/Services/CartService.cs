using BeeProductApp.Core.Contracts;
using BeeProductApp.Infrastructure.Data;
using BeeProductApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CartItem> GetCartItems(string userId)
        {
            return _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public bool AddOrUpdate(int productId, string userId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null || product.Quantity < quantity)
                return false;

            var existing = _context.CartItems
                .SingleOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (existing != null)
            {
                existing.Quantity += quantity;
                _context.CartItems.Update(existing);
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity,
                    AddedOn = DateTime.Now
                });
            }

            return _context.SaveChanges() != 0;
        }

        public bool Remove(int productId, string userId)
        {
            var item = _context.CartItems
                .SingleOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (item == null) return false;

            _context.CartItems.Remove(item);
            return _context.SaveChanges() != 0;
        }

        public bool Clear(string userId)
        {
            var items = _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();

            _context.CartItems.RemoveRange(items);
            return _context.SaveChanges() != 0;
        }

        public decimal GetTotal(string userId)
        {
            return _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList()                          // materialise so EF doesn't try to translate the computed property
                .Sum(c => c.TotalPrice);
        }

       
    }
}
