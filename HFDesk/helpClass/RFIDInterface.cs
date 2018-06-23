using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HFDesk.helpClass
{
    public interface RFIDInterface
    {
        bool ScanBarcode(out string barcode);

        void Open();

        void Close();

        bool IsTagWrited(ref string tagId);

        byte[] ReadTagBuff();

        string ReadTagID();

        void WriteTagBuff(byte[] buff);
    }
}
