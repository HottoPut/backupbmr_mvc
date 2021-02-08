using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMR_MVC.Models;
using System.IO;

namespace BMR_MVC.Models
{
    public class Mixing
    {
        //DATABASE
        //String connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        String status = null;
        Double temp = 0;
        OracleCommand cmdORCL;
        OracleDataReader readerORCL;
        OracleConnection connORCL;
        ItemMasterInfo itemMasterInfo;


        //SQl server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;
        Mixing defaults;
        //Connection String
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();
        //CLASS
        QuerMixing query;
        ItemMasterInfo ItemMasterInfo;
        BmrVersionInfo BmrVersion;
        JobInfo jobInfo;
        ModalTimeInfo modalTimeInfo;
        ModalWeightInfo modalWeightInfo;
        Login login;
        ReturnCheckInfo returnCheckInfo;
        ModalEditJobInfo modalEditJobInfo;
        ModalImageInfo modalImageInfo;
        //LIST
        List<ItemMasterInfo> listItemMasterInfos;
        List<BmrVersionInfo> listbmrVersions;
        List<JobInfo> listJobInfos;
        List<ModalTimeInfo> listModalTimeInfos;
        List<ModalWeightInfo> listModalWeightInfos;
        List<ReturnCheckInfo> listReturnCheckInfos;
        List<ModalEditJobInfo> listModalEditJobInfos;

        Dictionary<String, String> dicString;
        public List<SelectListItem> SelectSource { get; set; }

        public Mixing()
        {
            connSQL = new SqlConnection(conStrSQL);
            connORCL = new OracleConnection(conStrORCL);
            query = new QuerMixing();
            login = new Login();
        }



        public List<ItemMasterInfo> GetItemMaster()
        {
            listItemMasterInfos = new List<ItemMasterInfo>();
            OracleConnection conn = new OracleConnection(conStrORCL);
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
                        itemName = reader["ITEM_NAME"].ToString()
                    };
                    listItemMasterInfos.Add(itemMasterInfo);
                }
            }
            cmd.Dispose();
            conn.Close();
            return listItemMasterInfos;
        }


        public List<ModalEditJobInfo> GetRunJobHead(Int64 jobSysId)
        {
            listModalEditJobInfos = new List<ModalEditJobInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryGetRunJobHead(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    modalEditJobInfo = new ModalEditJobInfo
                    {
                        jobSysId = readerSQL["JOB_SYS_ID"].ToString(),
                        jobLot = readerSQL["JOB_LOT"].ToString(),
                        jobStartDt= readerSQL["JOB_START_DT"].ToString(),
                        jobEndDt = readerSQL["JOB_END_DT"].ToString(),
                        jobBatchSize = readerSQL["JOB_BATCH_SIZE"].ToString(),
                        jobUom = readerSQL["JOB_UOM"].ToString(),
                        jobRemark = readerSQL["JOB_REMARK"].ToString()
                    };
                    listModalEditJobInfos.Add(modalEditJobInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listModalEditJobInfos;
        }
        public String UpdateRunJobHead(Int64 jobSysId, String lot, Double batchSize, String uom, String startDt, String endDt,String remark) {
            connSQL.Open();
            cmdSql = new SqlCommand(query.UpdateRunjobHead(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_LOT", lot);
            cmdSql.Parameters.AddWithValue("@P_BATCH_SIZE", batchSize);
            cmdSql.Parameters.AddWithValue("@P_UOM", uom);
            cmdSql.Parameters.AddWithValue("@P_START_DT", startDt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", endDt);
            cmdSql.Parameters.AddWithValue("@P_REMARK", remark);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }

        public String DeleteJob(Int64 jobSysId) {
            connSQL.Open();
            cmdSql = new SqlCommand(query.DeleteJob(),connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Dispose();
            return status;
        }

        public List<ReturnCheckInfo> ApprPdJobCheck(Int64 jobSysId,String password) {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.ApprPdJob(), connSQL);
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
            cmdSql = new SqlCommand(query.ApprQaJob(), connSQL);
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
        public List<JobInfo> GetJob()
        {
            listJobInfos = new List<JobInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryGetJob(), connSQL);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    jobInfo = new JobInfo
                    {
                        jobSysId = readerSQL["JOB_SYS_ID"].ToString(),
                        jobApprPd = readerSQL["APPR_JOB_PD"].ToString(),
                        jobApprQa = readerSQL["APPR_JOB_QA"].ToString(),
                        itemCode = readerSQL["JOB_ITEM_CODE"].ToString(),
                        itemName = readerSQL["JOB_ITEM_NAME2"].ToString(),
                        bmrVersion= readerSQL["BMR_VERSION"].ToString(),
                        startDt = readerSQL["JOB_START_DT"].ToString(),
                        endDt = readerSQL["JOB_END_DT"].ToString(),
                        batchSize = readerSQL["JOB_BATCH_SIZE"].ToString(),
                        lot = readerSQL["JOB_LOT"].ToString(),
                        status = readerSQL["JOB_STATUS"].ToString(),
                        ccrRunNo = readerSQL["CCR_RUN_NO"].ToString(),
                        ccrEquipmentNo = readerSQL["CCR_EQUIPMENT_NO"].ToString(),
                        ccrEquipmentName = readerSQL["CCR_EQUIPMENT_NAME"].ToString(),
                        ccrReqImageYn = readerSQL["CCR_REQ_IMAGE_YN"].ToString(),
                        ccrCleanUserName = readerSQL["USER_NAME_CC_CLEAN"].ToString(),
                        ccrCleanDt = readerSQL["USER_NAME_CC_CLEAN_DT"].ToString(),
                        ccrCheckUserName = readerSQL["USER_NAME_CC_CHECK"].ToString(),
                        ccrCheckDt = readerSQL["USER_NAME_CC_CHECK_DT"].ToString(),
                        mxOperateUserName = readerSQL["USER_NAME_MX_OPERATE"].ToString(),
                        mxOperateDt = readerSQL["USER_NAME_MX_OPERATE_DT"].ToString(),
                        mxCheckUserName = readerSQL["USER_NAME_MX_CHECK"].ToString(),
                        mxCheckDt = readerSQL["USER_NAME_MX_CHECK_DT"].ToString(),
                        mxsStep = readerSQL["MXS_STEP"].ToString(),
                        mxsRunNo = readerSQL["MXS_RUN_NO"].ToString(),
                        mxsStepDesc = readerSQL["MXS_STEP_DESC"].ToString(),
                        remark = readerSQL["JOB_REMARK"].ToString(),
                        userProgress = readerSQL["OPERATE_PERCENT"].ToString(),
                        qaProgress = readerSQL["CHECK_PERCENT"].ToString(),
                        reqTempYn = readerSQL["MXS_TEMP_YN"].ToString().ToUpper(),
                        reqPress =readerSQL["MXS_PRESS_YN"].ToString().ToUpper(),
                        reqHumidityYn =readerSQL["MXS_HUMIDITY_YN"].ToString().ToUpper(),
                        reqStartStopYn =readerSQL["MXS_STARTSTOP_YN"].ToString().ToUpper(),
                        reqWeightYn=readerSQL["MXS_WEIGHT_YN"].ToString().ToUpper(),
                        vaccYn = readerSQL["MXS_VACC_YN"].ToString().ToUpper(),
                        reqWeightSampleYn = readerSQL["MXS_REQ_WEIGHT_SAMPLE_YN"].ToString().ToUpper(),
                        reqImageYn =readerSQL["MXS_IMAGE_YN"].ToString().ToUpper(),
                        ccGroupCleanId = readerSQL["CCR_GROUP_CLEAN_ID"].ToString(),
                        ccGroupCheckId = readerSQL["CCR_GROUP_CHECK_ID"].ToString(),
                        mxGroupOperateId = readerSQL["MX_GROUP_OPRATE_ID"].ToString(),
                        mxGroupCheckId = readerSQL["MXS_GROUP_CHECK_ID"].ToString()
                    };
                    listJobInfos.Add(jobInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listJobInfos;
        }

        public String InsertTemp(Int64 jobSysid, Int64 step, Int64 runNo, Double temp, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.InsertTemp(), connSQL);
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
        public String InsertPressure(Int64 jobSysid, Int64 step, Int64 runNo, Double pressure, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.InsertPressure(), connSQL);
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
            cmdSql = new SqlCommand(query.InsertHumidity(), connSQL);
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
                cmdSql = new SqlCommand(query.InsertRunJobTime(), connSQL);
                cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
                cmdSql.Parameters.AddWithValue("@P_STEP", step);
                cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
                cmdSql.Parameters.AddWithValue("@P_LIST_NO", item.listNo);
                cmdSql.Parameters.AddWithValue("@P_START_DT", item.startTime);
                cmdSql.Parameters.AddWithValue("@P_END_DT", item.endTime);
                cmdSql.Parameters.AddWithValue("@P_RPM", item.rpm);
                cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
                cmdSql.Parameters.AddWithValue("@P_LENGTH", lengthTime);
                cmdSql.Parameters.AddWithValue("@P_STATUS_OPERATE", item.statusOperate);
                cmdSql.ExecuteNonQuery();
            }
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public String InsertWeight(Int64 jobSysid, Int64 step, Int64 runNo, Double sTotalWeight, Double sTheoretical, Double sYield, List<ModalWeightInfo> listModalWeightInfos, Int64 userId, Int64 lengthContainner)
        {
            connSQL.Open();
            foreach (var item in listModalWeightInfos)
            {
                cmdSql = new SqlCommand(query.InsertRunJobWeight(), connSQL);
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
        public String InsertVacuum(Int64 jobSysid, Int64 step, Int64 runNo, Double vacuum, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.InsertRunJobVacuum(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_VACUUM", vacuum);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Close();
            return status;
        }
        public String InsertInsertWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo, Double weight, Int64 userId)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.InsertRunJobWeightOfSample(), connSQL);
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
        public Double GetTemp(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobTemp(), connSQL);
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
            cmdSql = new SqlCommand(query.QueryRunJobPressure(), connSQL);
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
        public Double GetHumidity(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobHumidity(), connSQL);
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
        public Double GetVacuum(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobVacuum(), connSQL);
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
        public Double GetWeightOfSample(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobWeightOfSample(), connSQL);
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
        public List<ModalTimeInfo> GetTime(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalTimeInfos = new List<ModalTimeInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobTime(), connSQL);
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
                        listNo = readerSQL["MX_RJDT_LIST_NO"].ToString(),
                        startTime = readerSQL["MX_RJDT_START_DT"].ToString(),
                        endTime = readerSQL["MX_RJDT_END_DT"].ToString(),
                        rpm = Convert.ToDouble(readerSQL["MX_RJDT_RPM"]),
                        userOperate = readerSQL["USER_NAME_OPERATE"].ToString(),
                        userCheck = readerSQL["USER_NAME_CHECK"].ToString(),
                        checkDt = readerSQL["MX_RJDT_CHECK_BY_DT_CHECK"].ToString()
                    };
                    listModalTimeInfos.Add(modalTimeInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listModalTimeInfos;
        }

        public List<ModalWeightInfo> GetWeight(Int64 jobSysid, Int64 step, Int64 runNo)
        {
            listModalWeightInfos = new List<ModalWeightInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobWeight(), connSQL);
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
        public List<ReturnCheckInfo> CheckMxCCCleanCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.CheckMxCCCleanChk(), connSQL);
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
        public List<ReturnCheckInfo> CheckMxCCCheckCheck(Int64 jobSysId, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.CheckMxCCCheckChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            //var exe = cmdSql.ExecuteScalar();
            //if (exe != null) {
            //    status = exe.ToString();
            //}
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    returnCheckInfo = new ReturnCheckInfo {
                        status= readerSQL["STATUS_"].ToString(),
                        dt = readerSQL["CHECK_DT"].ToString()
                    };
                    listReturnCheckInfos.Add(returnCheckInfo);
                }
            }

            cmdSql.Dispose();
            connSQL.Close();
            return listReturnCheckInfos;
        }
        public List<ReturnCheckInfo> CheckMxOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.CheckMxOperateChk(), connSQL);
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
        public List<ReturnCheckInfo> CheckMxCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.CheckMxCheckChk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_USERID", HttpContext.Current.Session["USERID"]);
            cmdSql.Parameters.AddWithValue("@P_STEP", step);
            cmdSql.Parameters.AddWithValue("@P_RUN_NO", runNo);
            cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobSysId);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", HttpContext.Current.Session["USERLOGIN"]);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", login.Encrypt(password));
            //var exe = cmdSql.ExecuteScalar();
            //if (exe != null)
            //{
            //    status = exe.ToString();
            //}

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
        public String WeightOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo,Int64 listNo,String password)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.MxWeightOperateCheck(), connSQL);
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
        public List<ReturnCheckInfo> WeightCheckCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            listReturnCheckInfos = new List<ReturnCheckInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.MxWeightCheckCheck(), connSQL);
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
        public String TimeOperateCheck(Int64 jobSysId, Int64 step, Int64 runNo, Int64 listNo, String password)
        {
            connSQL.Open();
            cmdSql = new SqlCommand(query.MxTimeOperateCheck(), connSQL);
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
            cmdSql = new SqlCommand(query.MxTimeCheckCheck(), connSQL);
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
        public List<ModalImageInfo> CCGetImage(Int64 jobSysid,Int64 runNo, String itemCode, String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryCCRunJobImage(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_SYS_ID", jobSysid);
            cmdSql.Parameters.AddWithValue("@P_RUNNO", runNo);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    imageInfo = new ModalImageInfo
                    {
                        imagename = readerSQL["MX_CC_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageMX/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" +runNo + "/" + readerSQL["MX_CC_RJPT_SYS_ID"].ToString() + "_" + readerSQL["MX_CC_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["MX_CC_RJPT_REMARK"].ToString()
                    };
                    listimageinfo.Add(imageInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listimageinfo;
        }
        public List<ModalImageInfo> GetImage(Int64 jobSysid, Int64 step, Int64 runNo,String itemCode,String lot)
        {
            ModalImageInfo imageInfo = new ModalImageInfo();
            List<ModalImageInfo> listimageinfo = new List<ModalImageInfo>();

            listimageinfo = new List<ModalImageInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryRunJobImage(), connSQL);
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
                        imagename = readerSQL["MX_RJPT_IMAGE_NAME"].ToString(),
                        imagepath = "UploadImageMX/"+itemCode+"/"+lot+"/PROCEDURE/"+step+"_"+runNo+"/"+readerSQL["MX_RJPT_SYS_ID"].ToString()+"_"+ readerSQL["MX_RJPT_IMAGE_NAME"].ToString(),
                        imageremark = readerSQL["MX_RJPT_REMARK"].ToString()
                    };
                    listimageinfo.Add(imageInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();
            return listimageinfo;
        }
        public String CCUploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid,  String jobrunno, String itemCode, String lot, String name_url)
        {
            List<ModalImageInfo> listImageinfo = new List<ModalImageInfo>();
            Int64 user_id = Convert.ToInt64(HttpContext.Current.Session["USERID"]);
            int i = 1;

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageMX/" + itemCode + "/" + lot+ "/"+"CLEAN_CONTROL"+"/"+ jobrunno + "/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }
            int count = 0;
            try
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        int testtt = files.Length;

                        count++;
                        string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageMX/" + itemCode + "/" + lot + "/CLEAN_CONTROL/" + jobrunno + "/");
                        string aaaa = file.FileName.ToString();
                        //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                        string file_url = name_url + "/BMR_MVC/UploadImageMX" + "/" + itemCode + "/" + lot + "/" + "/" + jobid + "_" + jobrunno + "_" + count + "_" + file.FileName.ToString();

                        connSQL.Open();
                        cmdSql = new SqlCommand(query.CCInsertRunJobImage(), connSQL);
                        cmdSql.Parameters.AddWithValue("@P_JOB_SYS_ID", jobid);
                        cmdSql.Parameters.AddWithValue("@P_RUN_NO", jobrunno);
                        cmdSql.Parameters.AddWithValue("@P_LIST_NO", count);
                        cmdSql.Parameters.AddWithValue("@P_LENGTH", files.Length);
                        cmdSql.Parameters.AddWithValue("@P_IMAGE_NAME", file.FileName.ToString());
                        cmdSql.Parameters.AddWithValue("@P_IMAGE_PATH", file_url.ToString());
                        cmdSql.Parameters.AddWithValue("@P_REMARK", remark);
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
            }
            catch (Exception ex) { }
            if (files == null) {
                connSQL.Open();
                cmdSql = new SqlCommand(query.CCUpdateRunJobImage(), connSQL);
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
        public String  UploadImage(HttpPostedFileBase[] files, JobInfo model, String remark, String jobid, String jobstep, String jobrunno,String itemCode,String lot, String name_url)
        {
            List<ModalImageInfo> listImageinfo = new List<ModalImageInfo>();
            Int64 user_id = Convert.ToInt64(HttpContext.Current.Session["USERID"]);
            int i = 1;

            string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageMX/" + itemCode + "/"+lot+"/PROCEDURE/"+jobstep+"_"+jobrunno+"/");

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
                    string filename = System.Web.HttpContext.Current.Server.MapPath("~/UploadImageMX/" + itemCode + "/"+lot+"/PROCEDURE/"+jobstep+"_"+jobrunno+"/");
                    string aaaa = file.FileName.ToString();
                    //string file_url = name_url + "/UploadImageMx" + "/" + jobid+"_"+ jobstep+"_"+ jobrunno + "/" + jobid +"_"+ jobstep +"_"+ jobrunno + "_" + file.FileName.ToString();
                    string file_url = name_url + "/BMR_MVC/UploadImageMX" + "/" +itemCode+"/"+lot+"/" + "/" + jobid + "_" + jobstep + "_" + jobrunno +"_"+count+ "_" + file.FileName.ToString();
                  
                    connSQL.Open();
                    cmdSql = new SqlCommand(query.InsertRunJobImage(), connSQL);
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
                    if (temp != null) {
                        filename = System.IO.Path.Combine(filename, temp.ToString()+ "_" + file.FileName);
                        file.SaveAs(filename);
                    }

                }

            }
           
            return status;
        }
        public String InsertRunJobCC(List<AddCleanControlInfo> listAddCleanControlInfo) {

            connSQL.Open();
            foreach (var item in listAddCleanControlInfo)
            {
                cmdSql = new SqlCommand(query.InsertRunJobCC(), connSQL);
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
    }
}