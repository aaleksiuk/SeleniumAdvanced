using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System.Linq;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class PopularProducts : TestBase
{
    [Test]
    [Repeat(2)]
    public void PopularProductsNames()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<StartPage>(x =>
        {
            var popularProductsNames = x.GetProductsNames().ToList();
            using (new AssertionScope())
            {
                popularProductsNames.Should().NotBeEmpty("There should be at least one popular product on the list");

                popularProductsNames
                    .Should()
                    .AllSatisfy(name => name.Should().NotBeNullOrEmpty("Each product should have name"));
            }
        });
    }
}