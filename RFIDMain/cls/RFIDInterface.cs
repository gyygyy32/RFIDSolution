using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDMain.cls
{
    public interface RFIDInterface
    {

        // bool ScanBarcode(out string barcode);
        #region Properties
        
        Int16 st { get; set; }

        
        /// <summary>
        /// RFID 设备 参数 ：串口、波特率。。。
        /// </summary>
        RFIDDeviceConfig rConfig { get; set; }
        #endregion
     
        
        
        #region Methods
        
       
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="strLog"></param>
        void Open(ref string strLog);
        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        ErrorCode IsTagWrited();

        byte[] ReadTagBuff();
        /// <summary>
        /// 读取标签 TagID
        /// </summary>
        /// <returns></returns>
        bool ReadTagID();
        /// <summary>
        /// 写入标签
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        bool WriteTagBuff(byte[] buff);
        /// <summary>
        /// 连接提示
        /// </summary>
        /// <param name="msec"></param>
        void Beep(int msec);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sWorld"></param>
        void Speech(string sWorld);

        #endregion
    }
    #region RFID 设备 参数 ：串口、波特率。。。
    /// <summary>
    /// RFID 设备 参数 ：串口、波特率。。。
    /// </summary>
    public class RFIDDeviceConfig
    {

        private string _strComPort = "COM0";
        /// <summary>
        /// 串口
        /// </summary>
        public string strComPort
        {
            get
            {

                return _strComPort;
            }
            set
            {
                _strComPort = value;
            }
        }


        private int _nBaudrate = 115200;//38400
        /// <summary>
        /// 波特率 默认 115200 、
        /// </summary>
        public int nBaudrate
        {
            get
            {

                return _nBaudrate;
            }
            set
            {
                _nBaudrate = value;
            }
        }

       private byte _btMemBank = 0x03;
        /// <summary>
       /// UHF超高频 的 密码区=0x00、EPC区=0x01、TID区=0x02、User区=0x03，默认 User区=0x03 
        /// </summary>
       public byte btMemBank
       {
           get
           {

               return _btMemBank;
           }
           set
           {
               _btMemBank = value;
           }
       }

        private UHFReader.ReaderMethod _reader = null;
        /// <summary>
        /// D-100  UHF 超高频 读写工具
        /// </summary>
        public UHFReader.ReaderMethod reader
        {
            get
            {
                return _reader;
            }
            set
            {
                _reader = value;
            }

        }
        private bool _writeSucceed = false;
        /// <summary>
        /// true 写入成功，false=写入失败
        /// </summary>
        public bool writeSucceed
        {
            get
            {
                return _writeSucceed;
            }
            set
            {
                _writeSucceed = value;
            }

        }


        private ReaderSetting _m_curSetting = new ReaderSetting();
        /// <summary>
        /// D-100  UHF 超高频设置
        /// </summary>
        public ReaderSetting m_curSetting
        {
            get
            {
                return _m_curSetting;
            }
            set
            {
                _m_curSetting = value;
            }

        }
        private InventoryBuffer _m_curInventoryBuffer = new InventoryBuffer();
        public InventoryBuffer m_curInventoryBuffer
        {
            get
            {
                return _m_curInventoryBuffer;
            }
            set
            {
                _m_curInventoryBuffer = value;
            }

        }
        private OperateTagBuffer _m_curOperateTagBuffer = new OperateTagBuffer();
        public OperateTagBuffer m_curOperateTagBuffer
        {
            get
            {
                return _m_curOperateTagBuffer;
            }
            set
            {
                _m_curOperateTagBuffer = value;
            }

        }

        private OperateTagISO18000Buffer _m_curOperateTagISO18000Buffer = new OperateTagISO18000Buffer();
        public OperateTagISO18000Buffer m_curOperateTagISO18000Buffer
        {
            get
            {
                return _m_curOperateTagISO18000Buffer;
            }
            set
            {
                _m_curOperateTagISO18000Buffer = value;
            }

        }
        private byte[] _m_btTagUID = new byte[8];
        /// <summary>
        /// HF高频 目标标签的  UID
        /// </summary>
        public byte[] m_btTagUID
        {
            get
            {
                return _m_btTagUID;
            }
            set
            {
                _m_btTagUID = value;
            }
        }
        private string _m_sTagUIDstring = string.Empty;
        /// <summary>
        /// HF高频 目标标签的  UID 字符串
        /// </summary>
        public string m_sTagUIDstring
        {
            get
            {
                return _m_sTagUIDstring;
            }
            set
            {
                _m_sTagUIDstring = value;
            }
        }

        private byte[] _readTempBuffer = null;
        /// <summary>
        /// 每读一次所读取的内容
        /// </summary>
        public byte[] readTempBuffer
        {
            get
            {
                return _readTempBuffer;
            }
            set
            {
                _readTempBuffer = value;
            }
        }


        private byte[] _readBuffer = null;
        /// <summary>
        /// 读取的所有内容
        /// </summary>
        public byte[] readBuffer
        {
            get
            {
                return _readBuffer;
            }
            set
            {
                _readBuffer = value;
            }
        }


        public RFIDDeviceConfig()
        {

        }
    }

    #endregion


}
