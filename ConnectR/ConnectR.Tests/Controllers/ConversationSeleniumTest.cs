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
    public class ConversationSeleniumTest : SeleniumTest
    {
        public ConversationSeleniumTest() : base("ConnectR") { }

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
            baseURL = this.GetAbsoluteUrl(" / ");
            this.driver.Navigate().GoToUrl(this.GetAbsoluteUrl("/"));
            //driver.Navigate().GoToUrl(@"localhost");
            verificationErrors = new StringBuilder();

            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("a@a.a");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("1!Asdf");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();

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
        public void TheConversationMessagingSeleniumTest()
        {
            driver.FindElement(By.LinkText("Message Users")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'JOIN IN ON THIS CONVERSATION')])[4]")).Click();
            driver.FindElement(By.Name("Text")).Clear();
            driver.FindElement(By.Name("Text")).SendKeys("I am slowly going crazy One, two, three, four, five, six, switch!");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.Name("Text")).Clear();
            driver.FindElement(By.Name("Text")).SendKeys("Crazy going slowly am I six, five, four, three, two, one, switch!");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Message Users")).Click();
        }

        [TestMethod]
        public void TheConversationAddDeleteSeleniumTest()
        {
            driver.FindElement(By.LinkText("Message Users")).Click();
            driver.FindElement(By.LinkText("START A NEW CONVERSATION")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[6]")).Click();
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
