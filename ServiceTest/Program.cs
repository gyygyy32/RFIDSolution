using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Service.RFID;
using Wcf.ServiceContracts.RFID;

namespace ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {

            RFIDService o = new RFIDService();

            ModuleInfo mi = o.getModuleInfo(new string[] { "123", "" });

            Console.ReadKey();
        }
    }
}
