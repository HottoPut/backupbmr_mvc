using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class UserManagement
    {
        String status;
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();

        SqlConnection connSql;
        SqlDataReader readerSql;
        SqlCommand cmdSql;
        QuerMixing query;
        QueryUserManagement queryUserManagement;
        UserManagementInfo userinfo;
        List<UserManagementInfo> listuserinfo;
        GroupUserInfo GroupUser;
        List<GroupUserInfo> listgroup;
        GroupDetialInfo GroupDetial;
        List<GroupDetialInfo> listgroupdetial;

        public UserManagement()
        {
            queryUserManagement = new QueryUserManagement();

        }

        public List<UserManagementInfo> getuser()
        {
            listuserinfo = new List<UserManagementInfo>();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QuerySelectUser(), connSql);
            readerSql = cmdSql.ExecuteReader();
            if (readerSql.HasRows)
            {
                while (readerSql.Read())
                {
                    userinfo = new UserManagementInfo
                    {
                        umm_ctrl_user_sys_id = readerSql["CTRL_SYS_ID"].ToString(),
                        umm_link_user_sys_id = readerSql["Link_SYS_ID"].ToString(),
                        umm_user_login = readerSql["User_Login"].ToString(),
                        umm_user_name = readerSql["user_name"].ToString(),
                        umm_user_pre = readerSql["User_pre"].ToString(),
                        umm_group_id = readerSql["Group_ID"].ToString(),
                        umm_group_name = readerSql["Group_name"].ToString(),
                        umm_start_date = readerSql["user_start_date"].ToString(),
                        umm_end_date = readerSql["user_end_date"].ToString(),
                        umm_group_create = readerSql["GROUP_CREATE"].ToString(),
                        umm_group_run_job = readerSql["GROUP_RUN_JOB"].ToString(),
                        umm_group_mix_cc_clean = readerSql["GROUP_MIXING_CC_CLEAN"].ToString(),
                        umm_group_mix_cc_check = readerSql["GROUP_MIXING_CC_CHECK"].ToString(),
                        umm_group_mix_operate = readerSql["GROUP_MIXING_OPERATE"].ToString(),
                        umm_group_mix_check = readerSql["GROUP_MIXING_CHECK"].ToString(),
                        umm_group_active = readerSql["GROUP_ACTIVE"].ToString(),
                        umm_user_token = readerSql["USER_TOKEN"].ToString(),
                        umm_err = readerSql["user_err"].ToString(),
                        umm_active = readerSql["user_Active"].ToString()
                    };
                    listuserinfo.Add(userinfo);
                }
            }

            connSql.Close();

            return listuserinfo;
        }

        public List<GroupUserInfo> getgroupuser(String Group)
        {
            listgroup = new List<GroupUserInfo>();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QuerySelectGroup(), connSql);
            readerSql = cmdSql.ExecuteReader();
            if (readerSql.HasRows)
            {
                while (readerSql.Read())
                {
                    GroupUser = new GroupUserInfo
                    {
                        gui_group_id = readerSql["GROUP_ID"].ToString(),
                        gui_group_name = readerSql["GROUP_NAME"].ToString()


                    };
                    listgroup.Add(GroupUser);
                }
            }
            connSql.Close();
            return listgroup;
        }

        public List<GroupDetialInfo> GetGroupDetials(String group_id)
        {
            listgroupdetial = new List<GroupDetialInfo>();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QuerySelectGroupDetial(), connSql);
            cmdSql.Parameters.AddWithValue("@P_GROUP_ID", group_id);
            readerSql = cmdSql.ExecuteReader();

            if (readerSql.HasRows)
            {
                while (readerSql.Read())
                {
                    GroupDetial = new GroupDetialInfo
                    {
                        gdi_group_id = readerSql["GROUP_ID"].ToString(),
                        gdi_group_name = readerSql["GROUP_NAME"].ToString(),
                        gdi_group_desc = readerSql["GROUP_DESC"].ToString(),
                        gdi_group_create = readerSql["GROUP_CREATE"].ToString(),
                        gdi_group_run_job = readerSql["GROUP_RUN_JOB"].ToString(),
                        gdi_group_mix_cc_clean = readerSql["GROUP_MIXING_CC_CLEAN"].ToString(),
                        gdi_group_mix_cc_check = readerSql["GROUP_MIXING_CC_CHECK"].ToString(),
                        gdi_group_mix_operate = readerSql["GROUP_MIXING_OPERATE"].ToString(),
                        gdi_group_mix_check = readerSql["GROUP_MIXING_CHECK"].ToString()
                    };
                    listgroupdetial.Add(GroupDetial);
                }
            }
            connSql.Close();
            return listgroupdetial;
        }

        public String DeleteUser(String user_login, String username, String group_id, String ctrl_user_sys_id, String link_user_sys_id)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryDelUser_ctrl(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_SYS_ID", ctrl_user_sys_id);
            cmdSql.Parameters.AddWithValue("@P_DEL_USER_LOGIN", user_login);
            cmdSql.Parameters.AddWithValue("@P_DEL_USER_NAME", username);
            cmdSql.ExecuteNonQuery();

            cmdSql = new SqlCommand(queryUserManagement.QueryDelUser_link(), connSql);
            cmdSql.Parameters.AddWithValue("@P_L_USER_SYS_ID", link_user_sys_id);
            cmdSql.Parameters.AddWithValue("@P_DEL_USER_SYS_ID", ctrl_user_sys_id);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }

        public String InsertUser(String user_login, String user_pass, String user_name, String user_pre, String start_dt, String end_dt, String group_id)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryInsert_User_ctrl(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", user_login);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", user_pass);
            cmdSql.Parameters.AddWithValue("@P_USER_NAME", user_name);
            cmdSql.Parameters.AddWithValue("@P_USERPRE", user_pre);
            cmdSql.Parameters.AddWithValue("@P_START_DT", start_dt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", end_dt);
            cmdSql.ExecuteNonQuery();

            String user_ctrl_id;
            cmdSql = new SqlCommand(queryUserManagement.QueryGetUser_CTRL_ID(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", user_login);
            cmdSql.Parameters.AddWithValue("@P_USER_NAME", user_name);
            cmdSql.Parameters.AddWithValue("@P_USER_PRE", user_pre);
            cmdSql.Parameters.AddWithValue("@P_START_DT", start_dt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", end_dt);
            user_ctrl_id = cmdSql.ExecuteScalar().ToString();


            cmdSql = new SqlCommand(queryUserManagement.QueryInsert_User_Link(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_SYS_ID", user_ctrl_id);
            cmdSql.Parameters.AddWithValue("@P_GROUP_ID", group_id);
            cmdSql.ExecuteNonQuery();


            connSql.Close();
            return status;

        }

        public String UpdateUser(String user_login, String user_name, String user_pre, String start_dt, String end_dt, String group_id, String user_ctrl_id, String user_link_id)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryUpdate_User_ctrl(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", user_login);
            cmdSql.Parameters.AddWithValue("@P_USER_NAME", user_name);
            cmdSql.Parameters.AddWithValue("@P_USERPRE", user_pre);
            cmdSql.Parameters.AddWithValue("@P_START_DT", start_dt);
            cmdSql.Parameters.AddWithValue("@P_END_DT", end_dt);
            cmdSql.Parameters.AddWithValue("@P_USER_CTRL_SYS_ID", user_ctrl_id);
            cmdSql.ExecuteNonQuery();

            String update_type = "1";
            cmdSql = new SqlCommand(queryUserManagement.QueryUpdateGroupUser(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_CTRL_SYS_ID", user_ctrl_id);
            cmdSql.Parameters.AddWithValue("@P_USER_GROUP_ID", group_id);
            cmdSql.Parameters.AddWithValue("@P_USER_LINK_SYS_ID", user_link_id);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }

        public String InsertGroup(String group_name, String group_desc, String group_create, String group_run_job, String group_mix_cc_clean, String group_mix_cc_check, String group_mix_operate, String group_mix_check, String group_active)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryInsertGroup(), connSql);
            cmdSql.Parameters.AddWithValue("@P_GROUP_NAME", group_name);
            cmdSql.Parameters.AddWithValue("@P_GROUP_DESC", group_desc);
            cmdSql.Parameters.AddWithValue("@P_GROUP_CREATE", group_create);
            cmdSql.Parameters.AddWithValue("@P_GROUP_RUN_JOB", group_run_job);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIX_CC_CLEAN", group_mix_cc_clean);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIX_CC_CHECK", group_mix_cc_check);
            cmdSql.Parameters.AddWithValue("@P_GROUP_OPERATE", group_mix_operate);
            cmdSql.Parameters.AddWithValue("@P_MIX_CHECK", group_mix_check);
            cmdSql.Parameters.AddWithValue("@P_ACTIVE", group_active);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }

        public String UpdateGroup(String group_name, String group_desc, String group_create, String group_run_job, String group_mix_cc_clean, String group_mix_cc_check, String group_mix_operate, String group_mix_check, String group_id, String group_active)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryUpdateGroupDetial(), connSql);
            cmdSql.Parameters.AddWithValue("@P_GROUP_NAME", group_name);
            cmdSql.Parameters.AddWithValue("@P_GROUP_DESC", group_desc);
            cmdSql.Parameters.AddWithValue("@P_GROUP_CREATE", group_create);
            cmdSql.Parameters.AddWithValue("@P_GROUP_RUN_JOB", group_run_job);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIXING_CC_CLEAN", group_mix_cc_clean);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIXING_CC_CHECK", group_mix_cc_check);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIXING_OPERATE", group_mix_operate);
            cmdSql.Parameters.AddWithValue("@P_GROUP_MIXING_CHECK", group_mix_check);
            cmdSql.Parameters.AddWithValue("@P_GROUP_ACTIVE", group_active);
            cmdSql.Parameters.AddWithValue("@P_GROUP_ID", group_id);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }

        public String DeleteGroup(String group_id)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryDeleteGroup(), connSql);
            cmdSql.Parameters.AddWithValue("@P_GROUP_ID", group_id);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }
        public String ResetPassword(String password, String ctrl_sys_id)
        {
            query = new QuerMixing();
            connSql = new SqlConnection(conStrSQL);
            connSql.Open();
            cmdSql = new SqlCommand(queryUserManagement.QueryResetPass(), connSql);
            cmdSql.Parameters.AddWithValue("@P_PASS", password);
            cmdSql.Parameters.AddWithValue("@P_USER_SYS_ID", ctrl_sys_id);
            cmdSql.ExecuteNonQuery();

            connSql.Close();
            return status;
        }
    }
}