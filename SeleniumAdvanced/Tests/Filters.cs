using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class Filters : TestBase
{
    [Test]
    [Repeat(2)]
    public void AccessoriesFilters()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        //new filter settings
        var priceFilterWhereToMoveSliderFrom = 13;
        var priceFilterWhereToMoveSliderTo = 15;

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.ClickTopMenuItem("Accessories");
        });

        GetPage<CategoryPage>(x =>
        {
            var productsNumber = x.DisplayedProductsNumber;
            var basePriceFilterSliderFrom = x.BasePriceFilterSliderFrom;
            var basePriceFilterSliderTo = x.BasePriceFilterSliderTo;

            x.MoveSliderFrom(basePriceFilterSliderFrom, priceFilterWhereToMoveSliderFrom);
            x.WaitForFilterBlockVisible();

            x.MoveSliderTo(basePriceFilterSliderTo, priceFilterWhereToMoveSliderTo);
            x.WaitForFilterBlockVisible();

            x.DisplayedFilterBlock
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
            .FirstOrDefault()
            .Trim()
            .Should()
            .Be($"Price: ${priceFilterWhereToMoveSliderFrom}.00 - ${priceFilterWhereToMoveSliderTo}.00");

            var productPrices = x.GetProductPrices().ToList();
            var productPricesStrings = x.GetProductPricesString().ToList();

            // Assert
            var assertionFailures = new List<string>();
            foreach (var price in productPrices)
            {
                try
                {
                    price
                    .Should()
                    .BeInRange(priceFilterWhereToMoveSliderFrom, priceFilterWhereToMoveSliderTo,
                    $"Because the product price {price} should be within the filter range {priceFilterWhereToMoveSliderFrom} to {priceFilterWhereToMoveSliderTo}");
                }
                catch (AssertionFailedException ex)
                {
                    assertionFailures.Add(ex.Message);
                }
            }

            // Check for any assertion failures
            assertionFailures
            .Should()
            .BeEmpty("All product prices should be within the specified filter range");

            x.ClearPriceFilter();
            x.WaitForFilterBlockToBeHidden().Should().BeTrue();
            x.DisplayedProductsNumber.Should().Be(productsNumber);
        });
    }
}