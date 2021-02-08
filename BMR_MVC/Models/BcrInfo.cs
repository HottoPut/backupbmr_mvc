using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class BcrInfo
    {
        public String hSysId { get; set; }
        public String numStep { get; set; }
        public String numRunNo { get; set; }
        public String stepDesc { get; set; }
        public String subStepDesc { get; set; }
        public String groupOpName { get; set; }
        public String groupCheckName { get; set; }
        public String reqImageYn { get; set; }
        public String reqHumidityYn { get; set; }
        public String reqPressYn { get; set; }
        public String reqTempYn { get; set; }
        public String reqVaccYn { get; set; }
        public String reqStartStopYn { get; set; }
        public String reqWeightYn { get; set; }
        public String reqAmountSampleYn { get; set; }
        public String reqCleanYn { get; set; }
        public String standardW { get; set; }
    }
}