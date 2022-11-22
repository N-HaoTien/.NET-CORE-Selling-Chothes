//using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using Microsoft.EntityFrameworkCore;
using ClothingShopping.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using ClothingShopping.Repository;
using ClothingShopping.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using ClothingShopping;
using ClothingShopping.Extension;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using ClothingShopping.Repository.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); ;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
      .AddEntityFrameworkStores<ApplicationDbContext>()

      .AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Users/AccessDenied");
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(cfg =>
{
    cfg.Cookie.Name = "SessionCart";             // Name
    cfg.IdleTimeout = new TimeSpan(0, 30, 0); // Time Exists
});

#region AddScoped
#region
builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IViewRenderService, ViewRendererService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
#endregion

#region
builder.Services.AddScoped<IDbFactory, DbFactory>();
#endregion
#region Product
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
#endregion
#region Size
builder.Services.AddScoped<ISize, SizeRepository>();
builder.Services.AddScoped<ISizeService, SizeService>();
#endregion
#region ProductSize
builder.Services.AddScoped<IProductSize, ProductSizeRepository>();
builder.Services.AddScoped<IProductSizeService, ProductSizeService>();
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
#region UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion
/*builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);*/
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
// set route start when build web app
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}/{id?}");*/
app.Run();
/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "default",
       areaName: "Admin",
       pattern: "{controller=Category}/{action=Index}");
    endpoints.MapAreaControllerRoute(
       name: "Manageindex2",
       areaName: "User",
       pattern: "User/{controller=users}/{action=Index2}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});*/