using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RFIDService.ClientData;
using System.IO;

namespace Helper
{
    class TagDataFormat
    {

        public static byte[] CreateByteArray(ModuleInfo mi)
        {
            int year = DateTime.Parse(mi.PackedDate).Year;
            int month = DateTime.Parse(mi.PackedDate).Month;
            int day = DateTime.Parse(mi.PackedDate).Day;
            DateTime dateOfModulePacked = new DateTime(year, month, day, 0, 0, 0);

            year = DateTime.Parse(mi.CellDate).Year;
            month = DateTime.Parse(mi.CellDate).Month;
            day = DateTime.Parse(mi.CellDate).Day;
            DateTime celldate = new DateTime(year, month, day, 0, 0, 0);

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write("@@");
                    writer.Write(mi.ProductType);
                    writer.Write(mi.Module_ID);
                    writer.Write(DateToInt16(dateOfModulePacked));
                    writer.Write((int)(mi.Pmax * 100));
                    writer.Write((short)(mi.Voc * 100));
                    writer.Write((short)(mi.Isc * 100));
                    writer.Write((short)(mi.Vpm * 100));
                    writer.Write((short)(mi.Ipm * 100));


                    writer.Write(DateToInt16(celldate));

                    writer.Write("##");
                    writer.Close();
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 当前日期减去基准日期相差的天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static short DateToInt16(DateTime date)
        {
            TimeSpan span = date - DateTime.Parse("2016-01-01");
            int days = span.Days;
            return (short)days;
        }
    }
}
