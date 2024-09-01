using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Admin: User
    {
        public Admin(string login, string password) : base(login, password) { _permission = 1; }

        public User CreateUser(string login, string password, int permission)
        {
            switch (permission)
            {
                case 0: BasicUser user = new BasicUser(login,password, new List<Task>()); return user;
                case 1: Admin admin = new Admin(login, password); return admin;
            }
            return null;
        }
        public Task CreateTask(int id,string taskTitle, string taskText)
        {
            var task = new Task(id, taskTitle, taskText);
            return task;
        }
        public void SetTaskToUser(Task task, string login)
        {
            using (var db = new LiteDatabase(@"DataBase.db"))
            {
                var users = db.GetCollection<UserDB>("Users");
                var tasks = db.GetCollection<TaskDB>("Tasks");

                TaskDB taskDB = new TaskDB { TaskTitle = task.Title, TaskText = task.Text, Status = task.Status };
                tasks.Insert(taskDB);

                users.EnsureIndex(x => x.Login);

                var result = users.FindOne(x => x.Login.Equals(login));
                result.Tasks.Add(taskDB);

                users.Update(result);

            }
        }

    }
}
