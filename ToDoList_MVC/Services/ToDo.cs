using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_MVC.Models
{
    public class ToDo
    {
        public ToDo()
        {
            Description = string.Empty;
            DueDate = DateTime.Now;
            IsCompleted = false;
        }

        public ToDo(string description)
        {
            Description = description;
            DueDate = DateTime.Now;
            IsCompleted = false;
        }

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
