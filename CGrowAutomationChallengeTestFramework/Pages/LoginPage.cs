using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CGrowAutomationChallengeTestFramework.Pages
{
    public class LoginPage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;
        public readonly LoginPageMap Map;

        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Map = new LoginPageMap(driver);
        }

        public void Goto(string url)
        {
            _driver.Navigate().GoToUrl(url);
            WaitForPageLoad();
        }

        public void WaitForPageLoad()
        {
            _wait.Until(driver => Map.SignInButton.Displayed);
        }

        public void SignIn(string email, string password)
        {
            Map.EmailField.SendKeys(email);
            Map.PasswordField.SendKeys(password);
            Map.SignInButton.Click();
        }
    }

    public class LoginPageMap
    {
        public readonly IWebDriver _driver;

        public LoginPageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement EmailField => _driver.FindElement(By.Name("email"));
        public IWebElement PasswordField => _driver.FindElement(By.Name("password"));
        public IWebElement SignInButton => _driver.FindElement(By.XPath("//button[@class='sign-in btn']"));
    }
}