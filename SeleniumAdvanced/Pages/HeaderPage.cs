using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class HeaderPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement SignInBtn => Driver.WaitAndFind(By.CssSelector(".user-info"));
    private IWebElement LogOutBtn => Driver.WaitAndFind(By.CssSelector("a.logout"));
    private IWebElement ViewCustomerAccountBtn => Driver.WaitAndFind(By.CssSelector("span.hidden-sm-down"));
    private IWebElement ContactUsBtn => Driver.WaitAndFind(By.CssSelector("#contact-link"));

    private IWebElement SearchWidget => Driver.WaitAndFind(By.CssSelector(".search-widget input[name='s']"));
    private IWebElement SearchBtn => Driver.WaitAndFind(By.CssSelector("#search_widget > form > button"));
    private IList<IWebElement> SearchDropdown => Driver.WaitAndFindAll(By.CssSelector("#ui-id-1 li"));

    private IList<IWebElement> TopMenu => Driver.WaitAndFindAll(By.CssSelector(".top-menu"));
    private IList<IWebElement> TopMenuItems => Driver.WaitAndFindAll(By.CssSelector("#top-menu > li.category"));
    private IList<IWebElement> TopMenuSubItems =>
        Driver.WaitAndFindAll(By.CssSelector("#top-menu > li.category li.category")).Where(i => i.Displayed).ToList();

    private IWebElement CategoriesTopMenu => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul"));
    private IWebElement CategoriesTopMenuName => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(1)"));
    private IWebElement SubCategoriesTopMenuNames => Driver.WaitAndFind(By.CssSelector("div.block-categories > ul > li:nth-child(2)"));

    public void SignIn() => Click(SignInBtn);
    public void LogOut() => Click(LogOutBtn);
    public string GetSignedInText => ViewCustomerAccountBtn.Text;
    public void ClickSearchWidget() => Click(SearchWidget);
    public void SetSearchText(string searchText) => SendKeys(SearchWidget, searchText);
    public void ClickSearchBtn() => Click(SearchBtn);
    public IEnumerable<string> GetSearchDropdownItemText => SearchDropdown.Select(item => item.Text);
    public IEnumerable<string> GetTopMenuItemsText => TopMenuItems.Select(item => item.Text.Trim());

    public string GetCategoryName => CategoriesTopMenuName.Text;
    public void ClickTopMenuItem(string menuItem) => PerformActionOnMenuItem(TopMenuItems, menuItem, Click);

    public void HoverTopMenuItem(string menuItem) => PerformActionOnMenuItem(TopMenuItems, menuItem, Hover);

    public void HoverContactUsBtn() => Hover(ContactUsBtn);

    public IEnumerable<string> GetTopMenuSubItemsText() => TopMenuSubItems.Select(item => item.FindElement(By.TagName("a")).Text.Trim());

    public void ClickTopMenuSubItem(string menuSubItem) => PerformActionOnMenuItem(TopMenuSubItems, menuSubItem, Click);

    private static void PerformActionOnMenuItem(IEnumerable<IWebElement> menuItems, string menuItem, Action<IWebElement> action)
    {
        var item = menuItems.FirstOrDefault(i => i.Text.EqualsTrimmedIgnoreCase(menuItem))
            ?? throw new ArgumentException($"Menu item '{menuItem}' not found.", nameof(menuItem));
        var element = item.FindElement(By.CssSelector("a.dropdown-item"));
        action(element);
    }
}