using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_MVC.Models
{
    public class User
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public List<ToDo> ToDoList { get; set; }
    }
}
