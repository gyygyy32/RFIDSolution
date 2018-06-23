using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RFIDMain.cls
{
    public static  class BasicConfigInfo
    {
        /// <summary>
        /// 组件生产厂商，如：RISEN ENERGY CO.,LTD
        /// </summary>
        public static string Manufacturer = "Jiangyin Hareon Power Co.,Ltd";
        /// <summary>
        /// 生产国,如：CHINA
        /// </summary>
        public static string MadeInCountry = "CHINA";
        /// <summary>
        /// 认证日期 如：2010-10-09
        /// </summary>
        public static string IecCertificateDate = "03-17-2016";
        /// <summary>
        /// 认证名称 如： IEC TUV
        /// </summary>
        public static string IecCertificate = "IEC TUV";
        /// <summary>
        /// 认证机构 如：TUV Germany
        /// </summary>
        public static string IecCertificateLib = "TUV SUD";


        /// <summary>
        /// Other relevant information on traceability of solar cells and module as per ISO 9001
        /// </summary>
        public static string ISO9001 = "02413Q2121737R1M";
        /// <summary>
        /// 基准日期
        /// </summary>
        public static DateTime MinDate = DateTime.Parse("2016-01-01");

        /// <summary>
        /// 数据开始标识
        /// </summary>
        public static string PacketStart = "@@";
        /// <summary>
        /// 数据结束标识
        /// </summary>
        public static string PacketEnd = "##";

        /// <summary>
        /// 程序版本
        /// </summary>
        public static int Version = 200;


        public static string PassWord = "123";
    }
}
