using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace TGSAutoTest.Utils
{
    class CustomExpectedConditions
    {
        /// <summary>
        /// An expectation for checking an element is hidden
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>True if it is hidden, false otherwise.</returns>
        public static Func<IWebDriver, bool> ElementIsHidden(IWebElement element)
        {
            return (driver) =>
            {
                    return !element.Displayed;   
            };
        }

        /// <summary>
        /// Function to allow javascript to run suggest/autocomplete features
        /// </summary>
        public static Func<IWebDriver, bool> ElementIsSuggested(By by, string name)
        {
            return (driver) =>
            {
                try
                {
                    Console.WriteLine(driver.FindElement(by).Text);
                    Console.WriteLine(driver.FindElement(by).Text.Contains(name));
                    return driver.FindElement(by).Text.Contains(name);
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("element not found");
                    return false;
                }  
            };
        }

        /// <summary>
        /// An expectation for checking an element is visible 
        /// </summary>
        /// <param name="locator">The element identifier.</param>
        /// <returns>The <see cref="IWebElement"/> once it is visible.</returns>
        public static Func<IWebDriver, IWebElement> ElementIsVisible(By locator)
        {
            return (webDriver) =>
            {
                try
                {
                    Console.WriteLine("element visible");
                    return ElementIfVisible(webDriver.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("element not found");
                    return null;
                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        Thread.Sleep(100);
                        new WebDriverWait(webDriver, TimeSpan.FromSeconds(Convert.ToInt16(TestContext.Parameters["waitTimeout"])));
                        return ElementIfVisible(webDriver.FindElement(locator));
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("element not found");
                        return null;
                    }
                }
            };
        }

        /// <summary>
        /// An expectation for checking that an element is either invisible or not present on the DOM.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns><see langword="true"/> if the element is not displayed; otherwise, <see langword="false"/>.</returns>

        public static Func<IWebDriver, bool> InvisibilityOfElementLocated(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element is not present in DOM. The
                    // try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element
                    // is no longer visible.
                    return true;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement webElement)
        {
            return webElement.Displayed ? webElement : null;
        }
        public static Match RegExSymbolCurrency(IWebElement GrandTotal)
        {
            Regex regex = new Regex(@"^(.)");
            Match symbolCurrency = regex.Match(GrandTotal.Text);
            return symbolCurrency;
        }

        public static string RegExDayChange(IWebElement DayChanged)
        {
            Regex regex = new Regex(@"^[0-9]{1,2}");
            Match day = regex.Match(DayChanged.Text);
            return day.Value.ToString();
        }

    }
}
