using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DBConnTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string m_dbAddress = "192.168.1.252";
            //const string m_sqlserverInstanse = "MSSQLSERVER";
            //const string m_userid="sa";
            //const string m_userpw = "!@soco2011soco";
            //const string m_schemaName = "SCMESDB";
            //const string m_mysql_conn = "server=" + m_dbAddress + ";uid=mesadmin;pwd=1qAZ2wSX;database=js_mes;";
            //const string m_ms_conn = "Data Source=" + m_dbAddress +
            //";Initial Catalog=" + m_sqlserverInstanse + "\\" + m_schemaName +
            //";Integrated Security=False;User Id=" + m_userid +
            //";Password=" + m_userpw + 
            //";MultipleActiveResultSets=True";

            //const string m_ms_conn1 =
            //   "Data Source=172.16.24.8;" +
            //   "Initial Catalog=RFID;" +
            //   "User id=sa;" +
            //   "Password=RSrs9999;";

            const string m_ms_conn1 =
                "Server=172.16.24.8\\MES;" +
               "Database=RFID;" +
               //"Initial Catalog=RFID;" +
               "User id=sa;" +
               "Password=RSrs9999;";

            try
            {
                SqlConnection cn = new SqlConnection(m_ms_conn1);

                cn.Open();

                Console.WriteLine("sql server database connected successful");

                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();

        }
    }
}
