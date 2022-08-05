using OpenQA.Selenium;

namespace Framework.Locators
{
    public class MobilePageLocators
    {
        public static By searchBar => By.Name("q");
        public static By mobileSearchResultsTitle => By.ClassName("_10Ermr");
        public static By allMobileDetails => By.XPath("//a[contains(@rel,'noopener noreferrer')]");
        public static By mobileCost => By.XPath("/div[2]/div[2]/div");
        public static By tags => By.ClassName("_3879cV");
        public static By sortBy => By.ClassName("_10UF8M");
        public static By category => By.XPath("//a[contains(@class,'_1jJQdf _2Mji8F')]");
        public static By mobileName => By.CssSelector("div:nth-child(2)>div>div");
        public static By price => By.CssSelector("div:nth-child(2)>div:nth-child(2)>div>div>div");
    }
}