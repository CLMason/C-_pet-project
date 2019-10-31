using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CsharpProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
namespace CsharpProject.Controllers
{
    public class HomeController : Controller
    {
        private MyContext context;
        public HomeController(MyContext mc)
        {
            context = mc;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        //=============Login Get page================
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Index");
        }
        //==============Login Post=====================
        [HttpPost("login")]
        public IActionResult Login(LoginUser userData)
        {
            User userInDb = context.Users.FirstOrDefault(u => u.Email == userData.LoginEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LoginEmail", "Email not found!");
            } 
            else
            {
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userData, userInDb.Password, userData.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Incorrect Password!");
                }
            }
            if(!ModelState.IsValid)
            {
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return Redirect("/dashboard");
        }
        //===========Register Get=====================
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("_Register");
        }
        //===========Register Post====================
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
            }
            if(ModelState.IsValid)
            {
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                context.Users.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                Console.WriteLine("anything");
                return Redirect("/dashboard");
            }
            return Redirect("/dashboard");
        }
        //=================User Dashboard================
        [HttpGet("dashboard")]
        public IActionResult ViewDashboard()
        {
            User user = context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.Pets = context.Pets.ToArray();

            if (user != null && user.IsAdmin) {
                return Redirect("/admin/dashboard");
            }
            return View ("dashboard");
        }

        //===============Admin Dashboard================
        [HttpGet("/admin/dashboard")]
        public IActionResult AdminDashboard()
        {
            ViewBag.Pet = context.Pets.ToArray();
            return View ("DashboardAdmin");
        }

        //==============Add Pet Form Page===============

        [HttpGet("admin/new")]
        public IActionResult AddPetPage()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }
            return View("AddPet");
        }

        //=============View Single Pet=================
        [HttpGet("admin/{id}")]
        public IActionResult ViewSinglePet(string id)
        {
            ViewBag.Pet = context.Pets.FirstOrDefault(p => p.PetId == id);
            return View("ViewSinglePet");
        }

        //=============Add Pet Post=================
        [HttpPost("admin/new")]
        public IActionResult Create(IFormFile Image)
        {   
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }
            MemoryStream ms = new MemoryStream();
            Image.CopyTo(ms);
            Console.WriteLine(ms.ToArray());
            Byte [] img = ms.ToArray();
            Pet newPet = new Pet();
            newPet.Image=img; 
            newPet.PetAge =  Int32.Parse(Request.Form["PetAge"]);
            newPet.PetName = Request.Form["PetName"];
            newPet.PetBreed = Request.Form["PetBreed"];
            newPet.Description = Request.Form["Description"];
            if(ModelState.IsValid)
            {
                context.Add(newPet);
                context.SaveChanges();
                return Redirect("/dashboard/admin");
            }
            ViewBag.Pet = context.Pets;
            return View("AddPet");
        }

        //============View Pet Profile==================
        [HttpGet("view")]
        public IActionResult ViewPet()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }
            return View("ViewPet");
         
        }

        //============Logout===========================
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            return Redirect("/");
        }
    
    }
}
