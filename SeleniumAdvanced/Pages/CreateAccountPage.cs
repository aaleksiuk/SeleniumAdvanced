using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvanced.Extensions;
using SeleniumAdvanced.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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

        public CreateAccountPage SetSocialTitle(string gender)
        {
            if (gender.Equals("Mr."))
            {
                SocialTitle[0].Click();
            }
            else if (gender.Equals("Mrs."))
            {
                SocialTitle[1].Click();
            }
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetFirstName(string firstName)
        {
            FirstName.SendKeys(firstName);
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetLastName(string lastName)
        {
            LastName.SendKeys(lastName);
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetEmail(string email)
        {
            Email.SendKeys(email);
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetPassword(string password)
        {
            Password.SendKeys(password);
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetBirthdate(string birthdate)
        {
            Birthdate.SendKeys(birthdate);
            return new CreateAccountPage(driver);
        }

        public CreateAccountPage SetReceiveOffers(bool receiveOffers)
        {
            if (receiveOffers)
            {
                ReceiveOffers.Click();
            }
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetDataPrivacy(bool dataPrivacy)
        {
            if (dataPrivacy)
            {
                DataPrivacy.Click();
            }
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetNewsletter(bool newsletter)
        {
            if (newsletter)
            {
                Newsletter.Click();
            }
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetTermsAndConditions(bool termsAndConditions)
        {
            if (termsAndConditions)
            {
                TermsAndConditions.Click();
            }
            return new CreateAccountPage(driver);
        }
        public CreateAccountPage SetSubmit()
        {
            Submit.Click();
            return new CreateAccountPage(driver);
        }
        public string GetValidationMsg() => SuccessMessage.Text;
    }
}
