using Framework.Locators;
using OpenQA.Selenium;
using System.Configuration;

namespace Framework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public LoginPage NavigateToWebSite()
        {
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["appURL"]);

            return this;
        }

        public LoginPage LoginWithCredentials()
        {
            return this;
        }

        public LoginPage CloseLoginPopUp()
        {
            _driver.FindElement(LoginLocators.closeLoginPopUp).Click();
            return this;
        }
    }
}