using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class StartPage(IWebDriver driver) : BasePage(driver)
{
    private IList<IWebElement> PopularProductsName => Driver.WaitAndFindAll(By.CssSelector("h3.product-title > a"));

    public IEnumerable<string> GetProductsNames()
    {
        return PopularProductsName.Select(item =>
        {
            return item.Text;
        });
    }
}