using Framework;
using Framework.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserStackTechnicalAssignment
{
    [TestFixture("chrome")]    
    [Parallelizable(ParallelScope.Fixtures)]
    public class Tests : TestBase
    {
        public Tests(string environment) : base(environment)
        { }

        [Test]
        public void PrintProductDetails()
        {
            LoginPage login = new LoginPage(driver);
            login.NavigateToWebSite().CloseLoginPopUp();
            MobilePage mobile = new MobilePage(driver);            
            List<SearchDetails> searchresults = mobile.SearchMobile().GetMobileDetails();

            foreach (var item in searchresults)
            {
                System.Diagnostics.Debug.WriteLine($"Product name: {item.Name} || Product Link: {item.Link} || Product Price: {item.Price}");
            }

            Assert.IsTrue(searchresults.Count >= 1, $"The search results contains less than 1 record {searchresults.Count}");
        }

    }
}
