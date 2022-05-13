using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumDocumentationTest
{
    public class SeleniumDocumentationTest
    {
        IWebDriver _driver;

        [SetUp]
        public void TestInit()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(20);
            _driver.Navigate().GoToUrl("https://www.selenium.dev/documentation/webdriver/getting_started/");
        }

        [Test]
        public void NavigatedToDifferentTabs_When_FindingElementsByXPath()
        {
            IWebElement gridOptionFromMenu = _driver.FindElement(By.XPath("//a[@id='m-documentationgrid']"));
            gridOptionFromMenu.Click();

            IWebElement gridComponentsFromMenu = _driver.FindElement(By.XPath("//a[@title='Selenium Grid Components']"));
            gridComponentsFromMenu.Click();

            IWebElement headerToConfirm = _driver.FindElement(By.XPath("//main//*[text()='Selenium Grid Components']"));

            Assert.AreEqual("Selenium Grid Components", headerToConfirm.Text);
            Assert.AreEqual("Selenium Grid Components | Selenium", _driver.Title);

            IWebElement githubRepo = _driver.FindElement(By.XPath("//footer//*[contains(@class, 'fa-github')]"));
            githubRepo.Click();

            var newTabHandler = _driver.WindowHandles[1];
            _driver.SwitchTo().Window(newTabHandler);
            var actualPageTitle = _driver.Title;

            Assert.AreEqual(@"GitHub - SeleniumHQ/selenium: A browser automation framework and ecosystem.", actualPageTitle);
        }

        [TearDown]
        public void TestCleanUp()
        {
            _driver.Quit();
        }
    }
}