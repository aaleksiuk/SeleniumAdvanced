using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumAdvanced.Enums;
using System;

namespace SeleniumAdvanced.Providers
{
    internal class DriverProvider
    {
        public IWebDriver InitializeDriver(Browsers browser)
        {
            switch (browser)
            {
                case Browsers.Chrome:
                    return InitializeChromeDriver();
                case Browsers.Firefox:
                    return InitializeFirefoxDriver();
                case Browsers.Edge:
                    return InitializeEdgeDriver();
                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
        private IWebDriver InitializeChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            return new ChromeDriver(options);
        }

        private IWebDriver InitializeFirefoxDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--start-maximized");
            return new FirefoxDriver(options);
        }

        private IWebDriver InitializeEdgeDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            return new EdgeDriver(options);
        }
    }
}