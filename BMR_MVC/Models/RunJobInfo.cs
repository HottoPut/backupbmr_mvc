using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class RunJobInfo
    {
        public String hSysId { get; set; }
        public String itemCode { get; set; }
        public String lot { get; set; }
        public String batchSize { get; set; }

    }
}