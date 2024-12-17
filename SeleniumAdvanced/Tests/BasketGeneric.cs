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
    [Repeat(1)]
    public void AddRandomProducts()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);

        // Act & Validate
        for (var i = 0; i < productsNumbers; i++)
        {
            string productName = null;
            decimal productPrice = 0;
            var productQuantity = 0;

            GetPage<ProductsGridPage>(x =>
            {
                var product = x.SelectRandomProduct();
                productName = product;
                x.ClickProductByName(product);

            });

            GetPage<ProductDetailsPage>(x =>
            {
                var rand = new Random();
                var quantity = rand.Next(minQuantity, maxQuantity);
                x.IncreaseQuantity(quantity);
                productQuantity = quantity;
                productPrice = x.ProductPrice;
                x.ClickAddToBasketBtn();

                Console.WriteLine($"Added to basket: {productName} {productQuantity} {productPrice}");

                var existingItem = addedBasketItems.Find(item => item.Name == productName);
                if (existingItem != null)
                {
                    existingItem.IncreaseQuantity(productQuantity);
                }
                else
                {
                    addedBasketItems.Add(new Basket(productName, productQuantity, productPrice));
                }
                totalFromModal = x.ModalSubtotal;
                x.ClickContinueModalBtn();
            });

            GetPage<HeaderPage>(x => x.ClickLogoImage());
        }
        GetPage<HeaderPage>(x => x.ClickCartBtn());
        GetPage<BasketPage>(x =>
        {
            addedBasketItems.Should().BeEquivalentTo(x.GetProductsListFromBasket());
            x.Subtotal.Should().Be(totalFromModal);
        });
    }
}