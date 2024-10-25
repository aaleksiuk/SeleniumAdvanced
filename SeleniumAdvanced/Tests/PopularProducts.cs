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
        GetPage<ProductsGridPage>(x =>
        {
            var popularProductsNames = x.GetProductsNames().ToList();
            var popularProductsPrices = x.GetProductPricesRegularAndDiscount().ToList();
            using (new AssertionScope())
            {
                popularProductsNames.Should().NotBeEmpty("There should be at least one popular product on the list");

                popularProductsNames
                    .Should()
                    .AllSatisfy(name => name.Should().NotBeNullOrEmpty("Each product should have name"));

                popularProductsPrices
                    .Should()
                    .AllSatisfy(price => price.Should().BeGreaterThan(0, "Each product price should be > 0"));
            }
        });
    }
}