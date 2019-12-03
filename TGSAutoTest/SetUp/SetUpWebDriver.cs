using BrowserStack;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using TGSAutoTest.SetUp;

namespace TGSAutoTest.SetUp
{
    class SetUpWebDriver : ISetUpWebDriver
    {
        private RemoteWebDriver webDriver;
        private static string browser = TestContext.Parameters["browser"];
        private static string targetURL = TestContext.Parameters["targetUrl"];
        private static string user = TestContext.Parameters["user"];
        private static string key = TestContext.Parameters["key"];
        private static string server = TestContext.Parameters["server"];
        protected string profile;
        protected string environment;
        private Local browserStackLocal;
        public static IConfiguration Configuration { get; set; }

        /* GetSetUpWebDriver() method to read configuration, configure the chrome webDriver, navigate to targetUrl */
        public RemoteWebDriver GetSetUpWebDriver()
        {
            if (webDriver != null)
            {
                return webDriver;
            }

            switch (browser)
            {
                case "BrowserStack":
                    BrowserStack();
                    break;
                case "Chrome":
                    webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        new ChromeOptions(),
                        TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["pageLoadTimeout"])));
                    break;
                case "Firefox":
                    webDriver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        new FirefoxOptions(),
                        TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["pageLoadTimeout"])));
                    break;
                case "IE":
                    webDriver = new InternetExplorerDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        new InternetExplorerOptions(),
                        TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["pageLoadTimeout"])));
                    break;
                default:
                    throw new Exception("Browser not configured");
            }

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["pageLoadTimeout"]));
            webDriver.Manage().Window.Maximize();
            GoTo(targetURL);
            return webDriver;
        }

        public void GoTo(string url)
        {
            webDriver.Url = url;
        }
               
        public void CloseWebDriver()
        {
            Thread.Sleep(2000);
            webDriver.Quit();
            if (hasQuit(webDriver) == false)
            {
                Thread.Sleep(2000);
                webDriver = null;
                Thread.Sleep(2000);
            }
            else if (browserStackLocal != null)
            {
                browserStackLocal.stop();
                Thread.Sleep(2000);
            }
            else
            {
                //WebDriver quit correctly
                //extent.Flush();
            }
            Thread.Sleep(2000);
        }

        public bool hasQuit(RemoteWebDriver webDriver)
        {
            try
            {
                if (webDriver.SessionId.ToString().Contains("null"))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Session: {0}", webDriver.SessionId.ToString());
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public void BrowserStack()
        {
            DesiredCapabilities capability = new DesiredCapabilities();

            capability.SetCapability("browserstack.user", user);
            capability.SetCapability("browserstack.key", key);
            capability.SetCapability("browserstack.local", "true");
            capability.SetCapability("browserstack.debug","true");
            capability.SetCapability("browserstack.console", "errors");
            capability.SetCapability("browserstack.networkLogs", "true");

            webDriver = new RemoteWebDriver(new Uri("http://" + server + "/wd/hub/"), capability);
        }
    }
}

