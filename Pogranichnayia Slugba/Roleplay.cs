using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pogranichnayia_Slugba
{
    public class Roleplay
    {
        public string Login { get; set; }

        public bool IsAdmin { get; }

        public string Status => IsAdmin ? "Admin" : "User";

        public Roleplay(string login, bool isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }

    }
}
