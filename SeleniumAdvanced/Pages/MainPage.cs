using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages
{
    public class MainPage(IWebDriver driver)
    {
        private IWebElement SignInBtn => driver.WaitAndFind(By.CssSelector(".user-info"));

        public MainPage SignIn()
        {
            SignInBtn.Click();
            return new MainPage(driver);
        }
    }
}
