using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class Filters : TestBase
{
    [Test]
    public void AccessoriesFilters()
    {
        // Arrange
        this.driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        //base filter setting
        var basePriceFilterSliderFrom = 11;
        var basePriceFilterSliderTo = 19;

        //new filter setting
        var priceFilterSliderFrom = 13;
        var priceFilterSliderTo = 15;

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.ClickTopMenuItem("Accessories");
        });

        GetPage<CategoryPage>(x =>
        {
            var productsNumber = x.GetDisplayedProductsNumber();
            x.MoveSliderFrom(basePriceFilterSliderFrom, priceFilterSliderFrom);
            Thread.Sleep(2000);
            x.MoveSliderTo(basePriceFilterSliderTo, priceFilterSliderTo);
            Thread.Sleep(2000);

            var productPrices = x.GetProductPrices().ToList();

            // Assert
            var assertionFailures = new List<string>();
            foreach (var price in productPrices)
            {
                try
                {
                    price.Should().BeInRange(priceFilterSliderFrom, priceFilterSliderTo, $"Because the product price {price} should be within the filter range {priceFilterSliderFrom} to {priceFilterSliderTo}");
                }
                catch (AssertionFailedException ex)
                {
                    assertionFailures.Add(ex.Message);
                }
            }

            // Check for any assertion failures
            assertionFailures.Should().BeEmpty("All product prices should be within the specified filter range");

            x.ClearPriceFilter();
            Thread.Sleep(2000);
            x.GetDisplayedProductsNumber().Should().Be(productsNumber);
        });
    }
}