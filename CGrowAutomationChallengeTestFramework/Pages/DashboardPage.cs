using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CGrowAutomationChallengeTestFramework
{
    public class DashboardPage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;
        public readonly DashboardPageMap Map;

        public DashboardPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Map = new DashboardPageMap(driver);
        }

        public void AddDashboard(string name)
        {
            Map.MainMenuButton.Click();
            Map.AddDashboardButton.Click();
            Map.NewDashboardNameField.SendKeys(name);
            Map.NewDashboardSubmitButton.Click();


        }

        public void CloseExpandedMetric()
        {
            _wait.Until(drvr => Map.ExpandedMetricContainer.Displayed);

            Actions actions = new Actions(_driver);
            actions.MoveToElement(Map.ExpandedHeaderActions);
            actions.Build().Perform();
            Map.CloseExpandedViewIcon.Click();
            Thread.Sleep(2);
        }

        public void ExpandFirstMetric()
        {
            _wait.Until(drvr => Map.MetricContainer.Displayed);

            Actions actions = new Actions(_driver);
            actions.MoveToElement(Map.MetricContainer).Build().Perform();
            Map.ExpandMetricButton.Click();
        }

        public IWebElement GetDashboard(string name)
        {
            _wait.Until(drvr => Map.AllDashboardsContainer.Displayed);

            var dashboards = Map.AllDashboardsContainer.FindElements(By.TagName("div"));
            return dashboards.FirstOrDefault(dash => dash.Text == name);
        }

        public void GotoDashboard(string name)
        {
            Map.MainMenuButton.Click();
            var dashboards = Map.AllDashboardsContainer.FindElements(By.TagName("div"));
            dashboards.FirstOrDefault(dash => dash.Text == name).Click();
        }

        public void WaitForPageLoad()
        {
            _wait.Until(drver => Map.MainMenuButton.Displayed);
        }
    }

    public class DashboardPageMap
    {
        public readonly IWebDriver _driver;

        public DashboardPageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        // Main Menu
        public IWebElement MainMenuButton => _driver.FindElement(By.CssSelector("[data-testid=open-button]"));
        public IWebElement AddDashboardButton => _driver.FindElement(By.CssSelector(".button > [class*='addDashboard']"));
        public IWebElement AllDashboardsContainer => _driver.FindElement(By.XPath("//div[@class='allDashboardsQA']"));

        // Add Dashboard modal
        public IWebElement NewDashboardNameField => _driver.FindElement(By.CssSelector("[id*=EnterDashboardName]"));
        public IWebElement NewDashboardSubmitButton => _driver.FindElement(By.XPath("//*[text()='Submit']"));

        // Dashboard Details
        public IWebElement MetricContainer => _driver.FindElement(By.CssSelector("[class*='metricTitle---container']"));
        public IWebElement ExpandMetricButton => _driver.FindElement(By.CssSelector("[class*='metricInfo---expand']"));

        // Expanded Dashboard Details
        public IWebElement CloseExpandedViewIcon => _driver.FindElement(By.CssSelector("[class*='expandedMetricDialog---close']"));
        public IWebElement ExpandedMetricContainer => _driver.FindElement(By.CssSelector("[class*='expandedMetricDialog---content']"));
        public IWebElement ExpandedHeaderActions => _driver.FindElement(By.CssSelector("[class*='expandedMetricDialog---headerActions']"));
    }
}