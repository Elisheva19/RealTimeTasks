using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealTimeTasks.Data
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<TaskItem> Tasks { get; set; }
      

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
