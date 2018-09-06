using Microsoft.VisualStudio.TestTools.UnitTesting;
using CGrowAutomationChallengeTestFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;
using CGrowAutomationChallengeTestFramework.Pages;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class GrowTests
    {
        IWebDriver Driver;
        WebDriverWait Wait;

        [TestInitialize]
        public void Setup()
        {
            Driver = new ChromeDriver(@"/Users/vernkofford/Projects/CGrowAutomationChallengeTestFramework");
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

            var home = new LoginPage(Driver, Wait);
            home.Goto("https://app.gogrow.com");
            home.SignIn("vern.kofford@gmail.com", "3kvk15Kof!");
        }

        [TestCleanup]
        public void CleanUp()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void User_can_close_expanded_dashboards_using_x_icon()
        {
            // Acceptance Criteria: User and close dashboards.

            var dashboard = new DashboardPage(Driver, Wait);
            dashboard.WaitForPageLoad();
            dashboard.ExpandFirstMetric();
            dashboard.Map.CloseExpandedViewIcon.Click();
            
            // assert expanded view is closed
            Assert.IsFalse(dashboard.Map.CloseExpandedViewIcon.Displayed);
        }

        [TestMethod]
        public void User_can_add_a_dashboard()
        {
            var name = "Dashboard BoomshakalakaYeehah";
            var page = new DashboardPage(Driver, Wait);

            page.AddDashboard(name);
            var dashboard = page.GetDashboard(name);

            // if dashboard == null, it was not added to the list            
            Assert.IsNotNull(dashboard);
        }
    }
}