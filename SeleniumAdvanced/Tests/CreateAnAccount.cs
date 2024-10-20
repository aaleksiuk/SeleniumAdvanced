using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using static SeleniumAdvanced.Helpers.CreateAccountHelper;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class CreateAnAccount : TestBase
{
    [Test]
    [Repeat(10)]
    public void CreateAccount()
    {
        // Arrange
        var personService = new CreateAccountService(driver);

        //Assert
        GetPage<HeaderPage>(x =>
        {
            var person = personService.CreateNewAccount();
            x.GetSignedInText.Should().Be($"{person.FullName}");
        });
    }
}