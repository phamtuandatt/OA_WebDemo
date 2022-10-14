using Microsoft.AspNetCore.Mvc;
using OA.Dataa;
using OA.Service;
using OA_Web_Demo.Models;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace OA_Web_Demo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;

        public UserController(IUserService userService, IUserProfileService userProfileService)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
        }

        public IActionResult Index()
        {
            int CountUser = 0;
            List<UserViewModel> model = new List<UserViewModel>();
            userService.GetUsers().ToList().ForEach(u =>
            {
                UserProfile userProfile = userProfileService.GetUserProfile(u.Id);
                UserViewModel user = new UserViewModel
                {
                    Id = u.Id,
                    Name = $"{userProfile.FirstName} {userProfile.LastName}",
                    Email = u.Email,
                    Address = userProfile.Address
                };
                model.Add(user);
                CountUser++;
            });

            ViewBag.CountUser = CountUser + "";

            return View(model);
        }

        [HttpGet]
        public ActionResult AddUsers()
        {
            UserViewModel model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUsers(UserViewModel model)
        {
            User userEntity = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserProfile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    AddedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                }
            };
            userService.InsertUser(userEntity);
            if (userEntity.Id > 0)
            {
                return RedirectToAction("index");
            }
            return View(model);
        }


        public ActionResult EditUser(int? id)
        {
            UserViewModel model = new UserViewModel();
            if (id.HasValue && id.Value != 0)
            {
                User userEntity = userService.GetUser(id.Value);
                UserProfile userProfileEntity = userProfileService.GetUserProfile(id.Value);
                model.FirstName = userProfileEntity.FirstName;
                model.LastName = userProfileEntity.LastName;
                model.Address = userProfileEntity.Address;
                model.Email = userEntity.Email;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            User userEntity = userService.GetUser(model.Id);
            userEntity.Email = model.Email;
            userEntity.ModifiedDate = DateTime.UtcNow;
            userEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            UserProfile userProfileEntity = userProfileService.GetUserProfile(model.Id);
            userProfileEntity.FirstName = model.FirstName;
            userProfileEntity.LastName = model.LastName;
            userProfileEntity.Address = model.Address;
            userProfileEntity.ModifiedDate = DateTime.UtcNow;
            userProfileEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userEntity.UserProfile = userProfileEntity;
            userService.UpdateUser(userEntity);

            if (userEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult DeleteUser(long id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
