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
            if (string.IsNullOrEmpty(userId)) return Challenge();

            var favourites = _favouriteService.GetFavouritesByUser(userId);

            var viewModel = favourites.Select(f => new FavouriteIndexVM
            {
                ProductId = f.ProductId,
                ProductName = f.Product.ProductName,
                Picture = f.Product.Picture,
                Price = f.Product.Price,
                Discount = f.Product.Discount,
                AddedOn = f.AddedOn
            }).ToList();

            return View(viewModel);
        }

        // POST: /Favourite/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, string returnUrl = "/Product/Index")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Challenge();

            // Използваме сервиза за добавяне
            _favouriteService.Add(productId, userId);

            // Връщаме потребителя там, откъдето е дошъл
            return Redirect(returnUrl);
        }

        // POST: /Favourite/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId, string returnUrl = "/Favourite/Index")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Challenge();

            _favouriteService.Remove(productId, userId);

            return Redirect(returnUrl);
        }
    }
}