using WLdemo.Models;
using WLdemo.Models.ViewModels;

namespace WLdemo.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        void Save();

        IEnumerable<CategoryAvgPriceViewModel> GetAveragePricePerCategoryFromSp();
        HighestStockValueCategoryViewModel? GetHighestStockValueCategoryFromSp();
    }
}
