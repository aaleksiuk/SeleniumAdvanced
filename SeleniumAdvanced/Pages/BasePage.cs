using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumAdvanced.Pages
{
    public abstract class BasePage(IWebDriver driver)
    {
        public IWebDriver Driver { get; set; } = driver;
        public WebDriverWait DefaultWait { get; set; } = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        public Actions ActionsDriver { get; set; } = new Actions(driver);

        public void SendKeys(IWebElement element, string text, bool shouldClear)
        {
            if (shouldClear)
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
                ActionsDriver.ScrollToElement(element);
                ActionsDriver.ScrollByAmount(0, 10);
                DefaultWait.Until(_ => element.Displayed && element.Enabled);
                element.Click();
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}