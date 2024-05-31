using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvanced.Providers
{
    public class UrlProvider
    {
        //private static readonly string BaseUrl;
        public static string BaseUrl => "http://146.59.32.4/index.php";
        public static string AppUrl => BaseUrl;
        public static string RegistartionUrl => $"{BaseUrl}?controller=authentication&create_account=1";
        public static string SignInUrl => $"{BaseUrl}?controller=authentication&back=my-account";


        //appsetings.json dodać baseurtl
    }
}
