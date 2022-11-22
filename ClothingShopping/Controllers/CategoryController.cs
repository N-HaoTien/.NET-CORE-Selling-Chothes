using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Common;
using ClothingShopping.Models;
using ClothingShopping.Services;
using Microsoft.AspNetCore.Mvc;
using ClothingShopping.Extension;
namespace ClothingShopping.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IViewRenderService _viewRender;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, ApplicationDbContext dbContext, IViewRenderService viewRender)
        {
            _categoryService = categoryService;
            _logger = logger;
            _dbContext = dbContext;
            _viewRender = viewRender;
        }
        // GET: Admin/Category
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pageSize = 3;

            var request = new GetPagedRequest()
            {
                pageIndex = 1,
                pageSize = pageSize
            };
            return PartialView(_categoryService.GetPagingCategory(request));
        }
        [HttpPost]
        public async Task<IActionResult> SearchPagination(int Index, string keyword = null)
        {
            int pageSize = 3;
            //ComponentHtml = await _viewRender.RenderToStringAsync("Default", _categoryService.GetPagingCategory(request))
            var request = new GetPagedRequest()
            {
                Keyword = keyword,
                pageIndex = Index,
                pageSize = pageSize,
            };

            return Json(new
            {
                isSuccess = true,
                html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetPagingCategory(request)),
                ComponentHtml = Helper.RenderRazorView(this, "DefaultPaginationCategory", _categoryService.GetPagingCategory(request))
            });

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
            int pageSize = 3;

            var request = new GetPagedRequest()
            {
                pageIndex = 1,
                pageSize = pageSize
            };
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
                return Json(new { isSuccess = true, html = Helper.RenderRazorView(this, "ListCategory", _categoryService.GetPagingCategory(request)) });
            }
            return Json(new { isSuccess = false, html = Helper.RenderRazorView(this, "AddorUpdate", category) });

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
