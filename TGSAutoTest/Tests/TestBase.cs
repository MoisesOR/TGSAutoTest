using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Threading;
using TGSAutoTest.SetUp;
using TGSAutoTest.Test.Config;
using TGSAutoTest.Test.Data.DataBaseConector;
using TGSAutoTest.WebPages;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.Tests
{
    [SetUpFixture]
    public class SetUpFixtureBase
    {
        #region Definitions
        public static ExtentReports extent;
        public static int n_threads = 0;
        public static string HTMLPath = $@"{TestContext.CurrentContext.WorkDirectory}\Screenshots\" + DateTime.Now.ToString("yyyyMMdd-HHmm");
        #endregion

        [OneTimeSetUp]
        public void StartReport()
        {
            n_threads++;
            if (extent == null)
            {
                if (!Directory.Exists(SetUpFixtureBase.HTMLPath))
                {
                    Directory.CreateDirectory(HTMLPath);
                }

                var reportPath = new ExtentHtmlReporter(HTMLPath + @"\");

                extent = new ExtentReports();
                extent.AttachReporter(reportPath);

                extent.AddSystemInfo("TGS: ", "Windows 10");
                extent.AddSystemInfo("Operating System: ", "Windows 10");
                extent.AddSystemInfo("Hostname: ", "Selenium");
                extent.AddSystemInfo("Browser: ", "Google Chrome");
            }
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            n_threads--;
            if (n_threads == 0)
            {
                extent.Flush();
            }
        }
    }

    public class TestSetCleanBase
    {
        #region Definitions
        protected ISetUpWebDriver setUpWebDriver;
        protected Page page;
        protected ObjectsTests objectsTests;
        protected ExtentTest test;
        protected TestHelpers testHelpers;
        protected HomePage homePage;
        protected TopMenuPage topMenuPage;
        protected LeftMenuPage leftMenuPage;
        protected CreateAlbumPage createAlbumPage;
        protected UserConnection userConnection;
        protected AlbumConection albumConection;
        protected AlbumListPage albumListPage;
        protected UpdateAlbumPage updateAlbumPage;
        protected CreateGroupPage createGroupPage;
        protected UpdateGroupPage updateGroupPage;
        protected GroupListPage groupListPage;
        protected GroupConnection groupConnection;
        #endregion

        [SetUp]
        public void TestSetUp()
        {
            #region Init Definitions
            test = SetUpFixtureBase.extent.CreateTest(TestContext.CurrentContext.Test.Name);
            setUpWebDriver = new SetUpWebDriver();
            page = new Page(setUpWebDriver);
            testHelpers = new TestHelpers();
            objectsTests = new ObjectsTests();
            homePage = new HomePage(setUpWebDriver);
            topMenuPage = new TopMenuPage(setUpWebDriver);
            leftMenuPage = new LeftMenuPage(setUpWebDriver);
            createAlbumPage = new CreateAlbumPage(setUpWebDriver);
            userConnection = new UserConnection();
            albumConection = new AlbumConection();
            albumListPage = new AlbumListPage(setUpWebDriver);
            updateAlbumPage = new UpdateAlbumPage(setUpWebDriver);
            createGroupPage = new CreateGroupPage(setUpWebDriver);
            updateGroupPage = new UpdateGroupPage(setUpWebDriver);
            groupListPage = new GroupListPage(setUpWebDriver);
            groupConnection = new GroupConnection();
            #endregion

            Logger(Status.Info, "Empieza el test: " + TestContext.CurrentContext.Test.Name, true);
        }

        [TearDown]
        public void TestCleanUp()
        {
            page = new Page(setUpWebDriver);
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            var message = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? "" : string.Format("{0}", TestContext.CurrentContext.Result.Message);

            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            Thread.Sleep(1000);
            test.Log(logstatus, "Test ended with " + logstatus + " Message: " + message + "<br/>" + stackTrace + "<br/><br/>", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());

            page.TakeScreenshot(SetUpFixtureBase.HTMLPath);
            page.CloseDriver();
            Thread.Sleep(1000);
        }
        public void Logger(Status statusLog, string msg, bool doIt)
        {
            if (doIt)
            {
                test.Log(statusLog, msg, MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            }
            else
            {
                test.Log(statusLog, msg);
            }
        }
    }
    public class TestHelpers
    {
        public void IsTrueOrFalse(bool element, string trueMsg, string falseMsg)
        {
            if (element)
            {
                Assert.Pass(falseMsg);
                Thread.Sleep(1000);
            }
            else
            {
                Assert.Pass(trueMsg);
                Thread.Sleep(1000);
            }
        }
    }
}