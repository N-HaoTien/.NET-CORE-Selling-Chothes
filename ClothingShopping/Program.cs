//using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using Microsoft.EntityFrameworkCore;
using ClothingShopping.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using ClothingShopping.Repository;
using ClothingShopping.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); ;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

    options.SignIn.RequireConfirmedAccount = false;
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;


})
      .AddEntityFrameworkStores<ApplicationDbContext>()

      .AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#region AddScoped
#region Product
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion
#region ProductPicture
builder.Services.AddScoped<IProductPicture, ProductPictureRepository>();
builder.Services.AddScoped<IProductPictureService, ProductPictureService>();
#endregion
#region User
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion
#region Category
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion 
#region Comment
builder.Services.AddScoped<IComment, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
#endregion
#region OrderItem
builder.Services.AddScoped<IOrderItem, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
#endregion
#region Order
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
#endregion
#region Picture
builder.Services.AddScoped<IPicture, PictureRepository>();
builder.Services.AddScoped<IPictureService, PictureService>();
#endregion
#region ProductPicture
builder.Services.AddScoped<IProductPicture, ProductPictureRepository>();
builder.Services.AddScoped<IProductPictureService, ProductPictureService>();
#endregion
#region UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion
#endregion
var app = builder.Build();
//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkJhUH9adXxXRGFZV0A=");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "ManageCategory",
       areaName: "Admin",
       pattern: "Admin/{controller=Category}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}/{id?}");*/
app.Run();
