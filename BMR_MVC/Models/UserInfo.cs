using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class UserInfo
    {
        public String userId { get; set; }
        public String userName { get; set; }
        public String userPass { get; set; }
        public String userPre { get; set; }
        public String userActive { get; set; }
        public String token { get; set; }
        public Boolean create { get; set; }
        public Boolean runJob { get; set; }
        public Boolean mxCleanClean { get; set; }
        public Boolean mxCleanCheck { get; set; }
        public Boolean mxOperate { get; set; }
        public Boolean mxCheck { get; set; }
    }
}