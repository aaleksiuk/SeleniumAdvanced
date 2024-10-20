using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class SearchTest : TestBase
{
    [Test]
    [Repeat(10)]
    public void Search()
    {
        // Arrange
        var searchText = "MUG THE BEST";
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.SetSearchText(searchText);
            x.ClickSearchBtn();
        });

        //Assert
        GetPage<SearchResultPage>(x =>
        {
            x.GetProductTitle.Should().Be(searchText);
        });
    }
}