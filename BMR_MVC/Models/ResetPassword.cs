using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class ResetPassword
    {
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();

        SqlConnection connSql;
        SqlDataReader readerSql;
        SqlCommand cmdSql;

        List<UserInfo> listuser;
        UserInfo user;
        QueryResetPassword queryResetPassword;

        public ResetPassword()
        {
            connSql = new SqlConnection(conStrSQL);
            queryResetPassword = new QueryResetPassword();
        }

        public List<UserInfo> ChackUsername(String username, String new_password, String old_password)
        {
            listuser = new List<UserInfo>();
            connSql.Open();
            cmdSql = new SqlCommand(queryResetPassword.QueryCheckResetPassword(), connSql);
            cmdSql.Parameters.AddWithValue("@username", username);
            cmdSql.Parameters.AddWithValue("@P_USER_PASS", old_password);
            readerSql = cmdSql.ExecuteReader();
            if (readerSql.HasRows)
            {
                while (readerSql.Read())
                {

                    user = new UserInfo
                    {
                        userId = readerSql["USER_SYS_ID"].ToString(),
                        userName = readerSql["USER_NAME"].ToString(),
                        userPass = readerSql["USER_PASS"].ToString(),
                        userPre = readerSql["USER_PRE"].ToString(),
                        userActive = readerSql["USER_ACTIVE"].ToString()
                    };
                    listuser.Add(user);
                }
            }
            try
            {
                if (user.userName.Length > 0 && user.userPre.Length > 0 && user.userPass == old_password)
                {

                    cmdSql = new SqlCommand(queryResetPassword.QuerySavePass(), connSql);
                    cmdSql.Parameters.AddWithValue("@P_PASS", new_password);
                    cmdSql.Parameters.AddWithValue("@P_USER_SYS_ID", user.userId);

                    cmdSql.ExecuteNonQuery();
                    connSql.Close();
                    return listuser;
                }
                else
                {
                    connSql.Close();
                    return listuser;
                }
            }
            catch (NullReferenceException ex)
            {
                connSql.Close();
                return listuser;
            }





        }
    }
}