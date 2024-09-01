using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Task
    {
        private int _id;
        private string _taskTitle;
        private string _taskText;
        private string _status;
        public int ID { get { return _id; } }
        public string ChangeStatus { set { _status = value; } }
        public string Title { get { return _taskTitle; } }
        public string Text { get { return _taskText; } }
        public string Status { get { return _status; } }
        public Task(int id, string taskTitle, string taskText)
        {
            _id = id;
            _taskTitle = taskTitle;
            _taskText = taskText;
            _status = "To do";
        }
        public Task(int id, string taskTitle, string taskText, string status)
        {
            _id = id;
            _taskTitle = taskTitle;
            _taskText = taskText;
            _status = status;
        }
    }
}
