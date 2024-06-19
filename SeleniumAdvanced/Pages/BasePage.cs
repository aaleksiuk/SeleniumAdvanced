using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvanced.Extensions;
using System;

namespace SeleniumAdvanced.Pages
{
    public abstract class BasePage
    {
        public IWebDriver Driver { get; }
        public Actions ActionsDriver { get; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            ActionsDriver = new Actions(Driver);
        }

        public void SendKeys(IWebElement element, string text)
        {
            Console.WriteLine($"Typing: {text}");
            element.SendKeys(text);
        }

        public void ClearAndSendKeys(IWebElement element, string text)
        {
            element.Clear();
            SendKeys(element,text);
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
                Driver.DefaultWait().Until(_ => element.Displayed && element.Enabled);
                element.Click();
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}