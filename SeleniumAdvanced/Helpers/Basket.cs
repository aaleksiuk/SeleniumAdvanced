using System;
using System.Collections.Generic;

namespace SeleniumAdvanced.Helpers;

public class Basket(string name, int quantity, decimal price)
{
    public string Name { get; } = name;
    public int Quantity { get; private set; } = quantity;
    public decimal Price { get; } = price;

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public static Basket CreateBasketItem(string productName, decimal productPrice, int minQuantity, int maxQuantity)
    {
        var rand = new Random();
        var quantity = rand.Next(minQuantity, maxQuantity);
        return new Basket(productName, quantity, productPrice);
    }
    public static void AddOrUpdateBasketItem(List<Basket> basketItems, Basket newItem)
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