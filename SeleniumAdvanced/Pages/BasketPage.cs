using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using SeleniumAdvanced.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class BasketPage(IWebDriver driver) : BasePage(driver)
{
    public decimal Subtotal => Driver.WaitAndFind(By.CssSelector("#cart-subtotal-products .value")).GetPrice();
    public string SubtotalProducts => Driver.WaitAndFind
        (By.CssSelector("#cart-subtotal-products .label.js-subtotal")).Text;

    private IList<IWebElement> BasketItems => Driver.FindElements(By.CssSelector("ul.cart-items > li.cart-item"));

    private IWebElement FirstProductDeleteButton =>
    Driver.WaitAndFindAll(By.CssSelector("i.material-icons.float-xs-left")).First();

    private IWebElement LastProductDeleteButton =>
    Driver.WaitAndFindAll(By.CssSelector("i.material-icons.float-xs-left")).Last();

    public void ClickFirstProductDeleteButton() => Click(FirstProductDeleteButton);
    public void ClickLastProductDeleteButton() => Click(LastProductDeleteButton);

    public bool WaitUntilSubtotalIs(string expected) => Driver.WaitForValueToBe
        (By.CssSelector("#cart-subtotal-products .label.js-subtotal"), expected);

    public List<BasketItem> GetProductsListFromBasket()
    {
        var actualBasketItems = new List<BasketItem>();

        foreach (var row in BasketItems)
        {
            string name = row.FindElement(By.CssSelector("a.label")).Text;
            int quantity = int.Parse(row.FindElement(By.CssSelector("input.js-cart-line-product-quantity")).GetDomAttribute("value"));
            decimal price = row.FindElement(By.CssSelector("div.current-price span.price")).GetPrice();
            actualBasketItems.Add(new BasketItem(name, quantity, price));
        }
        return actualBasketItems;
    }

    public Basket GetBasket()
    {
        return new Basket(GetProductsListFromBasket(), Subtotal);
    }
}