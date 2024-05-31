using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V122.Browser;
using SeleniumAdvanced.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace SeleniumAdvanced.Providers
{
    internal class DriverProvider
    {
        public IWebDriver GetDriver(Browsers browsers)
        { 
            //przekazac enum z testbase switchem
            throw new NotImplementedException();
        }
    }
}

//appsetings.json dodać browser
