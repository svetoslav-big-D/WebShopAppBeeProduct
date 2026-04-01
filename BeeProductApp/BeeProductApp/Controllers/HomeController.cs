using System.Diagnostics;

using BeeProductApp.Core.Contracts;
using BeeProductApp.Core.Services;
using BeeProductApp.Models;
using BeeProductApp.Models.Product;

using Microsoft.AspNetCore.Mvc;

namespace BeeProductApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            List<ProductIndexVM> topProductsEntities = _productService.GetTop3Products()
            .Select(product => new ProductIndexVM
             {
                 Id = product.Id,
                 ProductName = product.ProductName,
                 Picture = product.Picture,
                 Price = product.Price,
             }).ToList();
            return View(topProductsEntities);
        }

        public IActionResult Privacy()
        {
            return View();
        }

  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
