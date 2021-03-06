﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_MVC.Models.DALModels
{
    public class ToDoDAL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity(1, 1)
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
