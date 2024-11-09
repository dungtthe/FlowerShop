using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess
{
    public class FlowerShopContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<CartDetail>? CartDetails { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Conversation>? Conversations { get; set; }
        public DbSet<FeedBack>? FeedBacks { get; set; }
        public DbSet<FeedBackResponse>? FeedBackResponses { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<Packaging>? Packagings { get; set; }
        public DbSet<ParameterConfiguration>? ParameterConfigurations { get; set; }
        public DbSet<PaymentMethod>? PaymentMethods { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductCategory>? ProductCategories { get; set; }
        public DbSet<ProductItem>? ProductItems { get; set; }
        public DbSet<ProductPrice>? ProductPrices { get; set; }
        public DbSet<ProductProductItem>? ProductProductItems { get; set; }
        public DbSet<SaleInvoice>? SaleInvoices { get; set; }
        public DbSet<SaleInvoiceDetail>? SaleInvoiceDetails { get; set; }
        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<SupplierInvoice>? SupplierInvoices { get; set; }
        public DbSet<SupplierInvoiceDetail>? SupplierInvoiceDetails { get; set; }



        //bổ trợ phần DI để có thể cấu hình context bên file startup
        public FlowerShopContext(DbContextOptions<FlowerShopContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region add default
            modelBuilder.Entity<Product>()
                .Property(e => e.Images)
                .HasDefaultValue("[\"no_img.png\"]");

            modelBuilder.Entity<AppUser>()
                .Property(e => e.Images)
                .HasDefaultValue("[\"no_img.png\"]");

            modelBuilder.Entity<ProductItem>()
                .Property(e => e.Images)
                .HasDefaultValue("[\"no_img.png\"]");

            modelBuilder.Entity<Supplier>()
               .Property(e => e.Images)
               .HasDefaultValue("[\"no_img.png\"]");

            #endregion

            #region add khóa chính
            modelBuilder.Entity<CartDetail>()
            .HasKey(cd => new { cd.CartId, cd.ProductId });

            modelBuilder.Entity<SupplierInvoiceDetail>()
            .HasKey(s => new { s.SupplierInvoiceId, s.ProductItemId }); 

            modelBuilder.Entity<ProductProductItem>()
            .HasKey(p => new { p.ProductId, p.ProductItemId });

            modelBuilder.Entity<ProductCategory>()
           .HasKey(p => new { p.CategoryId, p.ProductId });
            #endregion


            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName?.StartsWith("AspNet") == true)
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
