using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HFDesk.helpClass
{
    public class WriteCSVLog
    {
        List<string> lstFields = new List<string>();

        static List<string> _listHeader = new List<string> { "DATETIME", "MODULEID", "STATUS", "TAGID", "TAGVALUE" };

        const string _logFolder = @"c:\rfidLog";

        //private static string _fileName = "";

        private static int writelogNameSurfix=0;

        public static void WriteCSV(List<string> lstValues, int surfix=0)
        {
            List<string> lstFields = new List<string>();

            try
            {
                if (!Directory.Exists(_logFolder))
                {
                    Directory.CreateDirectory(_logFolder);
                }

                string logName = System.DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

                if (surfix!=0)
                {
                    logName = System.DateTime.Now.ToString("yyyy-MM-dd") +"_" +surfix.ToString() + ".csv";
                }

                string logFile = Path.Combine(_logFolder, logName);

                StringBuilder strBuilder = new StringBuilder();

                if (!File.Exists(logFile))
                {
                    //write the header first
                    BuildStringOfRow(strBuilder, _listHeader, "CSV");
                }


                //format the string value
                foreach (string item in lstValues)
                {
                    lstFields.Add(FormatField(item, "CSV"));
                }

                //write the data then
                BuildStringOfRow(strBuilder, lstFields, "CSV");

                StreamWriter sw = new StreamWriter(logFile, true);
                sw.Write(strBuilder.ToString());
                sw.Flush();
                sw.Close();

                writelogNameSurfix = 0;
            }
            catch(IOException)
            {
                WriteCSV(lstValues, ++writelogNameSurfix);
            }
            catch (Exception)
            {

            }
        }

        private static void BuildStringOfRow(StringBuilder strBuilder, List<string> lstFields, string strFormat)
        {
            switch (strFormat)
            {
                case "XML":
                    strBuilder.AppendLine("<Row>");
                    strBuilder.AppendLine(String.Join("\r\n", lstFields.ToArray()));
                    strBuilder.AppendLine("</Row>");
                    break;
                case "CSV":
                    strBuilder.AppendLine(String.Join(",", lstFields.ToArray()));
                    break;
            }
        }

        private static string FormatField(string data, string format)
        {
            switch (format)
            {
                case "XML":
                    return String.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", data);
                case "CSV":
                    return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
            }
            return data;
        }
    }
}
