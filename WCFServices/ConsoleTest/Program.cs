using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("serial number: AE60P61702150189");
            //string serial = Console.ReadLine();

            WcfCaller.querySerialInfo((o, ex) =>
            {
                if (ex == null)
                {
                    Console.WriteLine("begin parsing data:");

                    //int year = DateTime.Parse(o.PackedDate).Year;
                    //int month = DateTime.Parse(o.PackedDate).Month;
                    //int day = DateTime.Parse(o.PackedDate).Day;
                    //DateTime dateOfModulePacked = new DateTime(year, month, day, 0, 0, 0);

                    //year = DateTime.Parse(o.CellDate).Year;
                    //month = DateTime.Parse(o.CellDate).Month;
                    //day = DateTime.Parse(o.CellDate).Day;
                    //DateTime celldate = new DateTime(year, month, day, 0, 0, 0);

                    Console.WriteLine("@@");
                    Console.WriteLine("产品类型: {0}",o.ProductType);
                    Console.WriteLine("组件序列号: {0}", o.Module_ID);
                    Console.WriteLine("包装时间: {0}", o.PackedDate);
                    //Console.WriteLine(DateToInt16(dateOfModulePacked));
                    Console.WriteLine("Pmax = {0}", o.Pmax);
                    Console.WriteLine("Voc = {0}", o.Voc);
                    Console.WriteLine("Isc = {0}", o.Isc);
                    Console.WriteLine("Vpm = {0}", o.Vpm);
                    Console.WriteLine("Ipm = {0}", o.Ipm);
                    Console.WriteLine("电池日期 {0}", o.CellDate);
                    //Console.WriteLine(DateToInt16(celldate));
                    Console.WriteLine("##");

                    Console.WriteLine("service request successful.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("发生异常：");
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }, new string[] { "AE60P61702150189", "testtag" }

            );


            //Console.Read();

        }

        private static short DateToInt16(DateTime date)
        {
            TimeSpan span = date - DateTime.Parse("2016-01-01");
            int days = span.Days;
            return (short)days;
        }
  
        private const string m_dbAddress = "localhost\\SQLEXPRESS";
        private const string m_userid = "sa";
        private const string m_userpw = "1qAZ2wSX";
        private const string m_schemaName = "test_larry";
        private const string m_mysql_conn = "server=" + m_dbAddress + ";uid=mesadmin;pwd=1qAZ2wSX;database=mes_main;";
        private const string m_ms_conn = "Data Source=" + m_dbAddress +
            ";Initial Catalog=" + m_schemaName +
            ";Integrated Security=False;User Id=" + m_userid +
            ";Password=" + m_userpw +
            ";MultipleActiveResultSets=True";
        
        private static void sqlProcedureTest()
        { 
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add("v_serial", "123456");

                using (SqlConnection cn = new SqlConnection(m_ms_conn))
                {
                    cn.Open();

                    Console.WriteLine("sql server connect successful");
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_get_flash_data";

                    
                    foreach (KeyValuePair<string, object> kvp in dic)
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(Convert.ToString(rdr[0]));
                            Console.WriteLine(Convert.ToString(rdr[1]));
                            Console.WriteLine(Convert.ToString(rdr[2]));

                            //yield return copyRow(rdr);
                        }
                        rdr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occurs:"+e.Message);
                //throw;
            }

            Console.Read();
        }
    }
}
