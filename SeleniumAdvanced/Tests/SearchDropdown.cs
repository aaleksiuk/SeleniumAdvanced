using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class SearchDropdown : TestBase
{
    private readonly string _searchText = "HUMMINGBIRD";
    [Test]
    [Repeat(2)]
    public void SearchText()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>().SetSearchText(_searchText);

        //Assert
        GetPage<HeaderPage>().GetSearchDropdownItemText.Should().AllSatisfy(ContainsText);
    }
    private void ContainsText(string text)
    {
        if (!text.Contains(_searchText))
        {
            throw new System.Exception($"Item \"{text}\" doesn't contain \"{_searchText}\" search text");
        }
    }
}