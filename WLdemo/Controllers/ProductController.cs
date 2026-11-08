using Microsoft.AspNetCore.Mvc;
using WLdemo.Models;
using WLdemo.Services.Interfaces;

namespace WLdemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProductsCached();
            return View(products);
        }

        [HttpGet]
        public JsonResult GetProductsJson()
        {
            var products = _productService.GetAllProductsCached();
            return Json(products);
        }

        [HttpPost]
        public JsonResult Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid product data submitted." });
            }

            string responseMessage = "";
            bool isSuccess = false;

            _productService.CreateProduct(product, (msg, success) =>
            {
                responseMessage = msg;
                isSuccess = success;
            });

            return Json(new { success = isSuccess, message = responseMessage });
        }

        [HttpPost]
        public JsonResult Update([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid product data submitted." });
            }

            string responseMessage = "";
            bool isSuccess = false;

            _productService.UpdateProduct(product, (msg, success) =>
            {
                responseMessage = msg;
                isSuccess = success;
            });

            return Json(new { success = isSuccess, message = responseMessage });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string responseMessage = "";
            bool isSuccess = false;

            _productService.DeleteProduct(id, (msg, success) =>
            {
                responseMessage = msg;
                isSuccess = success;
            });

            return Json(new { success = isSuccess, message = responseMessage });
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            return Json(new { success = true, data = product });
        }
    }
}
