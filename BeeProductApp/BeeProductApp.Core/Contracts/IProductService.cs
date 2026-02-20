using BeeProductApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeProductApp.Core.Contracts
{
    public interface IProductService
    {
        bool Create(string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);

        bool Update(int productId, string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);

        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int dogproductId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName);
        object Create(object productName, object brandId, object categoryId, object pro);
        bool Create(object productName, object brandId, object categoryId, object picture, object quantity, object price, object discount);
    }
}
