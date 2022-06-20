using Microsoft.EntityFrameworkCore;
using RealTimeTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTasks.Data
{
    public class TaskRepository
    {
        public readonly string _connection;
        public TaskRepository(string connect)
        {
            _connection = connect;

        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new TaskDataContext(_connection);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }

            return user;

        }
        public User GetByEmail(string email)
        {
            using var context = new TaskDataContext(_connection);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public TaskItem AddTask(TaskItem newtask)
        {
            using var context = new TaskDataContext(_connection);
            newtask.TaskStatus = TaskStatus.Available;
            context.Tasks.Add(newtask);
            context.SaveChanges();
            return newtask;
        }


        public List<TaskItem> GetTasks()
        {
            using var context = new TaskDataContext(_connection);
            return context.Tasks.Where(t => t.TaskStatus == TaskStatus.Available || t.TaskStatus == TaskStatus.Taken).Include(t => t.User).ToList();
        }

        public void MarkTaken(int taskid, int userid)
        {
            using var context = new TaskDataContext(_connection);
            var update = context.Tasks.FirstOrDefault(t => t.Id == taskid);
            update.TaskStatus = TaskStatus.Taken;

            update.UserId = userid;
            context.Tasks.Update(update);
            context.SaveChanges();
        }
        public void MarkFinished(int taskid)
        {
            using var context = new TaskDataContext(_connection);
            var update = context.Tasks.FirstOrDefault(t => t.Id == taskid);
            update.TaskStatus = TaskStatus.Finished;
            
            context.Tasks.Update(update);
            context.SaveChanges();
        }
    }
}
