using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class User
    {
        private string _login;
        private string _password;
        protected int _permission;
        public int IdentifyPermission { get { return _permission; } }
        public string Login { get { return _login; } }
        public User(string login, string password) 
        {
            _login = login;
            _password = password;
        }
    }
}
