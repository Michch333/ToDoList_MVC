using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_MVC.Models.DALModels;

namespace ToDoList_MVC.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        public UserDashboardViewModel()
        {
            ToDoItems = new List<ToDo>();
        }
        public string EmailAddress { get; set; }
        public List<ToDo> ToDoItems { get; set; }
    }
}
