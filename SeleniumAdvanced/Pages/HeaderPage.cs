using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class HeaderPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement SignInBtn => Driver.WaitAndFind(By.CssSelector(".user-info"));
    private IWebElement ViewCustomerAccountBtn => Driver.WaitAndFind(By.CssSelector("span.hidden-sm-down"));

    private IWebElement SearchWidget => Driver.WaitAndFind(By.CssSelector(".search-widget input[name='s']"));
    private IWebElement SearchBtn => Driver.WaitAndFind(By.CssSelector("#search_widget > form > button"));
    private IList<IWebElement> SearchDropdown => Driver.WaitAndFindAll(By.CssSelector("#ui-id-1 li"));

    private IList<IWebElement> TopMenu => Driver.WaitAndFindAll(By.CssSelector(".top-menu"));
    private IList<IWebElement> TopMenuItems => Driver.WaitAndFindAll(By.CssSelector("#top-menu > li.category"));

    public void SignIn() => Click(SignInBtn);
    public string GetSignedInText() => ViewCustomerAccountBtn.Text;
    public void ClickSearchWidget() => Click(SearchWidget);
    public void SetSearchText(string SearchText) => SendKeys(SearchWidget, SearchText);
    public void ClickSearchBtn() => Click(SearchBtn);
    public IEnumerable<string> GetSearchDropdownItemText()
    {
        return SearchDropdown.Select(item => item.Text);
    }
    public IEnumerable<string> GetTopMenuItemsText()
    {
        return TopMenuItems.Select(item => item.Text.Trim());
    }

    public void ClickTopMenuItem(string menuItem)
    {
        var item = TopMenuItems.FirstOrDefault(i => i.Text.Trim().Equals(menuItem, StringComparison.Ordinal));
        if (item != null)
        {
            item.FindElement(By.CssSelector("a.dropdown-item")).Click();
        }
        else
        {
            throw new ArgumentException($"Menu item '{menuItem}' not found.", nameof(menuItem));
        }
    }
}