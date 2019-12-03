using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGSAutoTest.SetUp
{
    public interface ISetUpWebDriver
    {
        RemoteWebDriver GetSetUpWebDriver();
        void CloseWebDriver();
        //string MakeScreenshot(string test);
    }
}
