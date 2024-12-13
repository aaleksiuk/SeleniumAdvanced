using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvanced.Pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Extensions;

public static class WebDriverExtensions
{
    public static T GetPage<T>(this IWebDriver driver, Action<T> action = null) where T : BasePage
    {
        var page = (T)Activator.CreateInstance(typeof(T), driver);
        Console.WriteLine($"At {typeof(T).Name}");
        action?.Invoke(page);
        return page;
    }
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
        return driver.GetWait().Until(d =>
        {
            var element = d.FindElement(by);
            if (element.Displayed)
            {
                return element;
            }
            else
            {
                throw new NoSuchElementException($"Element with locator '{by}' was not found.");
            }
        });
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

    public static List<IWebElement> WaitAndFindAll(this IWebDriver driver, By by, IWebElement parent)
    {
        try
        {
            return driver.GetWait().Until(d =>
            {
                var elements = parent.FindElements(by).ToList();
                return elements.Any() ? elements : throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
            });
        }
        catch (NoSuchElementException)
        {
            throw new NoSuchElementException($"Element with locator '{by}' was not found within the specified timeout.");
        }
    }
    public static bool WaitForValueChange(this IWebDriver driver, By by, string initialValue, int seconds = 3)
    {
        try
        {
            return driver.GetWait(seconds).Until(_ =>
            {
                var currentValue = driver.FindElement(by).Text;
                return currentValue != initialValue;
            });
        }
        catch (WebDriverTimeoutException)
        {
            throw new Exception($"Element with locator '{by}' was not changed within specified timeout.");
        }
    }
}