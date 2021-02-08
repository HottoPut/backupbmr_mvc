using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class CoatedController : Controller
    {
        // GET: Coated
        Coated coated;
        ActionResult view;
        public CoatedController()
        {
            coated = new Coated();
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
            return Json(new { data = coated.GetJob() });
        }
        [HttpPost]
        public JsonResult ApprPdJob(Int64 jobSysId, String password)
        {
            return Json(coated.ApprPdJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult ApprQaJob(Int64 jobSysId, String password)
        {
            return Json(coated.ApprQaJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {
            coated.InsertRunJobCC(listAddCleanControlInfo);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Modal
        //Temp
        [HttpPost]
        public JsonResult SaveTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp)
        {
            coated.InsertTemp(jobSysid, step, runNo, temp, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(coated.GetTemp(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Pressure
        [HttpPost]
        public JsonResult SavePressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure)
        {
            coated.InsertPressure(jobSysid, step, runNo, pressure, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetPressure(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(coated.GetPressure(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Humidity
        [HttpPost]
        public JsonResult SaveHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double humidity)
        {
            coated.InsertHumidity(jobSysid, step, runNo, humidity, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(coated.GetHumidity(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Time
        [HttpPost]
        public JsonResult SaveTime(Int64 jobSysid, Int64 step, Int64 runNo, List<ModalTimeInfo> listModalTimeInfos, Int64 lengthTime)
        {
            coated.InsertTime(jobSysid, step, runNo, listModalTimeInfos, Convert.ToInt64(Session["USERID"]), lengthTime);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetTime = coated.GetTime(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight
        [HttpPost]
        public JsonResult SaveWeight(Int64 jobSysid, Int64 step, Int64 runNo, Double sTotalWeight, Double sTheoretical, Double sYield, List<ModalWeightInfo> listModalWeightInfos, Int64 lengthContainner)
        {
            coated.InsertWeight(jobSysid, step, runNo, sTotalWeight, sTheoretical, sYield, listModalWeightInfos, Convert.ToInt64(Session["USERID"]), lengthContainner);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeight(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetWeight = coated.GetWeight(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Image
        //CC
        [HttpPost]
        public JsonResult CCGetImage(Int64 jobSysid,Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = coated.CCGetImage(jobSysid, runNo, itemCode, lot) });
        }
        [HttpPost]
        public JsonResult CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            coated.CCUploadImage(files, model, remark, jobid, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = coated.GetImage(jobSysid, step, runNo, itemCode, lot) });

        }
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno, String itemCode, String lot, String name_url)
        {
            coated.UploadImage(files, model, remark, jobid, jobstep, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight Sample
        [HttpPost]
        public JsonResult SaveWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight)
        {
            coated.InsertWeightOfSample(jobSysid, step, runNo, weight, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(coated.GetWeightOfSample(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Operate - Check Main Table - Clean Control
        //CC
        [HttpPost]
        public JsonResult CheckBcaCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(coated.CheckBcaCCCleanCheck(jobSysId, runNo, password));
        }
        //Check
        [HttpPost]
        public JsonResult CheckBcaCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(coated.CheckBcaCCCheckCheck(jobSysId, runNo, password));
        }
        //Bca operate
        [HttpPost]
        public JsonResult CheckBcaOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(coated.CheckBcaOperateCheck(jobSysId, step, runNo, password));
        }
        //Bca Check
        [HttpPost]
        public JsonResult CheckBcaCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(coated.CheckBcaCheckCheck(jobSysId, step, runNo, password));
        }
        [HttpPost]
        public JsonResult TimeOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(coated.TimeOperateCheck(jobSysId, step, runNo, listNo, password));
        }
        [HttpPost]
        public JsonResult TimeCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(coated.TimeCheckCheck(jobSysId, step, runNo, listNo, password));

        }
    }
}