using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class Cored
    {
        String status;
        Double temp;
        JobBcrInfo jobBcrInfo;
        List<JobBcrInfo> listJobBcrInfos;
        List<ReturnCheckInfo> listReturnCheckInfos;
        List<ModalTimeInfo> listModalTimeInfos;
        List<ModalWeightInfo> listModalWeightInfos;

       //Connection String
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();

        //SQL Server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;

        Login login;
        QueryCored queryCored;
        ReturnCheckInfo returnCheckInfo;
        ModalTimeInfo modalTimeInfo;
        ModalImageInfo modalImageInfo;
        ModalWeightInfo modalWeightInfo;

        public Cored()
        {
            connSQL = new SqlConnection(conStrSQL);
            queryCored = new QueryCored();
            login = new Login();
        }
        public List<JobBcrInfo> GetJob()
        {
            listJobBcrInfos = new List<JobBcrInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.GetJob(), connSQL);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    jobBcrInfo = new JobBcrInfo
                    {
                        jobSysId = readerSQL["JOB_SYS_ID"].ToString(),
                        jobApprPd = readerSQL["APPR_JOB_PD"].ToString(),
                        jobApprQa =readerSQL["APPR_JOB_QA"].ToString(),
                        itemCode = readerSQL["JOB_ITEM_CODE"].ToString(),
                        itemName = readerSQL["JOB_ITEM_NAME2"].ToString(),
                        lot = readerSQL["JOB_LOT"].ToString(),
                        bmrVersion = readerSQL["BMR_VERSION"].ToString(),
                        startDt = readerSQL["JOB_START_DT"].ToString(),
                        endDt = readerSQL["JOB_END_DT"].ToString(),
                        batchSize = readerSQL["JOB_BATCH_SIZE"].ToString(),
                        status = readerSQL["JOB_STATUS"].ToString(),
                        ccrRunNo = readerSQL["BCRR_CC_RUN_NO"].ToString(),
                        ccrEquipmentNo = readerSQL["BCRR_CC_EQUIPMENT_NO"].ToString(),
                        ccrEquipmentName = readerSQL["BCRR_CC_EQUIPMENT_NAME"].ToString(),
                        ccrReqImageYn = readerSQL["BCRR_CC_REQ_IMAGE_YN"].ToString(),
                        bcrStep = readerSQL["BCRR_STEP_NO"].ToString(),
                        bcrRunNo = readerSQL["BCRR_RUN_NO"].ToString(),
                        bcrStepDesc = readerSQL["BCRR_STEP_DESC"].ToString(),
                        userProgress = readerSQL["OPERATE_PERCENT"].ToString(),
                        qaProgress = readerSQL["CHECK_PERCENT"].ToString(),
                        remark = readerSQL["JOB_REMARK"].ToString(),
                        bcrGroupOperateId = readerSQL["BCRR_GROUP_OPERATE_ID"].ToString(),
                        bcrGroupCheckId = readerSQL["BCRR_GROUP_CHECK_ID"].ToString(),
                        ccGroupCleanId = readerSQL["BCRR_CC_GROUP_CLEAN_ID"].ToString(),
                        ccGroupCheckId = readerSQL["BCRR_CC_GROUP_CHECK_ID"].ToString(),
                        reqTempYn = readerSQL["BCRR_REQ_TEMP_YN"].ToString(),
                        reqHumidityYn = readerSQL["BCRR_REQ_HUMUDITY_YN"].ToString(),
                        reqPressYn = readerSQL["BCRR_REQ_PRESS_YN"].ToString(),
                        reqVaccYn = readerSQL["BCRR_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["BCRR_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["BCRR_REQ_WEIGHT_YN"].ToString(),
                        reqWeightSampleYn = readerSQL["BCRR_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        reqImageYn = readerSQL["BCRR_REQ_IMAGE_YN"].ToString(),
                        ccrCleanUserName = readerSQL["USER_NAME_CC_CLEAN"].ToString(),
                        ccrCleanDt = readerSQL["USER_NAME_CC_CLEAN_DT"].ToString(),
                        ccrCheckUserName = readerSQL["USER_NAME_CC_CHECK"].ToString(),
                        ccrCheckDt = readerSQL["USER_NAME_CC_CHECK_DT"].ToString(),
                        bcrOperateUserName = readerSQL["USER_NAME_BCR_OPERATE"].ToString(),
                        bcrOperateDt = readerSQL["USER_NAME_BCR_OPERATE_DT"].ToString(),
                        bcrCheckUserName = readerSQL["USER_NAME_BCR_CHECK"].ToString(),
                        bcrCheckDt = readerSQL["USER_NAME_BCR_CHECK_DT"].ToString()
                    };
                    listJobBcrInfos.Add(jobBcrInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listJobBcrInfos;
        }
        public String InsertTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.InsertTemp(), connSQL);
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
            cmdSql = new SqlCommand(queryCored.QueryRunJobGetTemp(), connSQL);
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
            cmdSql = new SqlCommand(queryCored.QueryRunJobGetPressure(), connSQL);
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
            cmdSql = new SqlCommand(queryCored.InsertPressure(), connSQL);
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
        public Double GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.QueryRunJobGetHumidity(), connSQL);
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
        public String InsertHumidity(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.InsertHumidity(), connSQL);
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

        public String InsertTime(Int64 jobSysid, Int64 step, Int64 runNo, List<ModalTimeInfo> listModalTimeInfos, Int64 userId, Int64 lengthTime)
        {
            connSQL.Open();
            foreach (var item in listModalTimeInfos)
            {
                cmdSql = new SqlCommand(queryCored.InsertRunJobTime(), connSQL);
                cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
                cmdSql.Parameters.AddWithValue("@P_STEP", step);
                cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
                cmdSql.Parameters.AddWithValue("@P_LIST_NO", item.listNo);
                cmdSql.Parameters.AddWithValue("@P_START_DT", item.startTime);
                cmdSql.Parameters.AddWithValue("@P_END_DT", item.endTime);
                cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
                cmdSql.Parameters.AddWithValue("@P_LENGTH", lengthTime);
                cmdSql.Parameters.AddWithValue("@P_STATUS_OPERATE", item.statusOperate);
                cmdSql.ExecuteNonQuery();
            }
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public List<ModalTimeInfo> GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalTimeInfos = new List<ModalTimeInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.QueryRunJobTime(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalTimeInfo = new ModalTimeInfo
                    {
                        listNo = readerSQL["BCR_RJDT_LIST_NO"].ToString(),
                        startTime = readerSQL["BCR_RJDT_START_DT"].ToString(),
                        endTime = readerSQL["BCR_RJDT_END_DT"].ToString(),
                        userOperate = readerSQL["USER_NAME_OPERATE"].ToString(),
                        userCheck = readerSQL["USER_NAME_CHECK"].ToString(),
                        checkDt = readerSQL["BCR_RJDT_CHECK_BY_DT_CHECK"].ToString()
                    };
                    listModalTimeInfos.Add(modalTimeInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listModalTimeInfos;
        }
        public List<ModalImageInfo> GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.QueryRunJobImage(), connSQL);
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
                        imagename = readerSQL["BCR_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageBCR/" + itemCode + "/" + lot + "/PROCEDURE/" + step + "_" + runNo + "/" + readerSQL["BCR_RJPT_SYS_ID"].ToString() + "_" + readerSQL["BCR_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["BCR_RJPT_REMARK"].ToString()
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

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCR/" + itemCode + "/" + lot + "/PROCEDURE/" + jobstep + "_" + jobrunno + "/");

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
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCR/" + itemCode + "/" + lot + "/PROCEDURE/" + jobstep + "_" + jobrunno + "/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImageBCR" + "/" + itemCode + "/" + lot  + "/" + jobid + "_" + jobstep + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryCored.InsertRunJobImage(), connSQL);
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

            return status;
        }
        public List<ReturnCheckInfo> ApprPdJobCheck(Int64 jobSysId, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.ApprPdJob(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> ApprQaJobCheck(Int64 jobSysId, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.ApprQaJob(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> CheckBcrCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.CheckBcrCCCleanChk(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> CheckBcrCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.CheckBcrCCChkChk(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> CheckBcrOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.CheckBcrOperateChk(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> CheckBcrCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.CheckBcrChkChk(), connSQL);
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public String TimeOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.BcrTimeOperateCheck(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_LIST_NO", listNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_TOKEN", HttpContext.Current.Session["TOKEN"]);
            cmdSql.Parameters.AddWithValue("@P_PASSWORD", login.Encrypt(password));
            var exe = cmdSql.ExecuteScalar();
            if (exe != null)
            {
                status = exe.ToString();
            }
            return status;
        }
        public List<ReturnCheckInfo> TimeCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.BcrTimeCheckCheck(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_LIST_NO", listNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_TOKEN", HttpContext.Current.Session["TOKEN"]);
            cmdSql.Parameters.AddWithValue("@P_PASSWORD", login.Encrypt(password));
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
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ModalImageInfo> CCGetImage(Int64 jobSysid, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.QueryCCRunJobImage(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_RUNNO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    imageInfo = new ModalImageInfo
                    {
                        imagename = readerSQL["BCR_CC_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageBCR/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + runNo + "/" + readerSQL["BCR_CC_RJPT_SYS_ID"].ToString() + "_" + readerSQL["BCR_CC_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["BCR_CC_RJPT_REMARK"].ToString()
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

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCR/" + itemCode + "/" + lot + "/" + "CLEAN_CONTROL" + "/" + jobrunno + "/");

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
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCR/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + jobrunno + "/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImageBCR" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryCored.CCInsertRunJobImage(), connSQL);
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
            if (files == null)
            {
                connSQL.Open();
                cmdSql = new SqlCommand(queryCored.CCUpdateRunJobImage(), connSQL);
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
        public String InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {

            connSQL.Open();
            foreach (var item in listAddCleanControlInfo)
            {
                cmdSql = new SqlCommand(queryCored.InsertRunJobCC(), connSQL);
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
        public String InsertWeight(Int64 jobSysId,Int64 step,Int64 runNo,Double tankAmount,Double net,Double theoretical,Double yield)
        {
            status = "";
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.InsertRunJobWeight(),connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID",jobSysId);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_LIST_NO", 1);
            cmdSql.Parameters.AddWithValue("@P_TANK_AMOUNT", tankAmount);
            cmdSql.Parameters.AddWithValue("@P_NET", net);
            cmdSql.Parameters.AddWithValue("@P_THEORETICAL", theoretical);
            cmdSql.Parameters.AddWithValue("@P_YIELD", yield);
            cmdSql.Parameters.AddWithValue("@P_USR_ID", HttpContext.Current.Session["USERID"]);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Dispose();
            return status;
        }
        public List<ModalWeightInfo> GetWeight(Int64 jobSysId, Int64 step, Int64 runNo)
        {
            listModalWeightInfos = new List<ModalWeightInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.GetWeight(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_STEP_NO", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalWeightInfo = new ModalWeightInfo {
                        tankAmount = readerSQL["BCR_RJW_TANK_AMOUNT"].ToString(),
                        netWeight = readerSQL["BCR_RJW_NET"].ToString(),
                        sTheoretical = readerSQL["BCR_RJW_THEORETICAL"].ToString(),
                        sYield = readerSQL["BCR_RJW_YIELD"].ToString()
                    };
                    listModalWeightInfos.Add(modalWeightInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Dispose();
            return listModalWeightInfos;
        }
        public String InsertInsertWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.InsertRunJobWeightOfSample(), connSQL);
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
            temp = 0;
            connSQL.Open();
            cmdSql = new SqlCommand(queryCored.QueryRunJobWeightOfSample(), connSQL);
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