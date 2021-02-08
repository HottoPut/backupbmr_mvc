using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ExcelMixStepInfo2
    {
        public String FG_CODE { get; set; }
        public String RUN_NO { get; set; }
        public String STEP { get; set; }
        public String STEP_DESC { get; set; }
        public String USER_BY { get; set; }
        public String USER_DT { get; set; }
        public String CHECK_BY { get; set; }
        public String CHECK_DT { get; set; }
        public String REQUIRED_YN { get; set; }
        public String TEMP_ENV { get; set; }
        public String HUMUDITY_ENV { get; set; }
        public String PRESSURE_ENV { get; set; }
        public String REQ_TEMP_ENV_YN { get; set; }
        public String REQ_HUMUDITY_YN { get; set; }
        public String REQ_PRESS_YN { get; set; }
        public String TEMP { get; set; }
        public String REQ_TEMP_YN { get; set; }
        public String VACC_RATE { get; set; }
        public String REQ_VACC_YN { get; set; }
        public String START_DT { get; set; }
        public String END_DT { get; set; }
        public String REQ_START_STOP_YN { get;set;}
        public String RESULT { get; set; }
        public String REQ_RESULT_YN { get; set; }
        public String REQ_WEIGHT { get; set; }
        public String STANDARD_W { get; set; }
    }
}