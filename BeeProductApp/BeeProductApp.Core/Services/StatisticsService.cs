using BeeProductApp.Core.Contracts;
using BeeProductApp.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Core.Services
{
    public class StatisticsService : IStatisticService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CountClients()
        {
            // Връща броя потребители без администратора
            return _context.Users.Count() - 1;
        }

        public int CountOrders()
        {
            return _context.Orders.Count();
        }

        public int CountProducts()
        {
            return _context.Products.Count();
        }

        public decimal SumOrders()
        {
            var suma = _context.Orders.Sum(x => x.Quantity * x.Price - x.Quantity * x.Price *x.Discount / 100);
            return suma;
        }
    }

}
