using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class SignInPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement CreateAccountLink => Driver.WaitAndFind(By.CssSelector(".no-account"));
    private IWebElement EmailInput => Driver.WaitAndFind(By.CssSelector("input[name='email']"));
    private IWebElement PasswordInput => Driver.WaitAndFind(By.CssSelector("input[name='password']"));
    private IWebElement SignIn => Driver.WaitAndFind(By.CssSelector("#submit-login"));

    public void CreateAccount() => Click(CreateAccountLink);
    public void EnterEmail(string Mail) => SendKeys(EmailInput, Mail);
    public void EnterPassword(string Password) => SendKeys(PasswordInput, Password);
    public void SubmitLogin() => Click(SignIn);
}