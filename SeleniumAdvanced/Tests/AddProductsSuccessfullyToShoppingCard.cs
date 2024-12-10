using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class AddToBasket : TestBase
{
    private readonly int quantity = 3;
    private readonly string productName = "THE BEST IS YET POSTER";
    [Test]
    [Repeat(1)]
    public void AddSuccessfullyToBasket()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(x => x.ClickTopMenuItem("ART"));

        GetPage<ProductsGridPage>(x =>
        {
            x.ClickProductByName(productName);
        });

        GetPage<ProductDetailsPage>(x =>
        {
            x.IncreaseQuantity(quantity);
            x.ClickAddToBasketBtn();
        });

        //Assert
        GetPage<ProductDetailsPage>(x =>
        {
            using (new AssertionScope())
            {
                x.ModalProductName.Should().Be(productName);
                x.ModalPrice.Should().Be(x.ProductPrice);
                x.ModalQuantity.Should().Be(quantity);
                x.ModalTotalItemsText.Should().Be($"({quantity})");

                var subTotal = x.CalculateSubtotal(quantity, x.ModalPrice);
                x.ModalSubtotal.Should().Be(subTotal);

                x.ClickContinueModalBtn();
            }
        });

        GetPage<HeaderPage>(x =>
        {
            x.CartProductCount.Should().Be($"({quantity})");
        });
    }
}