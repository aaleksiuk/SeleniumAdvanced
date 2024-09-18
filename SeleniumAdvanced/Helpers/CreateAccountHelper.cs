﻿using FluentAssertions;
using OpenQA.Selenium;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Helpers
{
    internal class CreateAccountHelper
    {
        public class CreateAccountService(IWebDriver driver)
        {
            private readonly IWebDriver _driver = driver;

            public PersonGenerator CreateNewAccount()
            {
                var person = new PersonGenerator();

                _driver.Navigate().GoToUrl(UrlProvider.AppUrl);

                var headerPage = new HeaderPage(_driver);
                headerPage.SignIn();

                var signInPage = new SignInPage(_driver);
                signInPage.CreateAccount();

                var createAccountPage = new CreateAccountPage(_driver);
                createAccountPage.SetSocialTitle(person.Title);
                createAccountPage.SetFirstName(person.FirstName);
                createAccountPage.SetLastName(person.LastName);
                createAccountPage.SetEmail(person.Mail);
                createAccountPage.SetPassword(person.Password);
                createAccountPage.SetBirthdate(person.BirthDate);
                createAccountPage.ClickReceiveOffers();
                createAccountPage.ClickDataPrivacy();
                createAccountPage.ClickNewsletter();
                createAccountPage.ClickTermsAndConditions();
                createAccountPage.SetSubmit();

                headerPage.GetSignedInText().Should().Be($"{person.FullName}");
                headerPage.GetSignedInText().Should().Be($"{person.FullName}");

                return person;
            }
        }
    }
}