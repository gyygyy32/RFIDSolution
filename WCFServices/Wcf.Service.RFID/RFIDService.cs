using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.ServiceContracts.RFID;
using System.ServiceModel;

namespace Wcf.Service.RFID
{
    public class RFIDService : IRFIDService
    {
        /*
         * if using INI, that may decrease performance because need I/O every time.
         * and may cause multithread confilct.
         * 
         * so i choose doing configuration in code.
         * 
         * m_dbType:            database type;
         * m_dbAddress:         the database server ip address or machine name
         * m_sqlserverInstanse  sql server need instanse name in connection string
         * m_userid:            the user of database
         * m_userpw:            the password of user
         * m_schemaName:        the database schema
         * 
         */
        private DB_Type m_dbType = DB_Type.MySql; //DB_Type.MySql;
        private const string m_dbAddress = "localhost";//localhost
        //private const string m_sqlserverInstanse = "SQLEXPRESS";
        //private const string m_userid="sa";
        //private const string m_userpw = "!@soco2011soco";
        //private const string m_schemaName = "SCMESDB";
        private const string m_mysql_conn = "server=" + m_dbAddress + ";uid=mesadmin;pwd=1qAZ2wSX;database=js_mes;";
        //private const string m_ms_conn = "Data Source=" + m_dbAddress +
        //    ";Initial Catalog=" + m_schemaName +"\\"+m_sqlserverInstanse+
        //    ";Integrated Security=False;User Id=" + m_userid +
        //    ";Password=" + m_userpw + 
        //    ";MultipleActiveResultSets=True";

        const string m_ms_conn1 =
               "Server=182.61.37.23;" +
               "Database=mes_level2_iface;" +
            //"Initial Catalog=RFID;" +
               "User id=mes;" +
               "Password=1qAZ2wSX;";

        private const string m_errLogFileName = "rfidErrorLog.txt";

        public void WriteLog(object[] parms)
        { 
            string tagID=(string)parms[0];
            string moduleid = (string)parms[1];
            string basic = (string)parms[2];

            tagID=tagID.Replace(" ","");

            string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = @"
insert into rt_rfid_writedlog(tagid,moduleid,createtime,basicbuff,pointbuff)
values('{0}','{1}','{2}','{3}','')
";
            sql = string.Format(sql, tagID, moduleid, createtime, basic);

            if (m_dbType==DB_Type.MySql)
            {
                DBOperation.ExecuteNonQuery(m_mysql_conn, sql);
            }
            else if (m_dbType == DB_Type.SqlServer)
            {
               // DBOperation.ExecuteMSSqlServerNonQuery(m_ms_conn1, sql);
            }

            
        }

        public void writeTag()
        { 
            
        }

        public void readTag()
        { 
        }

        public ModuleInfo getModuleInfo(object[] parms)
        {
            string serial=(string)parms[0];
            string uniqueTid = (string)parms[1];

            #region check if this tag has writen data before by unique tid
            bool b_writenDataBefore = false;

            //if (true)
            //{
                
            //}

            #endregion

            #region get module information
            Dictionary<string, object> dic = new Dictionary<string, object>();

            string dfRS = "SolarNo";
            string commonVarname = "strModuleID";


            string conn = "";
            ModuleInfo mi = null;
            if (m_dbType==DB_Type.MySql)
            {
                dic.Add(commonVarname, serial);

                #region mysql db
                conn = m_mysql_conn;

                IEnumerable<ModuleInfo> miList = DBOperation.GetMySqlRowsBySP<ModuleInfo>(conn, "Get_RFID_BasicInfo", dic, r =>
                {
                    //int i = 0;
                    ModuleInfo obj = new ModuleInfo();
                    try
                    {
                        obj.ProductType = r.IsDBNull(0) ? "" : r.GetString(0);
                        obj.ELGrade = r.IsDBNull(1) ? "" : r.GetString(1);
                        obj.Status = r.IsDBNull(2) ? "" : r.GetString(2);
                        obj.PackedDate = r.IsDBNull(3) ? "0" : r.GetString(3);
                        obj.Pmax = r.IsDBNull(4) ? "" : r.GetString(4);
                        obj.Voc = r.IsDBNull(5) ? "" : r.GetString(5);
                        obj.Isc = r.IsDBNull(6) ? "" : r.GetString(6);
                        obj.Vpm = r.IsDBNull(7) ? "" : r.GetString(7);
                        obj.Ipm = r.IsDBNull(8) ? "" : r.GetString(8);
                        obj.Pivf = r.IsDBNull(9) ? "" : r.GetString(9);
                        obj.Module_ID = r.IsDBNull(10) ? "" : r.GetString(10);
                        obj.PalletNO = r.IsDBNull(11) ? "" : r.GetString(11);
                        obj.CellDate = r.IsDBNull(12) ? "" : r.GetString(12);
                        obj.Cellsource = r.IsDBNull(13) ? "" : r.GetString(13);
                        obj.EqpID = r.IsDBNull(14) ? "" : r.GetString(14);
                        obj.IVFilePath = r.IsDBNull(15) ? "" : r.GetString(15);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.writeErrorLog(m_errLogFileName, ex);
                    }
                    return obj;
                });
                #endregion
                
                mi=miList.FirstOrDefault();

                if (mi!=null)
                {
                    mi.b_writenDataBefore = b_writenDataBefore;
                }

                return mi;
            }
            else if (m_dbType==DB_Type.SqlServer)
            {
                dic.Add(dfRS, serial);

                #region sqlserver db
                conn = m_ms_conn1;
                //"Get_RFID_BasicInfo"
                IEnumerable<ModuleInfo> miList = DBOperation.GetMSRowsBySP<ModuleInfo>(conn, "get_rfid_basicInfo_v2", dic, r =>
                {
                    //int i = 0;
                    ModuleInfo obj = new ModuleInfo();
                    try
                    {

                        obj.ProductType = r.IsDBNull(0) ? "" : r.GetString(0);
                        obj.PackedDate = r.IsDBNull(3) ? "" : r.GetString(3);//1
                        obj.ELGrade = r.IsDBNull(1) ? "" : r.GetString(1);//2
                        obj.Status = "";
                        obj.Pmax = r.IsDBNull(4) ? "" : r.GetDecimal(4).ToString();//3
                        obj.Voc = r.IsDBNull(5) ? "" : r.GetDecimal(5).ToString();//4
                        obj.Isc = r.IsDBNull(6) ? "" : r.GetDecimal(6).ToString();//5
                        obj.Vpm = r.IsDBNull(7) ? "" : r.GetDecimal(7).ToString();//6
                        obj.Ipm = r.IsDBNull(8) ? "" : r.GetDecimal(8).ToString();//7
                        obj.Pivf = r.IsDBNull(8) ? "" : r.GetString(8);
                        obj.Module_ID = r.IsDBNull(11) ? "" : r.GetString(11);//9
                        obj.CellDate = r.IsDBNull(13) ? "" : r.GetString(13);//10
                        obj.PalletNO = "";
                        obj.Cellsource = "";
                        obj.EqpID = "";
                        obj.IVFilePath = "";

                        //return obj;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.writeErrorLog(m_errLogFileName, ex);
                    }
                    return obj;
                    
                });
                #endregion

                mi = miList.FirstOrDefault();
            }
            #endregion

            if (mi != null)
            {
                mi.b_writenDataBefore = b_writenDataBefore;
            }

            return mi;
            
        }

        
    }

    enum DB_Type { 
        MySql,
        SqlServer,
        Oracle
    }
}
