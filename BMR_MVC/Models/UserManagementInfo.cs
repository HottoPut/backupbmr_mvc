using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class UserManagementInfo
    {
        public String umm_link_user_sys_id { get; set; }
        public String umm_ctrl_user_sys_id { get; set; }
        public String umm_user_login { get; set; }
        public String umm_user_name { get; set; }
        public String umm_user_pre { get; set; }
        public String umm_group_id { get; set; }
        public String umm_group_name { get; set; }
        public String umm_start_date { get; set; }
        public String umm_end_date { get; set; }
        public String umm_group_create { get; set; }
        public String umm_group_run_job { get; set; }
        public String umm_group_mix_cc_clean { get; set; }
        public String umm_group_mix_cc_check { get; set; }
        public String umm_group_mix_operate { get; set; }
        public String umm_group_mix_check { get; set; }
        public String umm_group_active { get; set; }
        public String umm_user_token { get; set; }
        public String umm_err { get; set; }
        public String umm_active { get; set; }
    }
}