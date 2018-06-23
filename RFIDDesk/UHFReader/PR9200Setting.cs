using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFReader
{
    class PR9200Setting
    {
        public static byte btReadId= 0xFF;
        public static byte btMajor = 0x00;
        public static byte btMinor = 0x00;
        public static byte btIndexBaudrate = 0x00;
        public static byte btPlusMinus = 0x00;
        public static byte btTemperature = 0x00;
        public static byte btOutputPower = 0x00;
        public static byte btWorkAntenna = 0x00;
        public static byte btDrmMode = 0x00;
        public static byte btRegion = 0x00;
        public static byte btFrequencyStart = 0x00;
        public static byte btFrequencyEnd = 0x00;
        public static byte btBeeperMode = 0x00;
        public static byte btGpio1Value = 0x00;
        public static byte btGpio2Value = 0x00;
        public static byte btGpio3Value = 0x00;
        public static byte btGpio4Value = 0x00;
        public static byte btAntDetector = 0x00;
        public static byte btMonzaStatus = 0x00;
        public static string btReaderIdentifier = "";
        public static byte btAntImpedance = 0x00;
        public static byte btImpedanceFrequency = 0x00;

        public static int nUserDefineStartFrequency = 0;
        public static byte btUserDefineFrequencyInterval = 0x00;
        public static byte btUserDefineChannelQuantity = 0x00;
        public static byte btLinkProfile = 0x00;

    }
}
