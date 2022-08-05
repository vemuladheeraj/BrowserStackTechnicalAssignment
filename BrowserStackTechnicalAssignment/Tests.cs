using Framework;
using Framework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BrowserStackTechnicalAssignment
{
    [TestFixture("Chrome", "OS X", "Big Sur")]
    [TestFixture("Firefox", "Windows", "10")]
    [TestFixture("Safari", "OS X", "Big Sur")]
    [TestFixture("Chrome", "Windows", "11")]
    [TestFixture("Edge", "Windows", "11")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Tests : TestBase
    {
        public Tests(string browser, string os, string osversion) : base(browser, os, osversion)
        {
        }

        [Test]
        public void PrintProductDetails()
        {
            try
            {
                LoginPage login = new LoginPage(driver);
                login.NavigateToWebSite().CloseLoginPopUp();
                MobilePage mobile = new MobilePage(driver, browser);
                List<SearchDetails> searchresults = mobile.SearchMobile().GetMobileDetails();

                foreach (var item in searchresults)
                {
                    string itemString = "Product name:" + item.Name + "|| Product Price: " + item.Price + "|| Product Link:" + item.Link;
                    if (ConfigurationManager.AppSettings["TestingInLocalMachine"] != "true")
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"annotate\", \"arguments\": {\"data\":\"" + itemString + "\", \"level\": \"info\"}}");
                    }

                    Console.WriteLine(itemString);
                }

                Assert.IsTrue(searchresults.Count >= 1, $"The search results contains less than 1 record {searchresults.Count}");
                if (ConfigurationManager.AppSettings["TestingInLocalMachine"] != "true")
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Product details printed\"}}");
                }
            }
            catch (Exception ex)
            {
                if (ConfigurationManager.AppSettings["TestingInLocalMachine"] != "true")
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + ex.Message + "\"}}");
                }
                throw;
            }
        }

        private static object[] FixtureArgs = {
        new object[] { "Chrome","OS X","snow Leopard"},
        new object[] { "FireFox", "Windows","10" },
         new object[] { "Safari", "OS X", "snow Leopard" },
          new object[] { "Chrome", "Windows","10" },
           new object[] { "Chrome", "Windows","11" }
    };
    }
}