using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class BomSFInfo
    {
        public String itemFGCode { get; set; }
        public String itemRMCode { get; set; }
        public String itemRMName { get; set; }
        public String itemQty { get; set; }
        public String itemQtyLs { get; set; }
        public String itemUom { get; set; }
        public String lose { get; set; }
        public String comvFactor { get; set; }
        public String batchSize { get; set; }
        public String bomUom { get; set; }
    }
}