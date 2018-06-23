using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace HFDesk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool test_version = false;
            bool isCreated;
            Mutex m = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out isCreated);

            if (isCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (test_version)
                {
                    Application.Run(new Q8Test());
                }
                else
                {
                    Application.Run(new HFDesk());
                }
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
            //Application.Run(new HFDesk());
        }
    }
}
