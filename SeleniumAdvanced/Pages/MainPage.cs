using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages
{
    public class MainPage(IWebDriver driver) : BasePage(driver)
    {
        private IWebElement SignInBtn => Driver.WaitAndFind(By.CssSelector(".user-info"), DefaultWait);
        private IWebElement ViewCustomerAccountBtn => Driver.WaitAndFind(By.CssSelector("span.hidden-sm-down"), DefaultWait);

        public void SignIn() => SignInBtn.Click();
        public string IsSignedIn() => ViewCustomerAccountBtn.Text;
    }
}