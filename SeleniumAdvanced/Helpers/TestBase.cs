using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumAdvanced.Enums;
using System;
using System.IO;
using Xunit.Abstractions;
using SeleniumAdvanced.Pages;

namespace SeleniumAdvanced.Helpers
{
    public abstract class TestBase : IDisposable
    {
        protected readonly IWebDriver driver;
        protected readonly ITestOutputHelper output;
        public Browsers Browser { get; set; }

        public TestBase(ITestOutputHelper output)
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
            var filePath = Path.Combine(projectRoot, "SeleniumAdvanced\\appconfig.json");

            var jsonString = File.ReadAllText(filePath);

            var browserConfig = JsonConvert.DeserializeObject<BrowserConfig>(jsonString);
            Browser = browserConfig.Browser;

            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = InitializeDriver(Browser, options);
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
        private IWebDriver InitializeDriver(Browsers browser, ChromeOptions options)
        {
            switch (browser)
            {
                case Browsers.Chrome:
                    options.AddArgument("start-maximized");
                    return new ChromeDriver(options);
                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}