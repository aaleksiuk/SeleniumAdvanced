using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class SearchResultPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement productTitle => Driver.WaitAndFind(By.CssSelector(".h3.product-title > a"));

    public string GetProductTitle() => productTitle.Text;
}