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
        const int PRICE_FILTER_TO_MOVE_SLIDER_FROM = 13;
        const int PRICE_FILTER_TO_MOVE_SLIDER_TO = 15;

        var productsNumber = 0;

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.ClickTopMenuItem("Accessories");
        });

        GetPage<ProductsGridPage>(x =>
        {
            productsNumber = x.DisplayedProductsNumber;
        });

        GetPage<CategoryPage>(x =>
        {

            var basePriceFilterSliderFrom = x.BasePriceFilterSliderFrom;
            var basePriceFilterSliderTo = x.BasePriceFilterSliderTo;

            x.MoveSliderFrom(basePriceFilterSliderFrom, PRICE_FILTER_TO_MOVE_SLIDER_FROM);
            x.WaitForFilterBlockVisible();

            x.MoveSliderTo(basePriceFilterSliderTo, PRICE_FILTER_TO_MOVE_SLIDER_TO);
            x.WaitForFilterBlockVisible();

            using (new AssertionScope())
            {
                x.DisplayedFilterBlock
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                .FirstOrDefault()
                .Trim()
                .Should()
                .Be($"Price: ${PRICE_FILTER_TO_MOVE_SLIDER_FROM}.00 - ${PRICE_FILTER_TO_MOVE_SLIDER_TO}.00");
            }
        });

        GetPage<ProductsGridPage>(x =>
        {
            var productPrices = x.GetProductPrices().ToList();

            // Assert product prices
            foreach (var price in productPrices)
            {
                price
                .Should()
                .BeInRange(PRICE_FILTER_TO_MOVE_SLIDER_FROM, PRICE_FILTER_TO_MOVE_SLIDER_TO,
                $"Because the product price {price} should be within the filter range {PRICE_FILTER_TO_MOVE_SLIDER_FROM} to {PRICE_FILTER_TO_MOVE_SLIDER_TO}");
            }
        });

        GetPage<CategoryPage>(x =>
        {
            // Ensure filter is cleared and state is reset
            x.ClearPriceFilter();
            x.WaitForFilterBlockToBeHidden().Should().BeTrue();
        });

        GetPage<ProductsGridPage>(x =>
        {
            x.DisplayedProductsNumber.Should().Be(productsNumber);
        });
    }
}