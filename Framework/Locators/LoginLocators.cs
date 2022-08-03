using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Locators
{
    public static class LoginLocators
    {
        public static By closeLoginPopUp => By.XPath("//button[contains(@class,'_2KpZ6l _2doB4z')]");
    }
}
