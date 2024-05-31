using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages
{
    public class MainPage(IWebDriver driver)
    {
        private IWebElement SignInBtn => driver.WaitAndFind(By.CssSelector(".user-info"));
        private IWebElement ViewCustomerAccountBtn => driver.WaitAndFind(By.CssSelector("span.hidden-sm-down"));

        public void SignIn()
        {
            SignInBtn.Click();
        }
        public string IsSignedIn() => ViewCustomerAccountBtn.Text;
    }
}