namespace WinApp.src.auth
{
    public static class Auth
    {
        public static string AccessToken = "";
        public static string Username = "";
        static public bool IsLoggedIn => !string.IsNullOrWhiteSpace(AccessToken);

        static public void Logout()
        {
            AccessToken = Username = "";
        }
    }
}
