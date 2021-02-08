using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class Packing
    {
        String status;
        Double temp;
        JobPkInfo jobPackingInfo;
        List<JobPkInfo> listJobPackingInfo;
        List<ModalPkTimeInfo> listModalPkTimeInfo;
        List<ReturnCheckInfo> listReturnCheckInfo;
        List<ModalCleanInfo> listModalCleanInfo;

        //Connection String
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();

        //SQL Server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;

        QueryPacking queryPacking;
        ModalPkTimeInfo modalPkTimeInfo;
        Login login;
        ReturnCheckInfo returnCheckInfo;
        ModalImageInfo modalImageInfo;
        ModalCleanInfo modalCleanInfo;

        public Packing()
        {

            connSQL = new SqlConnection(conStrSQL);
            queryPacking = new QueryPacking();
            login = new Login();
        }

        public List<JobPkInfo> GetJob()
        {
            listJobPackingInfo = new List<JobPkInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.getJob(), connSQL);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    jobPackingInfo = new JobPkInfo
                    {
                        jobSysId = readerSQL["JOB_SYS_ID"].ToString(),
                        jobApprPd =  readerSQL["APPR_JOB_PD"].ToString(),
                        jobApprQa = readerSQL["APPR_JOB_QA"].ToString(),
                        itemCode = readerSQL["JOB_ITEM_CODE"].ToString(),
                        itemName = readerSQL["JOB_ITEM_NAME2"].ToString(),
                        lot = readerSQL["JOB_LOT"].ToString(),
                        bmrVersion = readerSQL["BMR_VERSION"].ToString(),
                        startDt = readerSQL["JOB_START_DT"].ToString(),
                        endDt = readerSQL["JOB_END_DT"].ToString(),
                        batchSize = readerSQL["JOB_BATCH_SIZE"].ToString(),
                        status = readerSQL["JOB_STATUS"].ToString(),
                        ccrRunNo = readerSQL["PKR_CC_RUN_NO"].ToString(),
                        ccrEquipmentNo = readerSQL["PKR_CC_EQUIPMENT_NO"].ToString(),
                        ccrEquipmentName = readerSQL["PKR_CC_EQUIPMENT_NAME"].ToString(),
                        ccrReqImageYn = readerSQL["PKR_CC_REQ_IMAGE_YN"].ToString(),
                        pkStep = readerSQL["PKR_STEP_NO"].ToString(),
                        pkRunNo = readerSQL["PKR_RUN_NO"].ToString(),
                        pkStepDesc = readerSQL["PKR_STEP_DESC"].ToString(),
                        userProgress = readerSQL["OPERATE_PERCENT"].ToString(),
                        qaProgress = readerSQL["CHECK_PERCENT"].ToString(),
                        remark = readerSQL["JOB_REMARK"].ToString(),
                        pkGroupOperateId = readerSQL["PKR_GROUP_OPERATE_ID"].ToString(),
                        pkGroupCheckId = readerSQL["PKR_GROUP_CHECK_ID"].ToString(),
                        ccGroupCleanId = readerSQL["PKR_CC_GROUP_CLEAN_ID"].ToString(),
                        ccGroupCheckId = readerSQL["PKR_CC_GROUP_CHECK_ID"].ToString(),
                        reqTempYn = readerSQL["PKR_REQ_TEMP_YN"].ToString(),
                        reqHumidityYn = readerSQL["PKR_REQ_HUMUDITY_YN"].ToString(),
                        reqPressYn = readerSQL["PKR_REQ_PRESS_YN"].ToString(),
                        reqVaccYn = readerSQL["PKR_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["PKR_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["PKR_REQ_WEIGHT_YN"].ToString(),
                        reqWeightSampleYn = readerSQL["PKR_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        reqImageYn = readerSQL["PKR_REQ_IMAGE_YN"].ToString(),
                        reqCleanYn =  readerSQL["PKR_REQ_CLEAN_YN"].ToString(),
                        ccrCleanUserName = readerSQL["USER_NAME_CC_CLEAN"].ToString(),
                        ccrCleanDt = readerSQL["USER_NAME_CC_CLEAN_DT"].ToString(),
                        ccrCheckUserName = readerSQL["USER_NAME_CC_CHECK"].ToString(),
                        ccrCheckDt = readerSQL["USER_NAME_CC_CHECK_DT"].ToString(),
                        pkOperateUserName = readerSQL["USER_NAME_PK_OPERATE"].ToString(),
                        pkOperateDt = readerSQL["USER_NAME_PK_OPERATE_DT"].ToString(),
                        pkCheckUserName = readerSQL["USER_NAME_PK_CHECK"].ToString(),
                        pkCheckDt =  readerSQL["USER_NAME_PK_CHECK_DT"].ToString()
                    };
                    listJobPackingInfo.Add(jobPackingInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listJobPackingInfo;
        }
        public String InsertClean(Int64 jobSysid, Int64 step, Int64 runNo, Double amountBot,Double waterTemp,String botStartDt,String botEndDt,Double amountLid,String lidStartDt,String lidEndDt,Double amountBakeLid,Double icbtTemp,String bakeStartDt,String bakeEndDt)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertClean(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_WATER_TEMP", waterTemp);
            cmdSql.Parameters.AddWithValue("@P_AMOUNT_BOT", amountBot);
            cmdSql.Parameters.AddWithValue("@P_BOT_START_DT", botStartDt);
            cmdSql.Parameters.AddWithValue("@P_BOT_END_DT", botEndDt);
            cmdSql.Parameters.AddWithValue("@P_AMOUNT_LID", amountLid);
            cmdSql.Parameters.AddWithValue("@P_LID_START_DT", lidStartDt);
            cmdSql.Parameters.AddWithValue("@P_LID_END_DT", lidEndDt);
            cmdSql.Parameters.AddWithValue("@P_AMOUNT_BAKE_LID", amountBakeLid);
            cmdSql.Parameters.AddWithValue("@P_ICBT_TEMP", icbtTemp);
            cmdSql.Parameters.AddWithValue("@P_BAKE_START_DT", bakeStartDt);
            cmdSql.Parameters.AddWithValue("@P_BAKE_END_DT", bakeEndDt);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", HttpContext.Current.Session["USERID"]);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public List<ModalCleanInfo> GetClean(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalCleanInfo = new List<ModalCleanInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryGetClean(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalCleanInfo = new ModalCleanInfo {
                        amountBot = readerSQL["PK_RJC_AMOUNT_BOT"].ToString(),
                        waterTemp = readerSQL["PK_RJC_WATER_TEMP"].ToString(),
                        botStartDt = readerSQL["PK_RJC_BOT_START_DT"].ToString(),
                        botEndDt = readerSQL["PK_RJC_BOT_END_DT"].ToString(),
                        amountLid =  readerSQL["PK_RJC_AMOUNT_LID"].ToString(),
                        lidStartDt = readerSQL["PK_RJC_LID_START_DT"].ToString(),
                        lidEndDt =  readerSQL["PK_RJC_LID_END_DT"].ToString(),
                        amountBakeLid = readerSQL["PK_RJC_AMOUNT_BAKE_LID"].ToString(),
                        icbtTemp = readerSQL["PK_RJC_ICBT_TEMP"].ToString(),
                        bakeStartDt =  readerSQL["PK_RJC_BAKE_START_DT"].ToString(),
                        bakeEndDt = readerSQL["PK_RJC_BAKE_END_DT"].ToString()
                    };
                    listModalCleanInfo.Add(modalCleanInfo);
                }
            }
            return listModalCleanInfo;
        }

        public String InsertTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertTemp(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_TEMP", temp);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public Double GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryRunJobGetTemp(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                temp = double.Parse(exe.ToString());
            }
            cmdSql.Dispose();
            connSQL.Close();
            return temp;
        }
        public Double GetPressure(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryRunJobGetPressure(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                temp = double.Parse(exe.ToString());
            }
            cmdSql.Dispose();
            connSQL.Close();
            return temp;
        }
        public String InsertPressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertPressure(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_PRESSURE", pressure);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public String InsertHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertHumidity(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_HUMIDITY", pressure);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public String InsertTime(Int64 jobSysid, Int64 step, Int64 runNo, String fnMxDt, String stFilDt, String fnFilDt, String fnLabelDt, String fnCartonDt, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertTime(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_fnMxDt", fnMxDt);
            cmdSql.Parameters.AddWithValue("@P_stFilDt", stFilDt);
            cmdSql.Parameters.AddWithValue("@P_fnFilDt", fnFilDt);
            cmdSql.Parameters.AddWithValue("@P_fnLabelDt", fnLabelDt);
            cmdSql.Parameters.AddWithValue("@P_fnCartonDt", fnCartonDt);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public List<ModalPkTimeInfo> GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalPkTimeInfo = new List<ModalPkTimeInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryGetTime(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP_NO", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalPkTimeInfo = new ModalPkTimeInfo
                    {
                        fnMxDt = readerSQL["PK_RJDT_FN_MX_DT"].ToString(),
                        stFilDt = readerSQL["PK_RJDT_ST_FIL_DT"].ToString(),
                        fnFilDt = readerSQL["PK_RJDT_FN_FIL_DT"].ToString(),
                        fnLabelDt = readerSQL["PK_RJDT_FN_LABEL_DT"].ToString(),
                        fnCartonDt = readerSQL["PK_RJDT_FN_CARTON_DT"].ToString()
                    };
                    listModalPkTimeInfo.Add(modalPkTimeInfo);
                }
            }
            connSQL.Close();
            return listModalPkTimeInfo;
        }
        public Double GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryRunJobGetHumidity(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                temp = double.Parse(exe.ToString());
            }
            cmdSql.Dispose();
            connSQL.Close();
            return temp;
        }
        public List<ReturnCheckInfo> ApprPdJobCheck(Int64 jobSysId, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.ApprPdJob(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));


            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString(),
                        apprPdStatus = readerSQL["APPR_PD_STATUS"].ToString(),
                        apprQaStatus = readerSQL["APPR_QA_STATUS"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }
        public List<ReturnCheckInfo> ApprQaJobCheck(Int64 jobSysId, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.ApprQaJob(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));


            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString(),
                        apprPdStatus = readerSQL["APPR_PD_STATUS"].ToString(),
                        apprQaStatus = readerSQL["APPR_QA_STATUS"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }
        public List<ReturnCheckInfo> CheckPkCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.CheckPkCCCleanChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }
        public List<ReturnCheckInfo> CheckPkCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.CheckPkCCChkChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }

        public List<ReturnCheckInfo> CheckPkOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.CheckPkOperateChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }

        public List<ReturnCheckInfo> CheckPkCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfo = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.CheckPkChkChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo
                    {
                        status = readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString()
                    };
                    listReturnCheckInfo.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfo;
        }
        public List<ModalImageInfo> GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryRunJobImage(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUNNO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    imageInfo = new ModalImageInfo
                    {
                        imagename = readerSQL["PK_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImagePK/" + itemCode + "/" + lot +"/"+step+"_"+runNo+"/" + readerSQL["PK_RJPT_SYS_ID"].ToString() + "_" + readerSQL["PK_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["PK_RJPT_REMARK"].ToString()
                    };
                    listimageinfo.Add(imageInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listimageinfo;
        }

        public String UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno, String itemCode, String lot, String name_url)
        {
            List<ModalImageInfo> listImageinfo = new List<ModalImageInfo>();
            Int64 user_id = Convert.ToInt64(HttpContext.Current.Session["USERID"]);
            int i = 1;

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImagePK/" + itemCode + "/" + lot + "/"+jobstep+"_"+jobrunno+"/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }
            int count = 0;
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null)
                {
                    int testtt = files.Length;

                    count++;
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImagePK/" + itemCode + "/" + lot + "/"+jobstep+"_"+jobrunno+"/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImagePK" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobstep + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryPacking.InsertRunJobImage(), connSQL);
                    cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobid);
                    cmdSql.Parameters.AddWithValue("@P_STEP", jobstep);
                    cmdSql.Parameters.AddWithValue("@P_RUN_NO", jobrunno);
                    cmdSql.Parameters.AddWithValue("@P_LIST_NO", count);
                    cmdSql.Parameters.AddWithValue("@P_LENGTH", files.Length);
                    cmdSql.Parameters.AddWithValue("@P_IMAGE_NAME", file.FileName.ToString());
                    cmdSql.Parameters.AddWithValue("@P_IMAGE_PATH", file_url.ToString());
                    cmdSql.Parameters.AddWithValue("@P_REMARK", remark.ToString());
                    cmdSql.Parameters.AddWithValue("@P_USERID", user_id);
                    var temp = cmdSql.ExecuteScalar();
                    cmdSql.Dispose();
                    connSQL.Close();
                    if (temp != null)
                    {
                        filename = System.IO.Path.Combine(filename, temp.ToString() + "_" + file.FileName);
                        file.SaveAs(filename);
                    }

                }

            }
            if (files == null)
            {
                connSQL.Open();
                cmdSql = new SqlCommand(queryPacking.CCUpdateRunJobImage(), connSQL);
                cmdSql.Parameters.AddWithValue("@P_REMARK", remark);
                cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobid);
                cmdSql.Parameters.AddWithValue("@P_RUN_NO", jobrunno);
                cmdSql.Parameters.AddWithValue("@P_USER_ID", user_id);
                cmdSql.ExecuteNonQuery();
                cmdSql.Dispose();
                connSQL.Close();
            }

            return status;
        }
        public List<ModalImageInfo> CCGetImage(Int64 jobSysid, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryCCRunJobImage(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_RUNNO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    imageInfo = new ModalImageInfo
                    {
                        imagename = readerSQL["PK_CC_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImagePK/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + runNo + "/" + readerSQL["PK_CC_RJPT_SYS_ID"].ToString() + "_" + readerSQL["PK_CC_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["PK_CC_RJPT_REMARK"].ToString()
                    };
                    listimageinfo.Add(imageInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listimageinfo;
        }
        public String CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            List<ModalImageInfo> listImageinfo = new List<ModalImageInfo>();
            Int64 user_id = Convert.ToInt64(HttpContext.Current.Session["USERID"]);
            int i = 1;

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImagePK/" + itemCode + "/" + lot + "/" + "CLEAN_CONTROL" + "/" + jobrunno + "/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }
            int count = 0;
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null)
                {
                    int testtt = files.Length;

                    count++;
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImagePK/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + jobrunno + "/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImagePK" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryPacking.CCInsertRunJobImage(), connSQL);
                    cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobid);
                    cmdSql.Parameters.AddWithValue("@P_RUN_NO", jobrunno);
                    cmdSql.Parameters.AddWithValue("@P_LIST_NO", count);
                    cmdSql.Parameters.AddWithValue("@P_LENGTH", files.Length);
                    cmdSql.Parameters.AddWithValue("@P_IMAGE_NAME", file.FileName.ToString());
                    cmdSql.Parameters.AddWithValue("@P_IMAGE_PATH", file_url.ToString());
                    cmdSql.Parameters.AddWithValue("@P_REMARK", remark.ToString());
                    cmdSql.Parameters.AddWithValue("@P_USERID", user_id);
                    var temp = cmdSql.ExecuteScalar();
                    cmdSql.Dispose();
                    connSQL.Close();
                    if (temp != null)
                    {
                        filename = System.IO.Path.Combine(filename, temp.ToString() + "_" + file.FileName);
                        file.SaveAs(filename);
                    }

                }

            }

            return status;
        }

        public String InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {

            connSQL.Open();
            foreach (var item in listAddCleanControlInfo)
            {
                cmdSql = new SqlCommand(queryPacking.InsertRunJobCC(), connSQL);
                cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", item.jobSysId);
                cmdSql.Parameters.AddWithValue("@P_RUN_NO", item.runNo);
                cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NO", item.equipmentNo);
                cmdSql.Parameters.AddWithValue("@P_EQUIPMENT_NAME", item.equipmentName);
                cmdSql.Parameters.AddWithValue("@P_GROUP_CLEAN", item.groupClean);
                cmdSql.Parameters.AddWithValue("@P_GROUP_CHECK", item.groupCheck);
                cmdSql.Parameters.AddWithValue("@P_REQ_IMAGE_YN", item.reqImageYn);
                cmdSql.Parameters.AddWithValue("@P_USER_ID", HttpContext.Current.Session["USERID"]);
                cmdSql.ExecuteNonQuery();
            }
            cmdSql.Dispose();
            connSQL.Dispose();
            return status;
        }
        public String InsertWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.InsertRunJobWeightOfSample(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_WEIGHT", weight);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public Double GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryPacking.QueryRunJobWeightOfSample(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                temp = double.Parse(exe.ToString());
            }
            cmdSql.Dispose();
            connSQL.Close();
            return temp;
        }
    }
}