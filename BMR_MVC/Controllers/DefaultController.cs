using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    //[SessionExpireFilter]
    public class DefaultController : Controller
    {
        // GET: Default
        //[OutputCache(Duration =60)]
        Mixing mixing;
        BmrVersionInfo BmrVersion;
        String item_Code;
        ActionResult view;
        public DefaultController()
        {

            mixing = new Mixing();


        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Main
        public ActionResult index()
        {
            if (Session["USERID"] != null)
            {
                view = View(mixing.GetJob());
            }
            else
            {
                view = RedirectToAction("index", "Login");
            }
            return view;
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("index", "Login");
        }
        [HttpPost]
        public JsonResult GetItem(String itemCode)
        {
            return Json(new { GetItemFG = mixing.GetItemMaster() });
        }
        [HttpPost]
        public JsonResult GetJob()
        {
            return Json(new { data = mixing.GetJob() });
        }
        [HttpPost]
        public JsonResult GetRunJobHead(Int64 jobSysId) {
            return Json(new { GetRunJobHead = mixing.GetRunJobHead(jobSysId) });
        }
        [HttpPost]
        public JsonResult UpdateRunJobHead(Int64 jobSysId,String lot,Double batchSize,String uom,String startDt,String endDt,String remark) {
            mixing.UpdateRunJobHead(jobSysId, lot, batchSize, uom, startDt, endDt, remark);
            return Json("1");
        }
        [HttpPost]
        public JsonResult ApprPdJob(Int64 jobSysId,String password)
        {
            return Json(mixing.ApprPdJobCheck(jobSysId,password));
        }
        [HttpPost]
        public JsonResult ApprQaJob(Int64 jobSysId, String password)
        {
            return Json(mixing.ApprQaJobCheck(jobSysId, password));
        }
        [HttpPost]
        public JsonResult DeleteJob(Int64 jobSysId)
        {
            mixing.DeleteJob(jobSysId);
            return Json("1");
        }
        public PartialViewResult TMain()
        {
            return PartialView("TMain", mixing.GetJob());
        }
        public PartialViewResult ModalTMainTime()
        {
            return PartialView("ModalTMainTime");
        }
        [HttpPost]
        public JsonResult InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo) {
            mixing.InsertRunJobCC(listAddCleanControlInfo);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Modal
        //Temp
        [HttpPost]
        public JsonResult SaveTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp)
        {
            mixing.InsertTemp(jobSysid, step, runNo, temp, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(mixing.GetTemp(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Pressure
        [HttpPost]
        public JsonResult SavePressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure)
        {
            mixing.InsertPressure(jobSysid, step, runNo, pressure, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetPressure(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(mixing.GetPressure(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Humidity
        [HttpPost]
        public JsonResult SaveHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double humidity)
        {
            mixing.InsertHumidity(jobSysid, step, runNo, humidity, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(mixing.GetHumidity(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Time
        [HttpPost]
        public JsonResult SaveTime(Int64 jobSysid, Int64 step, Int64 runNo, List<ModalTimeInfo> listModalTimeInfos, Int64 lengthTime)
        {
            mixing.InsertTime(jobSysid, step, runNo, listModalTimeInfos, Convert.ToInt64(Session["USERID"]), lengthTime);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetTime = mixing.GetTime(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight
        [HttpPost]
        public JsonResult SaveWeight(Int64 jobSysid, Int64 step, Int64 runNo, Double sTotalWeight, Double sTheoretical, Double sYield, List<ModalWeightInfo> listModalWeightInfos, Int64 lengthContainner)
        {
            mixing.InsertWeight(jobSysid, step, runNo, sTotalWeight, sTheoretical, sYield, listModalWeightInfos, Convert.ToInt64(Session["USERID"]), lengthContainner);
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeight(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(new { GetWeight = mixing.GetWeight(jobSysid, step, runNo) });
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Vacuum
        [HttpPost]
        public JsonResult SaveVacuum(Int64 jobSysid, Int64 step, Int64 runNo, Double vacuum)
        {
            mixing.InsertVacuum(jobSysid, step, runNo, vacuum, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetVacuum(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(mixing.GetVacuum(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Weight Sample
        [HttpPost]
        public JsonResult SaveWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight)
        {
            mixing.InsertInsertWeightOfSample(jobSysid, step, runNo, weight, Convert.ToInt64(Session["USERID"]));
            return Json("1");
        }
        [HttpPost]
        public JsonResult GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            return Json(mixing.GetWeightOfSample(jobSysid, step, runNo));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Image
        //CC
        [HttpPost]
        public JsonResult CCGetImage(Int64 jobSysid, Int64 runNo, String itemCode, String lot)
        {
            return Json(new { GetImage = mixing.CCGetImage(jobSysid,runNo,itemCode, lot) });
        }
        [HttpPost]
        public JsonResult CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            mixing.CCUploadImage(files, model, remark, jobid, jobrunno, itemCode, lot, name_url);
            return Json("1");
        }
        //MX
        [HttpPost]
        public JsonResult GetImage(Int64 jobSysid, Int64 step, Int64 runNo,String itemCode,String lot)
        {
            return Json(new { GetImage = mixing.GetImage(jobSysid, step, runNo,itemCode,lot) });

        }
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno,String itemCode,String lot, String name_url) {
            mixing.UploadImage(files, model, remark, jobid, jobstep, jobrunno,itemCode,lot, name_url);
            return Json("1");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Operate - Check Main Table
        //CC
        [HttpPost]
        public JsonResult CheckMxCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {

            return Json(mixing.CheckMxCCCleanCheck(jobSysId, runNo, password));
        }
        [HttpPost]
        public JsonResult CheckMxCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            return Json(mixing.CheckMxCCCheckCheck(jobSysId, runNo, password));
        }
        //MX
        [HttpPost]
        public JsonResult CheckMxOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(mixing.CheckMxOperateCheck(jobSysId, step, runNo, password));
        }
        [HttpPost]
        public JsonResult CheckMxCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            return Json(mixing.CheckMxCheckCheck(jobSysId, step, runNo, password));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Modal Operate - Check
        //Weight
        [HttpPost]
        public JsonResult WeightOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password) {
            return Json(mixing.WeightOperateCheck(jobSysId,step,runNo,listNo,password));
        }
        [HttpPost]
        public JsonResult WeightCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(mixing.WeightCheckCheck(jobSysId, step, runNo, listNo, password));
        }
        //Time
        [HttpPost]
        public JsonResult TimeOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password) {
            return Json(mixing.TimeOperateCheck(jobSysId, step, runNo, listNo, password));
        }
        [HttpPost]
        public JsonResult TimeCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            return Json(mixing.TimeCheckCheck(jobSysId, step, runNo, listNo, password));

        }

    }
}