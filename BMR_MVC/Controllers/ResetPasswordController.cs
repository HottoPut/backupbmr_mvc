using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        ActionResult view;
        Login login;
        ResetPassword reset;
        public ResetPasswordController()
        {
            login = new Login();
            reset = new ResetPassword();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckUsername(String username, String new_password, String old_password)
        {
            String enc_newpass = login.Encrypt(new_password);
            String enc_oldpass = login.Encrypt(old_password);
            return Json(reset.ChackUsername(username, enc_newpass, enc_oldpass));
        }
    }
}