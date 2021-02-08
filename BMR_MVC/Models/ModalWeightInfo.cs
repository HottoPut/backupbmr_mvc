using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ModalWeightInfo
    {
        public String listNo { get; set; }
        public String tankAmount { get; set; }
        public String containerNo { get; set; }
        public String totalWeight { get; set; }
        public String tareWeight { get; set; }
        public String netWeight { get; set; }
        public Int32 statusOperate { get; set; }
        public String sToTal {get;set;}
        public String sTheoretical { get; set; }
        public String sYield { get; set; }
        public String userOperate { get; set; }
        public String userCheck { get; set; }
        public String dtCheck { get; set; }
    }
}