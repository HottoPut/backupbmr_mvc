using BMR_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Controllers
{
    public class ImportExcelController : Controller
    {

        ImportExcel importExcel;
        ActionResult view;
        public ImportExcelController() {
            importExcel = new ImportExcel();
        }

        // GET: Test
        public ActionResult Index()
        {
            if (Session["USERID"] != null)
            {
                view = View();
            }
            else {
                view = RedirectToAction("index", "Login");
            }
            return view;
        }
        [HttpPost]
        public JsonResult GetGroupDetail() {
            return Json(new { Group = importExcel.GetGroup() });
        }
        [HttpPost]
        public PartialViewResult UploadExcel(HttpPostedFileBase file)
        {
            return PartialView("TExcel", importExcel.GetExcel(file));
        }
        [HttpPost]
        public JsonResult GetItem(String itemCode)
        {
            return Json(new { GetItemFG = importExcel.GetItemMaster() });
        }
        [HttpPost]
        public JsonResult GetItemName(String itemCode)
        {
            return Json(new { ItemName = importExcel.GetItemNameFGByItem(itemCode) });
        }
        public ActionResult TImportExcel(HttpPostedFileBase file)
        {
            ImportExcel importExcel = new ImportExcel();
            importExcel.GetExcel(file);

            //GetExcel(file);
            ViewBag.Message = "Welcome to my demo!";
            ViewModel mymodel = new ViewModel();
            mymodel.ExcelCleanControlRecordInfoss = importExcel.GetExcel(file).ExcelCleanControlRecordInfo;
            mymodel.ExcelMixStepInfoss = importExcel.GetExcel(file).ExcelMixStepInfo;
            mymodel.ExcelPKCleanControlRecordInfos = importExcel.GetExcel(file).excelPKCleanControlRecordInfo;
            mymodel.ExcelPKProcedureInfos = importExcel.GetExcel(file).excelPKProcedureInfo;
            mymodel.ExcelBcrCleanControlRecordInfos = importExcel.GetExcel(file).excelBcrCleanControlRecordInfo;
            mymodel.ExcelBcrProcedureInfos = importExcel.GetExcel(file).excelBcrProcedureInfo;
            mymodel.ExcelBcaCleanControls = importExcel.GetExcel(file).excelBcaCleanControlRecordInfo;
            mymodel.ExcelBcaProcedureInfos = importExcel.GetExcel(file).excelBcaProcedureInfo;

            //mymodel.ExcelBcCleanControlRecordInfos = impo
            //mymodel.ExcelCleanControlRecordInfoss = listExcelCleanControlRecordInfos;
            //mymodel.ExcelMixStepInfoss = listExcelMixStepInfos;
            return View(mymodel);
        }

        [HttpPost]
        public JsonResult SaveTExcel(String itemCode, String itemName, String start_dt, String end_dt,String revision, List<ExcelCleanControlRecordInfo> listExcelCleanControlRecordInfos, List<ExcelMixStepInfo> listExcelMixStepInfos, List<ExcelPKCleanControlRecordInfo> listExcelPKCleanControlRecordInfo, List<ExcelPKProcedureInfo> listExcelPKProcedureInfo, List<ExcelBcrCleanControlRecordInfo> listExcelBcrCleanControlRecordInfo, List<ExcelBcrProcedureInfo> listExcelBcrProcedure, List<ExcelBcaCleanControlRecordInfo> listExcelBcaCleanControlRecordInfo, List<ExcelBcaProcedureInfo> listExcelBcaProcedureInfo)
        {

           
            return Json(importExcel.SaveExcel(itemCode, itemName, start_dt, end_dt, revision, listExcelCleanControlRecordInfos, listExcelMixStepInfos, listExcelPKCleanControlRecordInfo, listExcelPKProcedureInfo, listExcelBcrCleanControlRecordInfo, listExcelBcrProcedure, listExcelBcaCleanControlRecordInfo, listExcelBcaProcedureInfo));
        }
        [HttpPost]
        public JsonResult GetRevision(String itemCode) {
            return Json(importExcel.GetRevision(itemCode));
        }
       


    }
}