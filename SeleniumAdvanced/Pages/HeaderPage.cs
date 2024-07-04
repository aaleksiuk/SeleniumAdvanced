using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class HeaderPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement SignInBtn => Driver.WaitAndFind(By.CssSelector(".user-info"));
    private IWebElement ViewCustomerAccountBtn => Driver.WaitAndFind(By.CssSelector("span.hidden-sm-down"));

    public void SignIn() => Click(SignInBtn);
    public string GetSignedInText() => ViewCustomerAccountBtn.Text;
}