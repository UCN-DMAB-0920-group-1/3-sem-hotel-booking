namespace primestayWpf.src.auth
{
    public static class Auth
    {
        public static string AccessToken = "";
        public static string username = "";
        static public bool IsLoggedIn => AccessToken != null;
    }
}
