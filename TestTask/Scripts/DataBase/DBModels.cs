using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class UserDB
    {
        [BsonId]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
        public List<TaskDB> Tasks { get; set; }

    }
    public class TaskDB
    {
        [BsonId]
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public string TaskText { get; set; }
        public string Status { get; set; }
    }
}
