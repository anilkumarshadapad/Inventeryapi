using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace InventoryDAL
{
    public class DBL
    {
        SqlParameter[] Param;

        SqlCommand Cmd;
        SqlDataAdapter Adp;
        DataSet Ds = new DataSet();
        public Int32 status = 0, UserSessionID = 0;
        string Result = "", pageName = "InventoryDAL.cs";

        public static string DataSource, InitialCatalog, DBUserID, DBPassword;
        public TextInfo textInfo = new CultureInfo("en-us", false).TextInfo;
        public SqlConnection Conn;

        public void OpenConnection()
        {
            try
            {
                DBHelper objDBHelperIP = new DBHelper();
                DBHelper.OP objDBHelperOP = new DBHelper.OP();
                objDBHelperOP = objDBHelperIP.GetDBAccess();
                DataSource = objDBHelperOP.DB_DataSource;
                InitialCatalog = objDBHelperOP.DB_InitialCatalog;
                DBUserID = objDBHelperOP.DB_UserID;
                DBPassword = objDBHelperOP.DB_Password;

                
                Conn = new SqlConnection(@"Data Source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";user ID=" + DBUserID + ";pwd=" + DBPassword + ";");
                
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                LOG(ex.Message.Trim(), "OpenConnection", pageName);
            }
        }
            public static string connectionString = ConfigurationManager.ConnectionStrings["InventeryDetails"].ConnectionString;

        public DataSet GetDataSet(string spName, SqlParameter[] paramArr, string tablename, string APIURL)
        {
            try
            {
                OpenConnection();

                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = spName;
                Cmd.CommandTimeout = 0;
                Cmd.CommandType = CommandType.StoredProcedure;

                if (paramArr != null)
                {
                    foreach (SqlParameter sqlParam in paramArr)
                    {
                        Cmd.Parameters.Add(sqlParam);

                    }

                    Adp = new SqlDataAdapter(Cmd);
                    Ds = new DataSet();
                    Adp.Fill(Ds, tablename);

                    Cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                LOG(ex.Message.Trim(), APIURL, "");
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
                Conn.Dispose();
            }
            return Ds;
        }


        public int ExecuteNonQueryFromSP(string spName, SqlParameter[] paramArr)
        {
            try
            {
                OpenConnection();

                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = spName;
                Cmd.CommandTimeout = 0;
                Cmd.CommandType = CommandType.StoredProcedure;

                if (paramArr != null)
                {
                    foreach (SqlParameter sqlParam in paramArr)
                    {
                        Cmd.Parameters.Add(sqlParam);
                    }
                }

                status = Cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                LOG(Ex.Message.Trim(), "ExecuteNonQueryFromSP", pageName);
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed) Conn.Close();
                Conn.Dispose();
            }
            return status;
        }

        public void LOG(string Msg, string MethodName, string Pagename)
        {
            Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@Flag", 1);
            Param[1] = new SqlParameter("@ErrorMessage", Msg.Trim());
            Param[2] = new SqlParameter("@MethodName", MethodName.Trim());
            Param[3] = new SqlParameter("@PageName", Pagename.Trim());
            ExecuteNonQueryFromSP("SP_ErrorLog", Param);
        }


    }
}