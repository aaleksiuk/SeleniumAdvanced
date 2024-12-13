using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System.Collections.Generic;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class RemoveProductsFromBasket : TestBase
{
    private decimal subTotal = 0;
    private decimal subTotalFirstProduct = 0;
    private int basketQuantity = 0;
    private readonly List<string> selectedProducts = [];

    [Test]
    [Repeat(1)]
    public void RemoveProducts()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act
        AddRandomProductToBasket();
        subTotalFirstProduct = subTotal;
        basketQuantity++;
        ValidateBasketAfterAddingProducts(basketQuantity);

        GetPage<HeaderPage>(x => x.ClickLogoImage());
        AddRandomProductToBasket();
        basketQuantity++;
        ValidateBasketAfterAddingProducts(basketQuantity);

        // Assert
        RemoveProductAndValidate(remainingItems: "1 item", expectedSubtotal: subTotal - subTotalFirstProduct);
        RemoveProductAndValidate(remainingItems: "0 items", expectedSubtotal: 0);

        GetPage<HeaderPage>(x => x.CartProductCount.Should().Be("(0)"));
    }

    private void AddRandomProductToBasket(int quantity = 1)
    {
        GetPage<ProductsGridPage>(x =>
        {
            var product = x.SelectRandomProductExcludingSelectedBefore(selectedProducts);
            x.ClickProductByName(product);
            selectedProducts.Add(product);
        });

        GetPage<ProductDetailsPage>(page =>
        {
            page.ClickAddToBasketBtn();
            subTotal += page.CalculateSubtotal(quantity, page.ModalPrice);
            page.ClickContinueModalBtn();
        });
    }

    private void ValidateBasketAfterAddingProducts(int basketQuantity)
    {
        GetPage<HeaderPage>(page => page.ClickCartBtn());

        GetPage<BasketPage>(page =>
        {
            using (new AssertionScope())
            {
                page.Subtotal.Should().Be(subTotal);
                page.SubtotalProducts.Should().Be($"{basketQuantity} {GetExpectedItemsMessage(basketQuantity)}");
            }
        });
    }
    private void RemoveProductAndValidate(string remainingItems, decimal expectedSubtotal)
    {
        GetPage<BasketPage>(page =>
        {
            page.ClickFirstProductDeleteButton();
            if (page.HasChanged)
            {
                using (new AssertionScope())
                {
                    page.SubtotalProducts.Should().Be(remainingItems);
                    page.Subtotal.Should().Be(expectedSubtotal);
                }
            }
        });
    }
    private string GetExpectedItemsMessage(int quantity)
    {
        return quantity == 1 ? "item" : "items";
    }
}