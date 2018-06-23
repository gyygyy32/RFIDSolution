using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleTestApplication1
{
    public class WriteLog
    {
        List<string> lstFields = new List<string>();

        const List<string> _listHeader = new List<string> { "DATETIME","MODULEID","TAGID","TAGVALUE"};

        const string _logFolder = @"c:\rfidLog";

        //private static string _fileName = "";

        public static void WriteCSV(List<string> lstValues)
        {
            List<string> lstFields = new List<string>();

            try
            {
                if (!Directory.Exists(_logFolder))
                {
                    Directory.CreateDirectory(_logFolder);
                }

                string logName = System.DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
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
