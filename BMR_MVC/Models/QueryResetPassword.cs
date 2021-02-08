using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class QueryResetPassword
    {
        String query;
        public String QueryCheckResetPassword()
        {
            query = @"SELECT * FROM BMR_USER_CTRL where LOWER(USER_LOGIN) = LOWER(@username)";
            return query;
        }
        public String QuerySavePass()
        {
            query = @"UPDATE BMR_USER_CTRL SET USER_PASS = @P_PASS where USER_SYS_ID = @P_USER_SYS_ID";
            return query;
        }
    }
}