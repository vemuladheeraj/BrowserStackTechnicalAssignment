using BrowserStack;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class TestBase
    {

        protected IWebDriver driver;

        protected string environment;
        private Local browserStackLocal;

        public static Dictionary<string, string> ConfigurationValue { get; private set; }
        public static Dictionary<string, string> TestData { get; private set; }
        public TestBase(string environment)
        {
            this.environment = environment;
        }

        static DriverOptions getBrowserOption(String browser)
        {
            switch (browser)
            {
                case "chrome":
                    return new ChromeOptions();
                case "firefox":
                    return new FirefoxOptions();
                case "safari":
                    return new SafariOptions();
                case "edge":
                    return new EdgeOptions();
                default:
                    return new ChromeOptions();
            }
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
            ConfigurationValue=configurationValue;

            Dictionary<string, string> testData = new Dictionary<string, string>();
            NameValueCollection testdata = ConfigurationManager.GetSection("testData/" + "data") as NameValueCollection;

            foreach (var key in testdata.AllKeys)
            {
                testData.Add(key, testdata[key]);
            }
            TestData= testData;
        }


        [SetUp]
        public void Init()
        {
            NameValueCollection caps =  ConfigurationManager.GetSection("capabilities/" + "parallel") as NameValueCollection;
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;
            DriverOptions capability = getBrowserOption(settings["browser"]);

            capability.BrowserVersion = "latest";
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();

            foreach (string key in caps.AllKeys)
            {
                browserstackOptions.Add(key, caps[key]);
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            browserstackOptions.Add("userName", username);
            browserstackOptions.Add("accessKey", accesskey);

            if (caps.Get("local").ToString() == "true")
            {
                browserStackLocal = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("key", accesskey)
                };
                browserStackLocal.start(bsLocalArgs);
            }
            if (ConfigurationValue["localMachine"] != "true")
            {
                capability.AddAdditionalOption("bstack:options", browserstackOptions);
                driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"),capability);
            }

            if (ConfigurationValue["localMachine"] == "true")
            {
                var chromeOptions = new ChromeOptions();
                driver = new ChromeDriver(chromeOptions);
            }

            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
            }
        }
    }
}
