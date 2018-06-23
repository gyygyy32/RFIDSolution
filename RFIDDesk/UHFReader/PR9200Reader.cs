using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using UHFDesk.consts;
using Helper;

namespace UHFReader
{
    public delegate void WriteTagCallback(PR9200Msg msgTran);
    public delegate void ReadTagCallback(PR9200Msg msgTran);
    public delegate void GetFrequencyRegionDelegate(ActionResault resaultType);
    public delegate void TagIdentifyFailDelegate(ActionResault resaultType);

    public class PR9200Reader 
    {
        private SerialPort m_SerialPort;

        private int m_nType = -1;

        private ActionType actionType = ActionType.None;

        private string strErrorCode = string.Empty;
        public string ErrorCode { get { return strErrorCode; } }
        private string tagUid = "";
        public string TagUid { get { return tagUid; } }



        private bool readyReady = false;
        public bool ReaderReady { get { return readyReady; } }

        private bool readerBusyNow = false;
        public bool ReaderBusyNow { get { return readerBusyNow; } }

        public GetFrequencyRegionDelegate dlgt_GetFrequencyRegion;
        public TagIdentifyFailDelegate dlgt_TagIdentifyFail;
        //public UHFSendDataCallback SendCallback;
        //public UHFAnalyDataCallback AnalyCallback;

        #region constructor
        public PR9200Reader()
        {
            m_SerialPort = new SerialPort();
            m_SerialPort.DataReceived+=new SerialDataReceivedEventHandler(ReceivedComData);
        }
        #endregion

        #region Public functions
        public int OpenComAndInitReader(string strPort, int nBaudrate, out string strException)
        {
            strException = string.Empty;

            if (m_SerialPort.IsOpen)
            {
                m_SerialPort.Close();
            }

            try
            {
                m_SerialPort.PortName = strPort;
                m_SerialPort.BaudRate = nBaudrate;
                m_SerialPort.ReadTimeout = 200;
                m_SerialPort.Open();

                /*
                 * get reader frequency
                 */
                actionType = ActionType.GetFrequencyRegion;
                GetFrequencyRegion(PR9200Setting.btReadId);
            }
            catch (System.Exception ex)
            {
                strException = ex.Message;
                return -1;
            }

            m_nType = 0;
            return 0;
        }

        public void CloseCom()
        {
            if (m_SerialPort.IsOpen)
            {
                m_SerialPort.Close();
            }

            m_nType = -1;
        }

        private byte[] btWriteDataBuffer = null;
        public void WriteTag(byte[] tbWriteData)
        {
            /*
            *  writing tag consist of two steps
             *  
             * 1 step: identify the tag type(this step used for getting epc data and tag type)
             * 2 step: writing data to tag
            */

            btWriteDataBuffer = new byte[tbWriteData.Length];
            tbWriteData.CopyTo(btWriteDataBuffer, 0);

            actionType = ActionType.TagIdentify;

            /*
             * before writting data to tag, get the TID first.
             * the first 2 word of TID bank contains the manufacture and Tag info,
             * the next 4 word contains the unique TID of the tag.
             */
            byte wordoffset = 0x00;
            byte wordCnt = 0x06;
            ReadTag(PR9200Setting.btReadId, MemoryBank.TIDBank, wordoffset, wordCnt);
        }
        #endregion

        #region private functions
       
        private void ReceivedComData(object sender, SerialDataReceivedEventArgs e)
        {
            int rslength = 0;
            bool loop = true; // Judge whether the data of the receive buffer is all received
            try
            {
                #region receiving data from serial port
                Thread.Sleep(30);
                while (loop)
                {
                    if (m_SerialPort.BytesToRead == rslength)
                    {
                        loop = false;
                    }
                    else
                    {
                        rslength = m_SerialPort.BytesToRead;
                        // Get the number of bytes of data in the receive buffer
                    }
                    Thread.Sleep(30);
                }
                byte[] readBuffer = new byte[rslength];
                m_SerialPort.Read(readBuffer, 0, rslength); // Read the receive buffer data
                m_SerialPort.DiscardInBuffer();//Clear the receive buffer data
                #endregion

                //process the received data here
                DispatchPacket(readBuffer);
            }
            catch 
            {
               
            }
        }

        /// <summary>
        /// dispatch the packet to related function to handle by action type
        /// </summary>
        /// <param name="btAryReceiveData"></param>
        private void DispatchPacket(byte[] btAryReceiveData)
        {
            #region build response packets
            List<PR9200Msg> lstResponsePacket = new List<PR9200Msg>();

            for (int i = 0; i < btAryReceiveData.Length; i++)
            {
                if (btAryReceiveData.Length > i + 1)
                {
                    if (btAryReceiveData[i] == 0xA0)
                    {
                        int nLen = Convert.ToInt32(btAryReceiveData[i + 1]);
                        if (i + 1 + nLen < btAryReceiveData.Length)
                        {
                            byte[] btAryAnaly = new byte[nLen + 2];
                            Array.Copy(btAryReceiveData, i, btAryAnaly, 0, nLen + 2);

                            lstResponsePacket.Add(new PR9200Msg(btAryAnaly));

                            i += 1 + nLen;
                        }
                        else
                        {
                            i += 1 + nLen;
                        }
                    }
                }
            }
            #endregion

            #region dispatch response packet to related functions
            switch (actionType)
            {
                case ActionType.None:
                    break;
                case ActionType.GetFrequencyRegion:
                    /*
                     * this packet returnd when initialise the reader at the beginning
                     */
                    #region MyRegion
                    if (lstResponsePacket.Count==1)
                    {
                        PacketReceived_GetFrequencyRegion(lstResponsePacket[0]);
                    }
                    else
                    {
                        SetErrorCode("返回数据包格式不正确，请重试！");
                        dlgt_GetFrequencyRegion(ActionResault.GetFrequencyRegionFail);
                    }
                    #endregion
                    break;
                case ActionType.TagIdentify:
                    /*
                     * this packet returnd when writing tag
                     */
                    #region MyRegion
                    if (lstResponsePacket.Count == 1)
                    {
                        PacketReceived_TagIdentify(lstResponsePacket[0]);
                    }
                    else if (lstResponsePacket.Count == 0)
                    {
                        SetErrorCode("未找到标签，请重试！");
                        dlgt_TagIdentifyFail(ActionResault.ReadTIDBankWhenWriteFail);
                    }
                    else if (lstResponsePacket.Count == 0)
                    {
                        SetErrorCode("找到多个标签，请将多余标签拿走！");
                        dlgt_TagIdentifyFail(ActionResault.ReadTIDBankWhenWriteFail);
                    }
                    #endregion
                    break;
                case ActionType.WriteTag:
                    #region MyRegion
                    if (lstResponsePacket.Count == 1)
                    {
                        PacketReceived_WriteTag(lstResponsePacket[0]);
                    }
                    #endregion
                    break;
                case ActionType.ReadEPC:
                    break;
                case ActionType.ReadTIDBankWhenRead:
                    break;
                case ActionType.ReadUserBank:
                    break;
                
                default:
                    break;
            }
            #endregion
        }

        private void SetErrorCode(string strError)
        {
            strErrorCode = strError;
        }


        #endregion


        
        #region packet dispatch functions

        private void ProcessSetWorkAntenna(PR9200Msg msgTran)
        {
            //int intCurrentAnt = 0;
            //intCurrentAnt = PR9200Setting.btWorkAntenna + 1;
            //string strCmd = "设置工作天线成功,当前工作天线: 天线" + intCurrentAnt.ToString();

            //string strErrorCode = string.Empty;

            //if (msgTran.AryData.Length == 1)
            //{
            //    if (msgTran.AryData[0] == ErrorCode.CommandSuccess)
            //    {
            //        PR9200Setting.btReadId = msgTran.ReadId;
            //        ////WriteLog(lrtxtLog, strCmd, 0);

            //        ////校验是否盘存操作
            //        //if (m_bInventory)
            //        //{
            //        //    RunLoopInventroy();
            //        //}
            //        return;
            //    }
            //    else
            //    {
            //        strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            //    }
            //}
            //else
            //{
            //    strErrorCode = "未知错误";
            //}

            //string strLog = strCmd + "失败，失败原因： " + strErrorCode;
           
        }

        //private void TagIdentify(PR9200Msg msgTran)
        //{
        //    string strErrorCode = string.Empty;

        //    if (msgTran.AryData.Length == 1)
        //    {
        //        strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
        //        string strLog = "读标签失败，失败原因： " + strErrorCode;

        //        SetErrorCode(strLog);
        //        readTIDBankWhenWriteFailDelegate(ActionResault.ReadTIDBankWhenWriteFail);
        //    }
        //    else
        //    {
        //        int nLen = msgTran.AryData.Length;
        //        int nDataLen = Convert.ToInt32(msgTran.AryData[nLen - 3]);//lenght of the data we specified
        //        int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - nDataLen - 4;//totally data length minus specified data length minus PC&CRC(4 bytes)

        //        string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
        //        string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
        //        string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
        //        string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 7 + nEpcLen, nDataLen);//the specified data 

        //        byte byTemp = msgTran.AryData[nLen - 2];
        //        byte byAntId = (byte)((byTemp & 0x03) + 1);
        //        string strAntId = byAntId.ToString();

        //        string strReadCount = msgTran.AryData[nLen - 1].ToString();

        //        string sUniqueTID = "";
        //        bool b_tagValid = true;

        //        m_sTIDType = strData.Substring(0, 11);
        //        sUniqueTID = strData.Substring(12);

        //        if (m_sTIDType.Equals(UHFTIDTypes.Alien_Higgs3))
        //        {
        //            //WriteLog(lrtxtLog, "this is alien higgs 3 chip", 1);
        //        }
        //        else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4D))
        //        {
        //            //WriteLog(lrtxtLog, "this is monza 4d chip", 1);
        //        }
        //        else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4E))
        //        {
        //            //WriteLog(lrtxtLog, "this is monza 4e chip", 1);
        //        }
        //        else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4i))
        //        {
        //            //WriteLog(lrtxtLog, "this is monza 4i chip", 1);
        //        }
        //        else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4QT))
        //        {
        //            //WriteLog(lrtxtLog, "this is monza 4qtchip", 1);
        //        }
        //        else
        //        {
        //            string err = "标签类型未知";
        //            SetErrorCode(err);
        //            readTIDBankWhenWriteFailDelegate(ActionResault.ReadTIDBankWhenWriteFail);

        //            //WriteLog(lrtxtLog, "this tag is not valid", 1);
        //            b_tagValid = false;
        //        }

        //        if (b_tagValid)
        //        {
        //            tagUid = sUniqueTID;

        //            byte btMemBank = MemoryBank.USERBank;
        //            byte btWordAdd = 0x00;
        //            byte btWordCnt = 0x00;

        //            byte[] btAryPwd = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        //            //byte[] btAryData = Encoding.ASCII.GetBytes(data);

        //            Int16 dataLen = Convert.ToInt16(btWriteDataBuffer);
        //            byte[] btLen = BitConverter.GetBytes(dataLen);

        //            byte[] btAryDataAndLen = new byte[btWriteDataBuffer.Length + 2];
        //            btLen.CopyTo(btAryDataAndLen, 0);                                   //the first 2 bytes contains data length
        //            btWriteDataBuffer.CopyTo(btAryDataAndLen, 2);

        //            btWordCnt = Convert.ToByte(btAryDataAndLen.Length / 2 + btAryDataAndLen.Length % 2);//1 word equals 2 bytes

        //            /*
        //             * finnaly, we got the data to be write
        //             */
        //            actionType = ActionType.WriteTag;
        //            WriteTag(PR9200Setting.btReadId, btAryPwd, btMemBank, btWordAdd, btWordCnt, btAryDataAndLen);
        //        }
        //    }
        //}


        private void PacketReceived_TagIdentify(PR9200Msg msgTran)
        {
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = "读标签失败，失败原因： " + strErrorCode;

                SetErrorCode(strLog);
                dlgt_TagIdentifyFail(ActionResault.ReadTIDBankWhenWriteFail);
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nDataLen = Convert.ToInt32(msgTran.AryData[nLen - 3]);//lenght of the data we specified, stored in the third byte from last, according to application note
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - nDataLen - 4;//totally data length minus specified data length minus PC&CRC(4 bytes)

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 7 + nEpcLen, nDataLen);//the specified data 

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                string sUniqueTID = "";
                bool b_tagValid = true;

                m_sTIDType = strData.Substring(0, 11);
                sUniqueTID = strData.Substring(12);

                if (m_sTIDType.Equals(UHFTIDTypes.Alien_Higgs3))
                {
                    //WriteLog(lrtxtLog, "this is alien higgs 3 chip", 1);
                }
                else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4D))
                {
                    //WriteLog(lrtxtLog, "this is monza 4d chip", 1);
                }
                else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4E))
                {
                    //WriteLog(lrtxtLog, "this is monza 4e chip", 1);
                }
                else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4i))
                {
                    //WriteLog(lrtxtLog, "this is monza 4i chip", 1);
                }
                else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4QT))
                {
                    //WriteLog(lrtxtLog, "this is monza 4qtchip", 1);
                }
                else
                {
                    string err = "标签类型未知";
                    SetErrorCode(err);
                    dlgt_TagIdentifyFail(ActionResault.ReadTIDBankWhenWriteFail);

                    //WriteLog(lrtxtLog, "this tag is not valid", 1);
                    b_tagValid = false;
                }

                if (b_tagValid)
                {
                    tagUid = sUniqueTID;

                    byte btMemBank = MemoryBank.USERBank;
                    byte btWordAdd = 0x00;
                    byte btWordCnt = 0x00;

                    byte[] btAryPwd = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                    //byte[] btAryData = Encoding.ASCII.GetBytes(data);

                    Int16 dataLen = Convert.ToInt16(btWriteDataBuffer);
                    byte[] btLen = BitConverter.GetBytes(dataLen);

                    byte[] btAryDataAndLen = new byte[btWriteDataBuffer.Length + 2];
                    btLen.CopyTo(btAryDataAndLen, 0);                                   //the first 2 bytes contains data length
                    btWriteDataBuffer.CopyTo(btAryDataAndLen, 2);

                    btWordCnt = Convert.ToByte(btAryDataAndLen.Length / 2 + btAryDataAndLen.Length % 2);//1 word equals 2 bytes

                    /*
                     * finnaly, we got the data to be write, go to step 2: writing data to tag
                     */
                    actionType = ActionType.WriteTag;
                    WriteTag(PR9200Setting.btReadId, btAryPwd, btMemBank, btWordAdd, btWordCnt, btAryDataAndLen);
                }
            }
        }

        private void PacketReceived_WriteTag(PR9200Msg msgTran)
        {
            /*
             * after write tag, should check if the tag is the same tag as read before
             */
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = "读标签失败，失败原因： " + strErrorCode;

                SetErrorCode(strLog);
                dlgt_TagIdentifyFail(ActionResault.ReadTIDBankWhenWriteFail);
            }
            else
            {
                /*  response packet byte map
                 * 
                 *  TagCount        DataLen     Data        ErrorCode       AndID       WriteCount
                 *     2 byte       1byte       Nbyte       1byte           1byte       1byte
                 *  
                 */

                int nLen = msgTran.AryData.Length;
                int nDataLen = Convert.ToInt32(msgTran.AryData[2]);//lenght of useful data of a tag(PC+EPC+CRC)
                int nEpcLen = nDataLen - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);

                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen-3]);
                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();
                string writeCount = msgTran.AryData[nLen - 1].ToString();//the last byte contains write count, according to application note

                /*
                 * check these things:
                 *      1: writecount should be 1;
                 *      2: error code should be success
                 *      3: epc should equals the one read before
                 *      
                 * after all check passed, retun ok
                 */
            }
        }


        //private void ProcessReadTag(UHFReader.PR9200Msg msgTran)
        //{
        //    string strCmd = "读标签";
        //    string strErrorCode = string.Empty;

        //    if (msgTran.AryData.Length == 1)
        //    {
        //        strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
        //        string strLog = strCmd + "失败，失败原因： " + strErrorCode;

        //        //WriteLog(lrtxtLog, strLog, 1);
        //    }
        //    else
        //    {
        //        int nLen = msgTran.AryData.Length;
        //        int nDataLen = Convert.ToInt32(msgTran.AryData[nLen - 3]);//lenght of the data we specified
        //        int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - nDataLen - 4;//totally data length minus specified data length minus PC&CRC(4 bytes)

        //        string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
        //        string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
        //        string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
        //        string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 7 + nEpcLen, nDataLen);//the specified data 

        //        byte byTemp = msgTran.AryData[nLen - 2];
        //        byte byAntId = (byte)((byTemp & 0x03) + 1);
        //        string strAntId = byAntId.ToString();

        //        string strReadCount = msgTran.AryData[nLen - 1].ToString();

        //        if (action_Queue.Count > 0)
        //        {
        //            string sUniqueTID = "";
        //            bool b_tagValid = true;

        //            switch (action_Queue.Peek())
        //            {
        //                case ActionType.TagIdentify:
        //                    #region MyRegion
        //                    m_sTIDType = strData.Substring(0, 11);
        //                    sUniqueTID = strData.Substring(12);

        //                    if (m_sTIDType.Equals(UHFTIDTypes.Alien_Higgs3))
        //                    {
        //                        WriteLog(lrtxtLog, "this is alien higgs 3 chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4D))
        //                    {
        //                        WriteLog(lrtxtLog, "this is monza 4d chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4E))
        //                    {
        //                        WriteLog(lrtxtLog, "this is monza 4e chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4i))
        //                    {
        //                        WriteLog(lrtxtLog, "this is monza 4i chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4QT))
        //                    {
        //                        WriteLog(lrtxtLog, "this is monza 4qtchip", 1);
        //                    }
        //                    else
        //                    {
        //                        WriteLog(lrtxtLog, "this tag is not valid", 1);
        //                        b_tagValid = false;
        //                    }

        //                    if (b_tagValid)
        //                    {
        //                        m_sCurrentUniqueTID = sUniqueTID;

        //                        GetTIDCallback();
        //                    }
        //                    else
        //                    {
        //                        //tbx_SerialWrite.Enabled = true; EnableControl(1);
        //                        EnableControl(1);
        //                    }
        //                    #endregion
        //                    break;
        //                case ActionType.ReadEPC:
        //                    WriteLog(lrtxtLog, strData, 0);
        //                    break;
        //                case ActionType.ReadTIDBankWhenRead:
        //                    #region MyRegion
        //                    m_sTIDType = strData.Substring(0, 11);
        //                    sUniqueTID = strData.Substring(12);

        //                    if (m_sTIDType.Equals(UHFTIDTypes.Alien_Higgs3))
        //                    {
        //                        m_userMemorySizeInWord = UHFUserMemorySizeInWord.A_9662;
        //                        WriteLog(lrtxtLog, "this is alien higgs 3 chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4D))
        //                    {
        //                        m_userMemorySizeInWord = UHFUserMemorySizeInWord.Monza_4D;
        //                        WriteLog(lrtxtLog, "this is monza 4d chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4E))
        //                    {
        //                        m_userMemorySizeInWord = UHFUserMemorySizeInWord.Monza_4E;
        //                        WriteLog(lrtxtLog, "this is monza 4e chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4i))
        //                    {
        //                        m_userMemorySizeInWord = UHFUserMemorySizeInWord.Monza_4i;
        //                        WriteLog(lrtxtLog, "this is monza 4i chip", 1);
        //                    }
        //                    else if (m_sTIDType.Equals(UHFTIDTypes.Monza_4QT))
        //                    {
        //                        m_userMemorySizeInWord = UHFUserMemorySizeInWord.Monza_4QT;
        //                        WriteLog(lrtxtLog, "this is monza 4qtchip", 1);
        //                    }
        //                    else
        //                    {
        //                        m_userMemorySizeInWord = 0x00;
        //                        WriteLog(lrtxtLog, "this tag is not valid", 1);
        //                        b_tagValid = false;
        //                    }

        //                    if (b_tagValid)
        //                    {
        //                        m_sCurrentUniqueTID = sUniqueTID;

        //                        ReadUserBank();
        //                    }
        //                    else
        //                    {
        //                        //tbx_readSerial.Enabled = true;
        //                        EnableControl(2);
        //                    }
        //                    #endregion
        //                    break;
        //                case ActionType.ReadUserBank:
        //                    byte[] userdata = new byte[nDataLen];
        //                    msgTran.AryData.CopyTo(userdata, 7 + nEpcLen);
        //                    UserBankDataInterperater(userdata);
        //                    break;
        //                default:
        //                    break;
        //            }

        //            action_Queue.Dequeue();
        //        }
        //    }
        //}


        private void PacketReceived_GetFrequencyRegion(PR9200Msg msgTran)
        {
            string strCmd = "取得射频规范";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 3)
            {//system frequencies used
                PR9200Setting.btReadId = msgTran.ReadId;
                PR9200Setting.btRegion = msgTran.AryData[0];
                PR9200Setting.btFrequencyStart = msgTran.AryData[1];
                PR9200Setting.btFrequencyEnd = msgTran.AryData[2];

                dlgt_GetFrequencyRegion(ActionResault.GetFrequencyRegionSuccess);
                //WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 6)
            {//user defined frequencies used
                PR9200Setting.btReadId = msgTran.ReadId;
                PR9200Setting.btRegion = msgTran.AryData[0];
                PR9200Setting.btUserDefineFrequencyInterval = msgTran.AryData[1];
                PR9200Setting.btUserDefineChannelQuantity = msgTran.AryData[2];
                PR9200Setting.nUserDefineStartFrequency = msgTran.AryData[3] * 256 * 256 + msgTran.AryData[4] * 256 + msgTran.AryData[5];

                dlgt_GetFrequencyRegion(ActionResault.GetFrequencyRegionSuccess);
                //WriteLog(lrtxtLog, strCmd, 0);
                return;


            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            //WriteLog(lrtxtLog, strLog, 1);
            SetErrorCode(strLog);
            dlgt_GetFrequencyRegion(ActionResault.GetFrequencyRegionFail);
        }


        #endregion



        private int m_targetMemoryBank = 0;

        

        private void GetEPC()
        {
            /*
             * here, the word offset and wordcnt should referense the TAG spec, 
             * different tag may have different spec.
             * 
             * eg:Monza 4QT
             *      epc data is from the third word(or fifth byte).
             *      the first two byte are CRC-16, next two bytes are PC
             */
            try
            {
                //action_Queue.Enqueue(ActionType.ReadEPC);

                byte wordoffset = 0x02;     //2 words
                byte wordCnt = 0x06;        //8 words(16 byte/ 128 bit)
                //TagManger.reader.ReadTag(PR9200Setting.btReadId, MemoryBank.EPCBank, wordoffset, wordCnt);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private byte m_userMemorySizeInWord = 0x00;
        private void ReadUserBank()
        {
            //action_Queue.Enqueue(ActionType.ReadUserBank);

            try
            {
                byte wordoffset = 0x00;
                //TagManger.reader.ReadTag(PR9200Setting.btReadId, MemoryBank.USERBank, wordoffset, m_userMemorySizeInWord);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //tbx_readSerial.Enabled = true;
                //EnableControl(2);
            }

        }

        private string m_sTIDType = "";

        private bool DateLenthValidInUserMemory(int dataLength)
        {
            bool resault = false;

            switch (m_sTIDType)
            {
                case UHFTIDTypes.Alien_Higgs3:
                    resault = dataLength <= 128 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4D:
                    resault = dataLength <= 32 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4E:
                    resault = dataLength <= 128 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4i:
                    resault = dataLength <= 480 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4QT:
                    resault = dataLength <= 512 ? true : false;
                    break;
                default:
                    break;
            }

            return resault;
        }

        private bool DateLenthValidInEPCMemoryBank(int dataLength)
        {
            bool resault = false;

            switch (m_sTIDType)
            {
                case UHFTIDTypes.Alien_Higgs3:
                    resault = dataLength <= 480 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4D:
                    resault = dataLength <= 128 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4E:
                    resault = dataLength <= 496 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4i:
                    resault = dataLength <= 256 ? true : false;
                    break;
                case UHFTIDTypes.Monza_4QT:
                    resault = dataLength <= 128 ? true : false;
                    break;
                default:
                    break;
            }

            return resault;
        }

        
        private int SendMessage(byte[] btArySenderData)
        {
            //串口连接方式
            if (m_nType == 0)
            {
                if (!m_SerialPort.IsOpen)
                {
                    return -1;
                }

                m_SerialPort.Write(btArySenderData, 0, btArySenderData.Length);

                //if (SendCallback != null)
                //{
                //    SendCallback(btArySenderData);
                //}

                return 0;
            }

            return -1;
        }

        private int SendMessage(byte btReadId, byte btCmd)
        {
            PR9200Msg msgTran = new PR9200Msg(btReadId, btCmd);

            return SendMessage(msgTran.AryTranData);
        }

        private int SendMessage(byte btReadId, byte btCmd, byte[] btAryData)
        {
            PR9200Msg msgTran = new PR9200Msg(btReadId, btCmd, btAryData);

            return SendMessage(msgTran.AryTranData);
        }

        private byte CheckValue(byte[] btAryData)
        {
            PR9200Msg msgTran = new PR9200Msg();

            return msgTran.CheckSum(btAryData, 0, btAryData.Length);
        }

        #region part of reader commands
        
        private int Reset(byte btReadId)
        {
            byte btCmd = 0x70;

            int nResult = SendMessage(btReadId, btCmd);

            return nResult;
        }

        private int SetWorkAntenna(byte btReadId, byte btWorkAntenna)
        {
            byte btCmd = 0x74;
            byte[] btAryData = new byte[1];
            btAryData[0] = btWorkAntenna;
            int nResult = SendMessage(btReadId, btCmd, btAryData);
            return nResult;
        }

        private int GetWorkAntenna(byte btReadId)
        {
            byte btCmd = 0x75;

            int nResult = SendMessage(btReadId, btCmd);

            return nResult;
        }

        private int SetOutputPower(byte btReadId, byte btOutputPower)
        {
            byte btCmd = 0x76;
            byte[] btAryData = new byte[1];
            btAryData[0] = btOutputPower;

            int nResult = SendMessage(btReadId, btCmd, btAryData);

            return nResult;
        }

        private int GetOutputPower(byte btReadId)
        {
            byte btCmd = 0x77;

            int nResult = SendMessage(btReadId, btCmd);

            return nResult;
        }

        private int GetFrequencyRegion(byte btReadId)
        {
            byte btCmd = 0x79;

            int nResult = SendMessage(btReadId, btCmd);

            return nResult;
        }

        private int ReadTag(byte btReadId, byte btMemBank, byte btWordAdd, byte btWordCnt)
        {
            byte btCmd = 0x81;
            byte[] btAryData = new byte[3];
            btAryData[0] = btMemBank;
            btAryData[1] = btWordAdd;
            btAryData[2] = btWordCnt;

            int nResult = SendMessage(btReadId, btCmd, btAryData);

            return nResult;
        }

        private int WriteTag(byte btReadId, byte[] btAryPassWord, byte btMemBank, byte btWordAdd, byte btWordCnt, byte[] btAryData)
        {
            byte btCmd = 0x82;
            byte[] btAryBuffer = new byte[btAryData.Length + 7];
            btAryPassWord.CopyTo(btAryBuffer, 0);
            btAryBuffer[4] = btMemBank;
            btAryBuffer[5] = btWordAdd;
            btAryBuffer[6] = btWordCnt;
            btAryData.CopyTo(btAryBuffer, 7);

            int nResult = SendMessage(btReadId, btCmd, btAryBuffer);

            return nResult;
        }
        
        #endregion
        
    }
}
