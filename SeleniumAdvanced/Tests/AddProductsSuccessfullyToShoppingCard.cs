using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System.Web.UI.WebControls;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class AddToBasket : TestBase
{
    private int quantity = 3;
    private string productName = "THE BEST IS YET POSTER";
    [Test]
    [Repeat(1)]
    public void AddSuccessfullyToBasket()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        GetPage<HeaderPage>(x => x.ClickTopMenuItem("ART"));

        //Assert
        GetPage<ProductsGridPage>(x =>
        {
            x.ClickProductByName(productName);
        });

        GetPage<ProductDetailsPage>(x =>
        {
            x.IncreaseQuantity(quantity);
            x.ClickAddToBasketBtn();
        });

        GetPage<ProductDetailsPage>(x =>
        {
            x.ModalProductName.Should().Be(productName);
            x.ModalPrice.Should().Be(x.ProductPrice);
            x.ModalQuantity.Should().Be(quantity);
            x.ModalTotalItemsText.Should().Be($"({quantity})");
            decimal subTotal = x.Subtotal(quantity, x.ModalPrice);
            x.ModalSubtotal.Should().Be(subTotal);
        });
    }
}

//-	check if popup has correct name, price, quantity, there are X items in your cart, Total products value
//-	click continue shopping
//-	check if cart icon has update quantity of products – Cart(X)