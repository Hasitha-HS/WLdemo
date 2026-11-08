using Microsoft.AspNetCore.Mvc;
using WLdemo.Services.Interfaces;

namespace WLdemo.Controllers
{
    public class ReportController : Controller
    {
        private readonly IProductService _productService;

        public ReportController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var avgPrices = _productService.GetAveragePricePerCategoryCached();
            var highestStockCategory = _productService.GetHighestStockValueCategoryCachedLong();

            ViewBag.HighestStockCategory = highestStockCategory;
            return View(avgPrices);
        }
    }
}
