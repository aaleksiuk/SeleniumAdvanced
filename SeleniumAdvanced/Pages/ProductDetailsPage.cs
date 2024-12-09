using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;

namespace SeleniumAdvanced.Pages;

public class ProductDetailsPage(IWebDriver driver) : BasePage(driver)
{
    private IWebElement AddToBasketBtn => Driver.WaitAndFind(By.CssSelector(".add-to-cart"));
    private IWebElement UpQuantityBtn => Driver.WaitAndFind(By.CssSelector(".js-touchspin.bootstrap-touchspin-up"));
    public decimal ProductPrice => Driver.WaitAndFind(By.CssSelector(".current-price")).GetPrice();

    //modal
    public string ModalProductName => Driver.WaitAndFind(By.CssSelector(".product-name")).Text;
    public decimal ModalPrice => Driver.WaitAndFind(By.CssSelector(".product-price")).GetPrice();
    public int ModalQuantity => int.Parse(Driver.WaitAndFind(By.CssSelector(".product-quantity strong")).Text);
    public string ModalTotalItemsText => Driver.WaitAndFind(By.CssSelector(".cart-products-count")).Text;
    public decimal ModalSubtotal => Driver.WaitAndFind(By.CssSelector(".subtotal value")).GetPrice();

    private IWebElement ModalCloseBtn => Driver.WaitAndFind(By.CssSelector(".close"));


    public void IncreaseQuantity(int quantity)
    {
        var howManyTimesToClick = quantity - 1;
        while (howManyTimesToClick > 0)
        {
            UpQuantityBtn.Click();
            howManyTimesToClick--;
        }
    }
    public void ClickAddToBasketBtn() => Click(AddToBasketBtn);
    public void ClickCloseModalBtn() => Click(AddToBasketBtn);

    public decimal Subtotal(int quantity, decimal ProductPrice)
    {
        return quantity * ProductPrice;
    }
}
