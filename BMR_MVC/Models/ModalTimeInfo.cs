using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ModalTimeInfo
    {
        public String listNo { get; set; }
        public String startTime { get; set; }
        public String endTime { get; set; }
        public Double rpm { get; set; }
        public Int32 statusOperate { get; set; } //First time operate
        public String userOperate { get; set; }
        public String userCheck { get; set; }
        public String checkDt { get; set; }
    }
}