﻿using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using static SeleniumAdvanced.Helpers.CreateAccountHelper;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class LogAsAUser : TestBase
{
    private PersonGenerator _userToLogIn;

    [SetUp]
    public new void Setup()
    {
        // Use the service to create an account
        var personService = new CreateAccountService(driver);
        _userToLogIn = personService.CreateNewAccount();
    }

    [Test]
    [Repeat(10)]
    public void LogInUser()
    {
        // Act
        GetPage<HeaderPage>(x =>
        {
            x.LogOut();
        });

        GetPage<HeaderPage>(x =>
        {
            x.SignIn();
        });

        GetPage<SignInPage>(x =>
        {
            x.EnterEmail(_userToLogIn.Mail);
            x.EnterPassword(_userToLogIn.Password);
            x.SubmitLogin();
        });

        // Assert
        GetPage<HeaderPage>(x =>
        {
            x.GetSignedInText.Should().Be($"{_userToLogIn.FullName}");
        });
    }
}