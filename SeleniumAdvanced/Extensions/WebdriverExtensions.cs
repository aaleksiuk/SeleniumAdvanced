using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Extensions;

public static class WebDriverExtensions
{
    public static WebDriverWait GetWait(this IWebDriver driver, int seconds = 10) => new(driver, TimeSpan.FromSeconds(10));

    public static bool IsDisplayed(this IWebDriver driver, By by)
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
    public static bool IsDisplayedWithTimeout(this IWebDriver driver, By by, int seconds = 3)
    {
        try
        {
            return driver.GetWait(seconds).Until(_ => driver.IsDisplayed(by));
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }

    }
    public static IWebElement WaitAndFind(this IWebDriver driver, By by)
    {
        try
        {
            return driver.GetWait().Until(d =>
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
            return driver.GetWait().Until(d =>
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