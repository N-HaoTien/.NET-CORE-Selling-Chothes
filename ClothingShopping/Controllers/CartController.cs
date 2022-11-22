using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Extension;
using ClothingShopping.Models;
using ClothingShopping.Repository;
using ClothingShopping.Services;
using ClothingShopping.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace ClothingShopping.Controllers
{
    public class CartController : Controller
    {
        public IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public const string CartKey = "Cart";


        public CartController(IProductService productService, SignInManager<ApplicationUser> signInManager = null, UserManager<ApplicationUser> userManager = null, IUserService userService = null, IOrderService orderService = null, IOrderItemService orderItemService = null)
        {
            this._productService = productService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCartItems();
            return View(cart);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            CheckoutViewModel checkout = new CheckoutViewModel();

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userService.GetDetail(userId);
            if (user == null) return NotFound();

            checkout.UserId = user.Id;
            checkout.Name = user.Name;
            checkout.Address = user.Address;
            checkout.Phone = user.PhoneNumber;
            checkout.Email = user.Email;
            checkout.Cart = GetCartItems();
            return View(checkout);
        }
        public void SaveOrder(CheckoutViewModel checkout)
        {
            var user = _userService.GetDetail(checkout.UserId);
            user.Name = checkout.Name;
            user.Address = checkout.Address;
            user.PhoneNumber = checkout.Phone;
            _userService.Save();
            var order = new Order();
            order.UserId = checkout.UserId;
            order.Total = decimal.Parse(checkout.Cart.Sum(p => p.ThanhTien).ToString());
            order.CreatedDate = DateTime.Now;
            order.IsPayBill = checkout.IsCheckedPayment;
            _orderService.Add(order);
            _orderService.Save();

            foreach (var item in GetCartItems())
            {
                var orderItems = new OrderItem();
                orderItems.OrderId = order.Id;
                orderItems.ProductId = item.ProductId;
                orderItems.Quantity = item.Quantity;
                _orderItemService.Add(orderItems);
            }
            try
            {
                _orderItemService.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel checkout)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            checkout.Cart = GetCartItems();
            if (!checkout.IsCheckedPayment)
            {
                SaveOrder(checkout);
                CleanCart();
                TempData["Message"] = "Success";
                return RedirectToAction("Shopping", "Home");
            }
            return RedirectToAction("PaymentWithPaypal", "Payment", new { ckViewModel = checkout });


            //return RedirectToAction("Index", "Home");
        }
        // Get List Cart
        public List<CartViewModel> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CartKey);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartViewModel>>(jsoncart);
            }
            return new List<CartViewModel>();
        }
        // Save Session["Cart"] by GetCartItems()
        #region Save Cart
        [NonAction]
        void SaveCartSession(List<CartViewModel> cart)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(cart);
            session.SetString(CartKey, jsoncart);
        }
        [NonAction]
        public void CheckCountProduct()
        {
            var cart = GetCartItems();
            foreach (var item in cart.ToList())
            {
                if (item.Quantity == 0)
                {
                    cart.Remove(item);
                }
            }
            SaveCartSession(cart.ToList());
        }
        #endregion
        // End Save 
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var product = _productService.GetDetail(productId);
            if (product == null)
                return Json(new
                {
                    IsSuccess = false,

                });
            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.ProductId == productId);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartViewModel() { Quantity = 1, ProductId = product.Id, Name = product.Name, Price = product.NewPrice, urlPicture = product.UrlPictureBg });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return Json(new
            {
                IsSuccess = true,
                Count = GetCartItems().Count(),
                SumItems = GetCartItems().Sum(p => p.Quantity),
                Total = String.Format(info, "{0:c}", GetCartItems().Sum(p => p.ThanhTien)),
                ModalCart = Helper.RenderRazorView(this, "_Modal_Cart", GetCartItems())
            });
        }
        [HttpPost]

        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

            var cart = GetCartItems();
            CartViewModel sp = cart.SingleOrDefault(n => n.ProductId == productId);
            if (sp != null)
            {
                sp.Quantity = quantity;
                CheckCountProduct();
                SaveCartSession(cart);
                return Json(new
                {
                    IsSuccess = true,
                    Count = GetCartItems().Count(),
                    SumItems = GetCartItems().Sum(p => p.Quantity),
                    Total = String.Format(info, "{0:c}", GetCartItems().Sum(p => p.ThanhTien)),
                    ModalCart = Helper.RenderRazorView(this, "_Modal_Cart", GetCartItems()),
                    CartIndex = Helper.RenderRazorView(this, "_Cart", GetCartItems())
                });
            }
            return Json(new
            {
                IsSucess = false
            });
        }

        [HttpPost]

        public async Task<IActionResult> Remove(int productId)
        {
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var session = HttpContext.Session;
            List<CartViewModel> cart = GetCartItems();
            cart.RemoveAll(x => x.ProductId == productId);
            SaveCartSession(cart);
            return Json(new
            {
                IsSuccess = true,
                Count = GetCartItems().Count(),
                SumItems = GetCartItems().Sum(p => p.Quantity),
                Total = String.Format(info, "{0:c}", GetCartItems().Sum(p => p.ThanhTien)),
                ModalCart = Helper.RenderRazorView(this, "_Modal_Cart", GetCartItems()),
                CartIndex = Helper.RenderRazorView(this, "_Cart", GetCartItems())
            });
        }

        public void CleanCart()
        {
            var session = HttpContext.Session;
            session.Remove(CartKey);
        }

        public IActionResult Success(string mess,CheckoutViewModel model)
        {
            SaveOrder(model);
            CleanCart();
            TempData["Message"] = "Your order has been successfully placed";
            return RedirectToAction("Shopping","Home");
        }
    }
}
