using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System;
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

    public IEnumerable<string> GetProductsNames => ProductsMiniatures.Select(item => item.NameText);
    public IEnumerable<decimal> GetProductPrices => ProductsMiniatures.Select(item => item.Price);
    public IEnumerable<decimal> GetProductPricesRegularAndDiscount => ProductsMiniatures.Select(item => item.DiscountPrice);

    public void ClickProductByName (string productName)
    {
        var product = ProductsMiniatures.FirstOrDefault(item =>
            item.NameText.Equals(productName, StringComparison.OrdinalIgnoreCase));
        if (product != null)
        {
            product.ClickProduct();
        }
        else
        {
            throw new NoSuchElementException($"Product with name '{productName}' not found.");
        }
    }
}