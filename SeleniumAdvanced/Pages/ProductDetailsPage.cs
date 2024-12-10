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
    public decimal ModalSubtotal => Driver.WaitAndFind(By.CssSelector(".subtotal.value")).GetPrice();

    private IWebElement ModalCloseBtn => Driver.WaitAndFind(By.CssSelector(".close"));
    private IWebElement ModalContinueBtn => Driver.WaitAndFind(By.CssSelector(".btn-secondary"));
    private IWebElement ModalProceedToCheckoutBtn => Driver.WaitAndFind(By.CssSelector(".btn-primary"));
    public void IncreaseQuantity(int quantity)
    {
        for (var i = 1; i < quantity; i++)
        {
            UpQuantityBtn.Click();
        }
    }
    public void ClickAddToBasketBtn() => Click(AddToBasketBtn);
    public void ClickCloseModalBtn() => Click(ModalCloseBtn);
    public void ClickContinueModalBtn() => Click(ModalContinueBtn);
    public void ClickProceedToCheckoutModalBtn() => Click(ModalProceedToCheckoutBtn);

    public decimal CalculateSubtotal(int quantity, decimal ProductPrice) => quantity * ProductPrice;
}
