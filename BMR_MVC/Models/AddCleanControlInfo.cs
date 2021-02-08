using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class AddCleanControlInfo
    {
        public Int64 jobSysId { get; set; }
        public Int64 runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public Int32 groupClean { get; set; }
        public Int32 groupCheck { get; set; }
        public String reqImageYn { get; set; }
    }
}