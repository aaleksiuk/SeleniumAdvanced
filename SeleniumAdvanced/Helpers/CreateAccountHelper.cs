using FluentAssertions;
using OpenQA.Selenium;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Helpers;
internal class CreateAccountHelper
{
    public class CreateAccountService(IWebDriver driver)
    {
        public PersonGenerator CreateNewAccount()
        {
            var person = new PersonGenerator();

            driver.Navigate().GoToUrl(UrlProvider.AppUrl);

            var headerPage = new HeaderPage(driver);
            headerPage.SignIn();

            var signInPage = new SignInPage(driver);
            signInPage.CreateAccount();

            var createAccountPage = new CreateAccountPage(driver);
            createAccountPage.SetSocialTitle(person.Title);
            createAccountPage.SetFirstName(person.FirstName);
            createAccountPage.SetLastName(person.LastName);
            createAccountPage.SetEmail(person.Mail);
            createAccountPage.SetPassword(person.Password);
            createAccountPage.SetBirthdate(person.BirthDate);
            createAccountPage.CheckReceiveOffersCheckbox();
            createAccountPage.CheckDataPrivacyCheckbox();
            createAccountPage.CheckNewsletterCheckbox();
            createAccountPage.CheckClickTermsAndConditionsCheckbox();
            createAccountPage.SubmitForm();

            headerPage.GetSignedInText.Should().Be($"{person.FullName}");
            headerPage.GetSignedInText.Should().Be($"{person.FullName}");

            return person;
        }
    }
}