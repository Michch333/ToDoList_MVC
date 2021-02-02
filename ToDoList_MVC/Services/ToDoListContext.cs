using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_MVC.Models.DALModels;

namespace ToDoList_MVC.Services
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ToDoDAL> ToDoItems { get; set; }
        public DbSet<UserDAL> Users { get; set; }

    }
}
