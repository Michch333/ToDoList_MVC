using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_MVC.Models
{
    public interface IToDoList
    {
        public List<ToDo> ToDoItems { get; set; }
    }
}
