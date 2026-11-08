namespace WLdemo.Models.ViewModels
{
    public class CategoryAvgPriceViewModel
    {
        public string Category { get; set; } = string.Empty;
        public decimal AveragePrice { get; set; }
    }

    public class HighestStockValueCategoryViewModel
    {
        public string Category { get; set; } = string.Empty;
        public decimal TotalStockValue { get; set; }
    }
}
