using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Framework
{
    [TestFixture]
    public class TestBase
    {
        protected IWebDriver driver;

        protected string browser;
        protected string os;
        protected string osversion;

        public static Dictionary<string, string> ConfigurationValue { get; private set; }
        public static Dictionary<string, string> TestData { get; private set; }

        public TestBase(string browser, string os, string osversion)
        {
            this.browser = browser;
            this.os = os;
            this.osversion = osversion;
        }

        [OneTimeSetUp]
        public void ConfigurationValuesAndTestData()
        {
            NameValueCollection configValues = ConfigurationManager.AppSettings;
            Dictionary<string, string> configurationValue = new Dictionary<string, string>();
            foreach (var key in configValues.AllKeys)
            {
                configurationValue.Add(key, configValues[key]);
            }
            ConfigurationValue = configurationValue;

            Dictionary<string, string> testData = new Dictionary<string, string>();
            NameValueCollection testdata = ConfigurationManager.GetSection("testData/" + "data") as NameValueCollection;

            foreach (var key in testdata.AllKeys)
            {
                testData.Add(key, testdata[key]);
            }
            TestData = testData;
        }

        [SetUp]
        public void Init()
        {
            String BROWSERSTACK_USERNAME = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"))? ConfigurationManager.AppSettings["user"]: Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            String BROWSERSTACK_ACCESS_KEY = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY")) ? ConfigurationManager.AppSettings["key"] : Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            switch (browser)
            {
                case "Safari": //If browser is Safari, following capabilities will be passed to 'executetestwithcaps' function
                    SafariOptions safariOptions = new SafariOptions();
                    safariOptions.BrowserVersion = "latest";
                    Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
                    browserstackOptions.Add("os", os);
                    browserstackOptions.Add("osVersion", osversion);
                    browserstackOptions.Add("local", "false");
                    browserstackOptions.Add("userName", BROWSERSTACK_USERNAME);
                    browserstackOptions.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
                    browserstackOptions.Add("debug", true);
                    browserstackOptions.Add("buildName", ConfigurationManager.AppSettings.Get("browserStackBuildName"));
                    safariOptions.AddAdditionalOption("bstack:options", browserstackOptions);
                    safariOptions.AddAdditionalOption("browserstack.consoleLogs", "info");
                    driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), safariOptions);
                    break;

                case "Chrome": //If browser is Chrome, following capabilities will be passed to 'executetestwithcaps' function
                    if (ConfigurationManager.AppSettings["TestingInLocalMachine"] != "true")
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.BrowserVersion = "latest";
                        Dictionary<string, object> browserstackOptionsChrome = new Dictionary<string, object>();
                        browserstackOptionsChrome.Add("os", os);
                        browserstackOptionsChrome.Add("osVersion", osversion);
                        browserstackOptionsChrome.Add("local", "false");
                        //browserstackOptionsChrome.Add("seleniumVersion", "3.14.0");
                        browserstackOptionsChrome.Add("userName", BROWSERSTACK_USERNAME);
                        browserstackOptionsChrome.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
                        browserstackOptionsChrome.Add("debug", true);
                        browserstackOptionsChrome.Add("buildName", ConfigurationManager.AppSettings.Get("browserStackBuildName"));
                        chromeOptions.AddAdditionalOption("bstack:options", browserstackOptionsChrome);
                        chromeOptions.AddAdditionalOption("browserstack.consoleLogs", "info");
                        driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), chromeOptions);
                    }
                    else
                    {
                        driver = new ChromeDriver();
                    }
                    break;

                case "Firefox": //If browser is Firefox, following capabilities will be passed to 'executetestwithcaps' function
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.BrowserVersion = "latest";
                    Dictionary<string, object> browserstackOptionsFirefox = new Dictionary<string, object>();
                    browserstackOptionsFirefox.Add("os", os);
                    browserstackOptionsFirefox.Add("osVersion", osversion);
                    browserstackOptionsFirefox.Add("local", "false");
                    //browserstackOptionsFirefox.Add("seleniumVersion", "3.10.0");
                    browserstackOptionsFirefox.Add("userName", BROWSERSTACK_USERNAME);
                    browserstackOptionsFirefox.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
                    browserstackOptionsFirefox.Add("debug", true);
                    browserstackOptionsFirefox.Add("buildName", ConfigurationManager.AppSettings.Get("browserStackBuildName"));
                    firefoxOptions.AddAdditionalOption("bstack:options", browserstackOptionsFirefox);

                    driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), firefoxOptions);
                    break;

                case "Edge": //If browser is Edge, following capabilities will be passed to 'executetestwithcaps' function
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.BrowserVersion = "latest";
                    Dictionary<string, object> browserstackOptionsEdge = new Dictionary<string, object>();
                    browserstackOptionsEdge.Add("os", os);
                    browserstackOptionsEdge.Add("osVersion", osversion);
                    browserstackOptionsEdge.Add("local", "false");
                    //browserstackOptionsEdge.Add("seleniumVersion", "3.5.2");
                    browserstackOptionsEdge.Add("userName", BROWSERSTACK_USERNAME);
                    browserstackOptionsEdge.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
                    browserstackOptionsEdge.Add("debug", true);
                    browserstackOptionsEdge.Add("buildName", ConfigurationManager.AppSettings.Get("browserStackBuildName"));

                    edgeOptions.AddAdditionalOption("bstack:options", browserstackOptionsEdge);

                    driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), edgeOptions);
                    break;

                default:
                    ChromeOptions chromeOptions1 = new ChromeOptions();
                    chromeOptions1.BrowserVersion = "latest";
                    Dictionary<string, object> browserstackOptionsDefault = new Dictionary<string, object>();
                    browserstackOptionsDefault.Add("os", os);
                    browserstackOptionsDefault.Add("osVersion", osversion);
                    browserstackOptionsDefault.Add("local", "false");
                    //browserstackOptionsDefault.Add("seleniumVersion", "3.14.0");
                    browserstackOptionsDefault.Add("userName", BROWSERSTACK_USERNAME);
                    browserstackOptionsDefault.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
                    browserstackOptionsDefault.Add("debug", true);
                    browserstackOptionsDefault.Add("buildName", ConfigurationManager.AppSettings.Get("browserStackBuildName"));
                    chromeOptions1.AddAdditionalOption("bstack:options", browserstackOptionsDefault);

                    driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), chromeOptions1);
                    break;
            }

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
}