using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class ProductsGridPage(IWebDriver driver) : BasePage(driver)
{
    private IList<IWebElement> DisplayedProducts => Driver.WaitAndFindAll(By.CssSelector("div.products.row > div"));
    private IList<IWebElement> PopularProductsName => Driver.WaitAndFindAll(By.CssSelector("h3.product-title > a"));
    private IList<IWebElement> ProductsPrices => Driver.WaitAndFindAll(By.CssSelector("div.product-price-and-shipping"));
    private IList<IWebElement> ProductsPricesRegularAndDiscount => Driver.WaitAndFindAll(By.CssSelector(".product-price-and-shipping .regular-price, .product-price-and-shipping .price"));

    public int DisplayedProductsNumber => DisplayedProducts.Count;
    public IEnumerable<string> GetProductsNames()
    {
        return PopularProductsName.Select(item =>
        {
            return item.Text;
        });
    }

    public IEnumerable<decimal> GetProductPrices()
    {
        return ProductsPrices.Select(item =>
        {
            var priceText = item.Text.Replace("$", "").Trim();
            return decimal.Parse(priceText);
        });
    }
    public IEnumerable<decimal> GetProductPricesRegularAndDiscount()
    {
        return ProductsPricesRegularAndDiscount.Select(item =>
        {
            var priceText = item.Text.Replace("$", "").Trim();
            return decimal.Parse(priceText);
        });
    }
}