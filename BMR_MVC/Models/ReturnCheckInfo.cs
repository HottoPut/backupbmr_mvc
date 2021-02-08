using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ReturnCheckInfo
    {
        public String status { get; set; }
        public String dt { get; set; }
        public String apprPdStatus { get; set; }
        public String apprQaStatus { get; set; }
    }
}