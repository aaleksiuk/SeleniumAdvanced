using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumAdvanced.Enums;
using System;

namespace SeleniumAdvanced.Providers;

internal class DriverProvider
{
    public IWebDriver InitializeDriver(Browsers browser)
    {
        return browser switch
        {
            Browsers.Chrome => InitializeChromeDriver(),
            Browsers.Firefox => InitializeFirefoxDriver(),
            Browsers.Edge => InitializeEdgeDriver(),
            _ => throw new ArgumentException($"Unsupported browser: {browser}")
        };
    }
    private IWebDriver InitializeChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        options.AddArgument("--no-first-run");
        options.AddArgument("--no-default-browser-check");
        options.AddArgument("--disable-default-apps");
        options.AddArgument("--disable-popup-blocking");
        options.AddArgument("--disable-infobars");
        options.AddExcludedArgument("enable-automation");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-extensions");
        options.AddArgument("test-type");
        options.AddArgument("--disable-search-engine-choice-screen");
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