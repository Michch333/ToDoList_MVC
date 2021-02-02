using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList_MVC.Models;
using ToDoList_MVC.Models.DALModels;
using ToDoList_MVC.Models.ViewModels;
using ToDoList_MVC.Services;

namespace ToDoList_MVC.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IToDoList _toDoList;
        private readonly ToDoListContext _toDoListContext;

        public ToDoListController(IToDoList toDoList, ToDoListContext toDoListContext)
        {
            _toDoList = toDoList;
            _toDoListContext = toDoListContext;

        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public List<ToDo> ConvertDALToBasic()
        {
            var DALItems = _toDoListContext.ToDoItems;
            var basicItems = new List<ToDo>();
            foreach (var item in DALItems)
            {
                var newBasicToDo = new ToDo();
                newBasicToDo.Description = item.Description;
                newBasicToDo.DueDate = item.DueDate;
                newBasicToDo.IsCompleted = item.IsCompleted;
                newBasicToDo.ID = item.ID;
                basicItems.Add(newBasicToDo);
            }
            return basicItems;
        }

        //public UserDAL TryLookUpUser(string emailAddress, string password)
        //{
        //    var listOfUsers = _toDoListContext.Users;
        //    foreach (var nUser in listOfUsers)
        //    {
        //        if (nUser.EmailAddress == emailAddress && nUser.Password == password)
        //        {
        //            return nUser;
        //        }
        //    };
        //    return new UserDAL();
        //}

        public IActionResult TryCreateItem(ToDo item, UserDashboardViewModel model)
        {
            var newItem = new ToDoDAL();
            newItem.Description = item.Description;
            newItem.DueDate = item.DueDate;
            

            _toDoListContext.ToDoItems.Add(newItem);
            _toDoListContext.SaveChanges();

            model.ToDoItems = ConvertDALToBasic();

            return View("UserDashboard", model);
        }

        public IActionResult TryDeleteItem(int itemID, UserDashboardViewModel model)
        {
            _toDoListContext.Remove(_toDoListContext.ToDoItems.Find(itemID));
            _toDoListContext.SaveChanges();

            model.ToDoItems = ConvertDALToBasic();

            return View("UserDashboard", model);
        }

        public IActionResult TryCompleteItem(int itemID, UserDashboardViewModel model)
        {
            var thisItem = _toDoListContext.ToDoItems.Find(itemID);
            thisItem.IsCompleted = !thisItem.IsCompleted;
            _toDoListContext.SaveChanges();

            model.ToDoItems = ConvertDALToBasic();
            return View("UserDashboard", model);
        }

        //public IActionResult TryLoginOrCreate(UserDAL user, UserDashboardViewModel model)
        //{
        //    var thisUser = TryLookUpUser(user.EmailAddress, user.Password);
        //    model.EmailAddress = thisUser.EmailAddress;
        //    return View("UserDashboard");
        //}

        public IActionResult UserDashboard(UserDashboardViewModel model)
        {

            // TODO look up to see if old account exists
            var newUser = new User();
            newUser.EmailAddress = model.EmailAddress;
            model.ToDoItems = ConvertDALToBasic();
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
