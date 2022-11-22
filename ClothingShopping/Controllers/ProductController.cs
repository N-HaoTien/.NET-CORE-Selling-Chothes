using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Common;
using ClothingShopping.Extension;
using ClothingShopping.Models;
using ClothingShopping.Repository;
using ClothingShopping.Services;
using ClothingShopping.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using System.Data.Entity;
using System.Drawing;

namespace ClothingShopping.Controllers
{
    public class ProductController : Controller
    {
        public static List<string> UrlFile = new List<string>();
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISizeService _sizeService;
        private readonly IProductSizeService _productSizeService;
        private readonly ApplicationDbContext db;

        public ProductController(IProductService productService, ICategoryService categoryService, IPictureService pictureService, IWebHostEnvironment webHostEnvironment, ISizeService sizeService, IProductSizeService productSizeService, ApplicationDbContext db = null)
        {
            this._productService = productService;
            _categoryService = categoryService;
            _pictureService = pictureService;
            _webHostEnvironment = webHostEnvironment;
            _sizeService = sizeService;
            _productSizeService = productSizeService;
            this.db = db;
        }
        public IActionResult Index()
        {
            int pageSize = 3;
            List<SelectListItem> SelectStatus = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Active", Value = "true" },
                new SelectListItem() { Text = "NotActive", Value = "false" }
            };
            ViewBag.CategoryList = new SelectList(_categoryService.GetAll().ToList(), "Id", "NameWithIcon");

            ViewBag.SelectedStatus = SelectStatus;
            var request = new GetPagedRequest()
            {
                pageIndex = 1,
                pageSize = pageSize
            };
            var dataProduct = _productService.GetPagingProduct(request);

            return PartialView(dataProduct);
        }
        [HttpPost]
        public IActionResult SearchPagination(int Index, string keyword, int? CategoryId, bool? StatusProduct)
        {

            int pageSize = 3;

            //ViewBag.Status = 
            //ComponentHtml = await _viewRender.RenderToStringAsync("Default", _categoryService.GetPagingCategory(request))
            var request = new GetPagedRequest()
            {
                Keyword = keyword,
                ForeignKeyId = CategoryId,
                Status = StatusProduct,
                pageIndex = Index,
                pageSize = pageSize,
            };
            var dataProduct = _productService.GetPagingProduct(request);
            return Json(new
            {
                isSuccess = true,
                html = Helper.RenderRazorView(this, "ListProducts", dataProduct),
                ComponentHtml = Helper.RenderRazorView(this, "DefaultPaginationProduct", dataProduct),
            });
        }
        [HttpGet]
        public async Task<IActionResult> AddorUpdate(int id = 0)
        {
            ProductActionModel model = new ProductActionModel();
            model.CategoryList = new SelectList(_categoryService.GetAll().ToList(), "Id", "NameWithIcon");
            ViewBag.Product = "Create";
            //model.Sizes = _sizeService.GetAll();
            if (id == 0)
            {

                return View(model);
            }
            var product = _productService.GetDetail(id);
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.CreatedDate = product.CreatedDate;
            model.OldPrice = product.OldPrice;
            model.NewPrice = product.NewPrice;
            model.Status = product.Status;
            model.UrlPictureBg = product.UrlPictureBg;
            model.CategoryId = product.CategoryId;
            model.productPictures = _pictureService.GetPictureByProductId(product.Id);
            foreach (var item in model.productPictures)
            {
                UrlFile.Add(item.Url);
            }
            model.Sizes = _sizeService.GetSizebyCategoryId(model.CategoryId);
            model.ProductSize = _productSizeService.GetSizeByProductId(product.Id);
            ViewBag.Product = "Update";
            ViewBag.ProductName = model.Name;
            return View(model);
            //ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
        }
        [HttpPost]
        public async Task<IActionResult> AddorUpdate(ProductActionModel model)
        {
            List<int> SizeIDs = !string.IsNullOrEmpty(model.SizeIDs)
                ? model.SizeIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var sizes = _sizeService.GetSizebyId(SizeIDs);
            /*var pictures = _pictureService.GetPictureByProductId(model.Id);*/
            // sửa
            int a;

            if (model.Id == 0)
            {

                Product product = new Product();

                product.Name = model.Name;

                product.Description = model.Description;

                product.Status = true;

                product.NewPrice = model.NewPrice;

                product.CategoryId = model.CategoryId;

                product.CreatedDate = DateTime.Now;

                product.UrlPictureBg = model.UrlPictureBg;
                product.Pictures = new List<Picture>();
                foreach (var file in UrlFile)
                {
                    Picture picture = new Picture()
                    {
                        Url = file,
                        CreatedDate = DateTime.Now
                    };
                    product.Pictures.Add(picture);
                }
                product.ProductSizes = new List<ProductSize>();
                product.ProductSizes.AddRange(sizes.Select(p => new ProductSize() { SizeId = p.Id }));
                _productService.Add(product);

                return RedirectToAction("Index");


            }
            else
            {
                var product = _productService.GetDetail(model.Id);
                product.Id = model.Id;
                product.Name = model.Name;
                product.Description = model.Description;
                product.NewPrice = model.NewPrice;
                if (product.NewPrice != model.NewPrice)
                {
                    product.OldPrice = product.NewPrice;
                }
                product.UrlPictureBg = model.UrlPictureBg;
                product.CategoryId = model.CategoryId;
                model.ProductSize = _productSizeService.GetSizeByProductId(product.Id);
                product.Pictures = new List<Picture>();
                if (UrlFile.Count > 0)
                {
                    foreach (var item in _pictureService.GetPictureByProductId(product.Id))
                    {
                        product.Pictures.Remove(item);
                    }
                    foreach (var file in UrlFile)
                    {
                        Picture picture = new Picture()
                        {
                            Url = file,
                            CreatedDate = DateTime.Now
                        };
                        product.Pictures.Add(picture);
                    }
                }
                _productSizeService.DeleteMulti(model.ProductSize);
                _productService.Save();
                product.ProductSizes = sizes.Select(p => new ProductSize() { ProductId = product.Id, SizeId = p.Id }).ToList();
                _productSizeService.AddRange(product.ProductSizes);
                _productService.Save();
                UrlFile.Clear();
                return RedirectToAction("Index");

            }
        }
        [HttpPost]
        public IActionResult ChangeViewSize(int productId, int CategoryId)
        {
            ProductActionModel model = new ProductActionModel();
            model.Sizes = _sizeService.GetSizebyCategoryId(CategoryId);
            model.ProductSize = _productSizeService.GetSizeByProductId(productId);
            return Json(new { data = model, html = Helper.RenderRazorView(this, "_ListSize", model) });
        }

        [HttpPost]
        public async Task<IActionResult> UploadMultiImage()
        {
            UrlFile.Clear();

            var files = Request.Form.Files;
            var pictures = new List<Picture>();
            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];
                string fileName = UploadImage(picture.FileName);

                string ServerFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content", fileName);
                UrlFile.Add(fileName);
                picture.CopyTo(new FileStream(ServerFolder, FileMode.Create));
                var pic = new Picture();
                pic.Url = fileName;
                pictures.Add(pic);
            }
            return Json(new { data = pictures });
        }
        [HttpPost]
        public async Task<IActionResult> UploadImageBg(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
            string ServerFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Content", fileName);
            file.CopyTo(new FileStream(ServerFolder, FileMode.Create));
            return Json(new { data = fileName });
        }
        [HttpPost]
        public IActionResult MinusImage(string file)
        {
            var pictures = UrlFile;
            string FileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Content", file);
            FileInfo fi = new FileInfo(FileImage);
            if (fi != null)
            {
                //System.IO.File.Delete(FileImage);
                //fi.Delete();
                pictures.Remove(file);
            }
            return Json(new { data = pictures });

        }
        [NonAction]
        public string UploadImage(string file)
        {

            string fileName = Guid.NewGuid().ToString() + "-" + file;

            return fileName;
        }
    }
}
