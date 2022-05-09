using NUnit.Framework;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace WebDriverFundamentals_Exercise_1
{
    public class Tests
    {
        IWebDriver _driver;

        [SetUp]
        public void startBrowser()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(20);
            _driver.Navigate().GoToUrl("https://www.selenium.dev/documentation/webdriver/getting_started/");
        }

        [Test]
        public void NavigatedToDifferentTabs_When_FindingElementsByXPath()
        {
            var expectedPageTitle = @"GitHub - SeleniumHQ/selenium: A browser automation framework and ecosystem.";
            IWebElement gridOptionFromMenu = _driver.FindElement(By.XPath("//a[@id='m-documentationgrid']"));
            gridOptionFromMenu.Click();

            IWebElement gridComponentsFromMenu = _driver.FindElement(By.XPath("//a[@title='Selenium Grid Components']"));
            gridComponentsFromMenu.Click();

            IWebElement headerToConfirm = _driver.FindElement(By.XPath("//main//*[text()='Selenium Grid Components']"));

            Assert.AreEqual(headerToConfirm.Text, "Selenium Grid Components");
            Assert.AreEqual(_driver.Title, "Selenium Grid Components | Selenium");

            IWebElement githunRepo = _driver.FindElement(By.XPath("//footer//*[contains(@class, 'fa-github')]"));
            githunRepo.Click();

            var newTabHandler = _driver.WindowHandles[1];
            _driver.SwitchTo().Window(newTabHandler);
            var actualPageTitle = _driver.Title;

            Assert.AreEqual(actualPageTitle, expectedPageTitle);
        }

        [TearDown]
        public void closeBrowser()
        {
            _driver.Quit();
        }
    }
}