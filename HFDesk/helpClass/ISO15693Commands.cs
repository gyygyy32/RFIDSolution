using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HFDesk
{
    public class ISO15693Commands
    {
        [DllImport("mwrf32.dll", EntryPoint = "rf_inventory", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_inventory(
            int icdev, 
            byte flags, 
            byte AFI, 
            UInt16 maskLength, 
            out UInt16 rtn_length, 
            [MarshalAs(UnmanagedType.LPArray)]byte[] rtn_data
            );


        [DllImport("mwrf32.dll", EntryPoint = "rf_stay_quiet", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_stay_quiet(
            int icdev, 
            byte flags, 
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );


        [DllImport("mwrf32.dll", EntryPoint = "rf_select_uid", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_select_uid(
            int icdev,
            byte flags,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );


        [DllImport("mwrf32.dll", EntryPoint = "rf_reset_to_ready", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_reset_to_ready(
            int icdev,
            byte flags,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );


        [DllImport("mwrf32.dll", EntryPoint = "rf_readblock", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_readblock(
            int icdev,
            byte flags,
            byte startBlock,
            byte blockNum,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid,
            out byte rtn_length,
            [MarshalAs(UnmanagedType.LPArray)]byte[] rtn_data
            );



        [DllImport("mwrf32.dll", EntryPoint = "rf_writeblock", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_writeblock(
           int icdev,
            byte flags,
            byte startBlock,
            byte blockNum,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid,
            byte datalength,
            [MarshalAs(UnmanagedType.LPArray)]byte[] writeData
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_lock_block", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_lock_block(
            int icdev,
            byte flags,
            UInt16 blockIndex,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_write_afi", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write_afi(
            int icdev,
            byte flags,
            byte AFI,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_lock_afi", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_lock_afi(
            int icdev,
            byte flags,
            byte AFI,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_write_dsfid", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write_dsfid(
            int icdev,
            byte flags,
            byte DSFID,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_lock_dsfid", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_lock_dsfid(
            int icdev,
            byte flags,
            byte DSFID,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_get_systeminfo", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_get_systeminfo(
            int icdev,
            byte flags,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid,
            out UInt16 rtn_length,
            [MarshalAs(UnmanagedType.LPArray)]byte[] rtn_data
            );

        [DllImport("mwrf32.dll", EntryPoint = "rf_get_securityinfo", SetLastError = true,
           CharSet = CharSet.Auto, ExactSpelling = false,
           CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_get_securityinfo(
            int icdev,
            byte flags,
            UInt16 startBlock,
            UInt16 blockNum,
            [MarshalAs(UnmanagedType.LPArray)]byte[] uid,
            out UInt16 rtn_length,
            [MarshalAs(UnmanagedType.LPArray)]byte[] rtn_data
            );
    }
}
