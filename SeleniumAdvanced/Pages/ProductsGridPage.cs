using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class ProductsGridPage(IWebDriver driver) : BasePage(driver)
{
    public IList<ProductMiniaturePage> ProductsMiniatures =>
        Driver.WaitAndFindAll(By.CssSelector(".product-miniature"))
            .Select(item => new ProductMiniaturePage(item, Driver))
            .ToList();

    public int DisplayedProductsNumber => ProductsMiniatures.Count;

    //public string GetProductTitle => ProductTitle.Text;

    public IEnumerable<string> GetProductsNames() => ProductsMiniatures.Select(item => item.Name);

    public IEnumerable<decimal> GetProductPrices() => ProductsMiniatures.Select(item =>
        {
            var priceText = item.Price.Replace("$", "").Trim();
            return decimal.Parse(priceText);
        });

    public IEnumerable<decimal> GetProductPricesRegularAndDiscount() => ProductsMiniatures.Select(item =>
        {
            var cleanedPriceText = item.DiscountPrice.Replace("$", "").Trim();
            return decimal.Parse(cleanedPriceText);
        });
}