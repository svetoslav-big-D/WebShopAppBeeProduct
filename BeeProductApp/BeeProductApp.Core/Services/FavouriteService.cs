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
    public class FavouriteService : IFavouriteService
    {
        private readonly ApplicationDbContext _context;

        public FavouriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Favourite> GetFavouritesByUser(string userId)
        {
            return _context.Favourites
                .Where(f => f.UserId == userId)
                .ToList();
        }

        public bool Add(int productId, string userId)
        {
            // Already in favourites? Do nothing.
            if (IsFavourite(productId, userId))
                return false;

            var product = _context.Products.Find(productId);
            if (product == null) return false;

            _context.Favourites.Add(new Favourite
            {
                ProductId = productId,
                UserId = userId,
                AddedOn = DateTime.Now
            });

            return _context.SaveChanges() != 0;
        }

        public bool Remove(int productId, string userId)
        {
            var item = _context.Favourites
                .SingleOrDefault(f => f.ProductId == productId && f.UserId == userId);

            if (item == null) return false;

            _context.Favourites.Remove(item);
            return _context.SaveChanges() != 0;
        }

        public bool IsFavourite(int productId, string userId)
        {
            return _context.Favourites
                .Any(f => f.ProductId == productId && f.UserId == userId);
        }

        List<Favourite> IFavouriteService.GetFavouritesByUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
