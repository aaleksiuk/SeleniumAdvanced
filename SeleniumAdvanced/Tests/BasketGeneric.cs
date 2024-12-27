using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
using System.Collections.Generic;
namespace SeleniumAdvanced.Tests;
public class BasketGeneric : TestBase
{
    private readonly List<Basket> addedBasketItems = [];

    private readonly int productsNumbers = 10;
    private readonly int minQuantity = 1;
    private readonly int maxQuantity = 5;
    private decimal totalFromModal;

    [Test]
    [Repeat(10)]
    public void AddRandomProducts()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);
        addedBasketItems.Clear();

        // Act & Validate
        for (var i = 0; i < productsNumbers; i++)
        {
            string productName = null;
            decimal productPrice = 0;

            GetPage<ProductsGridPage>(x =>
            {
                productName = x.SelectRandomProduct();
                x.ClickProductByName(productName);
            });

            GetPage<ProductDetailsPage>(x =>
            {
                productPrice = x.ProductPrice;
                var newBasketItem = Basket.CreateBasketItem(productName, productPrice, minQuantity, maxQuantity);
                x.IncreaseQuantity(newBasketItem.Quantity);
                x.ClickAddToBasketBtn();

                Console.WriteLine($"Added to basket: {productName} {newBasketItem.Quantity} {productPrice}");

                Basket.AddOrUpdateBasketItem(addedBasketItems, newBasketItem);
                totalFromModal = x.ModalSubtotal;
                x.ClickContinueModalBtn();
            });

            GetPage<HeaderPage>(x => x.ClickLogoImage());
        }
        GetPage<HeaderPage>(x => x.ClickCartBtn());
        GetPage<BasketPage>(x =>
        {
            x.GetProductsListFromBasket().Should().BeEquivalentTo(addedBasketItems);
            x.Subtotal.Should().Be(totalFromModal);
        });
    }
}