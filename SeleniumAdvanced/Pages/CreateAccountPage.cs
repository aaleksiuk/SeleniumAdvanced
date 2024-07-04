using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using SeleniumAdvanced.Helpers;
using System;
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

        public void SetSocialTitle(PersonGenerator.Gender gender)
        {
            switch (gender)
            {
                case PersonGenerator.Gender.Male:
                    SocialTitle[0].Click();
                    break;
                case PersonGenerator.Gender.Female:
                    SocialTitle[1].Click();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender),gender, null);
            }
        }
        public void SetFirstName(string firstName) => SendKeys(FirstName, firstName);
        public void SetLastName(string lastName) => SendKeys(LastName, lastName);
        public void SetEmail(string email) => SendKeys(Email, email);
        public void SetPassword(string password) => SendKeys(Password, password);
        public void SetBirthdate(string birthdate) => SendKeys(Birthdate, birthdate);

        public void ClickReceiveOffers() => Click(ReceiveOffers);
        public void ClickDataPrivacy() => Click(DataPrivacy);
        public void ClickNewsletter() => Click(Newsletter);
        public void ClickTermsAndConditions() => Click(TermsAndConditions);
        public void SetSubmit() => Click(Submit);
    }
}