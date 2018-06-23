using System;
using System.Data;
using System.ServiceModel;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
//using Oracle.DataAccess.Client;
//using System.Data.OracleClient;


namespace Wcf.Service.RFID
{
    public class DBOperation
    {
        //execute sql
        public static void ExecuteNonQuery(string sqlconn, string s_sql)
        {
            using (MySqlConnection myConnection = new MySqlConnection(sqlconn))
            {
                using (MySqlCommand myCommand = myConnection.CreateCommand())
                {
                    try
                    {
                        myCommand.CommandType = CommandType.Text;
                        myCommand.CommandText = s_sql;
                        //myCommand.Parameters.AddWithValue("@id", id);

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        //LogException(ApplicationConfig.MysqlCommonSchemaConnString, ex.Message);
                        throw new FaultException(ex.Message);
                    }
                    
                }
            }
        }

        public static void ExecuteMSSqlServerNonQuery(string sqlconn, string s_sql)
        {
            using (SqlConnection myConnection = new SqlConnection(sqlconn))
            {
                using (SqlCommand myCommand = myConnection.CreateCommand())
                {
                    try
                    {
                        myCommand.CommandType = CommandType.Text;
                        myCommand.CommandText = s_sql;
                        //myCommand.Parameters.AddWithValue("@id", id);

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        //LogException(ApplicationConfig.MysqlCommonSchemaConnString, ex.Message);
                        throw new FaultException(ex.Message);
                    }
                    
                }
            }
        }
        
        //
        public static void ExecuteMySqlSP(string spName, Dictionary<string, object> prms, string MySqlConn)
        {
            using (MySqlConnection cn = new MySqlConnection(MySqlConn))
            {
                cn.Open();
                MySqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                if (prms != null)
                {
                    foreach (KeyValuePair<string, object> kvp in prms)
                        cmd.Parameters.Add(new MySqlParameter(kvp.Key, kvp.Value));
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //LogException(ApplicationConfig.MysqlCommonSchemaConnString, ex.Message);
                    throw new FaultException(ex.Message);
                }
            }
        }


        //get rows from mysql by calling procedure
        public static IEnumerable<T> GetMySqlRowsBySP<T>(string conn, string spName, Dictionary<string, object> prms, Func<IDataRecord, T> copyRow)
        {
            using (MySqlConnection cn = new MySqlConnection(conn))
            {
                cn.Open();
                MySqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                if (prms != null)
                {
                    foreach (KeyValuePair<string, object> kvp in prms)
                        cmd.Parameters.Add(new MySqlParameter(kvp.Key, kvp.Value));
                }
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        yield return copyRow(rdr);
                    }
                    rdr.Close();
                }
            }
        }

        //get rows from sqlserver by calling procedure
        public static IEnumerable<T> GetMSRowsBySP<T>(string conn, string spName, Dictionary<string, object> prms, Func<IDataRecord, T> copyRow)
        {
            using (SqlConnection cn = new SqlConnection(conn))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                if (prms != null)
                {
                    foreach (KeyValuePair<string, object> kvp in prms)
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        yield return copyRow(rdr);
                    }
                    rdr.Close();
                }
            }
        }

        //get rows from mysql
        public static IEnumerable<T> GetRows<T>(string conn, string sql, Func<IDataRecord, T> copyRow)
        {
            using (MySqlConnection cn = new MySqlConnection(conn))
            {
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;
                //addParameters(cmd.Parameters);
                cn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        yield return copyRow(rdr);
                    }
                    rdr.Close();
                }
            }
        }


        //get rows from oracle
        //public static IEnumerable<T> GetRows<T>(string sql, Func<IDataRecord, T> copyRow, string fclty_area_code)
        //{
        //    string oracleConnecction = "";
            //if (fclty_area_code=="6")
            //{
            //    oracleConnecction = ApplicationConfig.AchConnString;
            //}
            //else if (fclty_area_code=="7")
            //{
            //    oracleConnecction = ApplicationConfig.PyroSideConnString;
            //}
            //else if (fclty_area_code == "8")
            //{
            //    oracleConnecction = ApplicationConfig.PyroFrontalConnString;
            //}
            //else if (fclty_area_code == "UTS")
            //{
            //    oracleConnecction = ApplicationConfig.ACPDBConnString;
            //}
            //else if (fclty_area_code == "IFACE")
            //{
            //    oracleConnecction = ApplicationConfig.IFACEDBConnString;
            //}
            //else
            //{
            //    throw new Exception("cant't find connection! please check the fclty area code.");
            //}

            //using (OracleConnection cn = new OracleConnection(oracleConnecction))
            //{
            //    OracleCommand cmd = new OracleCommand(sql, cn);
            //    cmd.CommandType = CommandType.Text;
            //    //addParameters(cmd.Parameters);
            //    cn.Open();
            //    using (var rdr = cmd.ExecuteReader())
            //    {
            //        while (rdr.Read())
            //        {
            //            yield return copyRow(rdr);
            //        }
            //        rdr.Close();
            //    }
            //}
        //}

        private static void LogException(string conn, string exceptionDesc)
        {
            string sql = @"
              insert into exception_log(exception_desc,create_time)
values('{0}',now());
                ";

            sql = string.Format(sql, exceptionDesc.Replace("'", "''"));

            using (MySqlConnection myConnection = new MySqlConnection(conn))
            {
                using (MySqlCommand myCommand = myConnection.CreateCommand())
                {
                    try
                    {
                        myCommand.CommandType = CommandType.Text;
                        myCommand.CommandText = sql;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                    }
                    catch 
                    {
                    }
                }
            }
        }
    }
}
