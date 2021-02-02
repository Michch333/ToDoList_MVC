using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.Services
{
    public class ToDoList : IToDoList
    {
        public List<ToDo> ToDoItems { get; set; }
    }
}
