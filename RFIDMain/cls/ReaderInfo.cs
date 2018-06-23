using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDMain.cls
{
    class ReaderInfo
    {
        public static bool readerConnerted = false;
        /// <summary>
        /// >0 连接成功
        /// </summary>
        public static int icdev = 0; // 通讯设备标识符
    }
}
