using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class HomePage : Page
    {
        public enum FrontType
        {
            CA,
            SI
        }

        private IWebElement UsernameInput
        {
            get { return WebDriver.FindElementById("username"); }
        }
        private IWebElement PasswordInput
        {
            get { return WebDriver.FindElementById("password"); }
        }
        private IWebElement SignInButton
        {
            get { return WebDriver.FindElementById("signInButton"); }
        }
        private IWebElement ChangeFrontLink(string frontType)
        {
            return WebDriver.FindElementByCssSelector("a.footer-link[onclick*='" + frontType + "']");
        }
        private IWebElement LoginErrorDiv
        {
            get { return WebDriver.FindElementById("alertLogin"); }
        }
        private IWebElement NoUserErrorDiv
        {
            get { return WebDriver.FindElementById("alertUser"); }
        }
        private IWebElement NoPasswordErrorDiv
        {
            get { return WebDriver.FindElementById("alertNotPassword"); }
        }
        private IWebElement NoTextErrorDiv
        {
            get { return WebDriver.FindElementById("alertNoText"); }
        }
        private IWebElement NewUsernameInput
        {
            get { return WebDriver.FindElementById("newusername"); }
        }
        private IWebElement NewPasswordInput
        {
            get { return WebDriver.FindElementById("newpassword"); }
        }
        private IWebElement NewPasswordRepeatInput
        {
            get { return WebDriver.FindElementById("newpassword2"); }
        }
        private IWebElement CreateUserButton
        {
            get { return WebDriver.FindElementById("createUserButton"); }
        }

        public HomePage(ISetUpWebDriver setUpWebDriver)
            : base(setUpWebDriver)
        {
        }

        public HomePage SignIn(string user, string psswd)
        {
            UsernameInput.SendKeys(user);
            Thread.Sleep(1000);
            PasswordInput.SendKeys(psswd);
            Thread.Sleep(1000);
            return this;
        }

        public HomePage ContinueSignIn()
        {
            SignInButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public bool CheckErrorLogin()
        {
            bool isError = false;
            try
            {
                if (IsElementDisplayed(LoginErrorDiv) == true || IsElementDisplayed(NoPasswordErrorDiv) == true || IsElementDisplayed(NoTextErrorDiv) == true || IsElementDisplayed(NoUserErrorDiv) == true)
                {
                    isError = true;
                    return isError;
                }
                return isError;
            }
            catch(NoSuchElementException)
            {
                return isError;
            }
        }

        public HomePage ChangeFront(FrontType frontType)
        {
            if (frontType == FrontType.CA)
            {
                ChangeFrontLink("createAccount").Click();
            }
            else
            {
                ChangeFrontLink("signIn").Click();
            }
            return this;
        }

        public HomePage RegisterNewUser(string userName, string userPassword)
        {
            NewUsernameInput.SendKeys(userName);
            Thread.Sleep(1000);
            NewPasswordInput.SendKeys(userPassword);
            Thread.Sleep(1000);
            NewPasswordRepeatInput.SendKeys(userPassword);
            Thread.Sleep(1000);
            return this;
        }

        public HomePage ContinueRegister()
        {
            CreateUserButton.Click();
            Thread.Sleep(1000);
            return this;
        }
    }
}
