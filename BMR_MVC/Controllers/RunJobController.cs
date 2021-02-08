using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class RunJobController : Controller
    {
        // GET: RunJob
        RunJob runJob;
        ActionResult view;
        public RunJobController() {
            runJob = new RunJob();
        }

        public ActionResult Index()
        {
            if (Session["USERID"] != null)
            {
                return view = View();
            }
            else {
                view = RedirectToAction("index", "Login");
            }

            return view;
        }
        [HttpPost]
        public JsonResult UpdateBmrHead(String itemCode, String revision, Int32 statusBmr, String startDt, String endDt)
        {
            runJob.UpdateBmrHead(itemCode, revision, startDt, endDt, statusBmr);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetVersionByItem(String itemCode)
        {
            return Json(new { BMRVersion = runJob.GetVersion(itemCode), ItemName = runJob.GetItemNameFGByItem(itemCode), UomBom = runJob.GetUomBomByItem(itemCode) });
        }
        [HttpPost]
        public JsonResult GetStatusBmr(String itemCode, String version)
        {
            return Json(new { StatusBmr = runJob.GetStatus(itemCode, version) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewMxCleanControl()
        {
            return PartialView("TCleanControl");
        }
        [HttpPost]
        public JsonResult GetCleanControl(String itemCode, String revision) {
            return Json(new { data = runJob.GetCleanControl(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewMx()
        {
            return PartialView("TMixStep");
        }

        [HttpPost]
        public JsonResult GetMixStep(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetMixStep(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewPkCC() {
            return PartialView("TPKCleanControl");
        }
        [HttpPost]
        public JsonResult GetPkCleanControl(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetPkCleanControl(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewPk()
        {
            return PartialView("TPacking");
        }
        [HttpPost]
        public JsonResult GetPacking(String itemCode, String revision) {
            return Json(new { data = runJob.GetPacking(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewBcrCC()
        {
            return PartialView("TBCRCleanControl");
        }
        [HttpPost]
        public JsonResult GetBcrCleanControl(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetBcrCleanControl(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewBcr() {
            return PartialView("TBCR");
        }
        [HttpPost]
        public JsonResult GetBcr(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetBcr(itemCode, revision) });
        }
        [HttpPost]
        public PartialViewResult GetPartailViewBcaCC()
        {
            return PartialView("TBCACleanControl");
        }
        [HttpPost]
        public JsonResult GetBcaCleanControl(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetBcaCleanControl(itemCode, revision) }); 
        }
        [HttpPost]
        public PartialViewResult GetPartailViewBca()
        {
            return PartialView("TBCA");
        }
        [HttpPost]
        public JsonResult GetBca(String itemCode, String revision)
        {
            return Json(new { data = runJob.GetBca(itemCode, revision) }); 
        }
        [HttpPost]
        public JsonResult CreateRunJob(String itemCode,String itemName, String lot, String revision, Double batchSize,String uom,String remark,String startDt,String endDt) {
            runJob.InsertRunJob(itemCode,itemName, lot, revision, batchSize,uom,remark, Convert.ToInt64(Session["USERID"]),startDt,endDt);
            return Json("1");
        }

     
        [HttpPost]
        public JsonResult GetItemName(String itemCode)
        {
            return Json(runJob.GetItemNameFGByItem(itemCode));
        }
        [HttpPost]
        public JsonResult GetItem(String itemName)
        {
            return Json(new { GetItemFG = runJob.GetItemMaster() });
        }
    }
}