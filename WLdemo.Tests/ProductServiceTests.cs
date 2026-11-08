using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WLdemo.Models;
using WLdemo.Repositories.Interfaces;
using WLdemo.Services;
using WLdemo.Services.Interfaces;

namespace WLdemo.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo = null!;
        private IMemoryCacheService _cacheService = null!;
        private ProductService _productService = null!;
        private IMemoryCache _memoryCache = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new MemoryCacheService(_memoryCache);
            _productService = new ProductService(_mockRepo.Object, _cacheService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _memoryCache.Dispose();
        }

        [TestMethod]
        public void GetAllProductsCached_ShouldReturnProducts_AndUseCache()
        {
            var testProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200.00m, Stock = 10 },
                new Product { Id = 2, Name = "Desk", Category = "Furniture", Price = 250.00m, Stock = 5 }
            };

            _mockRepo.Setup(repo => repo.GetAll()).Returns(testProducts);

            var firstCallResult = _productService.GetAllProductsCached();

            _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Product>());

            var cachedResult = _productService.GetAllProductsCached();

            Assert.IsNotNull(firstCallResult);
            Assert.AreEqual(2, firstCallResult.Count());
            Assert.IsNotNull(cachedResult);
            Assert.AreEqual(2, cachedResult.Count());
            _mockRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [TestMethod]
        public void CreateProduct_ShouldInvokeRepositoryAddAndSave_AndTriggerCallback()
        {
            var newProduct = new Product { Name = "Mouse", Category = "Electronics", Price = 45.00m, Stock = 50 };
            bool callbackInvoked = false;
            string callbackMsg = string.Empty;
            bool callbackSuccess = false;

            OperationCallback callback = (msg, success) =>
            {
                callbackInvoked = true;
                callbackMsg = msg;
                callbackSuccess = success;
            };

            _productService.CreateProduct(newProduct, callback);

            _mockRepo.Verify(repo => repo.Add(newProduct), Times.Once);
            _mockRepo.Verify(repo => repo.Save(), Times.Once);
            Assert.IsTrue(callbackInvoked);
            Assert.IsTrue(callbackSuccess);
            StringAssert.Contains(callbackMsg, "created successfully");
        }

        [TestMethod]
        public void DeleteProduct_ShouldRemoveProduct_AndInvalidateCaches()
        {
            int productId = 1;
            var product = new Product
            {
                Id = productId,
                Name = "Keyboard",
                Category = "Electronics",
                Price = 75.00m,
                Stock = 20
            };

            _mockRepo.Setup(repo => repo.GetById(productId)).Returns(product);

            bool callbackInvoked = false;
            OperationCallback callback = (msg, success) => callbackInvoked = true;

            _productService.DeleteProduct(productId, callback);

            _mockRepo.Verify(repo => repo.Delete(productId), Times.Once);
            _mockRepo.Verify(repo => repo.Save(), Times.Once);
            Assert.IsTrue(callbackInvoked);
        }
    }
}
