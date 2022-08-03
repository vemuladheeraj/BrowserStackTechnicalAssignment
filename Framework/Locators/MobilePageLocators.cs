using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Locators
{
    public class MobilePageLocators
    {
        public static By searchBar => By.Name("q");
        public static By mobileSearchResultsTitle => By.ClassName("_10Ermr");
        public static By allMobileDetails => By.XPath("/a[contains(@rel,'noopener noreferrer')]");
        public static By mobileName => By.XPath("/div[2]/div/div");
        public static By mobileCost => By.XPath("/div[2]/div[2]/div");
        public static By tags => By.ClassName("_3879cV");
        public static By sortBy => By.ClassName("_10UF8M");
        public static By category => By.ClassName("_1jJQdf _2Mji8F");
        
    }
}
