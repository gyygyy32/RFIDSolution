﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFReader
{
    public class MessageTran
    {
        private byte btPacketType;     //数据包头，默认为0xA0
        private byte btDataLen;        //数据包长度，数据包从‘长度’字节后面开始的字节数，不包含‘长度’字节本身
        private byte btReadId;         //读写器地址
        private byte btCmd;            //数据包命令代码
        private byte[] btAryData;      //数据包命令参数，部分命令无参数
        private byte btCheck;          //校验和，除校验和本身外所有字节的校验和
        private byte[] btAryTranData;  //完整数据包

        //设置属性
        public byte[] AryTranData
        {
            get
            {
                return btAryTranData;
            }
        }

        public byte[] AryData
        {
            get
            {
                return btAryData;
            }
        }

        public byte ReadId
        {
            get
            {
                return btReadId;
            }
        }

        public byte Cmd
        {
            get
            {
                return btCmd;
            }
        }

        public byte PacketType
        {
            get
            {
                return btPacketType;
            }
        }
        
        public MessageTran()
        {

        }

        public MessageTran(byte btReadId, byte btCmd, byte[] btAryData)
        {
            int nLen = btAryData.Length;

            this.btPacketType = 0xA0;
            this.btDataLen = Convert.ToByte(nLen + 3);
            this.btReadId = btReadId;
            this.btCmd = btCmd;

            this.btAryData = new byte[nLen];
            btAryData.CopyTo(this.btAryData, 0);

            this.btAryTranData = new byte[nLen + 5];
            this.btAryTranData[0] = this.btPacketType;
            this.btAryTranData[1] = this.btDataLen;
            this.btAryTranData[2] = this.btReadId;
            this.btAryTranData[3] = this.btCmd;
            this.btAryData.CopyTo(this.btAryTranData, 4);

            this.btCheck = CheckSum(this.btAryTranData, 0, nLen + 4);
            this.btAryTranData[nLen + 4] = this.btCheck;
        }
        /*
         * this constructor used for transmit command to reader, 
         * which containers only command inforation, without data
         */
        public MessageTran(byte btReadId, byte btCmd)
        {
            this.btPacketType = 0xA0;
            this.btDataLen = 0x03;
            this.btReadId = btReadId;
            this.btCmd = btCmd;

            this.btAryTranData = new byte[5];
            this.btAryTranData[0] = this.btPacketType;
            this.btAryTranData[1] = this.btDataLen;
            this.btAryTranData[2] = this.btReadId;
            this.btAryTranData[3] = this.btCmd;

            this.btCheck = CheckSum(this.btAryTranData, 0, 4);
            this.btAryTranData[4] = this.btCheck;
        }
        /*
         * this constructor used for translate message received from serial port,
         * the data that come from serial port containers many information, 
         * and can be translated according to the protocaol.
         */
        public MessageTran(byte[] btAryTranData)
        {
            int nLen = btAryTranData.Length;

            this.btAryTranData = new byte[nLen];
            btAryTranData.CopyTo(this.btAryTranData, 0);


            byte btCK = CheckSum(this.btAryTranData, 0, this.btAryTranData.Length - 1);
            if (btCK != btAryTranData[nLen - 1])
            {
                return;
            }

            this.btPacketType = btAryTranData[0];
            this.btDataLen = btAryTranData[1];
            this.btReadId = btAryTranData[2];
            this.btCmd = btAryTranData[3];
            this.btCheck = btAryTranData[nLen - 1];

            if (nLen > 5)
            {
                this.btAryData = new byte[nLen - 5];
                for (int nloop = 0; nloop < nLen - 5; nloop++ )
                {
                    this.btAryData[nloop] = btAryTranData[4 + nloop];
                }
            }
        }
        /*
         * according to the serial protacal, each packet contains one byte of CHECK data.
         */
        public byte CheckSum(byte[] btAryBuffer, int nStartPos, int nLen)
        {
            byte btSum = 0x00;

            for (int nloop = nStartPos; nloop < nStartPos + nLen; nloop++ )
            {
                btSum += btAryBuffer[nloop];
            }

            return Convert.ToByte(((~btSum) + 1) & 0xFF);
        }
    }
}