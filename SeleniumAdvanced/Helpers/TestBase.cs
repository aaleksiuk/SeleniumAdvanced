using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAdvanced.Extensions;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;

namespace SeleniumAdvanced.Helpers;

public abstract class TestBase
{
    protected IWebDriver Driver;

    [SetUp]
    public void Setup()
    {
        var browser = Configuration.Instance.Browser;
        Driver = new DriverProvider().InitializeDriver(browser);
    }

    [TearDown]
    public void Dispose()
    {
        Driver.Quit();
    }
    protected T GetPage<T>(Action<T> action = null) where T : BasePage
    {
        return Driver.GetPage<T>(action);
    }
}