using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
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

            using (new AssertionScope())
            {
                x.DisplayedFilterBlock
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                .FirstOrDefault()
                .Trim()
                .Should()
                .Be($"Price: ${priceFilterWhereToMoveSliderFrom}.00 - ${priceFilterWhereToMoveSliderTo}.00");

                var productPrices = x.GetProductPrices().ToList();

                // Assert product prices
                foreach (var price in productPrices)
                {
                    price
                    .Should()
                    .BeInRange(priceFilterWhereToMoveSliderFrom, priceFilterWhereToMoveSliderTo,
                    $"Because the product price {price} should be within the filter range {priceFilterWhereToMoveSliderFrom} to {priceFilterWhereToMoveSliderTo}");
                }

                // Ensure filter is cleared and state is reset
                x.ClearPriceFilter();
                x.WaitForFilterBlockToBeHidden().Should().BeTrue();
                x.DisplayedProductsNumber.Should().Be(productsNumber);
            }
        });
    }
}