using Microsoft.EntityFrameworkCore;
using WLdemo.Data;
using WLdemo.Models;
using WLdemo.Models.ViewModels;
using WLdemo.Repositories.Interfaces;

namespace WLdemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product? GetById(int id) => _context.Products.Find(id);

        public void Add(Product product) => _context.Products.Add(product);

        public void Update(Product product) => _context.Products.Update(product);

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public void Save() => _context.SaveChanges();

        public IEnumerable<CategoryAvgPriceViewModel> GetAveragePricePerCategoryFromSp()
        {
            return _context.CategoryAvgPrices
                .FromSqlRaw("EXEC sp_GetAveragePricePerCategory")
                .ToList();
        }

        public HighestStockValueCategoryViewModel? GetHighestStockValueCategoryFromSp()
        {
            return _context.HighestStockValueCategories
                .FromSqlRaw("EXEC sp_GetHighestStockValueCategory")
                .AsEnumerable()
                .FirstOrDefault();
        }
    }
}
