using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Login login;
        public LoginController()
        {
            login = new Login();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CheckLogin(String userName,String password)
        {
            return Json(login.ChackLogin(userName,password));
        }
    }
}