using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System.Linq;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class CategoriesTest : TestBase
{
    [Test]
    public void Categories()
    {
        // Arrange
        this.driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(headerPage =>
        {
            var topMenuItems = headerPage.GetTopMenuItemsText().ToList();
            foreach (var menuItem in topMenuItems)
            {
                headerPage.ClickTopMenuItem(menuItem);

                GetPage<CategoryPage>(x =>
                {
                    var categoryName = x.GetCategoryName();
                    categoryName.Should().Be(menuItem, $"the clicked menu item '{menuItem}' should match the category name '{categoryName}'");
                    x.IsFiltersSideMenuDisplayed().Should().BeTrue();
                    var productsNumber = x.GetDisplayedProductsNumber();
                    var displayPaginationMessage = x.PaginationText;
                    var pattern = $@"Showing \d+-\d+ of {productsNumber} item\(s\)";
                    displayPaginationMessage.Should().MatchRegex(pattern);
                });
            }
        });
    }

    [Test]
    public void SubCategories()
    {
        // Arrange
        this.driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(headerPage =>
        {
            const int MAX_NUMBER_OF_SUBCATEGORIES = 10;
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
                        var categoryName = x.GetCategoryName();
                        categoryName.Should().Be(menuSubItem, $"the clicked menu sub item '{menuSubItem}' should match the category name '{categoryName}'");
                        x.IsFiltersSideMenuDisplayed().Should().BeTrue();
                        var productsNumber = x.GetDisplayedProductsNumber();
                        var displayPaginationMessage = x.PaginationText;
                        var pattern = $@"Showing \d+-\d+ of {productsNumber} item\(s\)";
                        displayPaginationMessage.Should().MatchRegex(pattern);
                    });

                    var isLastSubItem = i == topMenuSubItems.Count - 1;
                    if (isLastSubItem)
                    {
                        break;
                    }
                }
            }
        });
    }
}