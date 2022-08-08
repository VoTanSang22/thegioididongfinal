using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thegioididong.Models;
using System.Collections.Generic;
using System.Text;


namespace thegioididong.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDBConext _context;
        public AdminController(MyDBConext context)
        {
            _context = context;
        }

        

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(string login, string password)
        {
            
            if (login.Equals("admin")  && password.Equals("123456"))
            {
                HttpContext.Session.SetString("Login", "Yes");
                return RedirectToRoute(new { Controller = "Products", Action = "Index" });
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
        }


    }
}
