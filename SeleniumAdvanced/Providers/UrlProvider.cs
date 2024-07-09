namespace SeleniumAdvanced.Providers;

public static class UrlProvider
{
    private static string BaseUrl => Helpers.Configuration.Instance.BaseUrl;
    public static string AppUrl => BaseUrl;
    public static string RegistartionUrl => $"{BaseUrl}?controller=authentication&create_account=1";
    public static string SignInUrl => $"{BaseUrl}?controller=authentication&back=my-account";
}