using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BMR_MVC.Models
{
    public class ImportExcel
    {

        String filePath;
        String extension;
        String conString;
        String[] sheet;
        String path;
        String status;
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();
        Int32 status_n;

        //SQl server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;

        OleDbCommand cmdExcel;
        OleDbDataReader oleDbDataReader;
        OleDbConnection connExcel;

        ExcelCleanControlRecordInfo excelCleanControlRecordInfo;
        ExcelMixStepInfo excelMixStepInfo;
        ExcelInfo excelInfo;
        ExcelPKCleanControlRecordInfo excelPKCleanControlRecordInfo;
        ExcelPKProcedureInfo excelPKProcedureInfo;
        ExcelBcrCleanControlRecordInfo excelBcrCleanControlRecordInfo;
        ExcelBcrProcedureInfo excelBcrProcedureInfo;
        ExcelBcaCleanControlRecordInfo excelBcaCleanControlRecordInfo;
        ExcelBcaProcedureInfo excelBcaProcedureInfo;


        List<ExcelCleanControlRecordInfo> listExcelCleanControlRecordInfos;
        List<ExcelMixStepInfo> listExcelMixStepInfos;
        List<ItemMasterInfo> listItemMasterInfos;
        List<BmrVersionInfo> listbmrVersionInfos;
        List<GroupDetialInfo> listGroupDetialInfos;
        List<ExcelPKCleanControlRecordInfo> listExcelPKCleanControlRecordInfo;
        List<ExcelPKProcedureInfo> listExcelPKProcedureInfo;
        List<ExcelBcrCleanControlRecordInfo> listExcelBcrCleanControlRecordInfo;
        List<ExcelBcrProcedureInfo> listExcelBcrProcedureInfo;
        List<ExcelBcaCleanControlRecordInfo> listExcelBcaCleanControlRecordInfo;
        List<ExcelBcaProcedureInfo> listExcelBcaProcedureInfo;
        BmrVersionInfo bmrVersionInfo;
        ItemMasterInfo itemMasterInfo;
        DataTable dtExcelSchema;
        HttpPostedFileBase file;
        QuerMixing query;
        QueryImportExcel queryImportExcel;
        GroupDetialInfo groupDetialInfo;



        string oradb = "Data Source=(DESCRIPTION="
                 + "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.7)(PORT=1521))"
                 + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
                 + "User Id=mmnew;Password=mmnew;";

        public ImportExcel()
        {
            connSQL = new SqlConnection(conStrSQL);
            query = new QuerMixing();
            queryImportExcel = new QueryImportExcel();
            //GetExcel(file);
            //file = this.file;
            //path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            //connExcel = new OleDbConnection(conString);
            //filePath = path + Path.GetFileName(file.FileName);
            //extension = Path.GetExtension(file.FileName);

            //switch (extension)
            //{
            //    case ".xls": //Excel 97-03.
            //        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
            //        break;
            //    case ".xlsx": //Excel 07 and above.
            //        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
            //        break;
            //}
            //conString = String.Format(conString, filePath);
        }

        public List<GroupDetialInfo> GetGroup()
        {
            listGroupDetialInfos = new List<GroupDetialInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryGetGroup(), connSQL);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    groupDetialInfo = new GroupDetialInfo
                    {
                        gdi_group_id = readerSQL["GROUP_ID"].ToString(),
                        gdi_group_name = readerSQL["GROUP_NAME"].ToString(),
                        gdi_group_desc = readerSQL["GROUP_DESC"].ToString()
                    };
                    listGroupDetialInfos.Add(groupDetialInfo);
                }
            }
            connSQL.Close();
            return listGroupDetialInfos;
        }

        public Int32 SaveExcel(String itemCode, String itemName, String start_dt, String end_dt,String revision, List<ExcelCleanControlRecordInfo> listExcelCleanControlRecordInfos, List<ExcelMixStepInfo> listExcelMixStepInfos, List<ExcelPKCleanControlRecordInfo> listExcelPKCleanControlRecordInfo, List<ExcelPKProcedureInfo> listExcelPKProcedureInfo, List<ExcelBcrCleanControlRecordInfo> listExcelBcrCleanControlRecordInfo, List<ExcelBcrProcedureInfo> listExcelBcrProcedure, List<ExcelBcaCleanControlRecordInfo> listExcelBcaCleanControlRecordInfo, List<ExcelBcaProcedureInfo> listExcelBcaProcedureInfo)
        {

            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];
            status_n = 0;
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryInsertHeadBMR(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_ITEM_NAME", itemName);
            cmdSql.Parameters.AddWithValue("@P_START_DT", start_dt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", end_dt);
            cmdSql.Parameters.AddWithValue("@P_BMR_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_BMR_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_BMR_RV_DATE", rvDt);
            cmdSql.Parameters.AddWithValue("@P_USR_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                status_n = Convert.ToInt32(exe.ToString());
                cmdSql.Dispose();

                if (status_n == 1)
                {
                    if (listExcelCleanControlRecordInfos != null)
                    {
                        foreach (var item in listExcelCleanControlRecordInfos)
                        {
                            cmdSql = new SqlCommand(query.QuerySaveExcelCleanControlRecord(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo);
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NO", item.equipmentNo);
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NAME", item.equipmentName);
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_USER_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelMixStepInfos != null)
                    {
                        foreach (var item in listExcelMixStepInfos)
                        {
                            cmdSql = new SqlCommand(query.QuerySaveExcelMixStep(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : "0");
                            cmdSql.Parameters.AddWithValue("@P_STEP_NO", item.step);
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC", item.stepDesc != null ? item.stepDesc : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC_ENG", item.stepDescEng != null ? item.stepDescEng : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_HUMUDITY_YN", item.reqHumidityYn != null ? item.reqHumidityYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_PRESS_YN", item.reqPressYn != null ? item.reqPressYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_TEMP_YN", item.reqTempYn != null ? item.reqTempYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_VACC_YN", item.reqVaccYn != null ? item.reqVaccYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_STAR_STOP_YN", item.reqStartStopYn != null ? item.reqStartStopYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_YN", item.reqWeightYn != null ? item.reqWeightYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STANDARD_W", item.standardW != null ? item.standardW : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_SAMPLE_YN", item.reqWeightSampleYn != null ? item.reqWeightSampleYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_USER_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelPKCleanControlRecordInfo != null)
                    {
                        foreach (var item in listExcelPKCleanControlRecordInfo)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertClPK(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NO", item.equipmentNo != null ? item.equipmentNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NAME", item.equipmentName != null ? item.equipmentName : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_USER_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelPKProcedureInfo != null)
                    {
                        foreach (var item in listExcelPKProcedureInfo)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertPK(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP", item.step != null ? item.step : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC", item.stepDesc != null ? item.stepDesc : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC_ENG", item.stepDescEng != null ? item.stepDescEng : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_HUMUDITY_YN", item.reqHumidityYn != null ? item.reqHumidityYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_PRESS_YN", item.reqPressYn != null ? item.reqPressYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_TEMP_YN", item.reqTempYn != null ? item.reqTempYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_VACC_YN", item.reqVaccYn != null ? item.reqVaccYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_STAR_STOP_YN", item.reqStartStopYn != null ? item.reqStartStopYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_YN", item.reqWeightYn != null ? item.reqWeightYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STANDARD_W", item.standardW != null ? item.standardW : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_SAMPLE_YN", item.reqWeightSampleYn != null ? item.reqWeightSampleYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_CLEAN_YN", item.reqCleanYn != null ? item.reqCleanYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelBcrCleanControlRecordInfo != null)
                    {
                        foreach (var item in listExcelBcrCleanControlRecordInfo)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertClBCR(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NO", item.equipmentNo != null ? item.equipmentNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NAME", item.equipmentName != null ? item.equipmentName : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_USER_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelBcrProcedure != null)
                    {
                        foreach (var item in listExcelBcrProcedure)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertBCR(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP", item.step != null ? item.step : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC", item.stepDesc != null ? item.stepDesc : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC_ENG", item.stepDescEng != null ? item.stepDescEng : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_HUMUDITY_YN", item.reqHumidityYn != null ? item.reqHumidityYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_PRESS_YN", item.reqPressYn != null ? item.reqPressYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_TEMP_YN", item.reqTempYn != null ? item.reqTempYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_VACC_YN", item.reqVaccYn != null ? item.reqVaccYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_STAR_STOP_YN", item.reqStartStopYn != null ? item.reqStartStopYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_YN", item.reqWeightYn != null ? item.reqWeightYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STANDARD_W", item.standardW != null ? item.standardW : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_SAMPLE_YN", item.reqWeightSampleYn != null ? item.reqWeightSampleYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelBcaCleanControlRecordInfo != null)
                    {
                        foreach (var item in listExcelBcaCleanControlRecordInfo)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertClBCA(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NO", item.equipmentNo != null ? item.equipmentNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NAME", item.equipmentName != null ? item.equipmentName : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_USER_ID", Convert.ToInt64(HttpContext.Current.Session["USERID"]));
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                    if (listExcelBcaProcedureInfo != null)
                    {
                        foreach (var item in listExcelBcaProcedureInfo)
                        {
                            cmdSql = new SqlCommand(queryImportExcel.InsertBCA(), connSQL);
                            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper() != null ? itemCode.ToUpper() : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo != null ? item.runNo : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP", item.step != null ? item.step : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC", item.stepDesc != null ? item.stepDesc : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STEP_DESC_ENG", item.stepDescEng != null ? item.stepDescEng : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn != null ? item.reqImageYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_HUMUDITY_YN", item.reqHumidityYn != null ? item.reqHumidityYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_PRESS_YN", item.reqPressYn != null ? item.reqPressYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_TEMP_YN", item.reqTempYn != null ? item.reqTempYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_VACC_YN", item.reqVaccYn != null ? item.reqVaccYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_STAR_STOP_YN", item.reqStartStopYn != null ? item.reqStartStopYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_YN", item.reqWeightYn != null ? item.reqWeightYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_STANDARD_W", item.standardW != null ? item.standardW : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_REQ_WEIGHT_SAMPLE_YN", item.reqWeightSampleYn != null ? item.reqWeightSampleYn : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE_ID", item.groupOp != null ? item.groupOp : DBNull.Value.ToString());
                            cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK_ID", item.groupChk != null ? item.groupChk : DBNull.Value.ToString());
                            cmdSql.ExecuteNonQuery();
                        }
                    }
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return status_n;
        }

        public ExcelInfo GetExcel(HttpPostedFileBase file)
        {

            path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            connExcel = new OleDbConnection(conString);
            filePath = path + Path.GetFileName(file.FileName);
            extension = Path.GetExtension(file.FileName);
            file.SaveAs(filePath);
            switch (extension)
            {
                //case ".xls": //Excel 97-03.
                //    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                //    break;
                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }
            conString = String.Format(conString, filePath);


            listExcelCleanControlRecordInfos = new List<ExcelCleanControlRecordInfo>();
            listExcelMixStepInfos = new List<ExcelMixStepInfo>();
            listExcelPKCleanControlRecordInfo = new List<ExcelPKCleanControlRecordInfo>();
            listExcelPKProcedureInfo = new List<ExcelPKProcedureInfo>();
            listExcelBcrCleanControlRecordInfo = new List<ExcelBcrCleanControlRecordInfo>();
            listExcelBcrProcedureInfo = new List<ExcelBcrProcedureInfo>();
            listExcelBcaCleanControlRecordInfo = new List<ExcelBcaCleanControlRecordInfo>();
            listExcelBcaProcedureInfo = new List<ExcelBcaProcedureInfo>();

            connExcel = new OleDbConnection(conString);
            connExcel.Open();
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            String[] sheet = new String[dtExcelSchema.Rows.Count];
            for (int i = 0; i < sheet.Length; i++)
            {
                sheet[i] = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                if (sheet[i] == "MIXING_CLEAN_COTROL$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["RUN_NO"].ToString().Length > 0)
                            {
                                excelCleanControlRecordInfo = new ExcelCleanControlRecordInfo
                                {
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    equipmentNo = oleDbDataReader["EQUIPMENT_NO"].ToString(),
                                    equipmentName = oleDbDataReader["EQUIPMENT_NAME"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_CLEAN_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                                listExcelCleanControlRecordInfos.Add(excelCleanControlRecordInfo);
                            }
                        }
                    }
                    cmdExcel.Dispose();
                }
                if (sheet[i] == "MIXING_PROCEDURE$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["STEP"].ToString().Length > 0)
                            {
                                excelMixStepInfo = new ExcelMixStepInfo
                                {
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    step = oleDbDataReader["STEP"].ToString(),
                                    stepDesc = oleDbDataReader["STEP_DESC"].ToString(),
                                    stepDescEng = oleDbDataReader["STEP_DESC_ENG"].ToString(),
                                    reqHumidityYn = oleDbDataReader["REQ_HUMUDITY_YN"].ToString().ToUpper(),
                                    reqPressYn = oleDbDataReader["REQ_PRESS_YN"].ToString().ToUpper(),
                                    reqTempYn = oleDbDataReader["REQ_TEMP_YN"].ToString().ToUpper(),
                                    reqVaccYn = oleDbDataReader["REQ_VACC_YN"].ToString().ToUpper(),
                                    reqStartStopYn = oleDbDataReader["REQ_START_STOP_YN"].ToString().ToUpper(),
                                    standardW = oleDbDataReader["STANDARD_W"].ToString(),
                                    reqWeightSampleYn = oleDbDataReader["REQ_AMOUNT_SAMPLE_YN"].ToString().ToUpper(),
                                    reqWeightYn = oleDbDataReader["REQ_WEIGHT_YN"].ToString().ToUpper(),
                                    groupOp = oleDbDataReader["GROUP_OPERATE_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                                listExcelMixStepInfos.Add(excelMixStepInfo);
                            }
                        }
                    }
                }
                if (sheet[i] == "PACKING_CLEAN_CONTROL$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["RUN_NO"].ToString().Length > 0)
                            {
                                excelPKCleanControlRecordInfo = new ExcelPKCleanControlRecordInfo
                                {
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    equipmentNo = oleDbDataReader["EQUIPMENT_NO"].ToString(),
                                    equipmentName = oleDbDataReader["EQUIPMENT_NAME"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_CLEAN_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                                listExcelPKCleanControlRecordInfo.Add(excelPKCleanControlRecordInfo);
                            }
                        }
                    }
                }
                if (sheet[i] == "PACKING_PROCEDURE$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["STEP"].ToString().Length > 0)
                            {
                                excelPKProcedureInfo = new ExcelPKProcedureInfo
                                {
                                    step = oleDbDataReader["STEP"].ToString(),
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    stepDesc = oleDbDataReader["STEP_DESC"].ToString(),
                                    stepDescEng = oleDbDataReader["STEP_DESC_ENG"].ToString(),
                                    reqHumidityYn = oleDbDataReader["REQ_HUMUDITY_YN"].ToString(),
                                    reqPressYn = oleDbDataReader["REQ_PRESS_YN"].ToString(),
                                    reqTempYn = oleDbDataReader["REQ_TEMP_YN"].ToString(),
                                    reqVaccYn = oleDbDataReader["REQ_VACC_YN"].ToString(),
                                    reqStartStopYn = oleDbDataReader["REQ_START_STOP_YN"].ToString(),
                                    reqWeightYn = oleDbDataReader["REQ_WEIGHT_YN"].ToString(),
                                    reqWeightSampleYn = oleDbDataReader["REQ_AMOUNT_SAMPLE_YN"].ToString(),
                                    reqCleanYn = oleDbDataReader["REQ_CLEAN_YN"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_OPERATE_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                            }
                            listExcelPKProcedureInfo.Add(excelPKProcedureInfo);
                        }

                    }
                }
                if (sheet[i] == "CORED_CLEAN_CONTROL$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["RUN_NO"].ToString().Length > 0)
                            {
                                excelBcrCleanControlRecordInfo = new ExcelBcrCleanControlRecordInfo
                                {
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    equipmentNo = oleDbDataReader["EQUIPMENT_NO"].ToString(),
                                    equipmentName = oleDbDataReader["EQUIPMENT_NAME"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_CLEAN_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                                listExcelBcrCleanControlRecordInfo.Add(excelBcrCleanControlRecordInfo);
                            }
                        }
                    }
                }
                if (sheet[i] == "CORED_PROCEDURE$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["STEP"].ToString().Length > 0)
                            {
                                excelBcrProcedureInfo = new ExcelBcrProcedureInfo
                                {
                                    step = oleDbDataReader["STEP"].ToString(),
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    stepDesc = oleDbDataReader["STEP_DESC"].ToString(),
                                    stepDescEng = oleDbDataReader["STEP_DESC_ENG"].ToString(),
                                    reqHumidityYn = oleDbDataReader["REQ_HUMUDITY_YN"].ToString(),
                                    reqPressYn = oleDbDataReader["REQ_PRESS_YN"].ToString(),
                                    reqTempYn = oleDbDataReader["REQ_TEMP_YN"].ToString(),
                                    reqStartStopYn = oleDbDataReader["REQ_START_STOP_YN"].ToString(),
                                    reqWeightYn = oleDbDataReader["REQ_WEIGHT_YN"].ToString(),
                                    reqWeightSampleYn = oleDbDataReader["REQ_AMOUNT_SAMPLE_YN"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_OPERATE_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                            }
                            listExcelBcrProcedureInfo.Add(excelBcrProcedureInfo);
                        }

                    }
                }

                if (sheet[i] == "COATED_CLEAN_CONTROL$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["RUN_NO"].ToString().Length > 0)
                            {
                                excelBcaCleanControlRecordInfo = new ExcelBcaCleanControlRecordInfo
                                {
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    equipmentNo = oleDbDataReader["EQUIPMENT_NO"].ToString(),
                                    equipmentName = oleDbDataReader["EQUIPMENT_NAME"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_CLEAN_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_CHECK_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                                listExcelBcaCleanControlRecordInfo.Add(excelBcaCleanControlRecordInfo);
                            }
                        }
                    }
                }
                if (sheet[i] == "COATED_PROCEDURE$")
                {
                    cmdExcel = new OleDbCommand("SELECT * From [" + sheet[i] + "]", connExcel);
                    oleDbDataReader = cmdExcel.ExecuteReader();
                    if (oleDbDataReader.HasRows)
                    {
                        while (oleDbDataReader.Read())
                        {
                            if (oleDbDataReader["STEP"].ToString().Length > 0)
                            {
                                excelBcaProcedureInfo = new ExcelBcaProcedureInfo
                                {
                                    step = oleDbDataReader["STEP"].ToString(),
                                    runNo = oleDbDataReader["RUN_NO"].ToString(),
                                    stepDesc = oleDbDataReader["STEP_DESC"].ToString(),
                                    stepDescEng = oleDbDataReader["STEP_DESC_ENG"].ToString(),
                                    reqHumidityYn = oleDbDataReader["REQ_HUMUDITY_YN"].ToString(),
                                    reqPressYn = oleDbDataReader["REQ_PRESS_YN"].ToString(),
                                    reqTempYn = oleDbDataReader["REQ_TEMP_YN"].ToString(),
                                    reqStartStopYn = oleDbDataReader["REQ_START_STOP_YN"].ToString(),
                                    reqWeightYn = oleDbDataReader["REQ_WEIGHT_YN"].ToString(),
                                    reqWeightSampleYn = oleDbDataReader["REQ_AMOUNT_SAMPLE_YN"].ToString(),
                                    groupOp = oleDbDataReader["GROUP_OPERATE_ID"].ToString(),
                                    groupChk = oleDbDataReader["GROUP_OPERATE_ID"].ToString(),
                                    reqImageYn = oleDbDataReader["REQ_IMAGE_YN"].ToString()
                                };
                            }
                            listExcelBcaProcedureInfo.Add(excelBcaProcedureInfo);
                        }

                    }
                }

            }
            connExcel.Close();

            return excelInfo = new ExcelInfo
            {
                ExcelCleanControlRecordInfo = listExcelCleanControlRecordInfos,
                ExcelMixStepInfo = listExcelMixStepInfos,
                excelPKCleanControlRecordInfo = listExcelPKCleanControlRecordInfo,
                excelPKProcedureInfo = listExcelPKProcedureInfo,
                excelBcrCleanControlRecordInfo = listExcelBcrCleanControlRecordInfo,
                excelBcrProcedureInfo = listExcelBcrProcedureInfo,
                excelBcaCleanControlRecordInfo = listExcelBcaCleanControlRecordInfo,
                excelBcaProcedureInfo = listExcelBcaProcedureInfo
            };
        }
        public List<ItemMasterInfo> GetItemMaster()
        {
            listItemMasterInfos = new List<ItemMasterInfo>();
            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();
            OracleCommand cmd = new OracleCommand(query.QueryGetItemFG(), conn);
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    itemMasterInfo = new ItemMasterInfo
                    {
                        itemCode = reader["ITEM_CODE"].ToString(),
                        itemName = reader["ITEM_NAME"].ToString(),
                        itemNameUom = reader["ITEM_CODE_NAME"].ToString()
                    };
                    listItemMasterInfos.Add(itemMasterInfo);
                }
            }
            cmd.Dispose();
            conn.Close();
            return listItemMasterInfos;
        }
        public String GetItemNameFGByItem(String itemCode)
        {
            status = "";
            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();
            OracleCommand cmdExcel = new OracleCommand(query.QueryGetItemNameByItemCodeFG(), conn);
            cmdExcel.Parameters.Add(new OracleParameter("P_ITEM_CODE", itemCode.ToUpper()));
            var exe = cmdExcel.ExecuteScalar();
            if (exe != null)
            {
                status = exe.ToString();
            }
            cmdExcel.Dispose();
            conn.Close();
            return status;
        }
        public String GetRevision(String itemCode) {
            status = "";
            connSQL.Open();
            cmdSql = new SqlCommand(queryImportExcel.QueryGetRevision(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode);
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                status = exe.ToString();
            }
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
    }
}