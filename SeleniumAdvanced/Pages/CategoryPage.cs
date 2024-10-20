using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvanced.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;
public class CategoryPage(IWebDriver driver) : BasePage(driver)
{
    private IList<IWebElement> DisplayedProducts => Driver.WaitAndFindAll(By.CssSelector("div.products.row > div"));
    private IWebElement PriceSliderFromRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(1)"));
    private IWebElement PriceSliderToRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(2)"));

    private IWebElement SliderElement => Driver.WaitAndFind(By.CssSelector("ul.faceted-slider.collapse.in"));


    private IList<IWebElement> ProductsPrices => Driver.WaitAndFindAll(By.CssSelector("div.product-price-and-shipping"));
    private IWebElement ActiveFilterPriceElement => Driver.WaitAndFind(By.CssSelector("li.filter-block"));
    private IWebElement ActiveFilterPriceElementClose => Driver.WaitAndFind(By.CssSelector("li.filter-block i.material-icons.close"));
    private IWebElement Breadcrumb => Driver.WaitAndFind(By.CssSelector("#wrapper > div > nav > ol"));
    private IWebElement CategoriesTopMenu => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul"));
    private IWebElement CategoriesTopMenuName => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(1)"));
    private IWebElement SubCategoriesTopMenuNames => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(2)"));
    private IWebElement FiltersSideMenu => Driver.WaitAndFind(By.CssSelector("#search_filters"));
    private IWebElement Pagination => Driver.WaitAndFind(By.CssSelector("#js-product-list > nav > div.col-md-4"));

    public int DisplayedProductsNumber => DisplayedProducts.Count;

    public void MoveSliderFrom(int currentPosition, int desiredPosition, int expectedMinPrice)
    {
        MoveSlider(PriceSliderFromRange, currentPosition, desiredPosition);
        WaitForPriceRangeUpdate(expectedMinPrice, true);
    }

    public void MoveSliderTo(int currentPosition, int desiredPosition, int expectedMaxPrice)
    {
        MoveSlider(PriceSliderToRange, currentPosition, desiredPosition);
        WaitForPriceRangeUpdate(expectedMaxPrice, false);
    }

    private void MoveSlider(IWebElement sliderHandle, int currentPosition, int desiredPosition)
    {
        int steps = Math.Abs(desiredPosition - currentPosition);
        var keyToPress = (desiredPosition > currentPosition) ? Keys.ArrowRight : Keys.ArrowLeft;

        for (int i = 0; i < steps; i++)
        {
            sliderHandle.SendKeys(keyToPress);
        }
    }

    private void WaitForPriceRangeUpdate(int expectedPrice, bool isMinPrice)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        // Get the data-slider-values attribute
        var sliderValues = SliderElement.GetAttribute("data-slider-values");
        var values = ParseSliderValues(sliderValues);

        // Wait for the appropriate price to be updated
        if (isMinPrice)
        {
            wait.Until(d => values[0] == expectedPrice); // Check for minimum price
        }
        else
        {
            wait.Until(d => values[1] == expectedPrice); // Check for maximum price
        }
    }

    private int[] ParseSliderValues(string sliderValues)
    {
        // Clean and parse the slider values from the attribute
        var cleanedInput = sliderValues.TrimStart('[').TrimEnd(']').Replace("\"", "");

        return cleanedInput
            .Split(',')
            .Select(int.Parse) // Convert to integers
            .ToArray();
    }


    public IEnumerable<decimal> GetProductPrices()
    {
        return ProductsPrices.Select(item =>
        {
            var priceText = item.Text.Replace("$", "").Trim();
            return decimal.Parse(priceText);
        });
    }
    public bool IsPriceFilterDisplayed(string priceRange) => ActiveFilterPriceElement.Text.Contains(priceRange);
    public void ClearPriceFilter() => Click(ActiveFilterPriceElementClose);
    public string GetCategoryName => CategoriesTopMenuName.Text;
    public bool FiltersSideMenuDisplayed() => FiltersSideMenu.Displayed;
    public string PaginationText => Pagination.Text;
}