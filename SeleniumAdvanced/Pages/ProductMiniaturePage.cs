using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class ProductMiniaturePage(IWebElement parent, IWebDriver driver) : BasePage(driver)
{
    public IWebElement Name => parent.FindElement(By.CssSelector(".h3.product-title > a"));
    public string NameText => parent.FindElement(By.CssSelector(".h3.product-title > a")).Text;
    public decimal Price => parent.FindElement(By.CssSelector("div.product-price-and-shipping")).GetPrice();
    public decimal DiscountPrice => parent.FindElement(By.CssSelector(".product-price-and-shipping .regular-price, .product-price-and-shipping .price")).GetPrice();

    public void ClickProduct() => Name.Click();
}