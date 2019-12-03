using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class AlbumListPage : Page
    {
        private IList<IWebElement> TableTR
        {
            get { return WebDriver.FindElementsByCssSelector("tbody#albumList tr"); }
        }
        private IWebElement DeleteTRButton(int id)
        {
            return WebDriver.FindElementByCssSelector("tr#album" + id + " td:last-child button");
        }
        private IWebElement NextAlbumButton
        {
            get { return WebDriver.FindElementById("nextAlbumButton"); }
        }
        private IWebElement AlbumTR(int id)
        {
            return WebDriver.FindElementById("album" + id);
        }
        public AlbumListPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public AlbumListPage SureDelete()
        {
            WebDriver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
            WebDriver.SwitchTo().Alert().Accept();
            return this;
        }

        public AlbumListPage DeleteAlbum(int albumId)
        {
            IList<IWebElement> table = WebDriver.FindElementsByCssSelector("tbody#albumList tr");
            int i = 0;
            bool isDeleted = false;
            do
            {
                if (table.Count == i)
                {
                    Console.WriteLine("Entra en el while: {0}", i++);
                    NextAlbumButton.Click();
                    i = 0;
                    Thread.Sleep(1000);
                    table = WebDriver.FindElementsByCssSelector("tbody#albumList tr");
                }
                if (table[i].GetAttribute("Id") == ("album" + albumId))
                {
                    Console.WriteLine("Borrar album: {0}", albumId);
                    DeleteTRButton(albumId).Click();
                    SureDelete();
                    isDeleted = true;
                }
                i++;
            } while (isDeleted == false);
            return this;
        }

        public bool IsDeleted(int albumId)
        {
            Thread.Sleep(1000);
            return IsElementDisplayed(AlbumTR(albumId));
        }
    }
}
