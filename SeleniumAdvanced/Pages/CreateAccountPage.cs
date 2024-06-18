using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Collections.Generic;

namespace SeleniumAdvanced.Pages
{
    public class CreateAccountPage(IWebDriver driver) : BasePage(driver)
    {
        private IList<IWebElement> SocialTitle => Driver.WaitAndFindAll(By.CssSelector("input[name='id_gender']"), DefaultWait);
        private IWebElement FirstName => Driver.WaitAndFind(By.CssSelector("[name='firstname']"), DefaultWait);
        private IWebElement LastName => Driver.WaitAndFind(By.CssSelector("[name='lastname']"), DefaultWait);
        private IWebElement Email => Driver.WaitAndFind(By.CssSelector("[name='email']"), DefaultWait);
        private IWebElement Password => Driver.WaitAndFind(By.CssSelector("[name='password']"), DefaultWait);
        private IWebElement Birthdate => Driver.WaitAndFind(By.CssSelector("[name='birthday']"), DefaultWait);
        private IWebElement ReceiveOffers => Driver.WaitAndFind(By.XPath("//label[input[@name='optin']]"), DefaultWait);
        private IWebElement DataPrivacy => Driver.WaitAndFind(By.XPath("//label[input[@name='customer_privacy']]"), DefaultWait);
        private IWebElement Newsletter => Driver.WaitAndFind(By.XPath("//label[input[@name='newsletter']]"), DefaultWait);
        private IWebElement TermsAndConditions => Driver.WaitAndFind(By.XPath("//label[input[@name='psgdpr']]"), DefaultWait);
        private IWebElement Submit => Driver.WaitAndFind(By.CssSelector("button[data-link-action='save-customer']"), DefaultWait);
        private IWebElement SuccessMessage => Driver.WaitAndFind(By.Id("validator-message"), DefaultWait);

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
        public void SetFirstName(string firstName) => SendKeys(FirstName, firstName, false);
        public void SetLastName(string lastName) => SendKeys(LastName, lastName, false);
        public void SetEmail(string email) => SendKeys(Email, email, false);
        public void SetPassword(string password) => SendKeys(Password, password, false);
        public void SetBirthdate(string birthdate) => SendKeys(Birthdate, birthdate, false);

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