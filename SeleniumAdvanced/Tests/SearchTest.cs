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
    [Repeat(2)]
    public void Search()
    {
        // Arrange
        var searchText = "MUG THE BEST";
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.SetSearchText(searchText);
            x.ClickSearchBtn();
        });

        //Assert
        GetPage<ProductsGridPage>(x =>
        {
            x.GetProductsNames().Should().Contain(searchText);
        });
    }
}