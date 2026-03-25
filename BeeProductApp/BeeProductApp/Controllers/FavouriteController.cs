using BeeProductApp.Core.Contracts;
using BeeProductApp.Models.Favourite;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BeeProductApp.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        private readonly IFavouriteService _favouriteService;
        private readonly IProductService _productService;

        public FavouriteController(IFavouriteService favouriteService, IProductService productService)
        {
            _favouriteService = favouriteService;
            _productService = productService;
        }

        // GET: /Favourite/Index
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = _favouriteService.GetFavouritesByUser(userId)
                .Select(f => new FavouriteIndexVM
                {
                    ProductId = f.ProductId,
                    ProductName = f.Product.ProductName,
                    Picture = f.Product.Picture,
                    Price = f.Product.Price,
                    Discount = f.Product.Discount,
                    AddedOn = f.AddedOn
                }).ToList();

            return View(items);
        }

        // POST: /Favourite/Add/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string returnUrl = "/Product/Index")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favouriteService.Add(productId, userId);
            return Redirect(returnUrl);
        }

        // POST: /Favourite/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId, string returnUrl = "/Favourite/Index")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favouriteService.Remove(productId, userId);
            return Redirect(returnUrl);
        }
    }
}
