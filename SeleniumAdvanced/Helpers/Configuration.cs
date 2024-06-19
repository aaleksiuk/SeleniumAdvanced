using Newtonsoft.Json;
using SeleniumAdvanced.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvanced.Helpers
{
    public class Configuration
    {
        private static readonly Lazy <Configuration> instance = new Lazy<Configuration>(() => new Configuration ());
        public Browsers Browser { get; private set; }
        public string BaseUrl { get; private set; }

        private Configuration()
        {
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName;
            var filePath = Path.Combine(projectRoot, "SeleniumAdvanced", "appconfig.json");

            var jsonString = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<ConfigurationModel>(jsonString);

            Browser = config.Browser;
            BaseUrl = config.BaseUrl;
        }
        public static Configuration Instance => instance.Value;
    }
}
