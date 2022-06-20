using System;
using System.Text.Json.Serialization;

namespace RealTimeTasks.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

       
    }
}
