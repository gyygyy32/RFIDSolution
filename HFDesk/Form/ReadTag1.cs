using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HFDesk
{
    public partial class ReadTag1 : Form
    {
        private byte[] tagUIDbyte = new byte[8];
        private byte[] readBuffer = null;

        private Int16 st;

        private string tagUIDstring;

        public ReadTag1()
        {
            InitializeComponent();
        }

        private void ReadTag1_Load(object sender, EventArgs e)
        {
            /*
             * open reader if not connected
             */

            if (!ReaderInfo.readerConnerted)
            {
                Int16 iUsbPort = 100;
                ReaderInfo.icdev = common.rf_init(iUsbPort, 0);

                if (ReaderInfo.icdev > 0)
                {
                    common.rf_beep(ReaderInfo.icdev, 10);

                    string strLog = "读写器连接成功！";
                    WriteLog(lrtxtLog, strLog, 0);

                    //byte[] status = new byte[30];
                    //st = common.rf_get_status(icdev, status);
                    //lbHardVer.Text = System.Text.Encoding.ASCII.GetString(status);
                }
                else
                {
                    string strLog = "读写器连接失败";
                    WriteLog(lrtxtLog, strLog, 1);

                    return;
                }
            }
            else
            {
                string strLog = "读写器连接成功！";
                WriteLog(lrtxtLog, strLog, 0);
            }

        }


        private void btn_readTag_Click(object sender, EventArgs e)
        {
            paintBackgroundColor(statusType.START);

            if (!GetTagUID())
            {
                WriteLog(lrtxtLog, "没有发现标签！", 1);

                common.rf_beep(ReaderInfo.icdev, 20);

                paintBackgroundColor(statusType.FAIL);
                return;
            }
            else
            {
                System.Threading.Thread.Sleep(20);

                ErrorCode ec = ReadData();
                switch (ec) { 
                    case ErrorCode.CanNotFindTag:
                        paintBackgroundColor(statusType.FAIL);
                        common.rf_beep(ReaderInfo.icdev, 20);
                        string str = "无法找到标签，请重试！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.OtherException:
                        paintBackgroundColor(statusType.FAIL);
                        common.rf_beep(ReaderInfo.icdev, 20);
                        str = "其他异常，请重试";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.ReadFail:
                        paintBackgroundColor(statusType.FAIL);
                        common.rf_beep(ReaderInfo.icdev, 20);
                        str = "读取失败，请重试！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.ReadSuccessful:
                        paintBackgroundColor(statusType.PASS);
                        common.rf_beep(ReaderInfo.icdev, 10);
                        string storedDataString = Encoding.ASCII.GetString(readBuffer);
                        WriteLog(lrtxtLog, storedDataString, 0);
                        break;
                    case ErrorCode.TagHasNoData:
                        paintBackgroundColor(statusType.FAIL);
                        common.rf_beep(ReaderInfo.icdev, 20);
                        str = "空标签！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    default:
                        break;
                }
            }
        }

        private enum ErrorCode { 
            ReadSuccessful,
            ReadFail,
            TagHasNoData,
            CanNotFindTag,
            OtherException,
        }

        private ErrorCode ReadData()
        {
            // the application note says, max block number per read is 10 blocks.
            try
            {
                byte[] rtnData = new byte[4];    //read first block, get the data length
                byte rtnLen = 0;
                st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, 0x00, (byte)1, tagUIDbyte, out rtnLen, rtnData);
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

                    if (i_totalBytes==4)
                    {
                        return ErrorCode.TagHasNoData;
                    }

                    readBuffer = new byte[i_totalBytes - 4];



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

                        st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, blockIndex, blockNumber, tagUIDbyte, out rtnLen, readData);

                        if (st == 0)
                        {
                            int leftDataLength = readBuffer.Length - byteIndex;
                            int copyDataLength = leftDataLength > readData.Length ? readData.Length : leftDataLength;

                            //if (b_readLengthData)
                            //{
                            //    Array.Copy(readData, 2, readBuffer, byteIndex, copyDataLength == readData.Length ? copyDataLength - 2 : copyDataLength);

                            //    //b_readLengthData = true;
                            //}
                            //else
                            //{
                                Array.Copy(readData, 0, readBuffer, byteIndex, copyDataLength);
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

       

        private bool GetTagUID()
        {
            //only can inventery 1 tag, because the reader is a shit
            UInt16 byteLen = 0;
            byte[] ary_data = new byte[9];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                st = ISO15693Commands.rf_inventory(ReaderInfo.icdev, 0x36, 0x00, 0x00, out byteLen, ary_data);
                if (st != 0)
                {
                    MessageBox.Show("未发现单个标签");
                    return false;
                }
                else
                {
                    Array.Copy(ary_data, 1, tagUIDbyte, 0, 8);

                    byte[] msbFstUID = new byte[8];
                    Array.Copy(tagUIDbyte, msbFstUID, 8);
                    Array.Reverse(msbFstUID);

                    tagUIDstring = CCommondMethod.ByteArrayToString(msbFstUID, 0, 8);

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region control ui handler
        private delegate void WriteLogUnSafe(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType);
        private void WriteLog(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType)
        {
            if (this.InvokeRequired)
            {
                WriteLogUnSafe InvokeWriteLog = new WriteLogUnSafe(WriteLog);
                this.Invoke(InvokeWriteLog, new object[] { logRichTxt, strLog, nType });
            }
            else
            {
                if (nType == 0)
                {
                    logRichTxt.AppendTextEx(strLog, Color.Indigo);
                }
                else
                {
                    logRichTxt.AppendTextEx(strLog, Color.Red);
                }

                if (ckClearOperationRec.Checked)
                {
                    if (logRichTxt.Lines.Length > 50)
                    {
                        logRichTxt.Clear();
                    }
                }

                logRichTxt.Select(logRichTxt.TextLength, 0);
                logRichTxt.ScrollToCaret();
            }
        }

        enum statusType
        {
            START,
            PASS,
            FAIL
        }

        private delegate void paintBackgroundColorDlgt(statusType st);
        private void paintBackgroundColor(statusType st)
        {
            if (this.InvokeRequired)
            {
                paintBackgroundColorDlgt InvokepaintBackgroundColor = new paintBackgroundColorDlgt(paintBackgroundColor);
                this.Invoke(InvokepaintBackgroundColor, new object[] { st });
            }
            else
            {
                switch (st)
                {
                    case statusType.START:
                        this.BackColor = Color.White;
                        break;
                    case statusType.FAIL:
                        this.BackColor = Color.Red;
                        break;
                    case statusType.PASS:
                        this.BackColor = Color.LightGreen;
                        break;
                    default:
                        break;
                }

                this.Refresh();
            }
        }

        private delegate void EnableControlDlgt(int btnIndex);
        private void EnableControl(int idx)
        {
            if (this.InvokeRequired)
            {
                EnableControlDlgt InvokeEnableControl = new EnableControlDlgt(EnableControl);
                this.Invoke(InvokeEnableControl, new object[] { idx });
            }
            else
            {
                if (idx == 1)
                {
                    //tbx_SerialWrite.Enabled = true;
                }
                else if (idx == 2)
                {
                    //tbx_readSerial.Enabled = true;
                }
            }
        }

        #endregion

       
        
    }
}
