using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class UI
    {
        public void BasicUserUI(BasicUser user, DBManager dBManager)
        {
            Console.WriteLine("-------------------- Welcome ----------------------");
            while (true)
            {
                Console.WriteLine("Username: " + user.Login); Console.Write("Permission: " + user.IdentifyPermission);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" (Basic Permission)"); Console.ResetColor();
                Console.WriteLine("---------------------------------------------------");

                foreach (Task task in user.WatchMyTasks)
                {
                    Console.Write    ("      " + " TaskID:");                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(task.ID);                                      Console.ResetColor();
                    Console.WriteLine("             " + " TaskTitle: " + task.Title);
                    Console.WriteLine("             " + " TaskText: " + task.Text);
                    Console.Write    ("             " + " Status: ");                Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(task.Status);                                  Console.ResetColor();
                }

                Console.WriteLine();
                string temp;
                int idTask;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("SELECT "); Console.ResetColor();
                    Console.Write("TaskID: ");
                    Console.ForegroundColor = ConsoleColor.Green;

                    temp = Console.ReadLine();
                    if (temp != "" && char.IsDigit(temp[0]))
                    {
                        idTask = Convert.ToInt32(temp);
                        if (user.WatchMyTasks.Exists(x => x.ID.Equals(idTask)))
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No such task!");
                    Console.ResetColor();
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("CHANGE");
                Console.ResetColor();
                Console.WriteLine(" Task STATUS: "); Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.Write("[1]");
                Console.ResetColor();
                Console.WriteLine(" In progress");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[2]");
                Console.ResetColor();
                Console.WriteLine(" Done");
                Console.ForegroundColor = ConsoleColor.Green;

                int idStatus;
                while (true)
                {
                    temp = Console.ReadLine();
                    if (temp != "" && char.IsDigit(temp[0]))
                    {
                        idStatus = Convert.ToInt32(temp);
                        if (idStatus > 0 && idStatus < 3)
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No such option!");
                    Console.ResetColor();
                    temp = "";
                }
                switch (idStatus)
                {
                    case 1: user.ChangeTaskStatus(idTask, "In progress"); dBManager.UpdateTaskStatus(idTask, "In progress", user.Login); break;
                    case 2: user.ChangeTaskStatus(idTask, "Done"); dBManager.UpdateTaskStatus(idTask, "Done", user.Login); break;
                }
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("Status successfully changed!"); Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void AdminUI(Admin admin, DBManager dbManager)
        {
            Console.Clear();
            Console.WriteLine("-------------------- Welcome ----------------------");
            while (true)
            {
                Console.WriteLine("Username: " + admin.Login); Console.Write("Permission: " + admin.IdentifyPermission);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" (Administration)"); Console.ResetColor();
                Console.WriteLine("---------------------------------------------------");


                Console.ForegroundColor = ConsoleColor.Green; Console.Write("[1]"); Console.ResetColor(); Console.WriteLine("Show all users");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("[2]"); Console.ResetColor(); Console.WriteLine("Add new user");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("[3]"); Console.ResetColor(); Console.WriteLine("Create new task for user");
                Console.WriteLine();
                string temp;
                int state;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    temp = Console.ReadLine();
                    if (temp != "" && char.IsDigit(temp[0]))
                    {
                        state = Convert.ToInt32(temp);
                        if (state > 0 && state < 4)
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No such option!");
                    Console.ResetColor();
                    temp = "";
                }
                switch (state)
                {
                    case 1:
                        Console.Clear(); Console.ResetColor();
                        Console.WriteLine("-------------------- Users DataBase ----------------------");
                        Console.WriteLine();
                        dbManager.ShowDB();
                        Console.WriteLine("----------------------------------------------------------");
                        break;
                    case 2:
                        Console.Clear(); Console.ResetColor();
                        Console.WriteLine("--------------------- New user registration ------------------------");
                        Console.Write("User login: ");

                        string login;

                        while (true)
                        {
                            login = Console.ReadLine();
                            if (!dbManager.UserExist(login) && login != "")
                                break;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unacceptable login or user is already exist!");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Write("User login: ");
                        }

                        Console.Write("User password: ");

                        string password = Console.ReadLine();

                        string permission;
                        int permissionInt;

                        while (true)
                        {
                            Console.Write("User permission:");

                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[0] Basic Permission "); Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Red; Console.Write("[1] Administration "); Console.ResetColor();
                            permission = Console.ReadLine();
                            if (permission != "" && char.IsDigit(permission[0]))
                            {
                                permissionInt = Convert.ToInt32(permission);
                                if (permissionInt < 2 && permissionInt > -1)
                                    break;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No such permission!");
                            Console.ResetColor();

                        }

                        Console.WriteLine();
                        User user = admin.CreateUser(login, password, permissionInt);
                        dbManager.InsertUser(user, password);
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("User created successfully!"); Console.ResetColor();
                        break;
                    case 3:
                        Console.Clear(); Console.ResetColor();
                        Console.WriteLine("--------------------- New task for user ------------------------");
                        Console.Write("Task title: ");
                        string taskTitle = Console.ReadLine();
                        Console.Write("Task case: ");
                        string taskText = Console.ReadLine();

                        string userLogin;
                        while (true)
                        {
                            Console.Write("Select the executing user: ");
                            userLogin = Console.ReadLine();
                            if (dbManager.UserExist(userLogin))
                                break;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No such user!");
                            Console.ResetColor();

                        }
                        Task task = admin.CreateTask(dbManager.LastTaskID(), taskTitle, taskText);
                        admin.SetTaskToUser(task, userLogin);
                        //dbManager.InsertTask(task, userLogin);
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("User successfully get a task!"); Console.ResetColor();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
