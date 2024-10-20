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
public class CategoriesTest : TestBase
{
    [Test]
    [Repeat(10)]
    public void Categories()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage((Action<HeaderPage>)(headerPage =>
        {
            var topMenuItems = headerPage.GetTopMenuItemsText.ToList();
            foreach (var menuItem in topMenuItems)
            {
                headerPage.ClickTopMenuItem(menuItem);

                _ = GetPage((Action<CategoryPage>)(x =>
                {
                    using (new AssertionScope()) // Soft assertions block
                    {
                        ValidateCategoryNameAndFilters(x, menuItem);
                        ValidatePaginationMsg(x);
                    }
                }));
            }
        }));
    }

    [Test]
    [Repeat(10)]
    public void SubCategories()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(headerPage =>
        {
            const int MAX_NUMBER_OF_SUBCATEGORIES = 2;
            string[] topMenuItems = ["CLOTHES", "ACCESSORIES"];
            foreach (var menuItem in topMenuItems)
            {
                for (var i = 0; i < MAX_NUMBER_OF_SUBCATEGORIES; i++)
                {
                    headerPage.HoverTopMenuItem(menuItem);
                    var topMenuSubItems = headerPage.GetTopMenuSubItemsText().ToList();

                    var menuSubItem = topMenuSubItems[i];

                    headerPage.ClickTopMenuSubItem(menuSubItem);
                    GetPage<CategoryPage>(x =>
                    {
                        using (new AssertionScope())
                        {
                            ValidateCategoryNameAndFilters(x, menuSubItem);
                            ValidatePaginationMsg(x);
                        }
                    });
                }
            }
        });
    }
    private static void ValidatePaginationMsg(CategoryPage x)
    {
        var productsNumber = x.DisplayedProductsNumber;
        var displayPaginationMessage = x.PaginationText;
        var pattern = $@"Showing \d+-\d+ of {productsNumber} item\(s\)"; //'\d+' represents one or more digits (0-9), example: Showing 15-25 of 100 items

        displayPaginationMessage.Should().MatchRegex(pattern);
    }
    private static void ValidateCategoryNameAndFilters(CategoryPage x, string menuItem)
    {
        var categoryName = x.GetCategoryName;

        x.GetCategoryName
        .Should()
        .Be(menuItem, $"The clicked menu item/ sub item '{menuItem}' should match the category name '{categoryName}'");

        x.FiltersSideMenuDisplayed().Should().BeTrue();
    }
}