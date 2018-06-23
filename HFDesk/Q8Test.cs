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
    public partial class Q8Test : Form
    {
        private Int16 st;
        private int icdev; // 通讯设备标识符

        public Q8Test()
        {
            InitializeComponent();
        }

        private void Q8Test_Load(object sender, EventArgs e)
        {
            Int16 iUsbPort = 100;
            icdev = common.rf_init(iUsbPort, 0);

            if (icdev > 0)
            {
                common.rf_beep(icdev, 10);
            }
            else
            {
                common.rf_beep(icdev, 10);

                common.rf_beep(icdev, 20);

                return;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            inventoryTest();
        }

        private void inventoryTest()
        {
            //only can inventery 1 tag, because the reader is a shit
            UInt16 byteLen = 0;
            byte[] ary_data = new byte[9];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                st = ISO15693Commands.rf_inventory(icdev, 0x36, 0x00, 0x00, out byteLen, ary_data);
                if (st != 0)
                {
                    MessageBox.Show("未发现单个标签");
                    return;
                }
                else
                {
                    string UID = CCommondMethod.ByteArrayToString(ary_data, 1, 8);

                    Array.Copy(ary_data, 1, uid, 0, 8);

                    textBox1.Text = UID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readBlockTest();
        }

        byte[] uid=new byte[8];
        private void readBlockTest()
        {
            byte byteLen = 0;
            byte[] ary_data = new byte[128];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                byte[] a = new byte[8];
                st = ISO15693Commands.rf_readblock(icdev,0x22, 0x00, 0x0A, uid, out byteLen, ary_data);
                if (st != 0)
                {
                    MessageBox.Show("未发现单个标签");
                    return;
                }
                else
                {
                    string blockData = CCommondMethod.ByteArrayToString(ary_data, 0, 128);

                    textBox2.Text = blockData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool WriteData(string data)
        {
            try
            {
                byte[] dataLen = BitConverter.GetBytes(Convert.ToInt16(data.Length));
                byte[] dataBytes = Encoding.ASCII.GetBytes(data);
                byte[] writenDataAll = new byte[data.Length + 2];

                dataLen.CopyTo(writenDataAll, 0);               //first two bytes contains data length
                dataBytes.CopyTo(writenDataAll, 2);             //usefal data starts from third byte

                int i_totalBytes = data.Length + 2;    //UOM is byte
                st = 0;

                byte blockIndex = 0;
                int byteIndex=0;
                while (i_totalBytes > 0 && st==0)
                {
                    byte blockLen = 0;
                    if (i_totalBytes % 4==0)
                    {
                        blockLen = (byte)(i_totalBytes / 4);
                    }
                    else
                    {
                        blockLen = (byte)(i_totalBytes / 4 + 1);
                    }
                    //calculate block number required, max number is 10
                    byte blockNumber = blockLen > (byte)0x0A ? (byte)0x0A : blockLen;

                    //calculate the byte number of writen data, the max number is 10 block = 40 bytes
                    byte byteNumber = i_totalBytes > 40 ? (byte)0x28 : (byte)i_totalBytes;

                    byte[] writenData= new byte[byteNumber];
                    Array.Copy(writenDataAll, byteIndex, writenData,0, byteNumber);

                    st = ISO15693Commands.rf_writeblock(icdev, 0x22, blockIndex, blockNumber, uid, byteNumber, writenData);

                    System.Threading.Thread.Sleep(20);

                    byteIndex += byteNumber;
                    blockIndex += blockNumber;
                    i_totalBytes -= byteNumber;


                }

                return true;
               
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string data = textBox3.Text;

            WriteData(data);
        }

    }
}
