using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.IO;

namespace HFDesk
{
    class DealINI
    {
        public static string _inipath="";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //public DealINI(string INIPath)
        //{
        //    this.inipath = INIPath;
        //}

        //public DealINI()
        //{
        //    this.inipath = "";
        //}

        public static void IniWriteValue(string Section, string Key, string Value)
        {
            DealINI.WritePrivateProfileString(Section, Key, Value, _inipath);
        }

        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            int privateProfileString = DealINI.GetPrivateProfileString(Section, Key, "", stringBuilder, 500, _inipath);
            return stringBuilder.ToString();
        }

        //public bool ExistINIFile()
        //{
        //    return File.Exists(this.inipath);
        //}
    }
}
