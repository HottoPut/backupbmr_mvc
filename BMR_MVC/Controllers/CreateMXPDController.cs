using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class CreateMXPDController : Controller
    {
        // GET: CreateMXPD
        CreateMXPD createMXPD;
        ActionResult view;
        public CreateMXPDController()
        {
            createMXPD = new CreateMXPD();
        }

        public ActionResult Main()
        {
            if (Session["USERID"] != null)
            {
                view = View();
            }
            else {
                view = RedirectToAction("index", "Login");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetItem(String itemName)
        {
            
            return Json(new { GetItemFG = createMXPD.GetItemMaster()});
        }

        //public PartialViewResult TBOMSF(String itemCode)
        //{
        //    return PartialView("TBOMSF", createMXPD.GetBomSF(itemCode));
        //}
       
    }
}