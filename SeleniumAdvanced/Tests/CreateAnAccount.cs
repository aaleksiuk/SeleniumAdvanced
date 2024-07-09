using FluentAssertions;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Providers;
using SeleniumAdvanced.Pages;
using NUnit.Framework;


namespace SeleniumAdvanced.Tests;

[TestFixture]
public class CreateAnAccount : TestBase
{
    [Test]
    public void CreateAccount()
    {
        // Arrange
        this.driver.Navigate().GoToUrl(UrlProvider.AppUrl);
        var person = new PersonGenerator();

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.SignIn();
        });

        GetPage<SignInPage>(x =>
        {
            x.CreateAccount();
        });

        GetPage<CreateAccountPage>(x =>
        {
            x.SetSocialTitle(person.Title);
            x.SetFirstName(person.FirstName);
            x.SetLastName(person.LastName);
            x.SetEmail(person.Mail);
            x.SetPassword(person.Password);
            x.SetBirthdate(person.BirthDate);
            x.ClickReceiveOffers();
            x.ClickDataPrivacy();
            x.ClickNewsletter();
            x.ClickTermsAndConditions();
            x.SetSubmit();
        });

        //Assert
        GetPage<HeaderPage>(x =>
        {
            x.GetSignedInText().Should().Be($"{person.FullName}");
        });
    }
}