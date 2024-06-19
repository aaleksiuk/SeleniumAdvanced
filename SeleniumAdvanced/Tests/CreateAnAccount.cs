using FluentAssertions;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using Xunit;
using Xunit.Abstractions;

namespace SeleniumAdvanced
{
    public class CreateAnAccount : TestBase
    {
        public CreateAnAccount(ITestOutputHelper output) : base(output)
        {

        }
        [Fact]
        public void CreateAccount()
        {
            this.driver.Navigate().GoToUrl(UrlProvider.AppUrl);
            var person = new PersonGenerator();

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
                x.SetReceiveOffers(true);
                x.SetDataPrivacy(true);
                x.SetNewsletter(true);
                x.SetTermsAndConditions(true);
                x.SetSubmit();
            });

            GetPage<HeaderPage>(x =>
            {
                x.IsSignedIn().Should().Be($"{person.FullName}");
            });
        }
    }
}

