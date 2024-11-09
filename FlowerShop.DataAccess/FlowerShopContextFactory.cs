using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess
{
    public class FlowerShopContextFactory : IDesignTimeDbContextFactory<FlowerShopContext>
    {
        public FlowerShopContext CreateDbContext(string[] args)
        {
            // Cấu hình chuỗi kết nối
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FlowerShopContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDbConnectString"));

            return new FlowerShopContext(optionsBuilder.Options);
        }
    }
}
