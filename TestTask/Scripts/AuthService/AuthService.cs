using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class AuthService
    {
        public AuthService() { }
        private User GetAccess(string login, string password, DBManager DataBase)
        {
            return DataBase.PullUser(login, password);
        }
        public User Authorization(DBManager dbManager)
        {
            while (true)
            {
                Console.WriteLine("-------------------- Authorization ----------------------");
                Console.Write("login:");
                string login = Console.ReadLine();
                Console.Write("password:");
                string password = Console.ReadLine();

                User user = GetAccess(login, password, dbManager);
                if (user != null)
                    return user;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-------------------- Incorrect user data ----------------------");
                Console.ReadKey(); Console.Clear(); Console.ResetColor();
            }
        }
        public void CreateSession(User user, DBManager dBManager, UI mainUI)
        {
            switch (user.IdentifyPermission)
            {
                case 0: mainUI.BasicUserUI(user as BasicUser, dBManager); break;
                case 1: mainUI.AdminUI(user as Admin, dBManager); break;
            }
        }
    }
}
