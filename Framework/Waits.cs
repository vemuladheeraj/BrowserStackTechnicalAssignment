using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework
{
    public class Waits
    {
        public static void WaitForpageLoad(IWebDriver driver, By locator, int timeOutSec = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutSec));
                // wait.Until(x=>x.((IJavascriptExecutor)wd).executeScript("return document.readyState").equals("complete"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void WaitTillElementVisible(IWebDriver driver, By locator, int timeOutSec = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutSec));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                InternalWaitForVisible(driver, locator, timeOutSec, wait);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void TextToBePresentInElement(IWebDriver driver, By locator, string searchText, int timeOutSec = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutSec));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NotFoundException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(ElementClickInterceptedException));
                var element = wait.Until(condition =>
                {
                    try
                    {
                        var elementTextTobePresent = driver.FindElement(locator);
                        return (elementTextTobePresent != null && elementTextTobePresent.Displayed && elementTextTobePresent.Text.Contains(searchText) ? true : false);
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void WaitTillElementClickable(IWebDriver driver, By locator, int timeOutSec = 30)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutSec));
                wait.PollingInterval = TimeSpan.FromSeconds(2);
                wait.IgnoreExceptionTypes(typeof(NotFoundException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(ElementClickInterceptedException));
                var element = wait.Until(condition =>
                {
                    try
                    {
                        var elementToBeClicked = driver.FindElement(locator);
                        return (elementToBeClicked != null && elementToBeClicked.Displayed && elementToBeClicked.Enabled ? true : false);
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void InternalWaitForVisible(IWebDriver driver, By locator, int timeOutSec, WebDriverWait wait)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutSec));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(locator);
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
    }
}