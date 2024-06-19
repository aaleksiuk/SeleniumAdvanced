using OpenQA.Selenium;
using SeleniumAdvanced.Pages;
using SeleniumAdvanced.Providers;
using System;
using Xunit.Abstractions;

namespace SeleniumAdvanced.Helpers
{
    public abstract class TestBase : IDisposable
    {
        protected readonly IWebDriver driver;
        protected readonly ITestOutputHelper output;
        public UrlProvider UrlProvider { get; }

        public TestBase(ITestOutputHelper output)
        {
            var browser = Configuration.Instance.Browser;
            driver = new DriverProvider().InitializeDriver(browser);
            UrlProvider = new UrlProvider(Configuration.Instance.BaseUrl);
            this.output = output;
        }

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
}