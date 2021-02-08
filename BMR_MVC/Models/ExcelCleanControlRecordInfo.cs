using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC
{
    public class ExcelCleanControlRecordInfo
    {
        public String fgCode { get; set; }
        public String runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String reqImageYn { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get;set;}
        public String requireYn { get; set; }
        public String logNo { get; set; }
    }
    public class ExcelMixStepInfo
    {
        public String fgCode { get; set; }
        public String runNo { get; set; }
        public String step { get; set; }
        public String stepDesc { get; set; }
        public String stepDescEng { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requiredYn { get; set; }
        public String tempEnv { get; set; }
        public String humidityEnv { get; set; }
        public String pressureEnv { get; set; }
        public String reqTempEnvYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqPressYn { get; set; }
        public String temp { get; set; }
        public String reqTempYn { get; set; }
        public String vaccRate { get; set; }
        public String reqVaccYn { get; set; }
        public String startDt { get; set; }
        public String endDt { get; set; }
        public String reqStartStopYn { get; set; }
        public String result { get; set; }
        public String reqResultYn { get; set; }
        public String reqWeightYn { get; set; }
        public String standardW { get; set; }
        public String version { get; set; }
        public String reqWeightSampleYn { get; set; }
        public String reqImageYn { get; set; }
    }
    public class ExcelPKCleanControlRecordInfo
    {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requireYn { get; set; }
        public String logNo { get; set; }
        public String reqImageYn { get; set; }
    }

    public class ExcelPKProcedureInfo {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String step { get; set; }
        public String stepDesc { get; set; }
        public String stepDescEng { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requiredYn { get; set; }
        public String tempEnv { get; set; }
        public String humidityEnv { get; set; }
        public String pressureEnv { get; set; }
        public String reqTempEnvYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqPressYn { get; set; }
        public String temp { get; set; }
        public String reqTempYn { get; set; }
        public String vaccRate { get; set; }
        public String reqVaccYn { get; set; }
        public String startDt { get; set; }
        public String endDt { get; set; }
        public String reqStartStopYn { get; set; }
        public String result { get; set; }
        public String reqResultYn { get; set; }
        public String reqWeightYn { get; set; }
        public String standardW { get; set; }
        public String version { get; set; }
        public String reqWeightSampleYn { get; set; }
        public String reqCleanYn { get; set; }
        public String reqImageYn { get; set; }
    }



    public class ExcelBcrCleanControlRecordInfo
    {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requireYn { get; set; }
        public String logNo { get; set; }
        public String reqImageYn { get; set; }
    }
    public class ExcelBcrProcedureInfo
    {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String step { get; set; }
        public String stepDesc { get; set; }
        public String stepDescEng { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requiredYn { get; set; }
        public String tempEnv { get; set; }
        public String humidityEnv { get; set; }
        public String pressureEnv { get; set; }
        public String reqTempEnvYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqPressYn { get; set; }
        public String temp { get; set; }
        public String reqTempYn { get; set; }
        public String vaccRate { get; set; }
        public String reqVaccYn { get; set; }
        public String startDt { get; set; }
        public String endDt { get; set; }
        public String reqStartStopYn { get; set; }
        public String result { get; set; }
        public String reqResultYn { get; set; }
        public String reqWeightYn { get; set; }
        public String standardW { get; set; }
        public String version { get; set; }
        public String reqWeightSampleYn { get; set; }
        public String reqCleanYn { get; set; }
        public String reqImageYn { get; set; }
    }


    public class ExcelBcaCleanControlRecordInfo
    {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requireYn { get; set; }
        public String logNo { get; set; }
        public String reqImageYn { get; set; }
    }

    public class ExcelBcaProcedureInfo
    {
        public String itemCode { get; set; }
        public String runNo { get; set; }
        public String step { get; set; }
        public String stepDesc { get; set; }
        public String stepDescEng { get; set; }
        public String groupOp { get; set; }
        public String groupChk { get; set; }
        public String userBy { get; set; }
        public String userDt { get; set; }
        public String checkBy { get; set; }
        public String checkDt { get; set; }
        public String requiredYn { get; set; }
        public String tempEnv { get; set; }
        public String humidityEnv { get; set; }
        public String pressureEnv { get; set; }
        public String reqTempEnvYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqPressYn { get; set; }
        public String temp { get; set; }
        public String reqTempYn { get; set; }
        public String vaccRate { get; set; }
        public String reqVaccYn { get; set; }
        public String startDt { get; set; }
        public String endDt { get; set; }
        public String reqStartStopYn { get; set; }
        public String result { get; set; }
        public String reqResultYn { get; set; }
        public String reqWeightYn { get; set; }
        public String standardW { get; set; }
        public String version { get; set; }
        public String reqWeightSampleYn { get; set; }
        public String reqCleanYn { get; set; }
        public String reqImageYn { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<ExcelCleanControlRecordInfo> ExcelCleanControlRecordInfoss { get; set; }
        public IEnumerable<ExcelMixStepInfo> ExcelMixStepInfoss { get; set; }
        public IEnumerable<ExcelPKCleanControlRecordInfo> ExcelPKCleanControlRecordInfos { get; set; }
        public IEnumerable<ExcelPKProcedureInfo> ExcelPKProcedureInfos { get; set; }
        public IEnumerable<ExcelBcrCleanControlRecordInfo> ExcelBcrCleanControlRecordInfos { get; set; }
        public IEnumerable<ExcelBcrProcedureInfo> ExcelBcrProcedureInfos { get; set; }
        public IEnumerable<ExcelBcaCleanControlRecordInfo> ExcelBcaCleanControls { get; set; }
        public IEnumerable<ExcelBcaProcedureInfo> ExcelBcaProcedureInfos { get; set; }
    }
}