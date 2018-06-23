using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    class BuildStoreData
    {
        /* 
         * To create Public EPCs that do not conflict with already defined usage, Impinj recommends the following (also see Table 2-6):
         *      The first 8 bits of header should always be zero to avoid conflict with already standardized EPC formats.
         *      The next 32 bits should hold a Private Enterprise Number (PEN) (number obtainable from the Internet 
         *          Assigned Numbers Authority (IANA) at http://pen.iana.org/pen/app) that uniquely identifies a company or organization. 
         *          If tag users do not wish to have even this level of identification 
         *          (i.e., they desire full privacy), the PEN should be set to all zeros.
         *      The last 56 bits hold data fields specified by each entity for their application.
         */
        //    public static byte[] BuildEPCStoreData(string data)
        //    {
        //        try
        //        {
        //            byte btMemBank = MemoryBank.EPCBank;
        //            byte btWordAdd = 0x00;                  //according to the comments, application data should start from the 41 bit
        //            byte btWordCnt = 0x00;

        //            byte[] btAryPwd = new byte[]{0x00,0x00,0x00,0x00};
        //            byte[] btAryData = Encoding.ASCII.GetBytes(data);

        //            Int16 dataLen = Convert.ToInt16(btAryData.Length);
        //            byte[] btLen = BitConverter.GetBytes(dataLen);              

        //            byte[] btAryDataAndLen = new byte[btAryData.Length + 2];
        //            btLen.CopyTo(btAryDataAndLen, 0);                                   //the first 2 bytes contains data length
        //            btAryData.CopyTo(btAryDataAndLen, 2);

        //            btWordCnt = Convert.ToByte(btAryDataAndLen.Length / 2 + btAryDataAndLen.Length % 2);//1 word equals 2 bytes

        //            reader.WriteTag(m_curSetting.btReadId, btAryPwd, btMemBank, btWordAdd, btWordCnt, btAryDataAndLen);

        //            //paintBackgroundColor(statusType.PASS);

        //        }
        //        catch (System.Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        finally {
        //            //tbx_SerialWrite.Enabled = true;
        //            EnableControl(1);
        //        }
        //}
    }
}
