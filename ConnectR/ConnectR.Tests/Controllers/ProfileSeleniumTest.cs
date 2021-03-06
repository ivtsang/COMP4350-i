using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSelenium.Tests;

namespace SeleniumTests
{
    [TestClass]
    public class ProfileSeleniumTest : SeleniumTest
    {
        public ProfileSeleniumTest() : base("ConnectR") { }

        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        [TestInitialize]
        public void SetupTest()
        {
            //WebDevServer = new DevServer();
            //WebDevServer.Start();



            driver = new FirefoxDriver();
            this.driver.Navigate().GoToUrl(this.GetAbsoluteUrl("/"));
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheProfileTest()
        {
            driver.FindElement(By.Id("registerLink")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("test@a.a");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("1!Asdf");
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("1!Asdf");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("test");
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("test2");
            driver.FindElement(By.Id("Age")).Clear();
            driver.FindElement(By.Id("Age")).SendKeys("24");
            driver.FindElement(By.Id("Country")).Clear();
            driver.FindElement(By.Id("Country")).SendKeys("there");
            driver.FindElement(By.Id("City")).Clear();
            driver.FindElement(By.Id("City")).SendKeys("here");
            driver.FindElement(By.Id("School")).Clear();
            driver.FindElement(By.Id("School")).SendKeys("that one");
            driver.FindElement(By.Id("Degree")).Clear();
            driver.FindElement(By.Id("Degree")).SendKeys("good");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Hello test@a.a!")).Click();
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("test3");
            driver.FindElement(By.Id("About")).Clear();
            driver.FindElement(By.Id("About")).SendKeys("running a test");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Hello test@a.a!")).Click();
            driver.FindElement(By.LinkText("Permanently delete your account")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("test@a.a");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("1!Asdf");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
