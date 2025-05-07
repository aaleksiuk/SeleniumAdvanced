using FluentAssertions;
using NUnit.Framework;
using SeleniumAdvanced.Helpers;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SeleniumAdvanced.Tests;
public class BasketGeneric : TestBase
{
    private readonly List<BasketItem> addedBasketItems = [];

    private readonly int productsNumbers = 10;
    private readonly int minQuantity = 1;
    private readonly int maxQuantity = 5;

    [Test]
    [Repeat(10)]
    public void AddRandomProducts()
    {
        // Arrange
        Driver.Navigate().GoToUrl(UrlProvider.AppUrl);
        addedBasketItems.Clear();

        decimal totalBasketAmount = 0;

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
                var newBasketItem = BasketItem.CreateBasketItem(productName, productPrice, minQuantity, maxQuantity);
                x.IncreaseQuantity(newBasketItem.Quantity);
                x.ClickAddToBasketBtn();

                Console.WriteLine($"Added to basket: {productName}, quantity:{newBasketItem.Quantity}, price:{productPrice}, total basket amount: {totalBasketAmount}");

                BasketItem.AddOrUpdateBasketItem(addedBasketItems, newBasketItem);
                totalBasketAmount = addedBasketItems.Sum(item=>item.TotalAmount);

                x.ClickContinueModalBtn();
            });

            GetPage<HeaderPage>(x => x.ClickLogoImage());
        }
        GetPage<HeaderPage>(x => x.ClickCartBtn());

        var expectedBasket = new Basket(addedBasketItems, totalBasketAmount);
        GetPage<BasketPage>().GetBasket().Should().BeEquivalentTo(expectedBasket);
    }
}