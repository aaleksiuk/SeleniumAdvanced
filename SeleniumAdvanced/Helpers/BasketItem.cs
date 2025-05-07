using System;
using System.Collections.Generic;

namespace SeleniumAdvanced.Helpers;

public class BasketItem(string name, int quantity, decimal price)
{
    public string Name { get; } = name;
    public int Quantity { get; private set; } = quantity;
    public decimal Price { get; } = price;
    public decimal TotalAmount => Quantity * Price;


    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public static BasketItem CreateBasketItem(string productName, decimal productPrice, int minQuantity, int maxQuantity)
    {
        var rand = new Random();
        var quantity = rand.Next(minQuantity, maxQuantity);
        return new BasketItem(productName, quantity, productPrice);
    }
    public static void AddOrUpdateBasketItem(List<BasketItem> basketItems, BasketItem newItem)
    {
        var existingItem = basketItems.Find(item => item.Name == newItem.Name);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(newItem.Quantity);
        }
        else
        {
            basketItems.Add(newItem);
        }
    }
}
public class Basket(List<BasketItem> basketItems, decimal total)
{
    public List<BasketItem> BasketItems { get; } = basketItems;
    public decimal Total { get; } = total;
}