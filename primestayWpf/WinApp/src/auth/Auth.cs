namespace WinApp.src.auth
{
    public static class Auth
    {
        public static string AccessToken = "";
        public static string username = "";
        static public bool IsLoggedIn => !string.IsNullOrWhiteSpace(AccessToken);
    }
}
