using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ExcelInfo
    {
        public List<ExcelCleanControlRecordInfo> ExcelCleanControlRecordInfo { get; set; }
        public List<ExcelMixStepInfo> ExcelMixStepInfo { get; set; }
        public List<ExcelPKCleanControlRecordInfo> excelPKCleanControlRecordInfo { get; set; }
        public List<ExcelPKProcedureInfo> excelPKProcedureInfo { get; set; }
        public List<ExcelBcrCleanControlRecordInfo> excelBcrCleanControlRecordInfo { get; set; }
        public List<ExcelBcrProcedureInfo> excelBcrProcedureInfo { get; set; }
        public List<ExcelBcaCleanControlRecordInfo> excelBcaCleanControlRecordInfo { get; set; }
        public List<ExcelBcaProcedureInfo> excelBcaProcedureInfo { get; set; }
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String BmrVesion { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
    }
}