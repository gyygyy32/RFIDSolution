using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
namespace RFIDMain.cls
{
    /// <summary>
    /// 高频
    /// </summary>
    public class HFISO15693 : RFIDInterface
    {
        #region Properties
        
        private Int16 _st = 0;
        public Int16 st
        {
            get
            {
                return _st;
            }
            set
            {
                _st = value;
            }
        }

        private RFIDDeviceConfig _rConfig = new RFIDDeviceConfig();
        /// <summary>
        /// RFID 设备 参数 ：串口、波特率。。。
        /// </summary>
        public RFIDDeviceConfig rConfig
        {
            get
            {
                return _rConfig;
            }
            set
            {
                _rConfig = value;
            }
        }
        #endregion


        #region Methods
        
       
        public void Open(ref string strLog)
        {
            /*
             * open reader if not connected
             */
            //if (!ReaderInfo.readerConnerted)
            //{
                Int16 iUsbPort = 100;
                ReaderInfo.icdev = common.rf_init(iUsbPort, 0);
                if (ReaderInfo.icdev > 0)
                {
                    Beep(10);
                    strLog = "HF高频读写器连接成功！";
                    ReaderInfo.readerConnerted = true;
                }
                else
                {
                    strLog = "HF高频读写器连接失败";
                    return;
                }
           // }
        }

        public void Close()
        {
            try
            {
                common.rf_exit(ReaderInfo.icdev);
                
            }
            catch
            {

            }
            ReaderInfo.readerConnerted = false;
        }
        public ErrorCode IsTagWrited()
        {
            try
            {
                ReadTagID();
                
                byte[] rtnData = new byte[4];    //read first block, get the data length
                byte rtnLen = 0;
                st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, 0x00, (byte)1, rConfig.m_btTagUID, out rtnLen, rtnData);
                if (st != 0)
                {
                    //MessageBox.Show("error");
                    return ErrorCode.ReadFail;
                }
                else
                {
                    //bool b_readLengthData = true;

                    byte[] lenthData = new byte[2];
                    //the first two bytes stored data length
                    Array.Copy(rtnData, 2, lenthData, 0, 2);

                    Int32 i_totalBytes = BitConverter.ToInt16(lenthData, 0) + 4;

                    if (i_totalBytes == 4)
                    {
                        return ErrorCode.TagHasNoData;
                    }

                    rConfig.readBuffer = new byte[i_totalBytes - 4];



                    st = 0;
                    byte blockIndex = 1;
                    int byteIndex = 0;

                    while (i_totalBytes > 0 && st == 0)
                    {
                        byte blockLen = 0;
                        //if (i_totalBytes % 4 == 0)
                        //{
                        //    blockLen = (byte)(i_totalBytes / 4);
                        //}
                        //else
                        //{
                        //    blockLen = (byte)(i_totalBytes / 4 + 1);
                        //}

                        blockLen = (byte)((i_totalBytes + 3) / 4);

                        //calculate block number required, max number is 10
                        byte blockNumber = blockLen > (byte)10 ? (byte)10 : blockLen;

                        //byte byteNumber = 0;

                        byte[] readData = new byte[blockNumber * 4];

                        st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, blockIndex, blockNumber, rConfig.m_btTagUID, out rtnLen, readData);

                        if (st == 0)
                        {
                            int leftDataLength = rConfig.readBuffer.Length - byteIndex;
                            int copyDataLength = leftDataLength > readData.Length ? readData.Length : leftDataLength;

                            //if (b_readLengthData)
                            //{
                            //    Array.Copy(readData, 2, readBuffer, byteIndex, copyDataLength == readData.Length ? copyDataLength - 2 : copyDataLength);

                            //    //b_readLengthData = true;
                            //}
                            //else
                            //{
                            Array.Copy(readData, 0, rConfig.readBuffer, byteIndex, copyDataLength);
                            //}
                        }
                        else
                        {
                            return ErrorCode.ReadFail;
                        }

                        byteIndex += rtnLen;
                        //if (b_readLengthData)
                        //{
                        //    byteIndex -= 2;

                        //    b_readLengthData = false;
                        //}
                        blockIndex += blockNumber;
                        i_totalBytes -= rtnLen;

                        System.Threading.Thread.Sleep(20);
                    }

                    return ErrorCode.ReadSuccessful;

                }
            }
            catch (Exception)
            {
                return ErrorCode.OtherException;
            }
        }

        public byte[] ReadTagBuff()
        {
            return new byte[10];
        }

        public bool ReadTagID()
        {                       
            //only can inventery 1 tag, because the reader is a shit
            UInt16 byteLen = 0;
            byte[] ary_data = new byte[9];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                int loop = 0;
                bool stopLoop = false;
                st = 0;
                /*
                * loop 30 second to find tag
                */
                while (loop < 300 && !stopLoop)
                {
                    st = ISO15693Commands.rf_inventory(ReaderInfo.icdev, 0x36, 0x00, 0x00, out byteLen, ary_data);

                    stopLoop = st == 0 ? true : false;

                    loop++;

                    System.Threading.Thread.Sleep(100);
                }

                if (st != 0)
                {
                    //MessageBox.Show("未发现单个标签");
                    return false;
                }
                else
                {
                    Array.Copy(ary_data, 1, rConfig.m_btTagUID, 0, 8);

                    byte[] msbFstUID = new byte[8];
                    Array.Copy(rConfig.m_btTagUID, msbFstUID, 8);
                    Array.Reverse(msbFstUID);

                    rConfig.m_sTagUIDstring = CCommondMethod.ByteArrayToString(msbFstUID, 0, 8);

                    return true;

                }
            }
            catch (Exception)
            {
                return false; 
            }

            
        }

        public bool WriteTagBuff(byte[] dataBytes)
        {
             
            try
            {
                byte[] dataLen = BitConverter.GetBytes(dataBytes.Length);
                byte[] writenDataAll = new byte[dataBytes.Length + 4];

                dataLen.CopyTo(writenDataAll, 2);               //data length stored in the third and forth byte
                dataBytes.CopyTo(writenDataAll, 4);             //useful data starts from next block--the fifth byte

                int i_totalBytes = dataBytes.Length + 4;    //UOM is byte
                st = 0;

                byte blockIndex = 0;
                int byteIndex = 0;
                while (i_totalBytes > 0 && st == 0)
                {
                    //writing data block by block
                    byte byteNumber = i_totalBytes > 4 ? (byte)4 : (byte)i_totalBytes;

                    byte[] writenData = new byte[4];//the minimum writen unit is block
                    Array.Copy(writenDataAll, byteIndex, writenData, 0, byteNumber);

                    st = ISO15693Commands.rf_writeblock(ReaderInfo.icdev, 0x22, blockIndex, (byte)1, rConfig.m_btTagUID, (byte)4, writenData);

                    if (st != 0)
                    {
                        return false;
                    }

                    byteIndex += byteNumber;
                    blockIndex += 1;
                    i_totalBytes -= byteNumber;

                    System.Threading.Thread.Sleep(20);
                }

                return  true;

            }
            catch (Exception)
            {
                return  false;
            }
        }


        
        public void Beep(int msec)
        {
            common.rf_beep(ReaderInfo.icdev, msec);
        }
        public void Speech(string sWorld)
        {
            try
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(sWorld);
            }
            catch (Exception)
            {

                //throw;
            }
        }

        #endregion


    }
}
