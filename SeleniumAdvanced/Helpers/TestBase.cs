using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;

namespace SeleniumAdvanced.Helpers;

public abstract class TestBase
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var browser = Configuration.Instance.Browser;
        driver = new DriverProvider().InitializeDriver(browser);
    }

    [TearDown]
    public void Dispose()
    {
        driver.Quit();
    }

    public T GetPage<T>(Action<T> action) where T : BasePage
    {
        var page = (T)Activator.CreateInstance(typeof(T), driver);
        Console.WriteLine($"At {typeof(T).Name}");
        action(page);
        return page;
    }
}