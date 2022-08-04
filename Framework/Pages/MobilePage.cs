using Framework.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public MobilePage SearchMobile()
        {

            try
            {
                Waits.WaitTillElementClickable(_driver, MobilePageLocators.searchBar, 3);
                _driver.FindElement(MobilePageLocators.searchBar).SendKeys(TestBase.TestData["mobileName"]+ Keys.Enter);
                //var searchFeed = _driver.FindElement(MobilePageLocators.searchBar).FindElements(By.XPath("/../../../ul")).ToList();
                //_driver.FindElement(MobilePageLocators.searchBar).SendKeys(Keys.Enter);
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
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
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
                    _driver.FindElements(MobilePageLocators.sortBy).Where(x => x.Text.Contains("High to Low")).ToList()[0].Click();
                }
                
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                //Waits.WaitTillElementClickable(_driver, MobilePageLocators.sortBy);
     
                wait.Until(x => x.FindElement(MobilePageLocators.sortBy).Enabled);
                var allMobileDetails = _driver.FindElements(MobilePageLocators.allMobileDetails).ToList();
                List<SearchDetails> results = new List<SearchDetails>();
                int staleCount = -1;
                for (int i = 0; i <= allMobileDetails.Count-2; i++)
                {
                    try
                    {
                        if (staleCount>=0)
                        {
                            i = staleCount;
                            staleCount = -1;
                        }
                        System.Diagnostics.Debug.WriteLine(i);
                        allMobileDetails = _driver.FindElements(MobilePageLocators.allMobileDetails).ToList();
                        var currentElement = allMobileDetails[i];
                        var values = currentElement.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                        SearchDetails searchDetails = new SearchDetails();

                        searchDetails.Name = values[0].ToLower().Contains("add to compare") ? values[1] : values[2];
                        searchDetails.Link = currentElement.GetAttribute("href").ToString();
                        searchDetails.Price = values.Where(x => x.Contains("₹")).ToList()[0];
                        results.Add(searchDetails);
                        Console.WriteLine(i);
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


    }
}
