using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeTasks.Data;
using Microsoft.Extensions.Configuration;

namespace RealTimeTasks.Web
{
    public class TaskHub : Hub
    {
        private string _connect;
        public TaskHub(IConfiguration configuration)
        {
            _connect = configuration.GetConnectionString("ConStr");
        }
        public void NewLogin()
        {
            var repo = new TaskRepository(_connect);
            var tasks = repo.GetTasks();
            Clients.Caller.SendAsync("newlogin", tasks);
        }
    }
}
