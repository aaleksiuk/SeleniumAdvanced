using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using System.Collections.Generic;

namespace SeleniumAdvanced.Pages
{
    public class CreateAccountPage(IWebDriver driver)
    {
        private IList<IWebElement> SocialTitle => driver.WaitAndFindAll(By.CssSelector("input[name='id_gender']"));
        private IWebElement FirstName => driver.WaitAndFind(By.CssSelector("[name='firstname']"));
        private IWebElement LastName => driver.WaitAndFind(By.CssSelector("[name='lastname']"));
        private IWebElement Email => driver.WaitAndFind(By.CssSelector("[name='email']"));
        private IWebElement Password => driver.WaitAndFind(By.CssSelector("[name='password']"));
        private IWebElement Birthdate => driver.WaitAndFind(By.CssSelector("[name='birthday']"));
        private IWebElement ReceiveOffers => driver.WaitAndFind(By.XPath("//label[input[@name='optin']]"));
        private IWebElement DataPrivacy => driver.WaitAndFind(By.XPath("//label[input[@name='customer_privacy']]"));
        private IWebElement Newsletter => driver.WaitAndFind(By.XPath("//label[input[@name='newsletter']]"));
        private IWebElement TermsAndConditions => driver.WaitAndFind(By.XPath("//label[input[@name='psgdpr']]"));
        private IWebElement Submit => driver.WaitAndFind(By.CssSelector("button[data-link-action='save-customer']"));
        private IWebElement SuccessMessage => driver.WaitAndFind(By.Id("validator-message"));

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
        public void SetFirstName(string firstName)
        {
            FirstName.SendKeys(firstName);
        }
        public void SetLastName(string lastName)
        {
            LastName.SendKeys(lastName);
        }
        public void SetEmail(string email)
        {
            Email.SendKeys(email);
        }
        public void SetPassword(string password)
        {
            Password.SendKeys(password);
        }
        public void SetBirthdate(string birthdate)
        {
            Birthdate.SendKeys(birthdate);
        }

        public void SetReceiveOffers(bool receiveOffers)
        {
            if (receiveOffers)
            {
                ReceiveOffers.Click();
            }
        }
        public void SetDataPrivacy(bool dataPrivacy)
        {
            if (dataPrivacy)
            {
                DataPrivacy.Click();
            }
        }
        public void SetNewsletter(bool newsletter)
        {
            if (newsletter)
            {
                Newsletter.Click();
            }
        }
        public void SetTermsAndConditions(bool termsAndConditions)
        {
            if (termsAndConditions)
            {
                TermsAndConditions.Click();
            }
        }
        public void SetSubmit()
        {
            Submit.Click();
        }

    }
}
