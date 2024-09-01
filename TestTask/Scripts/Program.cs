using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI mainUI = new UI();
            DBManager dbManager = new DBManager();
            AuthService authService = new AuthService();

            while (true)
            {
                User user = authService.Authorization(dbManager);
                authService.CreateSession(user, dbManager, mainUI);
            }
        }
    }
}
