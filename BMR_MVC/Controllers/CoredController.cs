using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class CoredController : Controller
    {
        // GET: Cored
        Cored cored;
        ActionResult view;
        public CoredController()
        {
            cored = new Cored();
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
            return Json(new { data = cored.GetJob() });
        }
        [HttpPost]
        public JsonResult ApprPdJob(Int64 jobSysId, String password)
        {
            return Json(cored.ApprPdJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult ApprQaJob(Int64 jobSysId, String password)
        {
            return Json(cored.ApprQaJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {
            cored.InsertRunJobCC(listAddCleanControlInfo);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Modal
        //Temp
        [HttpPost]
        public JsonResult SaveTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp)
        {
            cored.InsertTemp(jobSysid, step, runNo, temp, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(cored.GetTemp(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Pressure
        [HttpPost]
        public JsonResult SavePressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure)
        {
            cored.InsertPressure(jobSysid, step, runNo, pressure, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetPressure(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(cored.GetPressure(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Humidity
        [HttpPost]
        public JsonResult SaveHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double humidity)
        {
            cored.InsertHumidity(jobSysid, step, runNo, humidity, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(cored.GetHumidity(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Time
        [HttpPost]
        public JsonResult SaveTime(Int64 jobSysid, Int64 step, Int64 runNo, List<ModalTimeInfo> listModalTimeInfos, Int64 lengthTime)
        {
            cored.InsertTime(jobSysid, step, runNo, listModalTimeInfos, Convert.ToInt64(Session["USERID"]), lengthTime);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetTime = cored.GetTime(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight
        [HttpPost]
        public JsonResult SaveWeight(Int64 jobSysId, Int64 step, Int64 runNo, Double tankAmount, Double net, Double theoretical, Double yield)
        {
            cored.InsertWeight(jobSysId, step, runNo, tankAmount, net, theoretical, yield);
            return Json("1");
        }
       [HttpPost]
       public JsonResult GetWeight(Int64 jobSysId, Int64 step, Int64 runNo)
        {
            return Json(new {GetWeight = cored.GetWeight(jobSysId,step,runNo)});
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight Sample
        [HttpPost]
        public JsonResult SaveWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight)
        {
            cored.InsertInsertWeightOfSample(jobSysid, step, runNo, weight, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(cored.GetWeightOfSample(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Image
        //CC
        [HttpPost]
        public JsonResult CCGetImage(Int64 jobSysid,Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = cored.CCGetImage(jobSysid, runNo, itemCode, lot) });
        }
        [HttpPost]
        public JsonResult CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            cored.CCUploadImage(files, model, remark, jobid, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        //M
        [HttpPost]
        public JsonResult GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = cored.GetImage(jobSysid, step, runNo, itemCode, lot) });

        }
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno, String itemCode, String lot, String name_url)
        {
            cored.UploadImage(files, model, remark, jobid, jobstep, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Operate - Check Main Table - Clean Control
        //CC
        [HttpPost]
        public JsonResult CheckBcrCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(cored.CheckBcrCCCleanCheck(jobSysId, runNo, password));
        }
        //Check
        [HttpPost]
        public JsonResult CheckBcrCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(cored.CheckBcrCCCheckCheck(jobSysId, runNo, password));
        }
        //Bcr operate
        [HttpPost]
        public JsonResult CheckBcrOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(cored.CheckBcrOperateCheck(jobSysId, step, runNo, password));
        }
        //Bcr Check
        [HttpPost]
        public JsonResult CheckBcrCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(cored.CheckBcrCheckCheck(jobSysId, step, runNo, password));
        }
        [HttpPost]
        public JsonResult TimeOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(cored.TimeOperateCheck(jobSysId, step, runNo, listNo, password));
        }
        [HttpPost]
        public JsonResult TimeCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(cored.TimeCheckCheck(jobSysId, step, runNo, listNo, password));

        }
    }
}