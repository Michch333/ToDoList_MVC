using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Models.ViewModels;

namespace ToDoList_MVC.Controllers
{
    public class ToDoListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult TryCreateItem(ToDo item, UserDashboardViewModel model)
        {
            var newItem = new ToDo(item.Description);

            model.ToDoItems.Add(newItem);

            return View("UserDashboard", model);
        }

        public IActionResult UserDashboard(UserDashboardViewModel model)
        {
            // TODO look up to see if old account exists
            var newUser = new User();
            newUser.EmailAddress = model.EmailAddress;

            return View(model);
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        public IActionResult DeleteItem(UserDashboardViewModel model, int toDoID)
        {
            foreach (var item in model.ToDoItems)
            {
                if (item.ID == toDoID)
                {
                    model.ToDoItems.Remove(item);
                }
            }
            return View("UserDashboard");
        }

    }
}
