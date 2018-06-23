using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFDesk.consts
{
    /*
     * TID type depends on different chip type, 
     * the most used chip suppliers are Impinj and Alien,
     * Higgs3 belongs to Alien, and Monza serials belone to Impinj.
     * 
     * these tag all have 32 bit TID, and 64 bit Unique TID
     */
    public class UHFTIDTypes
    {
        public const string Alien_Higgs3 = "E2 00 34 12";//Higgs-3 TID, stored in the first 2 word of TID memory bank

        public const string Monza_4E = "E2 80 11 0C";//Monza_4E TID, stored in the first 2 word of TID memory bank
        public const string Monza_4QT = "E2 80 11 05";//Monza_4QT TID, stored in the first 2 word of TID memory bank
        public const string Monza_4D = "E2 80 11 00";//Monza_4D TID, stored in the first 2 word of TID memory bank
        public const string Monza_4i = "E2 80 11 14";//Monza_4i TID, stored in the first 2 word of TID memory bank

    }

    public class UHFUserMemorySizeInWord
    {

        public const byte A_9662 = 0x08;          //128 bit(8 word)

        public const byte Monza_4E =  0x08;             //128 bit(8 word)
        public const byte Monza_4QT = 0x20;            //512 bit(32 word ox20)
        public const byte Monza_4D = 0x02;             //32 bit(2 word)
        public const byte Monza_4i = 0x1E;              //480 bit(30 word 0x1E)

    }

    public class UHFEPCMemorySizeInWord
    {
        public const byte A_9662 = 0x08;          //128 bit(8 word)

        public const byte Monza_4E = 0x1F;             //496 bit(31 word)
        public const byte Monza_4QT = 0x08;            //128 bit(8 word)
        public const byte Monza_4D = 0x08;             //128 bit(8 word)
        public const byte Monza_4i = 0x10;              //256 bit(16 word 0x10)
    
    }
}
