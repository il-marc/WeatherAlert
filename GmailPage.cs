using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAlert
{
    class GmailPage
    {
        private IWebDriver driver;

        private WebDriverWait wait;
        private string baseUrl = "https://mail.google.com/mail/u/0/#inbox";


        private IWebElement UsernameInput =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.Id("identifierId")
            ));
        private IWebElement PasswordInput =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//*[@id='password']//input")
            ));
        private IWebElement ComposeButton =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//div[contains(text(),'Написать') and @role=\"button\"]")
            ));
        private IWebElement ToInput =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//textarea[@name='to']")
            ));
        private IWebElement SubjectInput =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@name='subjectbox']")
            ));
        private IWebElement BodyInput =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//div[@aria-label='Тело письма' and @role='textbox']")
            ));
        private IWebElement SentNotification =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//span[text()='Письмо отправлено.']")
            ));
        private IWebElement InboxLink =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//a[@title='Входящие']")
            ));
        private IWebElement BodyContainer =>
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                By.XPath("//*[@data-message-id and @data-legacy-message-id]/div[2]/div[3]")
            ));
        public GmailPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        public void SighIn(string username, string password)
        {
            driver.Url = baseUrl;
            UsernameInput.SendKeys(username + Keys.Enter);
            PasswordInput.SendKeys(password + Keys.Enter);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("@gmail.com - Gmail"));
        }
        public void Send(string to, string subjext, string body)
        {
            driver.Url = baseUrl;
            ComposeButton.Click();
            ToInput.SendKeys(to);
            SubjectInput.SendKeys(subjext);
            BodyInput.SendKeys(body + Keys.Control + Keys.Enter);
            SentNotification.Click();
        }
        public string GetBodyBySubject(string subject)
        {
            driver.Url = baseUrl;
            if (!driver.Title.StartsWith("Входящие"))
            {
                InboxLink.Click();
            }
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
               By.XPath("//span[text()='" + subject + "' and position() = last()]"))).Click();
            return BodyContainer.Text;
        }
    }
}
