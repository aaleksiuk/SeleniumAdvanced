namespace SeleniumAdvanced.Providers
{
    public class UrlProvider
    {

        public static string BaseUrl => "http://146.59.32.4/index.php";
        public static string AppUrl => BaseUrl;
        public static string RegistartionUrl => $"{BaseUrl}?controller=authentication&create_account=1";
        public static string SignInUrl => $"{BaseUrl}?controller=authentication&back=my-account";
    }
}
