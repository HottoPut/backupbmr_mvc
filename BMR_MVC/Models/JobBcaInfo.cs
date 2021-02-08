using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class JobBcaInfo
    {
        public String jobSysId { get; set; }
        public String jobApprPd { get; set; }
        public String jobApprQa { get; set; }
        public String itemCode { get; set; }
        public String itemName { get; set; }
        public String bmrVersion { get; set; }
        public String startDt { get; set; }
        public String endDt { get; set; }
        public String lot { get; set; }
        public String batchSize { get; set; }
        public String status { get; set; }
        public String ccrRunNo { get; set; }
        public String ccrEquipmentNo { get; set; }
        public String ccrEquipmentName { get; set; }
        public String ccrReqImageYn { get; set; }
        public String ccrCleanUserName { get; set; }
        public String ccrCleanDt { get; set; }
        public String ccrCheckUserName { get; set; }
        public String ccrCheckDt { get; set; }
        public String bcaOperateUserName { get; set; }
        public String bcaCheckUserName { get; set; }
        public String bcaOperateDt { get; set; }
        public String bcaCheckDt { get; set; }
        public String bcaStep { get; set; }
        public String bcaRunNo { get; set; }
        public String bcaStepDesc { get; set; }
        public String bcaTemp { get; set; }
        public String userProgress { get; set; }
        public String qaProgress { get; set; }
        public String remark { get; set; }
        public String reqTempYn { get; set; }
        public String reqPressYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqStartStopYn { get; set; }
        public String reqWeightYn { get; set; }
        public String reqVaccYn { get; set; }
        public String reqWeightSampleYn { get; set; }
        public String reqImageYn { get; set; }
        public String ccGroupCleanId { get; set; }
        public String ccGroupCheckId { get; set; }
        public String bcaGroupOperateId { get; set; }
        public String bcaGroupCheckId { get; set; }
    }
}