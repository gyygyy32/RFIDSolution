using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using UHFDesk.consts;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using Helper;
using UHFReader;

namespace UHFDesk
{
    
    public partial class UHFDeskMain : Form
    {
        //private Queue<ActionType> action_Queue=new Queue<ActionType>();

        DealINI iniReader;

        const string writeFormTag = "write";
        const string readFormTag = "read";

        private static string _license = "";
        private static string _server = "";
        public static bool _licensedPorduct = false;

        public static PR9200Reader reader = new PR9200Reader();

        public UHFDeskMain()
        {
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config.ini"))
            {
                MessageBox.Show("Cant't find configuration file, please contact with administror!");
                System.Environment.Exit(0);
            }

            DealINI._inipath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";

            ReadIni();

#if !DEBUG  

            if (MD5Handler.verifyMd5Hash(_license))
            {
                _licensedPorduct = true;
            }

            _licensedPorduct = true;
#endif
#if DEBUG
            _licensedPorduct = true;
#endif

            if (!CheckServerAvailable(_server))
            {
                MessageBox.Show("Cant't connect server, please contact with administror!");
                System.Environment.Exit(0);
            }

            InitializeComponent();
        }

        private bool CheckServerAvailable(string url)
        {
            Ping requestServer = new Ping();
            PingReply serverResponse = requestServer.Send(url);

            if (serverResponse.Status != IPStatus.Success)
                return false;

            return true;
        }

        private bool CheckLicense(string license)
        {
            return MD5Handler.verifyMd5Hash(license);
        }

        private void ReadIni()
        {
            _license = DealINI.IniReadValue("SETTING", "LICENSE");
            _server = DealINI.IniReadValue("SETTING", "SERVER");
        }

        internal static class NativeWinAPI
        {
            internal static readonly int GWL_EXSTYLE = -20;
            internal static readonly int WS_EX_COMPOSITED = 0x02000000;

            [DllImport("user32")]
            internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32")]
            internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        }

        

        private void RFIDeskUHF_Load(object sender, EventArgs e)
        {
            if (!_licensedPorduct)
            {
                return;
            }

            //show write tag form by default
            UHFWriteTag newMDIChild = new UHFWriteTag();
            newMDIChild.Tag = writeFormTag;
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Normal;
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void menu_writeTag_Click(object sender, EventArgs e)
        {
            if (!_licensedPorduct)
            {
                MessageBox.Show("the product is not licensed!");
                return;
            }

            foreach (Form f in this.MdiChildren)
            {
                if (f.Tag.ToString() == writeFormTag)
                {
                    f.BringToFront();
                    return;
                }
            }
            // Couldn't find one, so open on

            UHFWriteTag newMDIChild = new UHFWriteTag();
            newMDIChild.Tag = writeFormTag;
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
            //newMDIChild.WindowState = FormWindowState.Normal;
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void menu_readTag_Click(object sender, EventArgs e)
        {
            //return;

            if (!_licensedPorduct)
            {
                MessageBox.Show("the product is not licensed!");
                return;
            }


            foreach (Form f in this.MdiChildren)
            {
                if (f.Tag.ToString() == readFormTag)
                {
                    f.BringToFront();
                    return;
                }
            }

            UHFReadTag newMDIChild = new UHFReadTag();
            newMDIChild.Tag = readFormTag;
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
            //newMDIChild.WindowState = FormWindowState.Normal;
            newMDIChild.WindowState = FormWindowState.Maximized;
        }

        private void menu_about_Click(object sender, EventArgs e)
        {

        }

        private void menu_register_Click(object sender, EventArgs e)
        {
            //return;

            ActivateProduct newWindow = new ActivateProduct(_licensedPorduct);

            DialogResult dialogresult = newWindow.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                newWindow.Close();
            }
        }



       
       

         
    }
}
