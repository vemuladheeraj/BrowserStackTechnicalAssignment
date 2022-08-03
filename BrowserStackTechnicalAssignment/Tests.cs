using Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserStackTechnicalAssignment
{
    [TestFixture("chrome")]
    public class Tests:TestBase
    {
        public Tests(string environment) : base(environment) 
        { }

        [Test]
        public void SearchBstackDemo()
        {
            driver.Navigate().GoToUrl("https://www.flipkart.com/");
          
        }

    }
}
