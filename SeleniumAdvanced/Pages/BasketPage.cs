using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Linq;

namespace SeleniumAdvanced.Pages;

public class BasketPage(IWebDriver driver) : BasePage(driver)
{
    public decimal Subtotal => Driver.WaitAndFind(By.CssSelector("#cart-subtotal-products .value")).GetPrice();
    public string SubtotalProducts => Driver.WaitAndFind(By.CssSelector("#cart-subtotal-products .label.js-subtotal")).Text;

    private IWebElement FirstProductDeleteButton =>
    Driver.WaitAndFindAll(By.CssSelector("i.material-icons.float-xs-left")).First();

    private IWebElement LastProductDeleteButton =>
    Driver.WaitAndFindAll(By.CssSelector("i.material-icons.float-xs-left")).Last();

    public void ClickFirstProductDeleteButton() => Click(FirstProductDeleteButton);
    public void ClickLastProductDeleteButton() => Click(LastProductDeleteButton);

    public bool HasChanged => Driver.WaitForValueChange(By.CssSelector("#cart-subtotal-products .label.js-subtotal"), SubtotalProducts);
}