using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Helper;
using UHFDesk.consts;

namespace UHFDesk
{
    public partial class UHFWriteTag : Form
    {
        private string m_sCurrentUniqueTID = "";
        private string m_sCurrentSerialNumber = "";

        private int m_targetMemoryBank = 1;//1 means EPC bank, 2 means User Memory Bank

        public UHFWriteTag()
        {
            InitializeComponent();

            UHFDeskMain.reader.dlgt_TagIdentifyFail = TagIdentifyFailCallback;
        }
 
        private void tbx_SerialWrite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ser = tbx_SerialWrite.Text.Trim().ToUpper();

                if (ser.Length > 0)
                {
                    tbx_SerialWrite.Enabled = false;

                    paintBackgroundColor(statusType.START);

                    m_sCurrentUniqueTID = "";

                    m_sCurrentSerialNumber = ser;

                    WcfCaller.querySerialInfo((o, ex) =>
                    {
                        if (ex == null)
                        {
                            byte[] btData = TagDataFormat.CreateByteArray(o);

                            //call reader to write data to tag
                            UHFDeskMain.reader.WriteTag(btData);

                            //if (WriteData(btData))
                            //{
                            //    paintBackgroundColor(statusType.PASS);
                            //    WriteLog(lrtxtLog, "烧录成功！", 0);
                            //    common.rf_beep(ReaderInfo.icdev, 10);
                            //}
                            //else
                            //{
                            //    paintBackgroundColor(statusType.FAIL);
                            //    WriteLog(lrtxtLog, "烧录失败！", 1);
                            //    common.rf_beep(ReaderInfo.icdev, 20);
                            //}
                        }
                        else
                        {
                            WriteLog(lrtxtLog, "与服务器通讯发生异常" + ex.Message, 1);
                            //common.rf_beep(ReaderInfo.icdev, 20);
                            paintBackgroundColor(statusType.FAIL);
                        }
                    }, new string[] { m_sCurrentSerialNumber, m_sCurrentUniqueTID }
                        );

                }
            }
        }


        #region reader calback funcitons
        /// <summary>
        /// this function will be called when tag identify fialed, 
        /// before writing data to tag, need know which tag type, 
        /// and get the epc data.
        /// </summary>
        /// <param name="ar"></param>
        private void TagIdentifyFailCallback(ActionResault ar)
        {
            if (ar==ActionResault.ReadTIDBankWhenWriteFail)
            {
                MessageBox.Show(UHFDeskMain.reader.ErrorCode);
            }
        }
        #endregion
        
        #region ui delegate funcitons 
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
        #endregion
    }
}
