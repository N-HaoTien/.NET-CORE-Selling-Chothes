using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using ClothingShopping.Repository;
using ClothingShopping.Services;
using System.Linq.Expressions;

namespace ClothingShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Controller]/[Action]")]

    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, ApplicationDbContext dbContext)
        {
            _categoryService = categoryService;
            _logger = logger;
            _dbContext = dbContext;
        }
        // GET: Admin/Category
        [Route("~/Category")]
        [Route("~/")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return PartialView(_categoryService.GetAll());
        }
        [HttpGet]

        public async Task<IActionResult> AddorUpdate(int id = 0)
        {
            if (id == 0)
            {
                return View(new Category());
            }
            else
            {
                if (_categoryService.GetDetail(id) == null)
                {
                    return NotFound();
                }
                return View(_categoryService.GetDetail(id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorUpdate(int id, [Bind("Image,Id,Name,Description,Status")] Category category)
        {
            category.Status = true;
            if (ModelState.IsValid)
            {
                if (_categoryService.CheckExistName(id, category.Name))
                {
                    if (id == 0)
                    {
                        _categoryService.Add(category);
                    }
                    else
                    {
                        _categoryService.Update(category);
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Category name already exists");
                    return Json(new { isSuccess = false, html = Helper.RenderRazorView(this, "AddorUpdate", category) });
                }

                //Helper.RenderRazorView(this,"ListCategory", _categoryService.GetAll())
                //Helper.RenderRazorView(this, "AddorUpdate", category)
                return Json(new { isSuccess = true, html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetAll()) });
            }
            return Json(new { isSuccess = false, html = Helper.RenderRazorView(this, "AddorUpdate", category) });

        }
        [HttpPost]
        public async Task<IActionResult> SearchCategory(string NameCategory)
        {
            if (string.IsNullOrEmpty(NameCategory))
            {
                return Json(new { isSuccess = false, html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetAll()) });
            }
            return Json(new { isSuccess = true, html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetAllByName(NameCategory)) });
        }
        [NonAction]
        public bool CheckStatus(int? Id)
        {
            return _categoryService.GetDetail(Id).Status;
        }
        // GET: Admin/Category/ChangeStatus/5
        [Route("{id?}")]
        public async Task<IActionResult> ChangeStatus(int? Id)
        {
            if (_categoryService.ChangeStatusCategory(Id))
            {
                _categoryService.ChangeStatusCategory(Id);
                return Json(new { isSuccess = true, message = "Đổi Trạng Thái từ " + CheckStatus(Id) + " sang " + !CheckStatus(Id) });
            }
            return Json(new { isSuccess = false });
        }
        // POST: Admin/Category/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _categoryService.Delete(id);
                return Json(new { isSuccess = true, html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetAll()) });
            }
            catch
            {
                return Json(new { isSuccess = false, toastr = "Error Bussiness by Product had CategoryId" });
            }
        }
    }
}
