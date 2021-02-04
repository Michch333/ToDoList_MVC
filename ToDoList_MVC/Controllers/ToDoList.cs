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

        public IActionResult TryLoginOrCreate(UserDAL newUser)
        {
            var newView = new UserDashboardViewModel();
            var allUsers = ConvertDALToUsers();
            foreach (var user in allUsers)
            {
                if (user.EmailAddress == newUser.EmailAddress && user.Password == newUser.Password)
                {
                    newView.EmailAddress = user.EmailAddress;
                    newView.ToDoItems = user.ToDoList;
                    newView.CurrentUser = user;
                    return View("UserDashboard", newView);
                }
            }
            newView.CurrentUser = new User();
            newView.CurrentUser.EmailAddress = newUser.EmailAddress;
            newView.CurrentUser.Password = newUser.Password;

            newView.CurrentUser.ToDoList = new List<ToDo>();
            _toDoListContext.Users.Add(newUser);
            _toDoListContext.SaveChanges();
            newView.CurrentUser.ID = newUser.UserID;
            return View("UserDashboard", newView);
        }

        public List<ToDo> ConvertDALToToDo()
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
        public List<ToDo> ConvertDALToToDo(int userID)
        {
            var DALItems = _toDoListContext.ToDoItems.Where(item => item.UserID == userID);
            var basicItems = new List<ToDo>();
            foreach (var item in DALItems)
            {
                var newBasicToDo = new ToDo();
                newBasicToDo.Description = item.Description;
                newBasicToDo.DueDate = item.DueDate;
                newBasicToDo.IsCompleted = item.IsCompleted;
                newBasicToDo.ID = item.ID;
                newBasicToDo.UserID = userID;
                basicItems.Add(newBasicToDo);
            }
            return basicItems;
        }

        public List<User> ConvertDALToUsers()
        {
            var DALItems = _toDoListContext.Users;
            var basicItems = new List<User>();
            foreach (var item in DALItems)
            {
                var newBasicUser = new User();
                newBasicUser.Name = item.Name;
                newBasicUser.EmailAddress = item.EmailAddress;
                newBasicUser.Password = item.Password;
                newBasicUser.ToDoList = new List<ToDo>();
                //newBasicUser.ToDoList = ConvertDALToToDo();
                newBasicUser.ID = item.UserID;
                basicItems.Add(newBasicUser);
            }
            return basicItems;
        }

        public User ConvertSingleDALToUser(UserDAL dalObject)
        {
            var user = new User();
            user.Name = dalObject.Name;
            user.EmailAddress = dalObject.EmailAddress;
            user.Password = dalObject.Password;
            user.ID = dalObject.UserID;
            user.ToDoList = ConvertDALToToDo(dalObject.UserID);

            return user;
        }

        public IActionResult TryCreateItem(int userID, ToDo item)
        {
            var newItem = new ToDoDAL();
            newItem.Description = item.Description;
            newItem.DueDate = item.DueDate;
            newItem.UserID = userID;
            _toDoListContext.ToDoItems.Add(newItem);
            _toDoListContext.SaveChanges();

            var model = new UserDashboardViewModel();
            var ph = _toDoListContext.Users.Where(user => user.UserID == userID).FirstOrDefault();

            model.CurrentUser = ConvertSingleDALToUser(ph);

            if (model.CurrentUser.ToDoList == null)
            {
                model.CurrentUser.ToDoList = new List<ToDo>();
            }

            return View("UserDashboard", model);
        }

        public IActionResult TryDeleteItem(int itemID, UserDashboardViewModel model)
        {
            _toDoListContext.Remove(_toDoListContext.ToDoItems.Find(itemID));
            _toDoListContext.SaveChanges();

            model.ToDoItems = ConvertDALToToDo();

            return View("UserDashboard", model);
        }

        public IActionResult TryCompleteItem(int itemID, UserDashboardViewModel model)
        {
            var thisItem = _toDoListContext.ToDoItems.Find(itemID);
            thisItem.IsCompleted = !thisItem.IsCompleted;
            _toDoListContext.SaveChanges();

            model.ToDoItems = ConvertDALToToDo();
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
            model.ToDoItems = ConvertDALToToDo();
            return View(model);
        }

        public IActionResult CreateNew(User currentUser, ToDo model)
        {
            model.UserID = currentUser.ID;
            return View(model);
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
