using BeeProductApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Core.Contracts
{
    public interface ICartService
    {
        List<CartItem> GetCartItems(string userId);
        bool AddOrUpdate(int productId, string userId, int quantity);
        bool Remove(int productId, string userId);
        bool Clear(string userId);
        decimal GetTotal(string userId);
    }
}
