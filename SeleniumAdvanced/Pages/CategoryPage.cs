﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvanced.Extensions;
using System;
using System.Linq;

namespace SeleniumAdvanced.Pages;
public class CategoryPage(IWebDriver driver) : BasePage(driver)
{
    public ProductsGridPage ProductsGridPage => Driver.GetPage<ProductsGridPage>();
    private IWebElement PriceSlider => Driver.WaitAndFind(By.CssSelector("ul.faceted-slider.collapse"));
    private IWebElement PriceSliderRangeActive => Driver.WaitAndFind(By.CssSelector("ul.faceted-slider.collapse.in"));

    private IWebElement PriceSliderFromRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(1)"));
    private IWebElement PriceSliderToRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(2)"));

    private static By PriceSliderFromRangeSelector => By.CssSelector("a.ui-slider-handle:nth-of-type(1)");
    private static By PriceSliderToRangeSelector => By.CssSelector("a.ui-slider-handle:nth-of-type(2)");

    private IWebElement FilterBlock => Driver.WaitAndFind(By.CssSelector("li.filter-block"));
    private IWebElement FilterBlockHidden => Driver.WaitAndFind(By.CssSelector("#js-active-search-filters.hide"));

    private IWebElement ActiveFilterPriceElementClose => Driver.WaitAndFind(By.CssSelector("li.filter-block i.material-icons.close"));
    private IWebElement Breadcrumb => Driver.WaitAndFind(By.CssSelector("#wrapper > div > nav > ol"));

    private IWebElement FiltersSideMenu => Driver.WaitAndFind(By.CssSelector("#search_filters"));
    private IWebElement Pagination => Driver.WaitAndFind(By.CssSelector("#js-product-list > nav > div.col-md-4"));

    public int BasePriceFilterSliderFrom => int.Parse(PriceSlider.GetDomAttribute("data-slider-min"));
    public int BasePriceFilterSliderTo => int.Parse(PriceSlider.GetDomAttribute("data-slider-max"));

    public void MoveSliderFrom(int currentPosition, int desiredPosition)
    {
        MoveSlider(currentPosition, desiredPosition, PriceSliderFromRangeSelector);
    }
    public void MoveSliderTo(int currentPosition, int desiredPosition)
    {
        MoveSlider(currentPosition, desiredPosition, PriceSliderToRangeSelector);
    }
    public void MoveSlider(int currentPosition, int desiredPosition, By sliderSelector)
    {
        var steps = Math.Abs(desiredPosition - currentPosition);
        if (steps == 0)
        {
            return;
        }

        var directionKey = desiredPosition > currentPosition ? Keys.ArrowRight : Keys.ArrowLeft;
        var stepIncrement = desiredPosition > currentPosition ? 1 : -1;

        for (var i = 0; i < steps; i++)
        {
            Driver.WaitAndFind(sliderSelector).SendKeys(directionKey);
            currentPosition += stepIncrement;
            WaitForPriceRangeUpdate(currentPosition);
        }
    }
    private void WaitForPriceRangeUpdate(int expectedPrice)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d =>
        {
            var sliderValues = PriceSliderRangeActive.GetDomAttribute("data-slider-values");
            var values = ParseSliderValues(sliderValues);
            return values.Any(c => c.Equals(expectedPrice));
        });
    }
    private static int[] ParseSliderValues(string sliderValues)
    {
        var cleanedValues = sliderValues.Replace("[", "")
                                            .Replace("]", "")
                                            .Replace("\"", "");

        var parts = cleanedValues.Split(',');
        return parts.Select(part => int.Parse(part.Trim())).ToArray();
    }
    public string DisplayedFilterBlock => FilterBlock.Text;

    public void WaitForFilterBlockVisible() => Driver.IsDisplayedWithTimeout(By.CssSelector("li.filter-block"));
    public void ClearPriceFilter() => Click(ActiveFilterPriceElementClose);
    public bool FiltersSideMenuDisplayed() => FiltersSideMenu.Displayed;
    public bool WaitForFilterBlockToBeHidden() => FilterBlockHidden.Displayed;
    public string PaginationText => Pagination.Text;
}