using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class LogAsAUser : TestBase
{
    [Test]
    public void LogInUser()
    {
        this.driver.Navigate().GoToUrl(UrlProvider.SignInUrl);
    }
}