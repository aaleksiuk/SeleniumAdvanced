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

    private IWebElement PriceSlider => Driver.WaitAndFind(By.CssSelector("ul.faceted-slider.collapse"));
    private IWebElement PriceSliderRangeActive => Driver.WaitAndFind(By.CssSelector("ul.faceted-slider.collapse.in"));

    private IWebElement PriceSliderFromRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(1)"));
    private IWebElement PriceSliderToRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(2)"));
    private IWebElement FilterBlock => Driver.WaitAndFind(By.CssSelector("li.filter-block"));
    private IWebElement FilterBlockHidden => Driver.WaitAndFind(By.CssSelector("#js-active-search-filters.hide"));

    private IList<IWebElement> ProductsPrices => Driver.WaitAndFindAll(By.CssSelector("div.product-price-and-shipping"));
    private IWebElement ActiveFilterPriceElementClose => Driver.WaitAndFind(By.CssSelector("li.filter-block i.material-icons.close"));
    private IWebElement Breadcrumb => Driver.WaitAndFind(By.CssSelector("#wrapper > div > nav > ol"));
    private IWebElement CategoriesTopMenu => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul"));
    private IWebElement CategoriesTopMenuName => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(1)"));
    private IWebElement SubCategoriesTopMenuNames => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(2)"));
    private IWebElement FiltersSideMenu => Driver.WaitAndFind(By.CssSelector("#search_filters"));
    private IWebElement Pagination => Driver.WaitAndFind(By.CssSelector("#js-product-list > nav > div.col-md-4"));

    public int BasePriceFilterSliderFrom => int.Parse(PriceSlider.GetAttribute("data-slider-min"));
    public int BasePriceFilterSliderTo => int.Parse(PriceSlider.GetAttribute("data-slider-max"));

    public int DisplayedProductsNumber => DisplayedProducts.Count;

    public void MoveSliderFrom(int currentPosition, int desiredPosition)
    {
        var steps = Math.Abs(desiredPosition - currentPosition);

        if (steps != 0)
        {
            PriceSliderFromRange.SendKeys(Keys.ArrowRight);
            --steps;
            currentPosition++;

            WaitForPriceRangeUpdate(currentPosition, true);
            for (var i = 0; i < steps; i++)
            {
                currentPosition++;
                PriceSliderFromRange.SendKeys(Keys.ArrowRight);
                WaitForPriceRangeUpdate(currentPosition, true);
            }
        }
    }

    public void MoveSliderTo(int currentPosition, int desiredPosition)
    {
        var steps = Math.Abs(desiredPosition - currentPosition);

        if (steps != 0)
        {
            PriceSliderToRange.SendKeys(Keys.ArrowLeft);
            --steps;
            currentPosition--;

            WaitForPriceRangeUpdate(currentPosition, false);

            for (var i = 0; i < steps; i++)
            {
                currentPosition--;
                PriceSliderToRange.SendKeys(Keys.ArrowLeft);
                WaitForPriceRangeUpdate(currentPosition, false);
            }
        }
    }

    private void WaitForPriceRangeUpdate(int expectedPrice, bool isMinPrice)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
        {
            var sliderValues = PriceSliderRangeActive.GetAttribute("data-slider-values");
            var values = ParseSliderValues(sliderValues);

            return isMinPrice ? values[0] == expectedPrice : values[1] == expectedPrice;
        });
    }

    private int[] ParseSliderValues(string sliderValues)
    {
        var cleanedValues = sliderValues.Replace("[", "")
                                            .Replace("]", "")
                                            .Replace("\"", "");

        var parts = cleanedValues.Split(',');

        return parts.Select(part => int.Parse(part.Trim())).ToArray();
    }

    public string DisplayedFilterBlock => FilterBlock.Text;

    public void WaitForFilterBlockVisible() => Driver.IsDisplayedWithTimeout(By.CssSelector("li.filter-block"));

    public IEnumerable<decimal> GetProductPrices()
    {
        return ProductsPrices.Select(item =>
        {
            var priceText = item.Text.Replace("$", "").Trim();
            return decimal.Parse(priceText);
        });
    }

    public IEnumerable<string> GetProductPricesString()
    {
        return ProductsPrices.Select(item =>
        {
            var priceText = item.Text.Replace("$", "").Trim();
            return item.Text;
        });
    }

    public void ClearPriceFilter() => Click(ActiveFilterPriceElementClose);
    public string GetCategoryName => CategoriesTopMenuName.Text;
    public bool FiltersSideMenuDisplayed() => FiltersSideMenu.Displayed;
    public bool WaitForFilterBlockToBeHidden() => FilterBlockHidden.Displayed;
    public string PaginationText => Pagination.Text;
}