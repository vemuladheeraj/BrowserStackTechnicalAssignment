
using AutomationFramework.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages
{
    public class MobilePage
    {
        private readonly IWebDriver _driver;
        private string _browser;

        public MobilePage(IWebDriver driver, string browser)
        {
            this._driver = driver;
            this._browser = browser;
        }

        public MobilePage SearchMobile()
        {
            try
            {
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.searchBar, 3);
                _driver.FindElement(MobilePageLocators.searchBar).SendKeys(TestBase.TestData["mobileName"] + Keys.Enter);
                return this;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SearchDetails> GetMobileDetails()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                Waits.TextToBePresentInElement(_driver, MobilePageLocators.mobileSearchResultsTitle, TestBase.TestData["mobileName"], 30);
                _driver.FindElement(MobilePageLocators.category).Click();
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.tags);
                List<IWebElement> allTags = _driver.FindElements(MobilePageLocators.tags).ToList();
                var brand = allTags.Where(x => x.Text.Equals(TestBase.TestData["brand"])).ToList()[0];
                brand.Click();
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);

                if (TestBase.TestData["flipkartAssured"] == "true")
                {
                    Thread.Sleep(1000);
                    allTags[1].Click();
                }
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);
                if (TestBase.TestData["sort"] == "H-L")
                {
                    InterceptExceptionCheck("High to Low", wait);
                }

                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                //Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);

                wait.Until(x => x.FindElement(MobilePageLocators.sortBy).Enabled);
                var allMobileDetails = _driver.FindElements(MobilePageLocators.allMobileDetails).ToList();
                List<SearchDetails> results = new List<SearchDetails>();
                int staleCount = -1;
                for (int i = 0; i < allMobileDetails.Count - 1; i++)
                {
                    try
                    {
                        if (staleCount >= 0)
                        {
                            i = staleCount;
                            staleCount = -1;
                        }
                        System.Diagnostics.Debug.WriteLine(i);
                        allMobileDetails = _driver.FindElements(MobilePageLocators.allMobileDetails).ToList();
                        IWebElement currentElement = allMobileDetails[i];
                        string mobileName = currentElement.FindElement(MobilePageLocators.mobileName).Text;
                        string price = currentElement.FindElement(MobilePageLocators.price).Text;
                        //string[] values =currentElement.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);                       

                        SearchDetails searchDetails = new SearchDetails();

                        //searchDetails.Name = values[0].ToLower().Contains("add to compare") ? values[1] : values[2];
                        searchDetails.Name = mobileName;
                        searchDetails.Price = price;
                        searchDetails.Link = currentElement.GetAttribute("href").ToString();
                        //searchDetails.Price = values.Where(x => x.StartsWith("₹")).ToList()[0];
                        results.Add(searchDetails);
                    }
                    catch (StaleElementReferenceException)
                    {
                        _driver.Navigate().Refresh();
                        staleCount = i;
                        continue;
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void InterceptExceptionCheck(string value, WebDriverWait wait)
        {
            try
            {
                _driver.FindElements(MobilePageLocators.sortBy).Where(x => x.Text.Contains("High to Low")).ToList()[0].Click();
            }
            catch (ElementClickInterceptedException)
            {
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);
                _driver.FindElements(MobilePageLocators.sortBy).Where(x => x.Text.Contains("High to Low")).ToList()[0].Click();
            }

        }
    }
}