﻿using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
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
            this.driver.Navigate().GoToUrl(UrlProvider.BaseUrl);
            //var generator = new PersonGenerator();

            GetPage<MainPage>(x =>
            {
                x.SignIn();
            });

            GetPage<SignInPage>(x =>
            {
                x.CreateAccount();
            });

            GetPage<CreateAccountPage>(x =>
            {
                var person = new PersonGenerator();
                
                x.SetSocialTitle(person.Title);
                x.SetFirstName(person.FirstName);
                x.SetLastName(person.LastName);
                x.SetEmail(person.Mail);
                x.SetPassword("12345");
                x.SetBirthdate("04/05/1990");
                x.SetReceiveOffers(true);
                x.SetDataPrivacy(true);
                x.SetNewsletter(true);
                x.SetTermsAndConditions(true);
                //x.SetSubmit();
            });
            GetPage<MainPage>(x => { 
            })
        }
    }
}