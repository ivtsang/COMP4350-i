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
    public class ConferenceSeleniumTest : SeleniumTest
    {
        public ConferenceSeleniumTest() : base("ConnectR") { }

        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [TestInitialize]
        public void SetupTest()
        {
            //WebDevServer = new DevServer();
            //WebDevServer.Start();

            driver = new FirefoxDriver();
            this.driver.Navigate().GoToUrl(this.GetAbsoluteUrl("/"));
            baseURL = "http://localhost:49950/";
            //driver.Navigate().GoToUrl(@"localhost");
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
        public void TheConferenceTest()
        {
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("a@a.a");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("1!Asdf");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Conferences")).Click();
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.FindElement(By.Id("Title")).Clear();
            driver.FindElement(By.Id("Title")).SendKeys("Test Conference");
            driver.FindElement(By.Id("Date")).Clear();
            driver.FindElement(By.Id("Date")).SendKeys("01/01/2017 1:30:00 PM");
            driver.FindElement(By.Id("Location")).Clear();
            driver.FindElement(By.Id("Location")).SendKeys("There");
            driver.FindElement(By.Id("Content")).Clear();
            driver.FindElement(By.Id("Content")).SendKeys("All the tests");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Test Conference")).Click();
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.Id("Content")).Clear();
            driver.FindElement(By.Id("Content")).SendKeys("All the tests!!!! (Need them Exclamation mark ;) )");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Test Conference")).Click();
            driver.FindElement(By.LinkText("Delete")).Click();
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
