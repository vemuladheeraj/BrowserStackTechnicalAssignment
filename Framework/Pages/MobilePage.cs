using Framework.Locators;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Pages
{
    public class MobilePage
    {
        private readonly IWebDriver _driver;

        public MobilePage(IWebDriver driver)
        {
            this._driver = driver;

        }
        public List<IWebElement> SearchMobile()
        {
            
            Waits.WaitTillElementClickable(_driver,MobilePageLocators.searchBar,3);
            _driver.FindElement(MobilePageLocators.searchBar).SendKeys(TestBase.TestData["mobileName"]);
            var searchFeed = _driver.FindElement(MobilePageLocators.searchBar).FindElements(By.XPath("/../../../ul")).ToList();
            _driver.FindElement(MobilePageLocators.searchBar).SendKeys(Keys.Enter);
            return searchFeed;
        }

        public List<SearchDetails> GetMobileDetails()
        {
            Waits.TextToBePresentInElement(_driver, MobilePageLocators.mobileSearchResultsTitle, TestBase.TestData["mobileName"], 30);
            _driver.FindElement(MobilePageLocators.category).Click();
            Waits.WaitTillElementClickable(_driver, MobilePageLocators.tags);
            List<IWebElement> allTags = _driver.FindElements(MobilePageLocators.tags).ToList();
            allTags.Where(x => x.Text.Equals(TestBase.TestData["brand"])).ToList()[0].Click();
            Waits.WaitTillElementClickable(_driver, MobilePageLocators.tags);
            if (TestBase.TestData["flipkartAssured"]=="true")
            {
                allTags.Where(x => x.Text.Equals(TestBase.TestData["flipkartAssured"])).ToList()[0].Click();
            }
            Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);
            if (TestBase.TestData["sort"] == "H-L")
            {
                _driver.FindElements(MobilePageLocators.sortBy).Where(x => x.Text.Contains("High to Low")).ToList()[0].Click();
            }

            var allMobileDetails = _driver.FindElements(MobilePageLocators.allMobileDetails).ToList();
            List<SearchDetails> results = new List<SearchDetails>();  
            foreach (var item in allMobileDetails)
            {
                SearchDetails searchDetails =new SearchDetails();
                searchDetails.Name = item.FindElement(MobilePageLocators.mobileName).Text;
                searchDetails.Link = item.GetAttribute("href").ToString();
                searchDetails.Price = item.FindElement(MobilePageLocators.mobileCost).Text;
                results.Add(searchDetails);
            }
            return results;
        }


    }
}
