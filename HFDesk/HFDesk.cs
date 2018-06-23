using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using HFDesk.helpClass;

namespace HFDesk
{
    public partial class HFDesk : Form
    {
        //DealINI iniReader;

        const string writeFormTag = "write";
        const string readFormTag = "read";

        private static string _license = "";
        private static string _server = "";
        public static bool _licensedPorduct = false;

        public HFDesk()
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

            //_licensedPorduct = true;
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

            /*
             * this is avoid flicker
             */
            int style = NativeWinAPI.GetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE);
            style |= NativeWinAPI.WS_EX_COMPOSITED;
            NativeWinAPI.SetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE, style);

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

        
        private void HFDesk_Load(object sender, EventArgs e)
        {
            if (!_licensedPorduct)
            {
                return;
            }

            //show write tag form by default
            WriteTag1 newMDIChild = new WriteTag1();
            newMDIChild.Tag = writeFormTag;
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
            newMDIChild.WindowState = FormWindowState.Normal;
            newMDIChild.WindowState = FormWindowState.Maximized;

          
        }


        private void btn_serverSetup_Click(object sender, EventArgs e)
        {

        }

        private void menu_about_Click(object sender, EventArgs e)
        {

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

            WriteTag1 newMDIChild = new WriteTag1();
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

            ReadTag1 newMDIChild = new ReadTag1();
            newMDIChild.Tag = readFormTag;
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.WindowState = FormWindowState.Maximized;
            newMDIChild.Show();
            //newMDIChild.WindowState = FormWindowState.Normal;
            newMDIChild.WindowState = FormWindowState.Maximized;
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
