using OpenQA.Selenium;
using System;

namespace SeleniumAdvanced.Extensions;

public static class WebElementExtensions
{
    public static decimal GetPrice(this IWebElement element)
    {
        var numericPriceText = RemoveCurrencySymbols(element.Text);
        return TextToDecimal(numericPriceText);

    }
    private static string RemoveCurrencySymbols(string priceText)
    {
        return priceText.TrimStart('$').Split(' ')[0]; // to get prices like $28.72 SAVE 20%
    }

    private static decimal TextToDecimal(string elementText)
    {
        return decimal.Parse(elementText);
    }
}