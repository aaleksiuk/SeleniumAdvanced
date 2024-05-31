using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitAndFind(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                return wait.Until(d =>
                d.FindElement(by).Displayed ? d.FindElement(by) : throw new NoSuchElementException($"Element with locator '{by}' was not found"));
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
            }
        }

        public static List<IWebElement> WaitAndFindAll(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                return wait.Until(d =>
                {
                    var elements = d.FindElements(by).ToList();
                    return elements.Any() ? elements : throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
                });
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
            }
        }
    }

}
