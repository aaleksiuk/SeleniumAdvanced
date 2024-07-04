using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class SignInPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement CreateAccountLink => Driver.WaitAndFind(By.CssSelector(".no-account"));

    public void CreateAccount() => Click(CreateAccountLink);
}