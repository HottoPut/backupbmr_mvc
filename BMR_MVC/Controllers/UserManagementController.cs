using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class UserManagementController : Controller
    {
        UserManagement user;
        Login login;
        ActionResult view;
        // GET: UserManagement
        public UserManagementController() {
            user = new UserManagement();
        }
        public ActionResult Index()
        {
            if (Session["USERID"] != null)
            {
                view = View(user.getuser());
            }
            else
            {
                view = RedirectToAction("index", "Login");
            }
            return view;
        }
        [HttpPost]
        public JsonResult Getgroup(String GroupID)
        {
            user = new UserManagement();
            return Json(user.getgroupuser(GroupID));
        }
        [HttpPost]
        public JsonResult Getgroupdetial(String GroupID)
        {
            user = new UserManagement();
            return Json(user.GetGroupDetials(GroupID));
        }

        [HttpPost]
        public JsonResult DellUser(String userLogin, String ctrl_user_sys_id, String link_user_sys_id, String userName, String Group_ID)
        {
            user = new UserManagement();
            user.DeleteUser(userLogin, userName, Group_ID, ctrl_user_sys_id, link_user_sys_id);
            return Json("1");
        }

        [HttpPost]
        public JsonResult InsertUser(String userLogin, String userPass, String userName, String userPre, String startDT, String endDT, String Group_ID)
        {
            login = new Login();
            user = new UserManagement();
            String enc = login.Encrypt(userPass);
            user.InsertUser(userLogin, enc, userName, userPre, startDT, endDT, Group_ID);
            return Json("1");
        }

        [HttpPost]
        public JsonResult UpdateUser(String userLogin, String userName, String userPre, String startDT, String endDT, String Group_ID, String user_ctrl_id, String user_link_id)
        {
            login = new Login();
            user = new UserManagement();
            //String enc = login.Enc(userPass);
            String start_date = startDT.Trim();
            user.UpdateUser(userLogin, userName, userPre, start_date, endDT, Group_ID, user_ctrl_id, user_link_id);
            return Json("1");
        }

        [HttpPost]
        public JsonResult InsertGroup(String group_name, String group_desc, String group_create, String group_run_job, String group_mix_cc_clean, String group_mix_cc_check, String group_mix_operate, String group_mix_check, String group_active)
        {
            user = new UserManagement();
            user.InsertGroup(group_name, group_desc, group_create, group_run_job, group_mix_cc_clean, group_mix_cc_check, group_mix_operate, group_mix_check, group_active);
            return Json("1");
        }

        [HttpPost]
        public JsonResult UpdateGroup(String group_name, String group_desc, String group_create, String group_run_job, String group_mix_cc_clean, String group_mix_cc_check, String group_mix_operate, String group_mix_check, String group_id, String group_active)
        {
            user = new UserManagement();
            user.UpdateGroup(group_name, group_desc, group_create, group_run_job, group_mix_cc_clean, group_mix_cc_check, group_mix_operate, group_mix_check, group_id, group_active);
            return Json("1");
        }

        [HttpPost]
        public JsonResult Delgroup(String group_id)
        {
            user = new UserManagement();
            user.DeleteGroup(group_id);
            return Json("1");
        }
        [HttpPost]
        public JsonResult ResetPassword(String password, String ctrl_sys_id)
        {
            login = new Login();
            String enc = login.Encrypt(password);
            user = new UserManagement();
            user.ResetPassword(enc, ctrl_sys_id);
            return Json("1");
        }
    }
}