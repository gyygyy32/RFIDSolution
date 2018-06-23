using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RFIDMain.helpClass;
using RFIDMain.Properties;
using System.Threading;
using System.Speech.Synthesis;
using CustomControl;
using RFIDService.ClientData;
using RFIDMain.cls;

namespace RFIDMain
{
    public partial class WriteTag : Form
    {
        private byte[] m_btTagUID = new byte[8];
        //private byte[] m_btReadBuffer = null;

        private Int16 st;

        private string m_sTagUIDstring = "";
        private string m_sSerialNumber = "";
        private string m_sBasicInfo = "";

        private ModuleInfo oModuleInfo = null;

        private System.Threading.Timer timer = null;

        /// <summary>
        /// RFID 设备
        /// </summary>
        private RFIDInterface _RFIDDevice = null;

        public WriteTag()
        {
            InitializeComponent();

            timer = new System.Threading.Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            //set color back to transparent
            paintBackgroundColor(statusType.START);
            //timer.Change(15000, Timeout.Infinite);
        }

        private void WriteTag_Load(object sender, EventArgs e)
        {
          

            SetLabelStatus(statusType.START);


            //_RFIDDevice = new HFISO15693();
            _RFIDDevice = new UHFISO18000();     
            string strLog = string.Empty;
            _RFIDDevice.Open(ref strLog);
            WriteLog(lrtxtLog, strLog, ReaderInfo.readerConnerted ? 0 : 1);
            chkbox_burningTag.Checked = ReaderInfo.readerConnerted;

            //初始化文本框内容 add by xue lei on 2018-6-23
            textBox2.Text = "ZNSHINE";
            textBox1.Text = "2016/4/29";
            textBox3.Text = "YOT";
            textBox4.Text = "ISO9001";
           
             
        }

        private void tbx_SerialWrite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CleanIVCurves();
                ShowModuleInfo(false);
                string ser = tbx_SerialWrite.Text.Trim().ToUpper();

                if (ser.Length > 0)
                {
                    paintBackgroundColor(statusType.START);
                    tbx_SerialWrite.Enabled = false;
                    
                    SetLabelStatus(statusType.START);

                    m_sSerialNumber = ser;
                   
                    if (chkbox_burningTag.Checked)
                    {
                        if (!_RFIDDevice.ReadTagID())//GetTagUID()
                        {
                            WriteLog(lrtxtLog, "没有发现标签！", 1);
                            common.rf_beep(ReaderInfo.icdev, 20);
                            paintBackgroundColor(statusType.FAIL);

                        

                            List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                            WriteCSVLog.WriteCSV(csvValueList);
                            SetSerialTxtFocus();
                            Speech("烧录失败");
                        }
                        else
                        {
                            #region query data from database
                            WcfCaller.querySerialInfo((o, ex) =>
                            {
                                if (ex == null)
                                {
                                    if (o==null)
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + "未找到组件记录！");
                                        return;
                                    }

                                    #region check region state

                                    if (string.IsNullOrEmpty(o.ProductType))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt01);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt01, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.CellDate))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt02);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt02, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.PackedDate))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt03);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt03, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.Pmax))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt04);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt04, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.Voc))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt05);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt05, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.Vpm))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt06);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt06, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.Ipm))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt07);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt07, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(o.Isc))
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt08);
                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt08, 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);
                                        //paintBackgroundColor(statusType.FAIL);
                                        return;
                                    }
                                    #endregion

                                    ShowIVCurves(double.Parse(o.Isc), double.Parse(o.Ipm), double.Parse(o.Vpm), double.Parse(o.Voc),o.Module_ID);

                                    byte[] btData = TagDataFormat.CreateByteArray(o);

                                    oModuleInfo = o;

                                    m_sBasicInfo = o.ProductType + "|" + o.PackedDate.Replace("-", ".") + "|" 
                                        + o.Pivf + "|" + o.Module_ID + "|" + o.CellDate.Replace("-", ".") + "|3";

                                    if (_RFIDDevice.WriteTagBuff(btData))//WriteData(btData)
                                    {
                                        WriteLog2DB();
                                        //paintBackgroundColor(statusType.PASS);
                                        //WriteLog(lrtxtLog, "烧录成功！", 0);
                                        //common.rf_beep(ReaderInfo.icdev, 10);
                                    }
                                    else
                                    {
                                        DoFailStuff(m_sSerialNumber + " " + "烧录失败！");
                                        //paintBackgroundColor(statusType.FAIL);
                                        //WriteLog(lrtxtLog, m_sSerialNumber + " " + "烧录失败！", 1);
                                        //common.rf_beep(ReaderInfo.icdev, 20);

                                        //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                        //WriteCSVLog.WriteCSV(csvValueList);
                                        //SetSerialTxtFocus();
                                        //Speech("烧录失败");
                                    }
                                }
                                else
                                {
                                    DoFailStuff("与服务器通讯发生异常" + ex.Message);
                                    //WriteLog(lrtxtLog, "与服务器通讯发生异常"+ex.Message, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //SetSerialTxtFocus();
                                    //Speech("烧录失败");
                                }
                            }, new string[] { m_sSerialNumber, m_sTagUIDstring });
                            #endregion
                    
                        }
                    }
                    else
                    {
                        #region query data from database
                        WcfCaller.querySerialInfo((o, ex) =>
                        {
                            if (ex == null)
                            {
                                if (o == null)
                                {
                                    DoFailStuff(m_sSerialNumber + " " + "未找到组件记录！");
                                    return;
                                }

                                #region check region state

                                if (string.IsNullOrEmpty(o.ProductType))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt01);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt01, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                if (string.IsNullOrEmpty(o.CellDate))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt02);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt02, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                //===============不取包装的数据 modify by xue lei on 2018-6-23==================
                                //if (string.IsNullOrEmpty(o.PackedDate))
                                //{
                                //    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt03);
                                //    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                //    //WriteCSVLog.WriteCSV(csvValueList);
                                //    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt03, 1);
                                //    //common.rf_beep(ReaderInfo.icdev, 20);
                                //    //paintBackgroundColor(statusType.FAIL);
                                //    return;
                                //}
                                //===============================================================
                                if (string.IsNullOrEmpty(o.Pmax))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt04);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt04, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                if (string.IsNullOrEmpty(o.Voc))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt05);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt05, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                if (string.IsNullOrEmpty(o.Vpm))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt06);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt06, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                if (string.IsNullOrEmpty(o.Ipm))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt07);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt07, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                if (string.IsNullOrEmpty(o.Isc))
                                {
                                    DoFailStuff(m_sSerialNumber + " " + Resources.strPrompt08);
                                    //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                    //WriteCSVLog.WriteCSV(csvValueList);
                                    //WriteLog(lrtxtLog, m_sSerialNumber + " " + Resources.strPrompt08, 1);
                                    //common.rf_beep(ReaderInfo.icdev, 20);
                                    //paintBackgroundColor(statusType.FAIL);
                                    return;
                                }
                                #endregion

                                ShowIVCurves(double.Parse(o.Isc), double.Parse(o.Ipm), double.Parse(o.Vpm), double.Parse(o.Voc),o.Module_ID);

                                oModuleInfo = o;

                                paintBackgroundColor(statusType.PASS);
                                
                                ShowModuleInfo(true);

                                WriteLog(lrtxtLog, m_sSerialNumber + " " + "获取功率信息成功！", 0);

                                SetSerialTxtFocus();
                            }
                            else
                            {
                                DoFailStuff("与服务器通讯发生异常" + ex.Message);
                                //WriteLog(lrtxtLog, "与服务器通讯发生异常"+ex.Message, 1);
                                //common.rf_beep(ReaderInfo.icdev, 20);
                                //paintBackgroundColor(statusType.FAIL);
                                //List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                                //WriteCSVLog.WriteCSV(csvValueList);
                                //SetSerialTxtFocus();
                                //Speech("烧录失败");
                            }
                        }, new string[] { m_sSerialNumber, m_sTagUIDstring });
                        #endregion
                    }

                    
                }
            }
        }

        private void DoFailStuff(string promptMsg)
        {
            paintBackgroundColor(statusType.FAIL);
            WriteLog(lrtxtLog, promptMsg, 1);
            common.rf_beep(ReaderInfo.icdev, 20);

            List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
            WriteCSVLog.WriteCSV(csvValueList);
            SetSerialTxtFocus();
            Speech("烧录失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioc"></param>
        /// <param name="ipm"></param>
        /// <param name="vpm"></param>
        /// <param name="voc"></param>
        /// <param name="oriPointArray">add by genhong.hu On 2017-12-31 </param>
        private void ShowIVCurves(double ioc, double ipm, double vpm, double voc, string LotID="",I_V_Point[] oriPointArray=null)
        {
            if (oriPointArray == null || oriPointArray.Length < 4 )// 4 个 以下用 ico、ipm、vpm、voc
            {
                oriPointArray = new I_V_Point[3];
                I_V_Point ivp = new I_V_Point(ioc, 0);
                oriPointArray[0] = ivp;
                ivp = new I_V_Point(ipm, vpm);
                oriPointArray[1] = ivp;
                ivp = new I_V_Point(0, voc);
                oriPointArray[2] = ivp;
            }
            ivCurves1.SetOriginalPoints(oriPointArray, true,LotID);
        }
       
        private void CleanIVCurves()
        { 
            ivCurves1.SetOriginalPoints(null, true);
        }

        private void WriteLog2DB()
        {

            //string basicInfo = "";

            WcfCaller.WriteLog(ex => {
                if (ex==null)
                {
                    paintBackgroundColor(statusType.PASS);
                    WriteLog(lrtxtLog, m_sSerialNumber + " " + "烧录成功！", 0);
                    common.rf_beep(ReaderInfo.icdev, 10);
                    List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Pass",m_sTagUIDstring, m_sBasicInfo };
                    WriteCSVLog.WriteCSV(csvValueList);
                    ShowModuleInfo(true);
                    Speech("烧录成功");
                }
                else
                {
                    paintBackgroundColor(statusType.FAIL);
                    WriteLog(lrtxtLog, m_sSerialNumber + " " + "写记录到数据库失败！", 1);
                    common.rf_beep(ReaderInfo.icdev, 20);
                    List<string> csvValueList = new List<string> { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_sSerialNumber, "Fail", "" };
                    WriteCSVLog.WriteCSV(csvValueList);
                    Speech("烧录失败");
                }

                SetSerialTxtFocus();


            }, new string[] { m_sTagUIDstring, m_sSerialNumber, m_sBasicInfo });
        }

        private void Speech(string sWorld)
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

        #region communication with RFID reader
        
      

        /*
         * write data to tag, the first two byte contains data length, 
         * the develop note says the reader can doing write 10 blocks one time, 
         * but reality is it only can write 1 block one time. 
         * totally shit
         */
        private bool WriteData(string data)
        {
            try
            {
                byte[] dataLen = BitConverter.GetBytes(Convert.ToInt16(data.Length));
                byte[] dataBytes = Encoding.ASCII.GetBytes(data);
                byte[] writenDataAll = new byte[data.Length + 4];

                dataLen.CopyTo(writenDataAll, 2);               //data length stored in the third and forth byte
                dataBytes.CopyTo(writenDataAll, 4);             //usefal data starts from next block--the fifth byte

                int i_totalBytes = data.Length + 4;    //UOM is byte
                st = 0;

                byte blockIndex = 0;
                int byteIndex = 0;
                while (i_totalBytes > 0 && st == 0)
                {
                    //calculate the byte number of writen data, the max number is 10 block = 40 bytes
                    byte byteNumber = i_totalBytes > 4 ? (byte)4 : (byte)i_totalBytes;

                    byte[] writenData = new byte[4];//the minimum writen unit is block
                    Array.Copy(writenDataAll, byteIndex, writenData, 0, byteNumber);

                    st = ISO15693Commands.rf_writeblock(ReaderInfo.icdev, 0x22, blockIndex, (byte)1, m_btTagUID, (byte)4, writenData);

                    if (st != 0)
                    {
                        return false;
                    }

                    byteIndex += byteNumber;
                    blockIndex += 1;
                    i_totalBytes -= byteNumber;

                    System.Threading.Thread.Sleep(20);
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        
        #endregion
        
        #region control ui handler =======================================================================================================
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
                    logRichTxt.AppendTextEx( strLog, Color.Indigo);
                }
                else
                {
                    logRichTxt.AppendTextEx( strLog, Color.Red);
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
                        //tbx_SerialWrite.Enabled = false;
                        m_sTagUIDstring = "";
                        m_sSerialNumber = "";
                        CleanIVCurves();// add by genhong.hu ON 2018-01-06
                        ShowModuleInfo(false);// add by genhong.hu ON 2018-01-06

                        //SetLabelStatus(statusType.START);
                        break;
                    case statusType.FAIL:
                        this.BackColor = Color.Red;
                        tbx_SerialWrite.Enabled = true;
                        SetLabelStatus(statusType.FAIL);
                        timer.Change(5000, System.Threading.Timeout.Infinite);
                        break;
                    case statusType.PASS:
                        this.BackColor = Color.LightGreen;
                        tbx_SerialWrite.Enabled = true;
                        SetLabelStatus(statusType.PASS);
                        timer.Change(5000, System.Threading.Timeout.Infinite);
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
                    tbx_SerialWrite.Enabled = true;
                }
                else if (idx == 2)
                {
                    //tbx_readSerial.Enabled = true;
                }
            }
        }


        private delegate void SetFocusDlgt();
        private void SetSerialTxtFocus() {
            if (this.InvokeRequired)
            {
                SetFocusDlgt InvokeEnableControl = new SetFocusDlgt(SetSerialTxtFocus);
                this.Invoke(InvokeEnableControl, null );
            }
            else
            {
                tbx_SerialWrite.Text = "";
                tbx_SerialWrite.Focus();
                //tbx_SerialWrite.SelectAll();

            }
        }

        private delegate void SetLabelStatusDlgt(statusType st);
        private void SetLabelStatus(statusType st)
        {
            if (this.InvokeRequired)
            {
                SetLabelStatusDlgt InvokeEnableControl = new SetLabelStatusDlgt(SetLabelStatus);
                this.Invoke(InvokeEnableControl, new object[] { st });
            }
            else
            {
                switch (st) { 
                    case statusType.START:
                        tbx_status.Text = "";
                        tbx_status.BackColor = Color.LightGray;
                        //CleanIVCurves();
                        break;
                    case statusType.PASS:
                        tbx_status.Text = "OK";
                        tbx_status.BackColor = Color.LightGreen;
                        break;
                    case statusType.FAIL:
                        tbx_status.Text = "NG";
                        tbx_status.BackColor = Color.Red;
                        break;
                    default:
                        break;
                }

            }
        }


        private delegate void ShowModuleInfoDlgt(bool bSuccess);
        private void ShowModuleInfo(bool bSuccess)
        {
            if (this.InvokeRequired)
            {
                ShowModuleInfoDlgt InvokeShowModuleInfo = new ShowModuleInfoDlgt(ShowModuleInfo);
                this.Invoke(InvokeShowModuleInfo, new object[] { bSuccess });
            }
            else
            {
                if (!bSuccess)
                {
                    tbx_celldate.Text = "";
                    tbx_ipm.Text = "";
                    tbx_isc.Text = "";
                    tbx_packdate.Text = "";
                    tbx_pmax.Text = "";
                    tbx_prodtype.Text = "";
                    tbx_voc.Text = "";
                    tbx_vpm.Text = "";
                    tbx_ff.Text = "";
                }
                else
                {
                    tbx_celldate.Text = oModuleInfo.CellDate;
                    tbx_ipm.Text = oModuleInfo.Ipm;
                    tbx_isc.Text = oModuleInfo.Isc;
                    tbx_packdate.Text = oModuleInfo.PackedDate;
                    tbx_pmax.Text = oModuleInfo.Pmax;
                    tbx_prodtype.Text = oModuleInfo.ProductType;
                    tbx_voc.Text = oModuleInfo.Voc;
                    tbx_vpm.Text = oModuleInfo.Vpm;

                    string stemp = oModuleInfo.Pivf;


                    tbx_ff.Text = stemp.Substring(stemp.LastIndexOf(',') + 1);
                }

            }
        }
        #endregion


        #region 读取验证===================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void BtRead_Click(object sender, EventArgs e)
        {
            paintBackgroundColor(statusType.START);

            if (!_RFIDDevice.ReadTagID())
            {
                WriteLog(lrtxtLog, "没有发现标签！", 1);
                _RFIDDevice.Beep( 20);
                paintBackgroundColor(statusType.FAIL);
                return;
            }
            else
            {
                System.Threading.Thread.Sleep(20);

                ErrorCode ec = _RFIDDevice.IsTagWrited();// ReadData();
                switch (ec)
                {
                    case ErrorCode.CanNotFindTag:
                        paintBackgroundColor(statusType.FAIL);
                        _RFIDDevice.Beep(20);
                        string str = "无法找到标签，请重试！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.OtherException:
                        paintBackgroundColor(statusType.FAIL);
                        _RFIDDevice.Beep(20);
                        str = "其他异常，请重试";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.ReadFail:
                        paintBackgroundColor(statusType.FAIL);
                        _RFIDDevice.Beep(20);
                        str = "读取失败，请重试！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    case ErrorCode.ReadSuccessful:
                        paintBackgroundColor(statusType.PASS);
                        _RFIDDevice.Beep(10);
                        oModuleInfo = TagDataFormat.ParserTag(_RFIDDevice.rConfig.readBuffer);
                        ShowModuleInfo(true);
                        ShowIVCurves(double.Parse(oModuleInfo.Isc), double.Parse(oModuleInfo.Ipm), double.Parse(oModuleInfo.Vpm), double.Parse(oModuleInfo.Voc),   oModuleInfo.Module_ID);
                        
                        string storedDataString = Encoding.ASCII.GetString(_RFIDDevice.rConfig.readBuffer);
                        WriteLog(lrtxtLog, storedDataString.Replace("@@","").Replace ("##",""), 0);
                        _RFIDDevice.Speech("读取成功");
                        break;
                    case ErrorCode.TagHasNoData:
                        paintBackgroundColor(statusType.FAIL);
                        _RFIDDevice.Beep(20);
                        str = "空标签！";
                        WriteLog(lrtxtLog, str, 1);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private void BtCloseReader_Click(object sender, EventArgs e)
        {
            _RFIDDevice.Close();
            WriteLog(lrtxtLog, "断开读写器", 1);
        }

        private void rb_UHF_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_UHF.Checked)
            {
                
                _RFIDDevice = new UHFISO18000();
                _RFIDDevice.Close();
                string strLog = string.Empty;
                _RFIDDevice.Open(ref strLog);
            }
        }

        private void rb_HF_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_HF.Checked)
            {
                _RFIDDevice = new HFISO15693();
                _RFIDDevice.Close();
                string strLog = string.Empty;
                _RFIDDevice.Open(ref strLog);
            }

        }

        private void WriteTag_Shown(object sender, EventArgs e)
        {
            tbx_SerialWrite.Focus();
        } 

    }
}
