using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using RealTimeTasks.Data;
using RealTimeTasks.Web.Models;

namespace RealTimeTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IHubContext<TaskHub> _context;

        private readonly string _connectionString;

        public TaskController(IHubContext<TaskHub> context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        [HttpPost]
        [Route("addtask")]
        public void AddTask(AddTaskViewModel vm)
        {
            var repo = new TaskRepository(_connectionString);



            var newtask = new TaskItem
            {

                Title = vm.TaskName
            };


            var newtasktoadd = repo.AddTask(newtask);
            _context.Clients.All.SendAsync("newtask", newtasktoadd);
        }
        [Authorize]
        [HttpPost]
        [Route("marktaken")]

        public void MarkTaken(StatusViewModel vm)
        {
            var repo = new TaskRepository(_connectionString);
            var email = User.Identity.Name;
            int userid = repo.GetByEmail(email).Id;
            repo.MarkTaken(vm.Id, userid);

            var tasks = repo.GetTasks();
            _context.Clients.All.SendAsync("statusupdate", tasks);

        }
        [Authorize]
        [HttpPost]
        [Route("markfinished")]
        public void MarkFinished(StatusViewModel vm)
        {
            var repo = new TaskRepository(_connectionString);
            repo.MarkFinished(vm.Id);
            var tasks = repo.GetTasks();
            _context.Clients.All.SendAsync("statusupdate", tasks);
        }



    }
}
