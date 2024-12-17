using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
using System.Collections.Generic;

namespace SeleniumAdvanced.Tests;

[TestFixture]
public class RemoveProductsFromBasket : TestBase
{
    private decimal currentSubTotal = 0;
    private decimal subTotalFirstProduct = 0;
    private int basketQuantity = 0;
    private readonly List<string> _selectedProducts = [];

    [Test]
    [Repeat(2)]
    public void RemoveProducts()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act & Validate
        AddProductAndValidate(1);
        subTotalFirstProduct = currentSubTotal;

        GetPage<HeaderPage>(x => x.ClickLogoImage());

        AddProductAndValidate(1);

        // Remove products and validate
        RemoveProductAndValidate(expectedQuantity: 1, expectedSubtotal: currentSubTotal - subTotalFirstProduct);
        RemoveProductAndValidate(expectedQuantity: 0, expectedSubtotal: 0);

        ValidateCartItemCount(expectedCount: 0);
    }

    private void AddProductAndValidate(int quantity)
    {
        AddRandomProductToBasket(quantity);
        basketQuantity += quantity;
        ValidateBasket(basketQuantity);
    }

    private void AddRandomProductToBasket(int quantity)
    {
        GetPage<ProductsGridPage>(page =>
        {
            var product = page.SelectRandomProductExcludingSelectedBefore(_selectedProducts);
            page.ClickProductByName(product);
            _selectedProducts.Add(product);
        });

        GetPage<ProductDetailsPage>(page =>
        {
            page.ClickAddToBasketBtn();
            currentSubTotal += page.CalculateSubtotal(quantity, page.ModalPrice);
            page.ClickContinueModalBtn();
        });
    }

    private void ValidateBasket(int basketQuantity)
    {
        GetPage<HeaderPage>(page => page.ClickCartBtn());

        GetPage<BasketPage>(page =>
        {
            using (new AssertionScope())
            {
                
                page.Subtotal.Should().Be(currentSubTotal);
                page.SubtotalProducts.Should().Be($"{basketQuantity} {GetExpectedItemsMessage(basketQuantity)}");
            }
        });
    }

    private void RemoveProductAndValidate(int expectedQuantity, decimal expectedSubtotal)
    {
        GetPage<BasketPage>(page =>
        {
            page.ClickFirstProductDeleteButton();
            if (page.HasChanged)
            {
                using (new AssertionScope())
                {
                    page.SubtotalProducts.Should().Be($"{expectedQuantity} {GetExpectedItemsMessage(expectedQuantity)}");
                    page.Subtotal.Should().Be(expectedSubtotal);
                }
                basketQuantity--;
            }
        });
    }

    private void ValidateCartItemCount(int expectedCount)
    {
        GetPage<HeaderPage>(page =>
        {
            page.CartProductCount.Should().Be($"({expectedCount})");
        });
    }

    public static string GetExpectedItemsMessage(int quantity)
    {
        return quantity == 1 ? "item" : "items";
    }
}