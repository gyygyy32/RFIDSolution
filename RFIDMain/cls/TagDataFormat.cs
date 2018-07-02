using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RFIDService.ClientData;
using System.IO;
using CustomControl;
namespace RFIDMain.cls
{
    class TagDataFormat
    {
        /// <summary>
        /// 数据开始标识
        /// </summary>
        private static readonly String PacketStart = BasicConfigInfo.PacketStart;
        /// <summary>
        /// 数据结束标识
        /// </summary>
        private static readonly String PacketEnd = BasicConfigInfo.PacketEnd;
        /// <summary>
        /// 基准日期 2016-01-01
        /// </summary>
        private static readonly DateTime MinDate = BasicConfigInfo.MinDate;


        public static byte[] CreateByteArray(ModuleInfo mi)
        {
            int year = DateTime.Parse(mi.PackedDate).Year;
            int month = DateTime.Parse(mi.PackedDate).Month;
            int day = DateTime.Parse(mi.PackedDate).Day;
            DateTime dateOfModulePacked = new DateTime(year, month, day, 0, 0, 0);

            year = DateTime.Parse(mi.CellDate).Year;
            month = DateTime.Parse(mi.CellDate).Month;
            day = DateTime.Parse(mi.CellDate).Day;
            string CellSource = mi.Cellsource;//add by genhong.hu On 2017-12-31，在此之前高频读卡器没有写入CellSource
            DateTime celldate = new DateTime(year, month, day, 0, 0, 0);

            decimal iPmax = Decimal.Parse(mi.Pmax)*100M;
            decimal iVoc = Decimal.Parse(mi.Voc) * 100M;
            decimal iIsc = Decimal.Parse(mi.Isc) * 100M;
            decimal iVpm = Decimal.Parse(mi.Vpm) * 100M;
            decimal iIpm = Decimal.Parse(mi.Ipm) * 100M;
            //添加ff add by xue lei on 2018-6-23
            decimal iFF = Decimal.Parse(mi.FF) * 10000M;

            using (MemoryStream stream = new MemoryStream())
            {

                

                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(PacketStart);//@@
                    writer.Write(mi.ProductType);
                    writer.Write(mi.Module_ID.Remove(0,2));
                    writer.Write(DateToInt16(dateOfModulePacked));
                    writer.Write((int)iPmax);
                    writer.Write((short)iVoc);
                    writer.Write((short)iIsc);
                    writer.Write((short)iVpm);
                    writer.Write((short)iIpm);
                    // 添加ff add by xue lei on 2018-6-23
                    writer.Write((short)iFF);
                    // writer.Write(CellSource);//add by genhong.hu On 2017-12-31，在此之前高频读卡器没有写入CellSource
                    //writer.Write(DateToInt16(celldate));

                    //writer.Write(PacketEnd);//##
                    writer.Close();
                }
                return stream.ToArray();

                //string res = PacketStart + "|" + mi.ProductType + "|" + mi.Module_ID + "|" + dateOfModulePacked.ToString() + "|";// +
                            //iPmax.ToString() + "|" + iVoc.ToString() + "|" + iIsc.ToString() + "|" + iVpm.ToString() + "|" + iIpm.ToString()
                            //+ "|" + iFF.ToString() + "|" + celldate.ToString();
                //return System.Text.Encoding.Default.GetBytes(res);
            }
        }





        #region============解析标签内容，读取时用到===============================================================================
        /// <summary>
        /// 解析标签内容，读取时用到
        /// </summary>
        /// <param name="tagBuff"></param>
        /// <returns></returns>
        public static ModuleInfo ParserTag(byte[] tagBuff)
        {
            try
            {
                ModuleInfo o = new ModuleInfo();
                
                MemoryStream memStream = new MemoryStream(tagBuff);
                BinaryReader buffReader = new BinaryReader(memStream);

              

                string packetStart = buffReader.ReadString();
                if (packetStart != PacketStart)
                {
                    throw new Exception("数据包开始标志出错");
                }
                o.ProductType = buffReader.ReadString();
                o.Module_ID = "ZX"+buffReader.ReadString();
                DateTime dateOfModulePacked = DateFormInt16(buffReader.ReadInt16());
                o.PackedDate =dateOfModulePacked.ToString("yyyy-MM");
                //string pivf = buffReader.ReadString();
                double Pmax = buffReader.ReadInt32() * 1.0 / 100;
                double Voc = buffReader.ReadInt16() * 1.0 / 100;
                double Isc = buffReader.ReadInt16() * 1.0 / 100;
                double Vpm = buffReader.ReadInt16() * 1.0 / 100;
                double Ipm = buffReader.ReadInt16() * 1.0 / 100;
                double FF = buffReader.ReadInt16() * 1.0 / 100;
                o.Pmax = Pmax.ToString("0.00");
                o.Voc = Voc.ToString("0.00");
                o.Isc = Isc.ToString("0.00");
                o.Vpm = Vpm.ToString("0.00");
                o.Ipm = Ipm.ToString("0.00");
                o.FF = (FF/100).ToString();
                //o.Cellsource = buffReader.ReadString();//add by genhong.hu On 2017-12-31，在此之前高频读卡器没有写入CellSource
                //DateTime celldate = DateFormInt16(buffReader.ReadInt16());// Add by genhong.hu On 2014-08-11 ;  
                o.CellDate = o.PackedDate;//celldate.ToString("yyyy-MM");
                //ff 从数据库查询 modify by xue lei on 2018-6-23
                //double FF = Math.Round(Vpm * Ipm / Voc / Isc * 100, 2);
             
                o.Pivf = Pmax + "Wp," + Voc + "V," + Isc + "A," + Vpm + "V," + Ipm + "A," + "0.0078" + "%";
               
                               
                return o;
                 
            }
            catch (Exception ex)
            {
                throw new Exception("解析数据包出错：\r\n" + ex.Message);
            }
        }



        ///// <summary>
        ///// 解析HR标签内容，读取时用到
        ///// </summary>
        ///// <param name="tagBuff"></param>
        ///// <returns></returns>
        //public static RfidTagTDO ParserHRTag(byte[] tagBuff)
        //{
        //    try
        //    {

        //        byte[] ret;

        //        //  组件序列号
        //        ret = new byte[15];
        //        Array.Copy(tagBuff, ret, 15);
        //        string serialNo = Encoding.ASCII.GetString(ret).Replace("\0", "");
        //        // 年
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 15, ret, 0, 1);
        //        int Moduleyear = 2000 + BitConverter.ToInt16(ret, 0);
        //        //月
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 16, ret, 0, 1);
        //        int Modulemonth = BitConverter.ToInt16(ret, 0);
        //        //年
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 17, ret, 0, 1);
        //        int Cellyear = 2000 + BitConverter.ToInt16(ret, 0);
        //        //月
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 18, ret, 0, 1);
        //        int Cellmonth = BitConverter.ToInt16(ret, 0);

        //        //Pmax * 100
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 19, ret, 0, 2);
        //        double Pmax = BitConverter.ToInt16(ret, 0) * 1.0 / 100;

        //        //Voc * 100
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 21, ret, 0, 2);
        //        double Voc = BitConverter.ToInt16(ret, 0) * 1.0 / 100;

        //        //Isc * 100
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 23, ret, 0, 2);
        //        double Isc = BitConverter.ToInt16(ret, 0) * 1.0 / 100;

        //        //FF * 100
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 25, ret, 0, 2);
        //        double FF = BitConverter.ToInt16(ret, 0) * 1.0 / 100;

        //        // DateOfIEC     03-17-2016
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 27, ret, 0, 2);
        //        string year = BitConverter.ToInt16(ret, 0).ToString();

        //        // DateOfIEC     03-17-2016
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 29, ret, 0, 1);
        //        string day = BitConverter.ToInt16(ret, 0).ToString("00");

        //        // DateOfIEC     03-17-2016
        //        ret = new byte[4];
        //        Array.Copy(tagBuff, 30, ret, 0, 1);
        //        string month = BitConverter.ToInt16(ret, 0).ToString("00");

        //        string DateOfIEC = month + "-" + day + "-" + year;

        //        // LabOfIEC    TUV SUD
        //        ret = new byte[8];
        //        Array.Copy(tagBuff, 31, ret, 0, 8);
        //        string LabOfIEC = Encoding.ASCII.GetString(ret);

        //        I_V_Point[] pointArray = new I_V_Point[11];
        //        for (int a = 39; a < 61; a = a + 2)
        //        {
        //            int i = (a - 39) / 2;
        //            pointArray[i] = new I_V_Point();
        //            ret = new byte[4];
        //            Array.Copy(tagBuff, a, ret, 0, 1);
        //            pointArray[i].Current = BitConverter.ToInt16(ret, 0) * 1.0 / 10;
        //            Array.Copy(tagBuff, a + 1, ret, 0, 1);
        //            pointArray[i].Voltage = BitConverter.ToInt16(ret, 0) * 1.0;


        //        }

        //        int grade = (int)(Pmax * 100 + 0.5) / 500 * 5;
        //        string modelNumber = "HR-" + grade.ToString() + "P-24/Ba";


        //        DateTime dateOfModulePacked = DateTime.Parse(Moduleyear + "." + Modulemonth + ".01");

        //        double Vpm = 0;
        //        double Ipm = 0;

        //        DateTime celldate = DateTime.Parse(Cellyear + "." + Cellmonth + ".01");

        //        string pivf = Pmax + "Wp," + Voc + "V," + Isc + "A," + FF + "%";
        //        RfidTagTDO rfidTag = new RfidTagTDO("");
        //        //rfidTag.SetBasicInfo(modelNumber, dateOfModulePacked, pivf, serialNo,  celldate );
        //        rfidTag.SetBasicInfo(modelNumber, dateOfModulePacked, Pmax, Voc, Isc, Vpm, Ipm, FF, serialNo, celldate);


        //        rfidTag.SetPointArray(pointArray);
        //        return rfidTag;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("解析数据包出错：\r\n" + ex.Message);
        //    }
        //}

        #endregion


        /// <summary>
        /// 当前日期减去基准日期相差的天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static short DateToInt16(DateTime date)
        {
            TimeSpan span = date - MinDate;// DateTime.Parse("2016-01-01");
            int days = span.Days;
            return (short)days;


        }


        /// <summary>
        /// 基准日期加上相差天数
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        private static DateTime DateFormInt16(short days)
        {
            DateTime date = MinDate.AddDays(days);
            return date;
        }
    }
}
