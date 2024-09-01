using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace TestTask
{
    internal class DBManager
    {
        public void InsertUser(User user, string password)
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var col = db.GetCollection<UserDB>("Users");
                var microsoft = new UserDB {Login = user.Login, Password = password, Permission = user.IdentifyPermission, Tasks = new List<TaskDB>() };
                col.Insert(microsoft);
            }
        }
        public int LastTaskID()
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var col = db.GetCollection<TaskDB>("Tasks");
                if(col == null)
                {
                    return col.Max(x => x.Id);
                }
                return 0;
            }
        }
        public void UpdateTaskStatus(int taskID,string status, string login)
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var col = db.GetCollection<UserDB>("Users");
                col.EnsureIndex(x => x.Login);
                var result = col.FindOne(x => x.Login.Equals(login));
                result.Tasks.Find(x => x.Id.Equals(taskID)).Status = status;
                col.Update(result);
            }
        }
        public User PullUser(string login, string password)
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var col = db.GetCollection<UserDB>("Users");
                col.EnsureIndex(x => x.Login);
                var result = col.FindOne(x => x.Login.Equals(login));
                if (result != null && result.Password==password) 
                {
                    switch (result.Permission)
                    {
                        case 0:
                            List<Task> tasks = new List<Task>();
                            foreach (TaskDB task in result.Tasks)
                            {
                                Task temp = new Task(task.Id,task.TaskTitle,task.TaskText, task.Status);
                                tasks.Add(temp);
                            }
                            return new BasicUser(result.Login, result.Password, tasks);
                        case 1:
                            return new Admin(result.Login, result.Password);
                    }
                    Console.WriteLine(2);
                }
                return null;
            }
        }
        public bool UserExist(string login)
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var col = db.GetCollection<UserDB>("Users");
                col.EnsureIndex(x => x.Login);
                if (col.Exists(x => x.Login == login))
                    return true;
                else return false;
            }
        }
        public void ShowDB()
        {
            using (var db = new LiteDatabase(@"Database.db"))
            {
                var col = db.GetCollection<UserDB>("Users");
                var result = col.FindAll();
                if (result != null)
                {
                    foreach (UserDB c in result)
                    {
                        Console.Write("UserID:");
                        Console.ForegroundColor = ConsoleColor.Blue; Console.Write(c.Id); Console.ResetColor();
                        Console.Write(" Username:");
                        Console.ForegroundColor = ConsoleColor.Blue; Console.Write(c.Login); Console.ResetColor();
                        Console.Write(" Permission:");
                        Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(c.Permission); Console.ResetColor();
                        if (c.Tasks != null)
                        {
                            foreach (var task in c.Tasks)
                            {
                                Console.Write    ("      " + " TaskID:");                             Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(task.Id);                                           Console.ResetColor();
                                Console.WriteLine("             " + " TaskTitle: " + task.TaskTitle);
                                Console.WriteLine("             " + " TaskText: " + task.TaskText);
                                Console.Write    ("             " + " Status: ");                     Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(task.Status);                                       Console.ResetColor();
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
