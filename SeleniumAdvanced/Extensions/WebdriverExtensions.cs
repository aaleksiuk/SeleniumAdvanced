using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Extensions
{
    public static class WebDriverExtensions
    {
        public static WebDriverWait DefaultWait(this IWebDriver driver) => new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        public static Boolean IsDisplayed(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public static Boolean IsDisplayedWithTimeout(this IWebDriver driver, By by, int seconds = 3)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(_ => driver.IsDisplayed(by));
            }
            catch (WebDriverTimeoutException)
            {
            }
            return driver.IsDisplayed(by);
        }
        public static IWebElement WaitAndFind(this IWebDriver driver, By by)
        {
            try
            {
                return driver.DefaultWait().Until(d =>
                d.FindElement(by).Displayed ? d.FindElement(by) : throw new NoSuchElementException($"Element with locator '{by}' was not found"));
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
            }
        }

        public static List<IWebElement> WaitAndFindAll(this IWebDriver driver, By by)
        {
            try
            {
                return driver.DefaultWait().Until(d =>
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
