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
    public HeaderPage Header => Driver.GetPage<HeaderPage>();

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        ActionsDriver = new Actions(Driver);
    }
    public static void SendKeys(IWebElement element, string text, bool clear = true)
    {
        if (clear)
        {
            element.Clear();

        }
        Console.WriteLine($"Typing: {text}");
        element.SendKeys(text);
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
            ScrollTo(element);
            element.Click();
            Console.WriteLine(e.ToString());
            throw;
        }
    }
    public void Hover(IWebElement element)
    {
        Console.WriteLine($"Hover on: {element.Text}");
        try
        {
            MoveToElementAction(element);
        }
        catch (ElementClickInterceptedException e)
        {
            ScrollTo(element);
            MoveToElementAction(element);
            Console.WriteLine(e.ToString());
            throw;
        }
    }
    private void ScrollTo(IWebElement element)
    {
        ActionsDriver.ScrollToElement(element);
        ActionsDriver.ScrollByAmount(0, 10);
        Thread.Sleep(500);
        Driver.GetWait().Until(_ => element.Displayed && element.Enabled);
    }
    private void MoveToElementAction(IWebElement element)
    {
        ActionsDriver.Reset();
        ActionsDriver.MoveToElement(element);
        ActionsDriver.Perform();
    }
}