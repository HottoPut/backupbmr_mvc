using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class CreateMXPD
    {
        //Oracle
        OracleCommand cmdORCL;
        OracleDataReader readerORCL;
        OracleConnection connORCL;

        //SQl server
        SqlConnection connSQL;
        SqlDataReader readerSQL;
        SqlCommand cmdSql;

        //Connection String
        String conStrSQL = ConfigurationManager.ConnectionStrings["SqlServerBMR"].ToString();
        String conStrORCL = ConfigurationManager.ConnectionStrings["ORCL"].ToString();

        //List
        List<ItemMasterInfo> listItemMasterInfos;
        List<BomSFInfo> listBomSFInfos;

        //Class
        QuerMixing query;
        ItemMasterInfo itemMasterInfo;
        BomSFInfo bomSFInfo;

        public CreateMXPD()
        {
            query = new QuerMixing();
            connORCL = new OracleConnection(conStrORCL);
            connSQL = new SqlConnection(conStrSQL);
        }

        public List<ItemMasterInfo> GetItemMaster()
        {
            listItemMasterInfos = new List<ItemMasterInfo>();
            connORCL.Open();
            cmdORCL = new OracleCommand(query.QueryGetItemFG(), connORCL);
            readerORCL = cmdORCL.ExecuteReader();
            if (readerORCL.HasRows)
            {
                while (readerORCL.Read())
                {
                    itemMasterInfo = new ItemMasterInfo
                    {
                        itemCode = readerORCL["ITEM_CODE"].ToString(),
                        itemName = readerORCL["ITEM_NAME"].ToString()
                    };
                    listItemMasterInfos.Add(itemMasterInfo);
                }
            }
            cmdORCL.Dispose();
            connORCL.Close();
            return listItemMasterInfos;
        }
        //public List<BomSFInfo> GetBomSF(String itemFGCode)
        //{
        //    listBomSFInfos = new List<BomSFInfo>();
        //    connORCL.Open();
        //    cmdORCL = new OracleCommand(query.QueryGetBomSF(), connORCL);
        //    cmdORCL.Parameters.Add(new OracleParameter("P_ITEM_CODE", itemFGCode));
        //    readerORCL = cmdORCL.ExecuteReader();
        //    if (readerORCL.HasRows)
        //    {
        //        while (readerORCL.Read())
        //        {
        //            bomSFInfo = new BomSFInfo
        //            {
        //                itemFGCode = readerORCL["item_"].ToString(),
        //                itemRMCode = readerORCL["BI_ITEM_CODE"].ToString(),
        //                itemRMName = readerORCL["ITEM_NAME2"].ToString(),
        //                itemQty = readerORCL["BI_QTY"].ToString(),
        //                itemQtyLs = readerORCL["BI_QTY_LS"].ToString(),
        //                itemUom = readerORCL["BI_UOM_CODE"].ToString(),
        //                lose = readerORCL["LOOSE"].ToString(),
        //                comvFactor = readerORCL["IU_CONV_FACTOR"].ToString(),
        //                batchSize = readerORCL["BOM_QTY"].ToString(),
        //                bomUom = readerORCL["BOM_UOM_CODE"].ToString()
        //            };
        //            listBomSFInfos.Add(bomSFInfo);
        //        }
        //    }
        //    cmdORCL.Dispose();
        //    connORCL.Close();
        //    return listBomSFInfos;
        //}
    }
}