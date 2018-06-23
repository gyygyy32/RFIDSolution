using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UHFDesk.consts;

namespace UHFDesk
{
    public partial class ConfigReader : Form
    {
        public ConfigReader()
        {
            InitializeComponent();

            //初始化连接读写器默认配置
            cmbComPort.SelectedIndex = 0;
            cmbBaudrate.SelectedIndex = 1;

            UHFDeskMain.reader.dlgt_GetFrequencyRegion = GetFrequencyRegionCallback;
        }

        private void btnConnectRs232_Click(object sender, EventArgs e)
        {
            string strException = string.Empty;
            string strComPort = cmbComPort.Text;
            int nBaudrate = Convert.ToInt32(cmbBaudrate.Text);

            int nRet = UHFDeskMain.reader.OpenComAndInitReader(strComPort, nBaudrate, out strException);
            if (nRet != 0)
            {
                string strLog = "连接读写器失败，失败原因： " + strException;
                MessageBox.Show(strLog);

                return;
            }
            else
            {
                string strLog = "连接读写器 " + strComPort + "@" + nBaudrate.ToString();

            }

            ////处理界面元素是否有效
            //SetFormEnable(true);


            
        }

        private void GetFrequencyRegionCallback(ActionResault errorCode)
        {
            if (errorCode==ActionResault.GetFrequencyRegionFail)
            {
                MessageBox.Show(UHFDeskMain.reader.ErrorCode);   
            }
            else
            {

                btnConnectRs232.Enabled = false;
                btnDisconnectRs232.Enabled = true;
                cmbComPort.Enabled = false;
                cmbBaudrate.Enabled = false;

                //设置按钮字体颜色
                btnConnectRs232.ForeColor = Color.Black;
                btnDisconnectRs232.ForeColor = Color.Indigo;
                SetButtonBold(btnConnectRs232);
                SetButtonBold(btnDisconnectRs232);
            }
        }
    
        private void SetButtonBold(Button btnBold)
        {
            Font oldFont = btnBold.Font;
            Font newFont = new Font(oldFont, oldFont.Style ^ FontStyle.Bold);
            btnBold.Font = newFont;
        }

        private void btnDisconnectRs232_Click(object sender, EventArgs e)
        {
            //处理串口断开连接读写器
            UHFDeskMain.reader.CloseCom();
            string strLog = "读写器已断开";
            //WriteLog(lrtxtLog, strLog, 1);

            MessageBox.Show(strLog);


            //处理界面元素是否有效
            //SetFormEnable(false);
            btnConnectRs232.Enabled = true;
            btnDisconnectRs232.Enabled = false;
            cmbComPort.Enabled = true;
            cmbBaudrate.Enabled = true;

            //设置按钮字体颜色
            btnConnectRs232.ForeColor = Color.Indigo;
            btnDisconnectRs232.ForeColor = Color.Black;
            SetButtonBold(btnConnectRs232);
            SetButtonBold(btnDisconnectRs232);
        }

    }
}
