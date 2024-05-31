using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages
{
    public class SignInPage(IWebDriver driver)
    {
        private IWebElement CreateAccountLink => driver.WaitAndFind(By.CssSelector(".no-account"));
        public SignInPage CreateAccount()
        {
            CreateAccountLink.Click();
            return new SignInPage(driver);
        }
    }
}
