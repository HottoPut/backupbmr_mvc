using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class PackingController : Controller
    {
        // GET: Packing
        ActionResult view;
        Packing packing;
        public PackingController()
        {
            packing = new Packing();

        }
        public ActionResult Index()
        {

            if (Session["USERID"] != null)
            {
                view = View();
            }
            else
            {
                view = RedirectToAction("index", "Login");
            }
            return view;
        }

        [HttpPost]
        public JsonResult GetJob()
        {
            return Json(new { data = packing.GetJob() });
        }
        [HttpPost]
        public JsonResult ApprPdJob(Int64 jobSysId, String password)
        {
            return Json(packing.ApprPdJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult ApprQaJob(Int64 jobSysId, String password)
        {
            return Json(packing.ApprQaJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {
            packing.InsertRunJobCC(listAddCleanControlInfo);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Modal
        //Temp
        [HttpPost]
        public JsonResult SaveTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp)
        {
            packing.InsertTemp(jobSysid, step, runNo, temp, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(packing.GetTemp(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Pressure
        [HttpPost]
        public JsonResult SavePressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure)
        {
            packing.InsertPressure(jobSysid, step, runNo, pressure, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetPressure(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(packing.GetPressure(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Humidity
        [HttpPost]
        public JsonResult SaveHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double humidity)
        {
            packing.InsertHumidity(jobSysid, step, runNo, humidity, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(packing.GetHumidity(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Time
        [HttpPost]
        public JsonResult SaveTime(Int64 jobSysid, Int64 step, Int64 runNo, String fnMxDt, String stFilDt, String fnFilDt, String fnLabelDt, String fnCartonDt)
        {
            packing.InsertTime(jobSysid, step, runNo, fnMxDt, stFilDt, fnFilDt, fnLabelDt, fnCartonDt, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetTime = packing.GetTime(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Image
        //CC
        [HttpPost]
        public JsonResult CCGetImage(Int64 jobSysid,Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = packing.CCGetImage(jobSysid, runNo, itemCode, lot) });
        }
        [HttpPost]
        public JsonResult CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            packing.CCUploadImage(files, model, remark, jobid, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        //M
        [HttpPost]
        public JsonResult GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = packing.GetImage(jobSysid, step, runNo, itemCode, lot) });

        }
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno, String itemCode, String lot, String name_url)
        {
            packing.UploadImage(files, model, remark, jobid, jobstep, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight Sample
        [HttpPost]
        public JsonResult SaveWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight)
        {
            packing.InsertWeightOfSample(jobSysid, step, runNo, weight, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(packing.GetWeightOfSample(jobSysid, step, runNo));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Clean
        [HttpPost]
        public JsonResult SaveClean(Int64 jobSysid, Int64 step, Int64 runNo, Double amountBot, Double waterTemp, String botStartDt, String botEndDt, Double amountLid, String lidStartDt, String lidEndDt, Double amountBakeLid, Double icbtTemp, String bakeStartDt, String bakeEndDt) {
            packing.InsertClean(jobSysid,step,runNo,amountBot,waterTemp,botStartDt,botEndDt,amountLid,lidStartDt,lidEndDt,amountBakeLid,icbtTemp,bakeStartDt,bakeEndDt);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetClean(Int64 jobSysid, Int64 step, Int64 runNo) {
            return Json(new { GetClean = packing.GetClean(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Operate - Check Main Table - Clean Control
        //CC
        [HttpPost]
        public JsonResult CheckPkCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(packing.CheckPkCCCleanCheck(jobSysId, runNo, password));
        }
        //Check
        [HttpPost]
        public JsonResult CheckPkCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(packing.CheckPkCCCheckCheck(jobSysId, runNo, password));
        }
        //Packing operate
        [HttpPost]
        public JsonResult CheckPkOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(packing.CheckPkOperateCheck(jobSysId, step, runNo, password));
        }
        //Packing Check
        [HttpPost]
        public JsonResult CheckPkCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(packing.CheckPkCheckCheck(jobSysId, step, runNo, password));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}