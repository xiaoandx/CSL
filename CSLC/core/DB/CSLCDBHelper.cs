using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CSLC.Core
{

    /// <summary>
    /// [封装、丰富对Oracle数据库的操作]
    /// </summary>
    public class CSLCDBHelper
    {
        public static string MesCon = ConfigurationManager.AppSettings["MESCon"];

        public static DataTable QueryFromMES(string sql)
        {
            DataTable dt = new DataTable();

            dt = queryMESInterface(sql, MesCon).Tables[0];

            return dt;
        }

        public static int UpdateToMES(string sqlStr)
        {
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(MesCon);
            Oracle.ManagedDataAccess.Client.OracleCommand commd = new Oracle.ManagedDataAccess.Client.OracleCommand();
            conn.Open();

            commd.Connection = conn;
            Oracle.ManagedDataAccess.Client.OracleTransaction tx = conn.BeginTransaction();
            commd.Transaction = tx;
            int count = 0;
            try
            {
                commd.CommandText = sqlStr;
                int rows = commd.ExecuteNonQuery();
                count++;

                tx.Commit();

            }
            catch (Exception e)
            {
                count = 0;
                tx.Rollback();
                throw e;
            }
            finally
            {
                if (commd != null)
                {
                    conn.Close();
                    commd.Dispose();
                }
            }
            return count;
        }

        public static int UpdateToMES_List(List<string> list)
        {
            string guid = Guid.NewGuid().ToString();
            string s_Sql = $@"begin 
";
            foreach (string item in list)
            {
                s_Sql += item + "\r\n";
            }
            s_Sql += " end ;";
            int i_result = UpdateToMES(s_Sql);
            return i_result;
        }

        public int Execute_UpdateToMES(List<string> list)
        {
            string guid = Guid.NewGuid().ToString();
            string s_Sql = $@"begin 
";
            foreach (string item in list)
            {
                s_Sql += item + "\r\n";
            }
            s_Sql += " end ;";
            int i_result = UpdateToMES(s_Sql);
            return i_result;
        }

        public static DataSet queryMESInterface(string sql, string connectionString)
        {
            DataSet ds = new DataSet();
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
            Oracle.ManagedDataAccess.Client.OracleDataAdapter command = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            try
            {
                conn.Open();
                command = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sql, conn);
                command.Fill(ds, "ds");
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                command.Dispose();
            }
            return ds;
        }

    }
}
