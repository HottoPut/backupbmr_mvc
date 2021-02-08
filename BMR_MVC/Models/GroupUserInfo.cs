using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class GroupUserInfo
    {
        public String gui_group_id { get; set; }
        public String gui_group_name { get; set; }
        public String gui_group_desc { get; set; }
        public String gui_group_create { get; set; }
        public String gui_group_run_job { get; set; }
        public String gui_group_mix_cc_clean { get; set; }
        public String gui_group_mix_cc_check { get; set; }
        public String gui_group_mix_operate { get; set; }
        public String gui_group_mix_check { get; set; }
        public String gui_group_active { get; set; }
        public String gui_group_cr_user_id { get; set; }
        public String gui_group_cr_dt { get; set; }
        public String gui_group_upd_user { get; set; }
        public String gui_group_upd_dt { get; set; }
    }
}