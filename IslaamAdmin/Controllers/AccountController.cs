using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IslaamAdmin.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace IslaamAdmin.Controllers
{
    public class AccountController : Controller
    {
        private List<User> users;
        private JSONReadWrite json;
        //public bool isValidUser { get; set; }

        public AccountController()
        {
            json = new JSONReadWrite();
            users = new List<User>();
            users = JsonConvert.DeserializeObject<List<User>>(json.Read("users.json", "./"));              
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            
            var userCheck= users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            //foreach (var u in users)
            //{
            //    if(u.Username == user.Username && u.Password == user.Password)
            //    {
            //        isValidUser = true;
            //    }
            //    else
            //    {
            //        isValidUser = false;
            //    }

            //}

            if (userCheck == null)
                return View();

            //Create Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userCheck.Username ),
                new Claim(ClaimTypes.Name,userCheck.Name)
            };

            //Create Scheme
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //Create Identity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, scheme);

            //Create Principal
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(scheme, claimsPrincipal);

            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            await HttpContext.SignOutAsync(scheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ContentResult AddUser([FromBody] User user)
        {
            var success = false;
            User checkUser = new User();
            Random number = new Random();
            int IdNumber = number.Next(10, 100);

            checkUser = users.FirstOrDefault(u => u.Id == user.Id);
            if(checkUser == null && !users.Any(u=>u.Id == IdNumber ))
            {
                user.Id = IdNumber;
                users.Add(user);
                string jsonString = JsonConvert.SerializeObject(users);
                json.Write("users.json", "./", jsonString);
                success = true;
            }
            else
            {
                success = false;
            }
          
            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(new { success }),
                ContentType = "application/json"
            };

        }

        [HttpPost]
        public ContentResult EditUser([FromBody] User user)
        {
            var success = false;
            User checkUser = new User();

            checkUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (checkUser != null)
            {
                int index = users.FindIndex(u => u.Id == user.Id);
                users[index] = user;
                string jsonString = JsonConvert.SerializeObject(users);
                json.Write("users.json", "./", jsonString);
                success = true;
            }
            else
            {
                success = false;
            }
            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(new { success }),
                ContentType = "application/json"
            };
        }

        public ContentResult GetUsers()
        {
            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(users),
                ContentType = "application/json"
            };
        }

        [HttpPost]
        public ContentResult DeleteUser(int Id)
        {
            var success = false;
            User checkUser = new User();

            checkUser = users.FirstOrDefault(u => u.Id == Id);
            if (checkUser != null)
            {
                //int index = users.FindIndex(u => u.Id == Id);
                users.Remove(checkUser);
                string jsonString = JsonConvert.SerializeObject(users);
                json.Write("users.json", "./", jsonString);
                success = true;
            }
            else
            {
                success = false;
            }

            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(new { success }),
                ContentType = "application/json"
            };

        }
    }
}
