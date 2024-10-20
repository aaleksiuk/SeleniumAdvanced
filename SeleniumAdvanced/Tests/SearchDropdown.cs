using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class SearchDropdown : TestBase
{
    private readonly string searchText = "HUMMINGBIRD";
    [Test]
    [Repeat(10)]
    public void SearchText()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(x =>
        {
            x.SetSearchText(searchText);
        });

        //Assert
        GetPage<HeaderPage>(x =>
        {
            x.GetSearchDropdownItemText.Should().AllSatisfy(ContainsText);
        });
    }
    private void ContainsText(string text)
    {
        if (!text.Contains(searchText))
        {
            throw new System.Exception($"Item \"{text}\" doesn't contain \"{searchText}\" search text");
        }
    }
}