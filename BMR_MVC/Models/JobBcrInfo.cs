using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class JobBcrInfo
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
        public String bcrOperateUserName { get; set; }
        public String bcrCheckUserName { get; set; }
        public String bcrOperateDt { get; set; }
        public String bcrCheckDt { get; set; }
        public String bcrStep { get; set; }
        public String bcrRunNo { get; set; }
        public String bcrStepDesc { get; set; }
        public String bcrTemp { get; set; }
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
        public String bcrGroupOperateId { get; set; }
        public String bcrGroupCheckId { get; set; }
    }
}