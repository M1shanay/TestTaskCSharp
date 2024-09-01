using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class BasicUser: User
    {
        private List<Task> _myTasks;
        public List<Task> WatchMyTasks { get { return _myTasks; } }
        public BasicUser(string login, string password, List<Task> tasks) : base(login, password) { _permission = 0; _myTasks = tasks; }
/*        public void TakeTask(Task task)
        {
            _myTasks.Add(task);
        }*/
        public void ChangeTaskStatus(int index, string status)
        {
            //_myTasks[index].ChangeStatus = status;
            _myTasks.Find(x => x.ID.Equals(index)).ChangeStatus = status;
        }
    }
}
