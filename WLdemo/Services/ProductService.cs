using WLdemo.Models;
using WLdemo.Models.ViewModels;
using WLdemo.Repositories.Interfaces;
using WLdemo.Services.Interfaces;

namespace WLdemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCacheService _cacheService;

        private const string ProductsCacheKey = "products_all_cache";
        private const string AvgPriceCacheKey = "reports_avg_price_cache";
        private const string HighestStockCacheKey = "reports_highest_stock_cache";

        public ProductService(IProductRepository productRepository, IMemoryCacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public IEnumerable<Product> GetAllProductsCached()
        {
            return _cacheService.GetOrCreate(ProductsCacheKey, () =>
            {
                return _productRepository.GetAll();
            }, TimeSpan.FromMinutes(5));
        }

        public Product? GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void CreateProduct(Product product, OperationCallback callback)
        {
            try
            {
                _productRepository.Add(product);
                _productRepository.Save();

                InvalidateCaches();

                callback?.Invoke($"Product '{product.Name}' created successfully.", true);
            }
            catch (Exception ex)
            {
                callback?.Invoke($"Error creating product: {ex.Message}", false);
            }
        }

        public void UpdateProduct(Product product, OperationCallback callback)
        {
            try
            {
                _productRepository.Update(product);
                _productRepository.Save();

                InvalidateCaches();

                callback?.Invoke($"Product '{product.Name}' updated successfully.", true);
            }
            catch (Exception ex)
            {
                callback?.Invoke($"Error updating product: {ex.Message}", false);
            }
        }

        public void DeleteProduct(int id, OperationCallback callback)
        {
            try
            {
                var product = _productRepository.GetById(id);
                string name = product?.Name ?? id.ToString();

                _productRepository.Delete(id);
                _productRepository.Save();

                InvalidateCaches();

                callback?.Invoke($"Product '{name}' deleted successfully.", true);
            }
            catch (Exception ex)
            {
                callback?.Invoke($"Error deleting product: {ex.Message}", false);
            }
        }

        public IEnumerable<CategoryAvgPriceViewModel> GetAveragePricePerCategoryCached()
        {
            return _cacheService.GetOrCreate(AvgPriceCacheKey, () =>
            {
                return _productRepository.GetAveragePricePerCategoryFromSp();
            }, TimeSpan.FromMinutes(5));
        }

        public HighestStockValueCategoryViewModel? GetHighestStockValueCategoryCachedLong()
        {
            return _cacheService.GetOrCreate(HighestStockCacheKey, () =>
            {
                return _productRepository.GetHighestStockValueCategoryFromSp();
            });
        }

        private void InvalidateCaches()
        {
            _cacheService.Remove(ProductsCacheKey);
            _cacheService.Remove(AvgPriceCacheKey);
            _cacheService.Remove(HighestStockCacheKey);
        }
    }
}
