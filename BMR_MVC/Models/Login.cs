using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BMR_MVC.Models
{
    public class Login
    {
        String status, token;
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();

        SqlConnection connSql;
        SqlDataReader readerSql;
        SqlCommand cmdSql;

        List<UserInfo> listUserInfo;
        UserInfo userInfo;
        QuerMixing query;

        public Login()
        {
            connSql = new SqlConnection(conStrSQL);
            query = new QuerMixing();
        }



        public List<UserInfo> ChackLogin(String username, String password)
        {
            token = GenToken();
            status = Encrypt(password);
            listUserInfo = new List<UserInfo>();
            connSql.Open();
            cmdSql = new SqlCommand(query.QueryCheckLogin(), connSql);
            cmdSql.Parameters.AddWithValue("@P_USER_LOGIN", username);
            cmdSql.Parameters.AddWithValue("@P_PASSWORD", Encrypt(password));
            cmdSql.Parameters.AddWithValue("@P_TOKEN", token);
            readerSql = cmdSql.ExecuteReader();
            if (readerSql.HasRows)
            {
                while (readerSql.Read())
                {

                    userInfo = new UserInfo
                    {
                        userId = readerSql["USER_SYS_ID"].ToString(),
                        userName = readerSql["USER_NAME"].ToString(),
                        //create = Convert.ToBoolean(readerSql["GROUP_CREATE"]),
                        //runJob = Convert.ToBoolean(readerSql["GROUP_RUN_JOB"]),
                        //mxCleanClean = Convert.ToBoolean(readerSql["GROUP_MIXING_CC_CLEAN"]),
                        //mxCleanCheck = Convert.ToBoolean(readerSql["GROUP_MIXING_CC_CHECK"]),
                        //mxOperate = Convert.ToBoolean(readerSql["GROUP_MIXING_OPERATE"]),
                        //mxCheck = Convert.ToBoolean(readerSql["GROUP_MIXING_CHECK"]),
                        token = token
                    };
                    listUserInfo.Add(userInfo);
                    HttpContext.Current.Session["USERID"] = readerSql["USER_SYS_ID"].ToString();
                    HttpContext.Current.Session["USERLOGIN"] = readerSql["USER_LOGIN"].ToString();
                    HttpContext.Current.Session["USERNAME"] = readerSql["USER_NAME"].ToString();
                    HttpContext.Current.Session["AUTH_EDIT_CC"] = readerSql["GROUP_EDIT_CC"].ToString();
                    HttpContext.Current.Session["AUTH_APPR_JOB_MX"] = readerSql["GROUP_APPR_JOB_MX"].ToString();
                    HttpContext.Current.Session["AUTH_APPR_JOB_BCR"] = readerSql["GROUP_APPR_JOB_BCR"].ToString();
                    HttpContext.Current.Session["AUTH_APPR_JOB_BCA"] = readerSql["GROUP_APPR_JOB_BCA"].ToString();
                    HttpContext.Current.Session["AUTH_APPR_JOB_PK"] = readerSql["GROUP_APPR_JOB_PK"].ToString();
                    HttpContext.Current.Session["AUTH_EDIT_JOB"] = Convert.ToBoolean(readerSql["GROUP_EDIT_JOB"]);
                    HttpContext.Current.Session["AUTH_DISABLE_BMR"] = Convert.ToBoolean(readerSql["GROUP_DISABLE_BMR"]);
                    HttpContext.Current.Session["AUTH_MANAGE_USER"] = Convert.ToBoolean(readerSql["GROUP_MANAGE_USER"]);
                    HttpContext.Current.Session["AUTH_CREATE"] = Convert.ToBoolean(readerSql["GROUP_CREATE"]);
                    HttpContext.Current.Session["AUTH_RUN_JOB"] = Convert.ToBoolean(readerSql["GROUP_RUN_JOB"]);
                    HttpContext.Current.Session["AUTH_MIXING"] = Convert.ToBoolean(readerSql["GROUP_MIXING"]);
                    HttpContext.Current.Session["AUTH_PACKING"] = Convert.ToBoolean(readerSql["GROUP_PACKING"]);
                    HttpContext.Current.Session["AUTH_BCR"] = Convert.ToBoolean(readerSql["GROUP_BCR"]);
                    HttpContext.Current.Session["AUTH_BCA"] = Convert.ToBoolean(readerSql["GROUP_BCA"]);
                    HttpContext.Current.Session["AUTH_CP"] = Convert.ToBoolean(readerSql["GROUP_CP"]);
                    HttpContext.Current.Session["AUTH_MIXING_CC_CLEAN"] = Convert.ToBoolean(readerSql["GROUP_MIXING_CC_CLEAN"]);
                    HttpContext.Current.Session["AUTH_MIXING_CC_CHECK"] = Convert.ToBoolean(readerSql["GROUP_MIXING_CC_CHECK"]);
                    HttpContext.Current.Session["AUTH_MIXING_OPERATE"] = Convert.ToBoolean(readerSql["GROUP_MIXING_OPERATE"]);
                    HttpContext.Current.Session["AUTH_MIXING_CHECK"] = Convert.ToBoolean(readerSql["GROUP_MIXING_CHECK"]);
                    HttpContext.Current.Session["AUTH_PACKING_CC_CLEAN"] = Convert.ToBoolean(readerSql["GROUP_PACKING_CC_CLEAN"]);
                    HttpContext.Current.Session["AUTH_PACKING_CC_CHECK"] = Convert.ToBoolean(readerSql["GROUP_PACKING_CC_CHECK"]);
                    HttpContext.Current.Session["AUTH_PACKING_OPERATE"] = Convert.ToBoolean(readerSql["GROUP_PACKING_OPERATE"]);
                    HttpContext.Current.Session["AUTH_PACKING_CHECK"] = Convert.ToBoolean(readerSql["GROUP_PACKING_CHECK"]);
                    HttpContext.Current.Session["AUTH_BCR_CC_CLEAN"] = Convert.ToBoolean(readerSql["GROUP_BCR_CC_CLEAN"]);
                    HttpContext.Current.Session["AUTH_BCR_CC_CHECK"] = Convert.ToBoolean(readerSql["GROUP_BCR_CC_CHECK"]);
                    HttpContext.Current.Session["AUTH_BCR_OPERATE"] = Convert.ToBoolean(readerSql["GROUP_BCR_OPERATE"]);
                    HttpContext.Current.Session["AUTH_BCR_CHECK"] = Convert.ToBoolean(readerSql["GROUP_BCR_CHECK"]);
                    HttpContext.Current.Session["AUTH_BCA_CC_CLEAN"] = Convert.ToBoolean(readerSql["GROUP_BCA_CC_CLEAN"]);
                    HttpContext.Current.Session["AUTH_BCA_CC_CHECK"] = Convert.ToBoolean(readerSql["GROUP_BCA_CC_CHECK"]);
                    HttpContext.Current.Session["AUTH_BCA_OPERATE"] = Convert.ToBoolean(readerSql["GROUP_BCA_OPERATE"]);
                    HttpContext.Current.Session["AUTH_BCA_CHECK"] = Convert.ToBoolean(readerSql["GROUP_BCA_CHECK"]);
                    HttpContext.Current.Session["TOKEN"] = token;
                    HttpContext.Current.Session["GROUP_ID_ALL"] = readerSql["GROUP_ID_ALL"].ToString();
                }
            }
            String test = HttpContext.Current.Session["AUTH_CREATE"].ToString();
            cmdSql.Dispose();
            connSql.Close();
            return listUserInfo;
        }
        public String Encrypt(String password)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = sha256.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = password.ToLower();
            return result;

        }
        public String GenToken()
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[32];
            rng.GetBytes(tokenData);
            token = Convert.ToBase64String(tokenData);
            return token;

        }
    }
}