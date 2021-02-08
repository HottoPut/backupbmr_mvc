using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class RunJob
    {
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();
        String status;

        //SQl server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;

        OracleCommand cmdORCL;
        OracleDataReader readerORCL;
        OracleConnection connORCL;

        BmrVersionInfo bmrVersionInfo;
        CleanControlInfo cleanControlInfo;
        RunJob runJob;
        QuerMixing query;
        QueryRunJob queryRunJob;
        MixStepInfo mixStepInfo;
        ItemMasterInfo itemMasterInfo;
        PkCleanControlInfo pkCleanControlInfo;
        PackingInfo packingInfo;
        BcrCleanControlInfo bcrCleanControlInfo;
        BcrInfo bcrInfo;
        BcaCleanControlInfo bcaCleanControlInfo;
        BcaInfo bcaInfo;

        List<BmrVersionInfo> listBMRVersionInfo;
        List<CleanControlInfo> listCleanControlInfos;
        List<MixStepInfo> listMixStepInfos;
        List<RunJob> listRunJob;
        List<ItemMasterInfo> listItemMasterInfos;
        List<PkCleanControlInfo> listPkCleanControlInfo;
        List<PackingInfo> listPackingInfo;
        List<BcrCleanControlInfo> listBcrCleanControlInfo;
        List<BcrInfo> listBcrInfos;
        List<BcaCleanControlInfo> listBcaCleanControlInfo;
        List<BcaInfo> listBcaInfo;
        public RunJob()
        {
            connSQL = new SqlConnection(conStrSQL);
            connORCL = new OracleConnection(conStrORCL);
            query = new QuerMixing();
            queryRunJob = new QueryRunJob();
        }
        public String GetItemNameFGByItem(String itemCode)
        {
            status = "";
            connORCL.Open();
            cmdORCL = new OracleCommand(query.QueryGetItemNameByItemCodeFG(), connORCL);
            cmdORCL.Parameters.Add(new OracleParameter("P_ITEM_CODE", itemCode.ToUpper()));
            var exe = cmdORCL.ExecuteScalar();
            if (exe != null)
            {
                status = exe.ToString();
            }
            cmdORCL.Dispose();
            connORCL.Close();
            return status;
        }

        public String GetUomBomByItem(String itemCode)
        {
            status = "";
            connORCL.Open();
            cmdORCL = new OracleCommand(query.QueryGetUomBomSF(), connORCL);
            cmdORCL.Parameters.Add(new OracleParameter("P_ITEM_CODE", itemCode.ToUpper()));
            var exe = cmdORCL.ExecuteScalar();
            if (exe != null)
            {
                status = exe.ToString();
            }
            cmdORCL.Dispose();
            connORCL.Close();
            return status;
        }

        public List<BmrVersionInfo> GetVersion(String itemCode)
        {
            listBMRVersionInfo = new List<BmrVersionInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetVersionByItem(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bmrVersionInfo = new BmrVersionInfo
                    {
                        ItemCode = readerSQL["BMR_ITEM_CODE"].ToString(),
                        Vesion = readerSQL["BMR_VERSION"].ToString(),
                        StartDate = readerSQL["BMR_START_DATE"].ToString(),
                        EndDate = readerSQL["BMR_END_DATE"].ToString(),
                        Revision = readerSQL["REVISION"].ToString()
                    };
                    listBMRVersionInfo.Add(bmrVersionInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();

            return listBMRVersionInfo;
        }
        public List<BmrVersionInfo> GetStatus(String itemCode,String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];
            listBMRVersionInfo = new List<BmrVersionInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetStatusBMR(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bmrVersionInfo = new BmrVersionInfo
                    {
                        ItemCode = readerSQL["BMR_ITEM_CODE"].ToString(),
                        Vesion = readerSQL["BMR_VERSION"].ToString(),
                        StartDate = readerSQL["BMR_START_DATE"].ToString(),
                        EndDate = readerSQL["BMR_END_DATE"].ToString(),
                        Status = readerSQL["BMR_STATUS"].ToString()
                    };
                    listBMRVersionInfo.Add(bmrVersionInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Close();

            return listBMRVersionInfo;
        }
        public List<CleanControlInfo> GetCleanControl(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listCleanControlInfos = new List<CleanControlInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryGetCleanControlByItem(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    cleanControlInfo = new CleanControlInfo
                    {
                        hSysId = readerSQL["MX_CC_BMR_H_SYS_ID"].ToString(),
                        runNo = readerSQL["MX_CC_RUN_NO"].ToString(),
                        equipmentNo = readerSQL["MX_CC_EQUIPMENT_NO"].ToString(),
                        equipmentName = readerSQL["MX_CC_EQUIPMENT_NAME"].ToString(),
                        groupClean = readerSQL["GROUP_OP_NAME"].ToString(),
                        groupCheck =  readerSQL["GROUP_CHECK_NAME"].ToString(),
                        reqImageYn =  readerSQL["MX_CC_REQ_IMAGE_YN"].ToString()
                    };
                    listCleanControlInfos.Add(cleanControlInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Dispose();
            return listCleanControlInfos;
        }

        public List<MixStepInfo> GetMixStep(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listMixStepInfos = new List<MixStepInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.QueryGetMixStepByItem(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    mixStepInfo = new MixStepInfo
                    {
                        hSysId = readerSQL["MX_BMR_H_SYS_ID"].ToString(),
                        numStep = readerSQL["MX_STEP_NO"].ToString(),
                        numRunNo = readerSQL["SUB_RUN_NO"].ToString(),
                        stepDesc = readerSQL["MX_STEP_DESC"].ToString(),
                        groupOpName = readerSQL["SUB_GROUP_OP"].ToString(),
                        groupCheckName = readerSQL["SUB_GROUP_CHECK"].ToString(),
                        reqImageYn =  readerSQL["SUB_REQ_IMAGE_YN"].ToString(),
                        reqHumidityYn = readerSQL["SUB_REQ_HUMIDITY_YN"].ToString(),
                        reqPressYn = readerSQL["SUB_REQ_PRESS_YN"].ToString(),
                        reqTempYn =readerSQL["SUB_REQ_TEMP_YN"].ToString(),
                        reqVaccYn = readerSQL["SUB_REQ_VACC_YN"].ToString(),
                        reqStartStopYn =readerSQL["SUB_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["SUB_REQ_WEIGHT_YN"].ToString(),
                       reqAmountSampleYn= readerSQL["SUB_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                       subStepDesc = readerSQL["SUB_DESC"].ToString()
                    };
                    listMixStepInfos.Add(mixStepInfo);
                }
            }
            cmdSql.Dispose();
            connSQL.Dispose();
            return listMixStepInfos;
        }

        public List<PackingInfo> GetPacking(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listPackingInfo = new List<PackingInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetPk(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    packingInfo = new PackingInfo
                    {
                        hSysId = readerSQL["PK_BMR_H_SYS_ID"].ToString(),
                        numStep = readerSQL["PK_STEP_NO"].ToString(),
                        numRunNo = readerSQL["SUB_RUN_NO"].ToString(),
                        stepDesc = readerSQL["PK_STEP_DESC"].ToString(),
                        groupOpName = readerSQL["SUB_GROUP_OP"].ToString(),
                        groupCheckName = readerSQL["SUB_GROUP_CHECK"].ToString(),
                        reqImageYn = readerSQL["SUB_REQ_IMAGE_YN"].ToString(),
                        reqHumidityYn = readerSQL["SUB_REQ_HUMIDITY_YN"].ToString(),
                        reqPressYn = readerSQL["SUB_REQ_PRESS_YN"].ToString(),
                        reqTempYn = readerSQL["SUB_REQ_TEMP_YN"].ToString(),
                        reqVaccYn = readerSQL["SUB_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["SUB_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["SUB_REQ_WEIGHT_YN"].ToString(),
                        reqAmountSampleYn = readerSQL["SUB_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        subStepDesc = readerSQL["SUB_DESC"].ToString(),
                        reqCleanYn=readerSQL["SUB_REQ_CLEAN_PROCESS_YN"].ToString()
                    };
                    listPackingInfo.Add(packingInfo);
                }
            }
            connSQL.Close();
            return listPackingInfo;
        }
        public List<PkCleanControlInfo> GetPkCleanControl(String itemCode, String revision)
        {

            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listPkCleanControlInfo = new List<PkCleanControlInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetPKCleanControl(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    pkCleanControlInfo = new PkCleanControlInfo
                    {
                        hSysId = readerSQL["PK_CC_BMR_H_SYS_ID"].ToString(),
                        runNo = readerSQL["PK_CC_RUN_NO"].ToString(),
                        equipmentNo = readerSQL["PK_CC_EQUIPMENT_NO"].ToString(),
                        equipmentName = readerSQL["PK_CC_EQUIPMENT_NAME"].ToString(),
                        groupClean =readerSQL["GROUP_OP_NAME"].ToString(),
                        groupCheck = readerSQL["GROUP_CHECK_NAME"].ToString(),
                        reqImageYn =readerSQL["PK_CC_REQ_IMAGE_YN"].ToString()
                    };
                    listPkCleanControlInfo.Add(pkCleanControlInfo);
                }
            }
            connSQL.Close();
            return listPkCleanControlInfo;
        }
        public List<BcrCleanControlInfo> GetBcrCleanControl(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listBcrCleanControlInfo = new List<BcrCleanControlInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetBcrCleanControl(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bcrCleanControlInfo = new BcrCleanControlInfo
                    {
                        hSysId = readerSQL["BCR_CC_BMR_H_SYS_ID"].ToString(),
                        runNo = readerSQL["BCR_CC_RUN_NO"].ToString(),
                        equipmentNo = readerSQL["BCR_CC_EQUIPMENT_NO"].ToString(),
                        equipmentName = readerSQL["BCR_CC_EQUIPMENT_NAME"].ToString()
                    };
                    listBcrCleanControlInfo.Add(bcrCleanControlInfo);
                }
            }
            connSQL.Close();
            return listBcrCleanControlInfo;
        }
        public List<BcrInfo> GetBcr(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listBcrInfos = new List<BcrInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGerBcr(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());

            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bcrInfo = new BcrInfo
                    {
                        hSysId = readerSQL["BCR_BMR_H_SYS_ID"].ToString(),
                        numStep = readerSQL["BCR_STEP_NO"].ToString(),
                        numRunNo = readerSQL["SUB_RUN_NO"].ToString(),
                        stepDesc = readerSQL["BCR_STEP_DESC"].ToString(),
                        groupOpName = readerSQL["SUB_GROUP_OP"].ToString(),
                        groupCheckName = readerSQL["SUB_GROUP_CHECK"].ToString(),
                        reqImageYn = readerSQL["SUB_REQ_IMAGE_YN"].ToString(),
                        reqHumidityYn = readerSQL["SUB_REQ_HUMIDITY_YN"].ToString(),
                        reqPressYn = readerSQL["SUB_REQ_PRESS_YN"].ToString(),
                        reqTempYn = readerSQL["SUB_REQ_TEMP_YN"].ToString(),
                        reqVaccYn = readerSQL["SUB_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["SUB_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["SUB_REQ_WEIGHT_YN"].ToString(),
                        reqAmountSampleYn = readerSQL["SUB_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        subStepDesc = readerSQL["SUB_DESC"].ToString()
                    };
                    listBcrInfos.Add(bcrInfo);
                }
            }
            connSQL.Close();
            return listBcrInfos;
        }
        public List<BcaCleanControlInfo> GetBcaCleanControl(String itemCode, String revision)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listBcaCleanControlInfo = new List<BcaCleanControlInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGetBcaCleanControl(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bcaCleanControlInfo = new BcaCleanControlInfo
                    {
                        hSysId = readerSQL["BCA_CC_BMR_H_SYS_ID"].ToString(),
                        runNo = readerSQL["BCA_CC_RUN_NO"].ToString(),
                        equipmentNo = readerSQL["BCA_CC_EQUIPMENT_NO"].ToString(),
                        equipmentName = readerSQL["BCA_CC_EQUIPMENT_NAME"].ToString()
                    };
                    listBcaCleanControlInfo.Add(bcaCleanControlInfo);
                }
            }
            connSQL.Close();
            return listBcaCleanControlInfo;
        }
        public List<BcaInfo> GetBca(String itemCode, String revision)
        {

            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listBcaInfo = new List<BcaInfo>();
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.QueryGerBca(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DT", rvDt);
            readerSQL = cmdSql.ExecuteReader();
            if (readerSQL.HasRows)
            {
                while (readerSQL.Read())
                {
                    bcaInfo = new BcaInfo
                    {
                        hSysId = readerSQL["BCA_BMR_H_SYS_ID"].ToString(),
                        numStep = readerSQL["BCA_STEP_NO"].ToString(),
                        numRunNo = readerSQL["SUB_RUN_NO"].ToString(),
                        stepDesc = readerSQL["BCA_STEP_DESC"].ToString(),
                        groupOpName = readerSQL["SUB_GROUP_OP"].ToString(),
                        groupCheckName = readerSQL["SUB_GROUP_CHECK"].ToString(),
                        reqImageYn = readerSQL["SUB_REQ_IMAGE_YN"].ToString(),
                        reqHumidityYn = readerSQL["SUB_REQ_HUMIDITY_YN"].ToString(),
                        reqPressYn = readerSQL["SUB_REQ_PRESS_YN"].ToString(),
                        reqTempYn = readerSQL["SUB_REQ_TEMP_YN"].ToString(),
                        reqVaccYn = readerSQL["SUB_REQ_VACC_YN"].ToString(),
                        reqStartStopYn = readerSQL["SUB_REQ_START_STOP_YN"].ToString(),
                        reqWeightYn = readerSQL["SUB_REQ_WEIGHT_YN"].ToString(),
                        reqAmountSampleYn = readerSQL["SUB_REQ_WEIGHT_SAMPLE_YN"].ToString(),
                        subStepDesc = readerSQL["SUB_DESC"].ToString()

                    };
                    listBcaInfo.Add(bcaInfo);
                }
            }
            connSQL.Close();
            return listBcaInfo;
        }
        public String InsertRunJob(String itemCode, String itemName, String lot, String revision, Double batchSize, String uom, String remark, Int64 userId, String startDt, String endDt)
        {
            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];

            listRunJob = new List<RunJob>();
            connSQL.Open();
            cmdSql = new SqlCommand(query.InsertRunJob(), connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode.ToUpper());
            cmdSql.Parameters.AddWithValue("@P_ITEM_NAME", itemName);
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DATE", rvDt);
            //cmdSql.Parameters.AddWithValue("@P_VERSION", revision);
            cmdSql.Parameters.AddWithValue("@P_LOT", lot);
            cmdSql.Parameters.AddWithValue("@P_BATCH_SIZE", batchSize);
            cmdSql.Parameters.AddWithValue("@P_UOM", uom);
            cmdSql.Parameters.AddWithValue("@P_REMARK", remark);
            cmdSql.Parameters.AddWithValue("@P_USER_ID", userId);
            cmdSql.Parameters.AddWithValue("@P_START_DT", startDt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", endDt);
            cmdSql.ExecuteNonQuery();
            connSQL.Dispose();
            connSQL.Close();
            return status;
        }
        public List<ItemMasterInfo> GetItemMaster()
        {
            listItemMasterInfos = new List<ItemMasterInfo>();
            connORCL.Open();
            cmdORCL = new OracleCommand(query.QueryGetItemFG(), connORCL);
            readerORCL = cmdORCL.ExecuteReader();
            if (readerORCL.HasRows)
            {
                while (readerORCL.Read())
                {
                    itemMasterInfo = new ItemMasterInfo
                    {
                        itemCode = readerORCL["ITEM_CODE"].ToString(),
                        itemName = readerORCL["ITEM_NAME"].ToString(),
                        itemNameUom = readerORCL["ITEM_CODE_NAME"].ToString()
                    };
                    listItemMasterInfos.Add(itemMasterInfo);
                }
            }
            cmdORCL.Dispose();
            connORCL.Close();
            return listItemMasterInfos;
        }
        public String UpdateBmrHead(String itemCode, String revision, String startDt, String endDt,Int32 statusBmr) {

            String[] rv = revision.Split('/');
            String rv1 = rv[0].Split('-')[0].Replace("R", "");
            String rv2 = rv[0].Split('-')[1];
            String rvDt = rv[1];


            status = "";
            connSQL.Open();
            cmdSql = new SqlCommand(queryRunJob.UpdateBmrHead(),connSQL);
            cmdSql.Parameters.AddWithValue("@P_ITEM_CODE", itemCode);
            cmdSql.Parameters.AddWithValue("@P_RV1", rv1);
            cmdSql.Parameters.AddWithValue("@P_RV2", rv2);
            cmdSql.Parameters.AddWithValue("@P_RV_DATE", rvDt);
            cmdSql.Parameters.AddWithValue("@P_STATUS_BMR", statusBmr);
            cmdSql.Parameters.AddWithValue("@P_START_DT", startDt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", endDt);
            cmdSql.ExecuteNonQuery();
            cmdSql.Dispose();
            connSQL.Dispose();
            return status;
        }
    }
}