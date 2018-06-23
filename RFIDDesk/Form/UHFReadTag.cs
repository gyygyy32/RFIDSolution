using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UHFDesk
{
    enum statusType
    {
        START,
        PASS,
        FAIL
    }

    public partial class UHFReadTag : Form
    {
        public UHFReadTag()
        {
            InitializeComponent();
        }


        private void btn_ReadTag_Click(object sender, EventArgs e)
        {
            //string ser = tbx_readSerial.Text.Trim().ToUpper();

            //if (ser.Length > 0)
            //{
            //    tbx_readSerial.Enabled = false;

            //    paintBackgroundColor(statusType.START);

            //    m_sCurrentUniqueTID = "";

            //    m_sCurrentSerialNumber = ser;

            //    byte wordoffset = 0;
            //    byte wordCnt = 8;
            //    reader.ReadTag(m_curSetting.btReadId, MemoryBank.USERBank, wordoffset, wordCnt);
            //}
        }

        private void UserBankDataInterperater(byte[] userdata)
        {
            try
            {
                byte[] Lendata = new byte[2];
                Lendata[0] = userdata[0];
                Lendata[1] = userdata[1];

                Int16 dataLength = BitConverter.ToInt16(Lendata, 0);

                byte[] data = new byte[dataLength];

                for (int i = 0; i < dataLength; i++)
                {
                    data[i] = userdata[i + 2];
                }

                string sData = Encoding.ASCII.GetString(data);

                WriteLog(lrtxtLog, sData, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //tbx_readSerial.Enabled = true;
                EnableControl(2);
            }

        }

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

        

    }
}
