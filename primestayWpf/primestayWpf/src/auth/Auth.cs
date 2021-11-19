using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primestayWpf.src.auth
{
    static class Auth
    {
        public static string AccessToken = "";
        public static string username = "";
        static public bool IsLoggedIn
        {
            get { return AccessToken != null; }
        }
    }
}
