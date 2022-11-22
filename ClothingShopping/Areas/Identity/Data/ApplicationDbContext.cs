using ClothingShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingShopping.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public ApplicationDbContext() { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        #region Mapping All Entity
        #region Create Key and Index for Entity
        builder.Entity<Chatting>(entity => { entity.ToTable("Chatting"); entity.HasKey(p => new { p.FromUserId,p.ToUserId }); });
        builder.Entity<Product>(entity => { entity.ToTable("Product"); entity.Property(e => e.Id).HasColumnName("ProductId"); entity.HasKey(p => p.Id); entity.HasIndex(p => p.Name).IsUnique(); });
        builder.Entity<Category>(entity => { entity.ToTable("Category"); entity.Property(e => e.Id).HasColumnName("CategoryId"); entity.HasKey(p => p.Id); entity.HasIndex(p => p.Name).IsUnique(); });
        builder.Entity<Picture>(entity => { entity.ToTable("Picture"); entity.Property(e => e.Id).HasColumnName("PictureId"); entity.HasKey(p => p.Id); });
        builder.Entity<Order>(entity => { entity.ToTable("Order"); entity.Property(e => e.Id).HasColumnName("OrderId"); entity.HasKey(p => p.Id);});
        builder.Entity<OrderItem>().HasKey(sc => new { sc.OrderId, sc.ProductId });
        builder.Entity<Comment>(entity => { entity.ToTable("Comment"); entity.Property(e => e.Id).HasColumnName("CommentId"); entity.HasKey(p => p.Id); });
        builder.Entity<Size>(entity => { entity.ToTable("Size"); entity.Property(e => e.Id).HasColumnName("SizeId"); entity.HasKey(p => p.Id); });
        builder.Entity<ProductSize>().HasKey(sc => new { sc.ProductId, sc.SizeId });
        #endregion
        #region Create ForeignKey for Table and OnDelete
        builder.Entity<Size>()
                    .HasOne<Category>(s => s.Category)
                    .WithMany(c => c.Sizes)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ProductSize>()
            .HasOne<Product>(sc => sc.Product)
            .WithMany(s => s.ProductSizes)
            .HasForeignKey(sc => sc.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ProductSize>()
            .HasOne<Size>(sc => sc.Size)
            .WithMany(s => s.ProductSizes)
            .HasForeignKey(sc => sc.SizeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<OrderItem>()
            .HasOne<Order>(sc => sc.Order)
            .WithMany(s => s.OrderItems)
            .HasForeignKey(sc => sc.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasOne<Product>(sc => sc.Product)
            .WithMany(s => s.OrderItems)
            .HasForeignKey(sc => sc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        /*       builder.Entity<ProductPicture>().HasKey(sc => new { sc.ProductId, sc.PictureId });

              builder.Entity<ProductPicture>()
                   .HasOne<Product>(sc => sc.Product)
                   .WithMany(s => s.ProductPictures)
                   .HasForeignKey(sc => sc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<ProductPicture>()
                   .HasOne<Picture>(sc => sc.Picture)
                   .WithMany(s => s.ProductPictures)
                   .HasForeignKey(sc => sc.PictureId)
                   .OnDelete(DeleteBehavior.Cascade);*/

        builder.Entity<Order>()
                    .HasOne<ApplicationUser>(s => s.User)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Order>()
                    .HasOne<ApplicationUser>(s => s.CheckerUser)
                    .WithMany(c => c.CheckerOrders)
                    .HasForeignKey(c => c.CheckerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);//maybe null

        builder.Entity<Product>()
                    .HasOne<Category>(s => s.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Picture>()
                    .HasOne<Product>(s => s.Products)
                    .WithMany(c => c.Pictures)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Comment>()
                    .HasOne<ApplicationUser>(s => s.User)
                    .WithMany(c => c.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Comment>()
                    .HasOne<Product>(s => s.Product)
                    .WithMany(c => c.Comments)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Chatting>()
                    .HasOne<ApplicationUser>(s => s.FromUser)
                    .WithMany(c => c.MessagesFrom)
                    .HasForeignKey(c => c.FromUserId)
                    .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Chatting>()
                    .HasOne<ApplicationUser>(a => a.ToUser)
                    .WithMany(c => c.MessagesTo)
                    .HasForeignKey(c => c.ToUserId)
                    .OnDelete(DeleteBehavior.NoAction);
        #endregion
        base.OnModelCreating(builder);
        #endregion
    }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Chatting> Messages { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }

    /*    public DbSet<ProductPicture> ProductPictures { get; set; }
    */
}
