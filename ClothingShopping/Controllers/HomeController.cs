using ClothingShopping.Common;
using ClothingShopping.Extension;
using ClothingShopping.Models;
using ClothingShopping.Services;
using ClothingShopping.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using RazorEngine;
using RazorEngine.Templating;
namespace ClothingShopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISizeService _sizeService;
        private readonly IEmailSender _emailSender;
        private readonly ITemplateService _templateService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, ISizeService sizeService, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment, ITemplateService templateService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _sizeService = sizeService;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
            this._templateService = templateService;
        }
        //[Route("~/")] set Default
        public async Task<IActionResult> Index()
        {
            /*string PathView = Path.Combine(_webHostEnvironment.ContentRootPath, "~/Views/Shared/_EmailContent.cshtml");
            ViewBag.Test = "a";
            //string body = System.IO.File.ReadAllText(PathView.Replace("~/", ""));
            string body = await GetGridContentAsync(_productService.GetListAllProductIncludeCategory());
            _emailSender.SendEmailAsync("nht.it19@gmail.com", "Trân trọng kính gửi...", body);*/
            int pageSize = 10;
            var request = new GetPagedRequest() 
            {
                pageIndex = 1,
                pageSize = pageSize
            };
            var dataProduct = _productService.GetPagingProduct(request);

            return PartialView(dataProduct);
        }
        private async Task<string> GetGridContentAsync(IEnumerable<Product> data)
        {
            string template = Url.Content("_EmailContent");
            return await _templateService.GetTemplateHtmlAsStringAsync(template, data.ToList());
        }
        public IActionResult Shopping()
        {
            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(_categoryService.GetAll().ToList(), "Id", "NameWithIcon");

            int pageSize = 10;
            var request = new GetPagedRequest()
            {
                pageIndex = 1,
                pageSize = pageSize
            };
            var dataProduct = _productService.GetPagingProduct(request);

            return PartialView(dataProduct);
        }
        [HttpPost]
        public async Task<IActionResult> SearchPagination(int Index, string keyword, int? CategoryId, bool? StatusProduct,string CategoriesId)
        {
            List<int> CategoriesIDs = !string.IsNullOrEmpty(CategoriesId)
                ? CategoriesId.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            int pageSize = 9;
            var request = new GetPagedRequest()
            {
                Keyword = keyword,
                ForeignKeyId = CategoryId,
                Status = StatusProduct,
                pageIndex = Index,
                pageSize = pageSize,
                CategoriesId = CategoriesIDs
            };
            var dataProduct = _productService.GetPagingProduct(request);
            return Json(new
            {
                isSuccess = true,
                dataList = _productService.GetPagingProduct(request),
                html = Helper.RenderRazorView(this, "ListProducts", dataProduct),
                ComponentHtml = Helper.RenderRazorView(this, "DefaultPaginationProduct", dataProduct),
            });
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //Product
        public IActionResult Details(int productId)
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