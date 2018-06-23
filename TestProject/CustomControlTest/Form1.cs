using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControl;

namespace CustomControlTest
{
    public partial class Form1 : Form
    {
        //RfidTagTDO obj = null;
        public Form1()
        {
            InitializeComponent();

            //obj = new RfidTagTDO("123456789");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            I_V_Point[] oriPointArray = new I_V_Point[3];
            I_V_Point ivp = new I_V_Point(8.97, 0);
            oriPointArray[0] = ivp;
            ivp = new I_V_Point(8.42, 30.73);
            oriPointArray[1] = ivp;
            ivp = new I_V_Point(0, 38.08);
            oriPointArray[2] = ivp;
            ivCurves1.SetOriginalPoints(oriPointArray, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ivCurves1.SetOriginalPoints(null, true);
        }
    }
}
