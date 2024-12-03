using OpenQA.Selenium;

namespace SeleniumAdvanced.Pages;

public class ProductMiniaturePage(IWebElement parent, IWebDriver driver) : BasePage(driver)
{
    public string Name => parent.FindElement(By.CssSelector(".h3.product-title > a")).Text;
    public string Price => parent.FindElement(By.CssSelector("div.product-price-and-shipping")).Text;
    public string DiscountPrice => parent.FindElement(By.CssSelector(".product-price-and-shipping .regular-price, .product-price-and-shipping .price")).Text;
}