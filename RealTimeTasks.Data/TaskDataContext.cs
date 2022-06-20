
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTasks.Data
{
  public  class TaskDataContext : DbContext
    {
       

            private string _connectionString;

            public TaskDataContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            public DbSet<User> Users { get; set; }
            public DbSet<TaskItem> Tasks { get; set; }


        }
    }

