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
public class PopularProducts : TestBase
{
    [Test]
    [Repeat(2)]
    public void PopularProductsNames()
    {
        // Arrange
        driver.Navigate().GoToUrl(UrlProvider.AppUrl);
    }
}