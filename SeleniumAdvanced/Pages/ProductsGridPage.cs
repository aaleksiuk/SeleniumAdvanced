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

    public void ClickProductByName(string productName)
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
    public string SelectRandomProductExcludingSelectedBefore(IEnumerable<string> excludedProducts)
    {
        var availableProducts = ProductsMiniatures
       .Select(item => item.NameText)
       .Except(excludedProducts)
       .ToList();

        if (availableProducts.Count == 0)
        {
            throw new InvalidOperationException("No more unique products available to select.");
        }

        var rand = new Random();
        var randomIndex = rand.Next(availableProducts.Count);
        return availableProducts[randomIndex];
    }

    public string SelectRandomProduct()
    {
        var availableProducts = ProductsMiniatures
       .Select(item => item.NameText)
       .ToList();

        if (availableProducts.Count == 0)
        {
            throw new InvalidOperationException("No products available to select.");
        }

        var rand = new Random();
        var randomIndex = rand.Next(availableProducts.Count);
        return availableProducts[randomIndex];
    }
}