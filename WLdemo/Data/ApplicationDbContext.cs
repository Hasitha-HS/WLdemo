using Microsoft.EntityFrameworkCore;
using WLdemo.Models;
using WLdemo.Models.ViewModels;

namespace WLdemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryAvgPriceViewModel> CategoryAvgPrices { get; set; }
        public DbSet<HighestStockValueCategoryViewModel> HighestStockValueCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryAvgPriceViewModel>().HasNoKey();
            modelBuilder.Entity<HighestStockValueCategoryViewModel>().HasNoKey();
        }
    }
}
