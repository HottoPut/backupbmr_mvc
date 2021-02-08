using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class BcaCleanControlInfo
    {
        public String hSysId { get; set; }
        public String runNo { get; set; }
        public String equipmentNo { get; set; }
        public String equipmentName { get; set; }
        public String groupClean { get; set; }
        public String groupCheck { get; set; }
        public String reqImageYn { get; set; }
    }
}