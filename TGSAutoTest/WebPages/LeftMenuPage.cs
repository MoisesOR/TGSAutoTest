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
    public class LeftMenuPage : Page
    {
        public enum MenusType
        {
            Album,
            Group
        }

        public enum SubSectionMenu
        {
            List,
            Create,
            Update
        }

        private IWebElement AlbumSection
        {
            get { return WebDriver.FindElementById("albumMenu1"); }
        }
        private IWebElement AlbumListSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='albumList']"); }
        }
        private IWebElement AlbumCreateSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='createAlbum']"); }
        }
        private IWebElement AlbumUpdateSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='updateAlbum']"); }
        }
        private IWebElement GroupSection
        {
            get { return WebDriver.FindElementById("albumMenu2"); }
        }
        private IWebElement GroupListSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='groupList']"); }
        }
        private IWebElement GroupCreateSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='createGroup']"); }
        }
        private IWebElement GroupUpdateSection
        {
            get { return WebDriver.FindElementByCssSelector("a[routerlink*='updateGroup']"); }
        }

        public LeftMenuPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public LeftMenuPage GoToSelector(MenusType menusType, SubSectionMenu subSectionMenu)
        {
            if (menusType == MenusType.Album)
            {
                AlbumSection.Click();
                Thread.Sleep(500);
                if (subSectionMenu == SubSectionMenu.List)
                {
                    AlbumListSection.Click();
                }
                else if (subSectionMenu == SubSectionMenu.Create)
                {
                    AlbumCreateSection.Click();
                }
                else if (subSectionMenu == SubSectionMenu.Update)
                {
                    AlbumUpdateSection.Click();
                }
            }
            else
            {
                GroupSection.Click();
                Thread.Sleep(500);
                if (subSectionMenu == SubSectionMenu.List)
                {
                    GroupListSection.Click();
                }
                else if (subSectionMenu == SubSectionMenu.Create)
                {
                    GroupCreateSection.Click();
                }
                else if (subSectionMenu == SubSectionMenu.Update)
                {
                    GroupUpdateSection.Click();
                }
            }
            return this;
        }
    }
}
