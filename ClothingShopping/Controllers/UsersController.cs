using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using ClothingShopping.Services;
using ClothingShopping.Store;
using ClothingShopping.Extension;

using ClothingShopping.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Security.Claims;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data.Entity;

namespace ClothingShopping.Controllers
{
    [Route("[Controller]/[Action]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext dbContext;

        private readonly IOrderItemService orderItemService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext _dbContext, IOrderService orderService, IOrderItemService orderItemService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            dbContext = _dbContext;
            _orderService = orderService;
            this.orderItemService = orderItemService;
        }


        public IActionResult Index()
        {
            var listUser = _userService.GetListApplicationUserByGroupId();
            return View(listUser);
        }
        [HttpGet]
        public ActionResult Details(string userId)
        {
            /*string id = _userManager.GetUserId(HttpContext.User);*/
            var user = _userService.GetDetail(userId);
            var model = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Details(string userId, UserViewModel model)
        {
            /*string id = _userManager.GetUserId(HttpContext.User);*/
            var user = _userService.GetDetail(userId);
            user.Name = model.Name;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            try
            {
                _userService.Save();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Orders(string userId)
        {
            var user = _userService.GetDetail(userId);
            var model = new OrderOfUserViewModel();
            model.UserId = userId;
            model.Address = user.Address;
            model.PhoneNumber = user.PhoneNumber;
            model.Name = user.Name;
            model.Orders = _orderService.GetListOrderbyUser(userId);
            return PartialView(model);
        }
        public async Task<IActionResult> OrderDetails(int OrderId)
        {
            string id = _userManager.GetUserId(HttpContext.User);
            var user = _userService.GetDetail(id);
            var model = new OrderItemViewModel();
            model.UserId = user.Id;
            model.Address = user.Address;
            model.PhoneNumber = user.PhoneNumber;
            model.Name = user.Name;
            model.Order = _orderService.GetDetail(OrderId);
            model.OrderItems = orderItemService.GetProductIdByOrder(OrderId);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string userId)
        {
            /*string id = _userManager.GetUserId(HttpContext.User);*/
            var model = new UserCPViewModel()
            {
                UserId = userId
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userId, UserCPViewModel model)
        {
            /*string id = _userManager.GetUserId(HttpContext.User);*/
            var user = _userService.GetDetail(userId);
            var oldPasswordHashed = _userManager.PasswordHasher.HashPassword(user, model.OldPassword);


            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }

            return RedirectToAction("Index", "Home");


        }
        public string GetName()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userService.GetDetail(userId);

            return user.Name;
        }


        public IActionResult Index2()
        {
            ViewBag.Check = "Nể Luôn2";
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            //Logout Trả về Url mặc đinh của page
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return Json(new { isSuccess = true, url = returnUrl });
                //return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return LocalRedirect(returnUrl);
            }
        }
        /*[Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUsers()
        {
            // Dành cho nhân viên quản lý hoặc là Admin
            return View(_userService.GetListApplicationUserByGroupId());
        }*/

        public async Task<IActionResult> ListRoles()
        {
            return View(_roleManager.Roles.ToList());
        }
        public async Task<IActionResult> UpdateUserToRole(string userId)
        {
            var user = _userService.GetDetail(userId);
            var UsertoRoleVM = new List<UsertoRoleViewModel>();
            foreach (var item in dbContext.Roles.ToList())
            {
                var UsertoRole = new UsertoRoleViewModel
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                    NormalizedName = item.NormalizedName
                };
                if (await _userManager.IsInRoleAsync(user, item.Name))
                {
                    UsertoRole.IsCheck = true;
                }
                else
                {
                    UsertoRole.IsCheck = false;
                }
                UsertoRoleVM.Add(UsertoRole);
            }
            return View(UsertoRoleVM);
        }
        [HttpPost]

        public async Task<IActionResult> UpdateUserToRole(List<UsertoRoleViewModel> UsertoRoleVM, string userId)
        {
            #region Dành cho MVC
            /*foreach (var item in UsertoRoleVM)
            {
                var role = await _roleManager.FindByIdAsync(item.RoleId);
                if (await _userManager.IsInRoleAsync(user, item.RoleName) && !item.IsCheck)
                {
                    var result = _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!await _userManager.IsInRoleAsync(user, item.RoleName) && item.IsCheck)
                {
                    var result = _userManager.AddToRoleAsync(user, role.Name);
                }
            }*/
            #endregion
            var user = _userService.GetDetail(userId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                return View(UsertoRoleVM);
            }
            result = await _userManager.AddToRolesAsync(user, UsertoRoleVM.Where(x => x.IsCheck).Select(p => p.RoleName));
            if (!result.Succeeded)
            {
                return View(UsertoRoleVM);
            }
            return RedirectToAction("ListUsers");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoles()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(IdentityRole role)
        {
            try
            {
                role.NormalizedName = role.Name;
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
                return RedirectToAction("ListRoles");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var claimsUser = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimViewModel
            {
                UserId = userId,
            };
            foreach (Claim item in ClaimStore.AllClaims)
            {
                var UserClaim = new UserClaim
                {
                    ClaimType = item.Type
                };
                if (claimsUser.Any(c => c.Type == item.Type))
                {
                    UserClaim.IsCheck = true;
                }

                model.Claims.Add(UserClaim);

            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            var claim = await _userManager.GetClaimsAsync(user);

            var result = await _userManager.RemoveClaimsAsync(user, claim);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot Remove"); return View(model);
            }
            result = await _userManager.
                AddClaimsAsync(user, model.Claims.Where(x => x.IsCheck).Select(x => new Claim(x.ClaimType, x.ClaimType)));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot Add"); return View(model);
            }

            return RedirectToAction("ListUsers");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
