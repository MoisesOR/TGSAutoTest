using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class GroupListPage : Page
    {
        private IList<IWebElement> TableTR
        {
            get { return WebDriver.FindElementsByCssSelector("tbody#groupList tr"); }
        }
        private IWebElement DeleteTRButton(int id)
        {
            return WebDriver.FindElementByCssSelector("tr#group" + id + " td:last-child button");
        }
        private IWebElement NextGroupButton
        {
            get { return WebDriver.FindElementById("nextGroupButton"); }
        }
        private IWebElement GroupTR(int id)
        {
            return WebDriver.FindElementById("group" + id);
        }

        public GroupListPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public GroupListPage SureDelete()
        {
            WebDriver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
            WebDriver.SwitchTo().Alert().Accept();
            return this;
        }

        public GroupListPage DeleteGroup(int groupId)
        {
            IList<IWebElement> table = WebDriver.FindElementsByCssSelector("tbody#groupList tr");
            int i = 0;
            bool isDeleted = false;
            do
            {
                if (table.Count == i)
                {
                    Console.WriteLine("Entra en el while: {0}", i++);
                    NextGroupButton.Click();
                    i = 0;
                    Thread.Sleep(1000);
                    table = WebDriver.FindElementsByCssSelector("tbody#groupList tr");
                }
                if (table[i].GetAttribute("Id") == ("group" + groupId))
                {
                    Console.WriteLine("Borrar group: {0}", groupId);
                    DeleteTRButton(groupId).Click();
                    SureDelete();
                    isDeleted = true;
                }
                i++;
            } while (isDeleted == false);
            return this;
        }

        public bool IsDeleted(int groupId)
        {
            Thread.Sleep(1000);
            return IsElementDisplayed(GroupTR(groupId));
        }
    }
}
