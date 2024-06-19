using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Collections.Generic;

namespace SeleniumAdvanced.Pages
{
    public class CreateAccountPage(IWebDriver driver) : BasePage(driver)
    {

        private IList<IWebElement> SocialTitle => Driver.WaitAndFindAll(By.CssSelector("input[name='id_gender']"));
        private IWebElement FirstName => Driver.WaitAndFind(By.CssSelector("[name='firstname']"));
        private IWebElement LastName => Driver.WaitAndFind(By.CssSelector("[name='lastname']"));
        private IWebElement Email => Driver.WaitAndFind(By.CssSelector("[name='email']"));
        private IWebElement Password => Driver.WaitAndFind(By.CssSelector("[name='password']"));
        private IWebElement Birthdate => Driver.WaitAndFind(By.CssSelector("[name='birthday']"));
        private IWebElement ReceiveOffers => Driver.WaitAndFind(By.XPath("//label[input[@name='optin']]"));
        private IWebElement DataPrivacy => Driver.WaitAndFind(By.XPath("//label[input[@name='customer_privacy']]"));
        private IWebElement Newsletter => Driver.WaitAndFind(By.XPath("//label[input[@name='newsletter']]"));
        private IWebElement TermsAndConditions => Driver.WaitAndFind(By.XPath("//label[input[@name='psgdpr']]"));
        private IWebElement Submit => Driver.WaitAndFind(By.CssSelector("button[data-link-action='save-customer']"));
        private IWebElement SuccessMessage => Driver.WaitAndFind(By.Id("validator-message"));

        public void SetSocialTitle(string gender)
        {
            if (gender.Equals("Mr."))
            {
                SocialTitle[0].Click();
            }
            else if (gender.Equals("Mrs."))
            {
                SocialTitle[1].Click();
            }
        }
        public void SetFirstName(string firstName) => SendKeys(FirstName, firstName);
        public void SetLastName(string lastName) => SendKeys(LastName, lastName);
        public void SetEmail(string email) => SendKeys(Email, email);
        public void SetPassword(string password) => SendKeys(Password, password);
        public void SetBirthdate(string birthdate) => SendKeys(Birthdate, birthdate);

        public void SetReceiveOffers(bool receiveOffers)
        {
            if (receiveOffers)
            {
                Click(ReceiveOffers);
            }
        }
        public void SetDataPrivacy(bool dataPrivacy)
        {
            if (dataPrivacy)
            {
                Click(DataPrivacy);
            }
        }
        public void SetNewsletter(bool newsletter)
        {
            if (newsletter)
            {
                Click(Newsletter);
            }
        }
        public void SetTermsAndConditions(bool termsAndConditions)
        {
            if (termsAndConditions)
            {
                Click(TermsAndConditions);
            }
        }
        public void SetSubmit() => Click(Submit);
    }
}