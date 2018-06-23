using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class TagInfo
    {
        public byte[] EpcData{get;set;}
        public int EpcDataLength { get; set; }

        public byte[] TidData { get; set; }
        public int TidDataLength { get; set; } 

        public byte[] ReservedData { get; set; }
        public int ReservedDataLenth { get; set; }

        public byte[] UserData { get; set; }
        public int UserDataLength { get; set; }

        //Manufacturer Identifier
        public string MrfCode { get {
            return CCommondMethod.ByteArrayToString(TidData,0,4);
        } }
    }
}
