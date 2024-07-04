using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAdvanced.Extensions;
using System;
using System.Threading;

namespace SeleniumAdvanced.Pages;

public abstract class BasePage
{
    public IWebDriver Driver { get; }
    public Actions ActionsDriver { get; }

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        ActionsDriver = new Actions(Driver);
    }

    public void SendKeys(IWebElement element, string text, bool clear = true)
    {
        if (clear)
        {
            element.Clear();
            Console.WriteLine($"Typing: {text}");
            element.SendKeys(text);
        }
        else
        {
            Console.WriteLine($"Typing: {text}");
            element.SendKeys(text);
        }
    }

    public void Click(IWebElement element)
    {
        Console.WriteLine($"Clicking: {element.Text}");
        try
        {
            element.Click();
        }
        catch (ElementClickInterceptedException e)
        {
            ActionsDriver.ScrollToElement(element);
            ActionsDriver.ScrollByAmount(0, 10);
            Thread.Sleep(500);
            Driver.GetWait().Until(_ => element.Displayed && element.Enabled);
            element.Click();
            Console.WriteLine(e.ToString());
            throw;
        }
    }
}