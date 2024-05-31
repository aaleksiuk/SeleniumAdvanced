using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit.Abstractions;

namespace SeleniumAdvanced.Helpers
{
    public abstract class TestBase : IDisposable
    {
        protected readonly IWebDriver driver;
        protected readonly ITestOutputHelper output;
        public TestBase(ITestOutputHelper output)
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            this.output = output;
        }

        public void Dispose()
        {
            driver.Quit();
        }

        //Typ generyczny T
        //Uruchomić konstruktor klasy T
        //Przekazać do niego drivera
        //Uruchamiamy go za pomocą reflekcji
        public T GetPage<T>(Action<T> action) where T : class
        {
            var page = (T)Activator.CreateInstance(typeof(T), driver);
            action(page);
            return page;
        }
    }
}