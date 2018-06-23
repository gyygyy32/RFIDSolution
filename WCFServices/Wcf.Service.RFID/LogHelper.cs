using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wcf.Service.RFID
{
    public static class LogHelper
    {
        private static string _logDir = @"c:\svcLog\";

        public static void writeErrorLog(string LogFileName,Exception ex)
        {
            if (!Directory.Exists(_logDir))
            {
                return;
            }

            StreamWriter sw = null;

            try
            {
                string combined = Path.Combine(_logDir, LogFileName);
                sw = new StreamWriter(combined, true);
                sw.WriteLine(DateTime.Now.ToString() + ":" + ex.Source.ToString().Trim() + ";" + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        //Create one more log method (WriteErrorLog) to log the custom messages.
        public static void writeErrorLog(string LogFileName,string Message)
        {
            if (!Directory.Exists(_logDir))
            {
                return;
            }

            StreamWriter sw = null;

            try
            {
                string combined = Path.Combine(_logDir, LogFileName);
                sw = new StreamWriter(combined, true);
                sw.WriteLine(DateTime.Now.ToString() + ":" + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }

        public static void writeLog(string LogFileName,string Message)
        {
            if (!Directory.Exists(_logDir))
            {
                return;
            }

            StreamWriter sw = null;

            try
            {
                string combined = Path.Combine(_logDir, LogFileName);
                sw = new StreamWriter(combined, true);
                sw.WriteLine(DateTime.Now.ToString() + ":" + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
