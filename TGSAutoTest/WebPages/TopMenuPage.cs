using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class TopMenuPage : Page
    {
        private IWebElement UserNameSpan
        {
            get { return WebDriver.FindElementById("userName"); }
        }

        public TopMenuPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public bool CheckTopUser(string userName)
        {
            bool isOk = true;
            if (userName != UserNameSpan.Text)
            {
                isOk = false;
            }
            return isOk;
        }
    }
}
