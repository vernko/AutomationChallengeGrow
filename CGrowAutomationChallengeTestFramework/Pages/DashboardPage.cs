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

        public void ExpandFirstMetric()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(Map.ExpandedViewIcon);
            actions.Click();
            actions.Perform();
        }

        public IWebElement GetDashboard(string name)
        {
            var dashboards = Map.AllDashboardsContainer.FindElements(By.TagName("div"));
            return dashboards.FirstOrDefault(dash => dash.Text == name);
        }

        public void GotoDashboard(string name)
        {
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
        public IWebElement AllDashboardsContainer => _driver.FindElement(By.TagName("allDashboardsQA"));

        // Add Dashboard modal
        public IWebElement NewDashboardNameField => _driver.FindElement(By.CssSelector("[id*=EnterDashboardName]"));
        public IWebElement NewDashboardSubmitButton => _driver.FindElement(By.XPath("//*[text()='Submit']"));

        // Dashboard Details
        public IWebElement ExpandedViewIcon => _driver.FindElement(By.XPath("//div[@class='metricInfo---transient---35-P9']"));
        public IWebElement CloseExpandedViewIcon => _driver.FindElement(By.XPath("//div[@class='expandedMetricDialog---close---2mgNl']"));
        public IWebElement ExpandedView => _driver.FindElement(By.ClassName("filters---chartFiltersContainer---sHggt"));
        public IWebElement BackgroundExpandedView => _driver.FindElement(By.ClassName("expandedMetricDialog---overlay---1kpAq"));
    }
}