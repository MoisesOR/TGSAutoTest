using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using TGSAutoTest.Entities;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class CreateGroupPage : Page
    {
        private IWebElement NameInput
        {
            get { return WebDriver.FindElementById("groupNameInput"); }
        }
        private IWebElement StartYearInput
        {
            get { return WebDriver.FindElementById("groupStartYearInput"); }
        }
        private IWebElement EndYearInput
        {
            get { return WebDriver.FindElementById("groupEndYearInput"); }
        }
        private IWebElement CountryInput
        {
            get { return WebDriver.FindElementById("groupCountryInput"); }
        }
        private IWebElement CityInput
        {
            get { return WebDriver.FindElementById("groupCityInput"); }
        }
        private IWebElement UrlWikiInput
        {
            get { return WebDriver.FindElementById("groupUrlWikiInput"); }
        }
        private IWebElement DescriptionInput
        {
            get { return WebDriver.FindElementById("groupDescriptionInput"); }
        }
        private IWebElement CreateGroupButton
        {
            get { return WebDriver.FindElementByCssSelector("button.btn.btn-primary"); }
        }
        private IWebElement ErrorDiv
        {
            get { return WebDriver.FindElementById("textCreationGroup"); }
        }
        private IList<IWebElement> AllForm
        {
            get { return WebDriver.FindElementsByCssSelector(".form-control"); }
        }

        public CreateGroupPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public CreateGroupPage CrearGroup(Group group)
        {
            foreach (var input in AllForm)
            {
                input.Clear();
            }
            Thread.Sleep(1000);
            NameInput.SendKeys(group.Name);
            Thread.Sleep(1000);
            StartYearInput.SendKeys(group.StartYear.ToString());
            Thread.Sleep(1000);
            EndYearInput.SendKeys(group.EndYear.ToString());
            Thread.Sleep(1000);
            CountryInput.SendKeys(group.Country);
            Thread.Sleep(1000);
            CityInput.SendKeys(group.City);
            Thread.Sleep(1000);
            UrlWikiInput.SendKeys(group.URLWiki);
            Thread.Sleep(1000);
            DescriptionInput.SendKeys(group.Description);
            Thread.Sleep(1000);
            return this;
        }

        public CreateGroupPage Continue()
        {
            CreateGroupButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public bool CheckErrorDiv()
        {
            bool displayed = false;
            if (ErrorDiv.Text == "Error")
            {
                displayed = true;
            }
            return displayed;
        }
    }
}






