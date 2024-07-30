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
                    //x.Should().Be(menuItem, $"the clicked menu item '{menuItem}' should match the category name '{categoryName}'");
                    x.IsFiltersSideMenuDisplayed().Should().BeTrue();
                    var productsNumber = x.GetDisplayedProductsNumber();
                    var displayPaginationMessage = x.GetPaginationText();
                    var pattern = $@"Showing \d+-\d+ of {productsNumber} item\(s\)";
                    displayPaginationMessage.Should().MatchRegex(pattern);
                });
            }
        });
    }
}