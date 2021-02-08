using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ModalCleanInfo
    {
        public String amountBot { get; set; }
        public String waterTemp { get; set; }
        public String botStartDt { get; set; }
        public String botEndDt { get; set; }
        public String amountLid { get; set; }
        public String lidStartDt { get; set; }
        public String lidEndDt { get; set; }
        public String amountBakeLid {get;set;}
        public String icbtTemp { get; set; }
        public String bakeStartDt { get; set; }
        public String bakeEndDt { get; set; }
    }
}