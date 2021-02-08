using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ModalEditJobInfo
    {
        public String jobSysId { get; set; }
        public String jobLot { get; set; }
        public String jobBatchSize { get; set; }
        public String jobUom { get; set; }
        public String jobStartDt { get; set; }
        public String jobEndDt { get; set; }
        public String jobRemark { get; set; }
    }
}