using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using TGSAutoTest.Entities;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class UpdateGroupPage : Page
    {
        private IWebElement IdDropDown
        {
            get { return WebDriver.FindElementById("updateGroup"); }
        }
        private IWebElement GroupNameInput
        {
            get { return WebDriver.FindElementById("groupName"); }
        }
        private IWebElement GroupStartYearInput
        {
            get { return WebDriver.FindElementById("groupStartYear"); }
        }
        private IWebElement GroupEndYearInput
        {
            get { return WebDriver.FindElementById("groupEndYear"); }
        }
        private IWebElement GroupCountryInput
        {
            get { return WebDriver.FindElementById("groupCountry"); }
        }
        private IWebElement GroupCityInput
        {
            get { return WebDriver.FindElementById("groupCity"); }
        }
        private IWebElement GroupUrlWikiInput
        {
            get { return WebDriver.FindElementById("groupUrlWiki"); }
        }
        private IWebElement GroupDescriptionInput
        {
            get { return WebDriver.FindElementById("groupDescription"); }
        }
        private IWebElement ErrorDiv
        {
            get { return WebDriver.FindElementById("textCreationGroup"); }
        }
        private IWebElement ContinueButton
        {
            get { return WebDriver.FindElementByCssSelector("button.btn-primary.btn-lg.btn-block"); }
        }

        public UpdateGroupPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public UpdateGroupPage SelectId(int groupId)
        {
            new SelectElement(IdDropDown).SelectByText(groupId.ToString());
            return this;
        }

        public bool IsError()
        {
            bool isShow = false;
            if (ErrorDiv.Text == "Error")
            {
                isShow = true;
            }
            return isShow;
        }

        public UpdateGroupPage UpdateGroup(Group group)
        {
            Thread.Sleep(1000);
            GroupNameInput.Clear();
            GroupNameInput.SendKeys(group.Name);
            Thread.Sleep(1000);
            GroupStartYearInput.Clear();
            GroupStartYearInput.SendKeys(group.StartYear.ToString());
            Thread.Sleep(1000);
            GroupEndYearInput.Clear();
            GroupEndYearInput.SendKeys(group.EndYear.ToString());
            Thread.Sleep(1000);
            GroupCountryInput.Clear();
            GroupCountryInput.SendKeys(group.Country);
            Thread.Sleep(1000);
            GroupCityInput.Clear();
            GroupCityInput.SendKeys(group.City);
            Thread.Sleep(1000);
            GroupUrlWikiInput.Clear();
            GroupUrlWikiInput.SendKeys(group.URLWiki);
            Thread.Sleep(1000);
            GroupDescriptionInput.Clear();
            GroupDescriptionInput.SendKeys(group.Description);

            return this;
        }

        public UpdateGroupPage Continue()
        {
            ContinueButton.Click();
            return this;
        }
    }
}
