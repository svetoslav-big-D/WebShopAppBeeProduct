using BeeProductApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Core.Contracts
{
    public interface IFavouriteService
    {
        List<Favourite> GetFavouritesByUser(string userId);
        bool Add(int productId, string userId);
        bool Remove(int productId, string userId);
        bool IsFavourite(int productId, string userId);
    }
}
