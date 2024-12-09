using OpenQA.Selenium;

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
        return priceText.Replace("$", "").Trim();
    }

    private static decimal TextToDecimal(string elementText)
    {
        return decimal.Parse(elementText);
    }
}