using BeeProductApp.Core.Contracts;
using BeeProductApp.Models.Cart;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BeeProductApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
        }

        // GET: /Cart/Index
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = _cartService.GetCartItems(userId)
                .Select(c => new CartItemVM
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.ProductName,
                    Picture = c.Product.Picture,
                    Price = c.Product.Price,
                    Discount = c.Product.Discount,
                    Quantity = c.Quantity,
                    TotalPrice = c.TotalPrice
                }).ToList();

            var vm = new CartIndexVM
            {
                Items = items,
                GrandTotal = items.Sum(i => i.TotalPrice)
            };

            return View(vm);
        }

        // POST: /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, int quantity = 1, string returnUrl = "/Product/Index")
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = _productService.GetProductById(productId);

            if (product == null || product.Quantity == 0)
                return RedirectToAction("Denied");

            _cartService.AddOrUpdate(productId, userId, quantity);
            return Redirect(returnUrl);
        }

        // POST: /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.Remove(productId, userId);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Checkout  — converts every cart item into an Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _cartService.GetCartItems(userId);

            if (!items.Any())
                return RedirectToAction(nameof(Index));

            foreach (var item in items)
            {
                var product = _productService.GetProductById(item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                    return RedirectToAction("Denied");

                _orderService.Create(item.ProductId, userId, item.Quantity);
            }

            _cartService.Clear(userId);
            return RedirectToAction("CheckoutSuccess");
        }

        public IActionResult CheckoutSuccess() => View();

        public IActionResult Denied() => View();
    }
}
