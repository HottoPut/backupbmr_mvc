using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMR_MVC.Models
{
    public class BmrVersionInfo
    {
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String Vesion { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String Revision { get; set; }
        public String rv1 { get; set; }
        public String rv2 { get; set; }
        public String rvDt { get; set; }
        public String Status { get; set; }
        //public List<SelectListItem> DropDownList { get; set; }
    }
}