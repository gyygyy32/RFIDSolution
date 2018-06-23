using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace UHFDesk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isCreated;
            Mutex m = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out isCreated);

            if (isCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UHFDeskMain());
            }
            else
            {
                MessageBox.Show("已经有相同的实例在运行!", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Application.Exit();
                //return;
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new UHF());
        }
    }
}
