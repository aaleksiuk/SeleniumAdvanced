using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages
{
    public class SignInPage(IWebDriver driver)
    {
        private IWebElement CreateAccountLink => driver.WaitAndFind(By.CssSelector(".no-account"));
        public void CreateAccount()
        {
            CreateAccountLink.Click();
        }
    }
}