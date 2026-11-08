using WLdemo.Models;
using WLdemo.Models.ViewModels;

namespace WLdemo.Services.Interfaces
{
    public delegate void OperationCallback(string message, bool isSuccess);

    public interface IProductService
    {
        IEnumerable<Product> GetAllProductsCached();
        Product? GetProductById(int id);

        void CreateProduct(Product product, OperationCallback callback);
        void UpdateProduct(Product product, OperationCallback callback);
        void DeleteProduct(int id, OperationCallback callback);

        IEnumerable<CategoryAvgPriceViewModel> GetAveragePricePerCategoryCached();
        HighestStockValueCategoryViewModel? GetHighestStockValueCategoryCachedLong();
    }
}
