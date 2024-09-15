using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;
public class CategoryPage(IWebDriver driver) : BasePage(driver)
{
    private IList<IWebElement> DisplayedProducts => Driver.WaitAndFindAll(By.CssSelector("div.products.row > div"));
    public IWebElement PriceSliderFromRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(1)"));
    public IWebElement PriceSliderToRange => Driver.WaitAndFind(By.CssSelector("a.ui-slider-handle:nth-of-type(2)"));
    private IList<IWebElement> ProductsPrices => Driver.WaitAndFindAll(By.CssSelector("div.product-price-and-shipping"));
    public IWebElement ActiveFilterPriceElement => Driver.WaitAndFind(By.CssSelector("li.filter-block"));
    private IWebElement ActiveFilterPriceElementClose => Driver.WaitAndFind(By.CssSelector("li.filter-block i.material-icons.close"));
    private IWebElement Breadcrumb => Driver.WaitAndFind(By.CssSelector("#wrapper > div > nav > ol"));
    private IWebElement CategoriesTopMenu => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul"));
    private IWebElement CategoriesTopMenuName => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(1)"));
    private IWebElement SubCategoriesTopMenuNames => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(2)"));
    private IWebElement FiltersSideMenu => Driver.WaitAndFind(By.CssSelector("#search_filters"));
    private IWebElement Pagination => Driver.WaitAndFind(By.CssSelector("#js-product-list > nav > div.col-md-4"));

    public int GetDisplayedProductsNumber() => DisplayedProducts.Count;

    public void MoveSliderFrom(int currentPosition, int desiredPosition)
    {
        var counter = Math.Abs(desiredPosition - currentPosition);
        var keyToPress = (desiredPosition > currentPosition) ? Keys.ArrowRight : Keys.ArrowLeft;
        for (var i = 0; i < counter; i++)
        {
            PriceSliderFromRange.SendKeys(keyToPress);
        }
    }

    public void MoveSliderTo(int currentPosition, int desiredPosition)
    {
        var counter = Math.Abs(desiredPosition - currentPosition);
        var keyToPress = (desiredPosition > currentPosition) ? Keys.ArrowRight : Keys.ArrowLeft;
        for (var i = 0; i < counter; i++)
        {
            PriceSliderToRange.SendKeys(keyToPress);
        }
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
    public string GetCategoryName() => CategoriesTopMenuName.Text;
    public bool IsFiltersSideMenuDisplayed() => FiltersSideMenu.Displayed;
    public string PaginationText => Pagination.Text;
}