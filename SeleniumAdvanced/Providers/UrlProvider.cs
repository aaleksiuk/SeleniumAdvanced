namespace SeleniumAdvanced.Providers
{
    public class UrlProvider(string BaseUrl)
    {
        public string AppUrl => BaseUrl;
        public string RegistartionUrl => $"{BaseUrl}?controller=authentication&create_account=1";
        public string SignInUrl => $"{BaseUrl}?controller=authentication&back=my-account";
    }
}