using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class QueryUserManagement
    {
        String query = "";
        public String QuerySelectUser()
        {
            query = @"select BMR_USER_CTRL.USER_SYS_ID as CTRL_SYS_ID,BMR_USER_LINK.USER_L_SYS_ID as Link_SYS_ID,BMR_USER_CTRL.User_Login,user_name,User_pre,BMR_USER_GROUP.Group_ID,Group_Name,CONVERT(char,user_start_date,111) as user_start_date,CONVERT(char,user_end_date,111) as user_end_date,GROUP_CREATE,GROUP_RUN_JOB,GROUP_MIXING_CC_CLEAN,GROUP_MIXING_CC_CHECK,GROUP_MIXING_OPERATE,GROUP_MIXING_CHECK,GROUP_ACTIVE,user_err,BMR_USER_CTRL.user_Active,USER_TOKEN from BMR_USER_CTRL,BMR_USER_LINK,BMR_USER_GROUP where BMR_USER_CTRL.USER_SYS_ID = USER_L_USER_SYS_ID and USER_L_GROUP_ID = BMR_USER_GROUP.Group_ID";
            return query;
        }
        public String QuerySelectGroup()
        {
            query = @"select GROUP_ID,GROUP_NAME from BMR_USER_GROUP where GROUP_ACTIVE is not null";
            return query;
        }
        public String QuerySelectGroupDetial()
        {
            query = @"SELECT GROUP_ID,GROUP_NAME,GROUP_DESC,GROUP_CREATE,GROUP_RUN_JOB,GROUP_MIXING_CC_CLEAN,GROUP_MIXING_CC_CHECK,GROUP_MIXING_OPERATE,GROUP_MIXING_CHECK,GROUP_ACTIVE,GROUP_CR_USR_ID,GROUP_CR_DT,GROUP_UPD_USR_ID,GROUP_UPD_DT FROM BMR_USER_GROUP WHERE GROUP_ID = @P_GROUP_ID";
            return query;
        }
        public String QueryDelUser_ctrl()
        {
            query = @"DELETE FROM bmr_User_ctrl WHERE USER_SYS_ID = @P_USER_SYS_ID and User_Login = @P_DEL_USER_LOGIN and User_name = @P_DEL_USER_NAME";
            return query;
        }
        public String QueryGetUser_CTRL_ID()
        {
            query = @"SELECT USER_SYS_ID FROM BMR_USER_CTRL where USER_LOGIN = @P_USER_LOGIN and USER_NAME = @P_USER_NAME and USER_PRE = @P_USER_PRE and USER_START_DATE = @P_START_DT and USER_END_DATE = @P_END_DT";
            return query;
        }
        public String QueryUpdate_User_ctrl()
        {
            query = @"UPDATE BMR_User_ctrl SET User_Login = @P_USER_LOGIN,User_name = @P_USER_NAME,User_pre = @P_USERPRE,user_start_date = @P_START_DT,user_end_date = @P_END_DT where USER_SYS_ID = @P_USER_CTRL_SYS_ID";
            return query;
        }
        public String QueryUpdateGroupUser()
        {
            query = @"update BMR_USER_LINK SET USER_L_USER_SYS_ID = @P_USER_CTRL_SYS_ID,USER_L_GROUP_ID = @P_USER_GROUP_ID where USER_L_SYS_ID = @P_USER_LINK_SYS_ID";
            return query;
        }
        public String QueryInsertGroup()
        {
            query = @"INSERT INTO BMR_USER_GROUP (GROUP_NAME,GROUP_DESC,GROUP_CREATE,GROUP_RUN_JOB,GROUP_MIXING_CC_CLEAN,GROUP_MIXING_CC_CHECK,GROUP_MIXING_OPERATE,GROUP_MIXING_CHECK,GROUP_ACTIVE) VALUES (@P_GROUP_NAME,@P_GROUP_DESC,@P_GROUP_CREATE,@P_GROUP_RUN_JOB,@P_GROUP_MIX_CC_CLEAN,@P_GROUP_MIX_CC_CHECK,@P_GROUP_OPERATE,@P_MIX_CHECK,@P_ACTIVE)";
            return query;
        }
        public String QueryUpdateGroupDetial()
        {
            query = @"UPDATE BMR_USER_GROUP SET GROUP_NAME = @P_GROUP_NAME,GROUP_DESC = @P_GROUP_DESC,GROUP_CREATE = @P_GROUP_CREATE,GROUP_RUN_JOB = @P_GROUP_RUN_JOB,GROUP_MIXING_CC_CLEAN = @P_GROUP_MIXING_CC_CLEAN,GROUP_MIXING_CC_CHECK = @P_GROUP_MIXING_CC_CHECK,GROUP_MIXING_OPERATE = @P_GROUP_MIXING_OPERATE,GROUP_MIXING_CHECK = @P_GROUP_MIXING_CHECK,GROUP_ACTIVE = @P_GROUP_ACTIVE WHERE GROUP_ID = @P_GROUP_ID";
            return query;
        }
        public String QueryDeleteGroup()
        {
            query = @"DELETE FROM BMR_USER_GROUP WHERE GROUP_ID = @P_GROUP_ID";
            return query;
        }
        public String QueryDelUser_link()
        {
            query = @"DELETE FROM BMR_USER_LINK WHERE  USER_L_SYS_ID = @P_L_USER_SYS_ID and USER_L_USER_SYS_ID = @P_DEL_USER_SYS_ID";
            return query;
        }
        public String QueryInsert_User_ctrl()
        {
            query = @"INSERT INTO BMR_USER_CTRL (User_Login,User_Pass,User_name,User_pre,USER_START_DATE,USER_END_DATE,USER_ERR,USER_ACTIVE) VALUES (@P_USER_LOGIN,@P_USER_PASS,@P_USER_NAME,@P_USERPRE,@P_START_DT,@P_END_DT,'0','1')";
            return query;
        }
        public String QueryInsert_User_Link()
        {
            query = @"INSERT INTO BMR_USER_LINK (USER_L_USER_SYS_ID,USER_L_GROUP_ID,USER_L_ACTIVE) VALUES (@P_USER_SYS_ID,@P_GROUP_ID,'1')";
            return query;
        }
        public String QueryResetPass()
        {
            query = @"UPDATE BMR_USER_CTRL SET USER_PASS = @P_PASS where USER_SYS_ID = @P_USER_SYS_ID";
            return query;
        }
    }
}