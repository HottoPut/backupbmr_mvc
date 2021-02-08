using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class Coated
    {
        String status;
        Double temp;
        JobBcaInfo jobBcaInfo;
        List<JobBcaInfo> listJobBcaInfos;
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
        QueryCoated queryCoated;
        ReturnCheckInfo returnCheckInfo;
        ModalTimeInfo modalTimeInfo;
        ModalImageInfo modalImageInfo;
        ModalWeightInfo modalWeightInfo;

        public Coated()
        {
            connSQL = new SqlConnection(conStrSQL);
            queryCoated = new QueryCoated();
            login = new Login();
        }
        public List<JobBcaInfo> GetJob()
        {
            listJobBcaInfos = new List<JobBcaInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.GetJob(), connSQL);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    jobBcaInfo = new JobBcaInfo
                    {
                        jobSysId = readerSQL["JOB_SYS_ID"].ToString(),
                        jobApprPd = readerSQL["APPR_JOB_PD"].ToString(),
                        jobApprQa = readerSQL["APPR_JOB_QA"].ToString(),
                        itemCode = readerSQL["JOB_ITEM_CODE"].ToString(),
                        itemName = readerSQL["JOB_ITEM_NAME2"].ToString(),
                        lot = readerSQL["JOB_LOT"].ToString(),
                        bmrVersion = readerSQL["BMR_VERSION"].ToString(),
                        startDt = readerSQL["JOB_START_DT"].ToString(),
                        endDt = readerSQL["JOB_END_DT"].ToString(),
                        batchSize = readerSQL["JOB_BATCH_SIZE"].ToString(),
                        status = readerSQL["JOB_STATUS"].ToString(),
                        ccrRunNo = readerSQL["BCAR_CC_RUN_NO"].ToString(),
                        ccrEquipmentNo = readerSQL["BCAR_CC_EQUIPMENT_NO"].ToString(),
                        ccrEquipmentName = readerSQL["BCAR_CC_EQUIPMENT_NAME"].ToString(),
                        ccrReqImageYn = readerSQL["BCAR_CC_REQ_IMAGE_YN"].ToString(),
                        bcaStep = readerSQL["BCAR_STEP_NO"].ToString(),
                        bcaRunNo = readerSQL["BCAR_RUN_NO"].ToString(),
                        bcaStepDesc = readerSQL["BCAR_STEP_DESC"].ToString(),
                        userProgress = readerSQL["OPERATE_PERCENT"].ToString(),
                        qaProgress = readerSQL["CHECK_PERCENT"].ToString(),
                        remark = readerSQL["JOB_REMARK"].ToString(),
                        bcaGroupOperateId = readerSQL["BCAR_GROUP_OPERATE_ID"].ToString(),
                        bcaGroupCheckId = readerSQL["BCAR_GROUP_CHECK_ID"].ToString(),
                        ccGroupCleanId = readerSQL["BCAR_CC_GROUP_CLEAN_ID"].ToString(),
                        ccGroupCheckId = readerSQL["BCAR_CC_GROUP_CHECK_ID"].ToString(),
                        reqTempYn = readerSQL["BCAR_REQ_TEMP_YN"].ToString(),
                        reqHumidityYn = readerSQL["BCAR_REQ_HUMUDITY_YN"].ToString(),
                        reqPressYn = readerSQL["BCAR_REQ_PRESS_YN"].ToString(),
                        reqVaccYn = readerSQL["BCAR_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["BCAR_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["BCAR_REQ_WEIGHT_YN"].ToString(),
                        reqWeightSampleYn = readerSQL["BCAR_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        reqImageYn = readerSQL["BCAR_REQ_IMAGE_YN"].ToString(),
                        ccrCleanUserName = readerSQL["USER_NAME_CC_CLEAN"].ToString(),
                        ccrCleanDt = readerSQL["USER_NAME_CC_CLEAN_DT"].ToString(),
                        ccrCheckUserName = readerSQL["USER_NAME_CC_CHECK"].ToString(),
                        ccrCheckDt = readerSQL["USER_NAME_CC_CHECK_DT"].ToString(),
                        bcaOperateUserName = readerSQL["USER_NAME_BCA_OPERATE"].ToString(),
                        bcaOperateDt = readerSQL["USER_NAME_BCA_OPERATE_DT"].ToString(),
                        bcaCheckUserName = readerSQL["USER_NAME_BCA_CHECK"].ToString(),
                        bcaCheckDt = readerSQL["USER_NAME_BCA_CHECK_DT"].ToString()
                    };
                    listJobBcaInfos.Add(jobBcaInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listJobBcaInfos;
        }
        public String InsertTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.InsertTemp(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.QueryRunJobGetTemp(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.QueryRunJobGetPressure(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.InsertPressure(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.QueryRunJobGetHumidity(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.InsertHumidity(), connSQL);
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
                cmdSql = new SqlCommand(queryCoated.InsertRunJobTime(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.QueryRunJobTime(), connSQL);
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
                        listNo = readerSQL["BCA_RJDT_LIST_NO"].ToString(),
                        startTime = readerSQL["BCA_RJDT_START_DT"].ToString(),
                        endTime = readerSQL["BCA_RJDT_END_DT"].ToString(),
                        userOperate = readerSQL["USER_NAME_OPERATE"].ToString(),
                        userCheck = readerSQL["USER_NAME_CHECK"].ToString(),
                        checkDt = readerSQL["BCA_RJDT_CHECK_BY_DT_CHECK"].ToString()
                    };
                    listModalTimeInfos.Add(modalTimeInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listModalTimeInfos;
        }
        public List<ReturnCheckInfo> ApprPdJobCheck(Int64 jobSysId, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.ApprPdJob(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.ApprQaJob(), connSQL);
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
        public List<ReturnCheckInfo> CheckBcaCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.CheckBcaCCCleanChk(), connSQL);
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
        public List<ReturnCheckInfo> CheckBcaCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.CheckBcaCCChkChk(), connSQL);
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
        public List<ReturnCheckInfo> CheckBcaOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.CheckBcaOperateChk(), connSQL);
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
        public List<ReturnCheckInfo> CheckBcaCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.CheckBcaChkChk(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.BcaTimeOperateCheck(), connSQL);
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
            cmdSql = new SqlCommand(queryCoated.BcaTimeCheckCheck(), connSQL);
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
        public List<ModalImageInfo> GetImage(Int64 jobSysid, Int64 step, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.QueryRunJobImage(), connSQL);
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
                        imagename = readerSQL["BCA_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageBCA/" + itemCode + "/" + lot + "/"+step+"_"+runNo +"/"+ readerSQL["BCA_RJPT_SYS_ID"].ToString() + "_" + readerSQL["BCA_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["BCA_RJPT_REMARK"].ToString()
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

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCA/" + itemCode + "/" + lot + "/PROCEDURE/" + jobstep + "_" + jobrunno + "/");

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
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCA/" + itemCode + "/" + lot + "/PROCEDURE/" + jobstep + "_" + jobrunno + "/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImageBCA" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobstep + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryCoated.InsertRunJobImage(), connSQL);
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
        public String CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobrunno, String itemCode, String lot, String name_url)
        {
            List<ModalImageInfo> listImageinfo = new List<ModalImageInfo>();
            Int64 user_id = Convert.ToInt64(HttpContext.Current.Session["USERID"]);
            int i = 1;

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCA/" + itemCode + "/" + lot + "/" + "CLEAN_CONTROL" + "/" + jobrunno + "/");

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
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageBCA/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + jobrunno + "/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImageBCA" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                    connSQL.Open();
                    cmdSql = new SqlCommand(queryCoated.CCInsertRunJobImage(), connSQL);
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
        public List<ModalImageInfo> CCGetImage(Int64 jobSysid, Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.QueryCCRunJobImage(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_RUNNO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    imageInfo = new ModalImageInfo
                    {
                        imagename = readerSQL["BCA_CC_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageBCA/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + runNo + "/" + readerSQL["BCA_CC_RJPT_SYS_ID"].ToString() + "_" + readerSQL["BCA_CC_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["BCA_CC_RJPT_REMARK"].ToString()
                    };
                    listimageinfo.Add(imageInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listimageinfo;
        }
        public String InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo)
        {

            connSQL.Open();
            foreach (var item in listAddCleanControlInfo)
            {
                cmdSql = new SqlCommand(queryCoated.InsertRunJobCC(), connSQL);
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

        public String InsertWeight(Int64 jobSysid, Int64 step, Int64 runNo, Double sTotalWeight, Double sTheoretical, Double sYield, List<ModalWeightInfo> listModalWeightInfos, Int64 userId, Int64 lengthContainner)
        {
            connSQL.Open();
            foreach (var item in listModalWeightInfos)
            {
                cmdSql = new SqlCommand(queryCoated.InsertRunJobWeight(), connSQL);
                cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
                cmdSql.Parameters.AddWithValue("@P_STEP", step);
                cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
                cmdSql.Parameters.AddWithValue("@P_LIST_NO", item.listNo);
                cmdSql.Parameters.AddWithValue("@P_CONTAINER_NO", item.containerNo);
                cmdSql.Parameters.AddWithValue("@P_TOTAL_WEIGHT", item.totalWeight);
                cmdSql.Parameters.AddWithValue("@P_TARE_WEIGHT", item.tareWeight);
                cmdSql.Parameters.AddWithValue("@P_NET_WEIGHT", item.netWeight);
                cmdSql.Parameters.AddWithValue("@P_S_TOTAL_WEIGHT", sTotalWeight);
                cmdSql.Parameters.AddWithValue("@P_S_THEORETICAL", sTheoretical);
                cmdSql.Parameters.AddWithValue("@P_S_YIELD", sYield);
                cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
                cmdSql.Parameters.AddWithValue("@P_LENGTH", lengthContainner);
                cmdSql.Parameters.AddWithValue("@P_STATUS_OPERATE", item.statusOperate);

                cmdSql.ExecuteNonQuery();
            }
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }

        public List<ModalWeightInfo> GetWeight(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalWeightInfos = new List<ModalWeightInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.QueryRunJobWeight(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalWeightInfo = new ModalWeightInfo
                    {
                        listNo = readerSQL["MX_RJW_LIST_NO"].ToString(),
                        containerNo = readerSQL["MX_RJW_CONTAINER_NO"].ToString(),
                        totalWeight = readerSQL["MX_RJW_TOTAL_WEIGHT"].ToString(),
                        tareWeight = readerSQL["MX_RJW_TARE_WEIGHT"].ToString(),
                        netWeight = readerSQL["MX_RJW_NET_WEIGHT"].ToString(),
                        sToTal = readerSQL["MX_RJW_S_TOTAL_WEIGHT"].ToString(),
                        sTheoretical = readerSQL["MX_RJW_S_THEORETICAL_WEIGHT"].ToString(),
                        sYield = readerSQL["MX_RJW_S_YIELD"].ToString(),
                        userOperate = readerSQL["USER_NAME_OPERATE"].ToString(),
                        userCheck = readerSQL["USER_NAME_CHECK"].ToString(),
                        dtCheck = readerSQL["MX_RJW_CHECK_BY_DT_CHECK"].ToString()
                    };
                    listModalWeightInfos.Add(modalWeightInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listModalWeightInfos;
        }


        public Double GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.QueryRunJobWeightOfSample(), connSQL);
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
        public String InsertWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(queryCoated.InsertRunJobWeightOfSample(), connSQL);
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
    }
}