using System;
using System.Runtime.InteropServices;

namespace HFDesk
{
	/// <summary>
	/// mifareone 的摘要说明。
	/// </summary>
	public class mifareone
	{
		public mifareone()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		[DllImport("mwrf32.dll", EntryPoint="rf_request",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_request(int icdev, int mode, out UInt16 tagtype);
		
		[DllImport("mwrf32.dll", EntryPoint="rf_request_std",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_request_std(int icdev, int mode, out UInt16 tagtype);

		[DllImport("mwrf32.dll", EntryPoint="rf_anticoll",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_anticoll(int icdev, int bcnt,out uint snr);

		[DllImport("mwrf32.dll", EntryPoint="rf_select",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_select(int icdev, uint snr,out byte size);

		[DllImport("mwrf32.dll", EntryPoint="rf_authentication",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_authentication(int icdev, int mode,int secnr);

		[DllImport("mwrf32.dll", EntryPoint="rf_authentication_2",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_authentication_2(int icdev, int mode,int keynr,int blocknr);

		[DllImport("mwrf32.dll", EntryPoint="rf_read",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_read(int icdev,int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("mwrf32.dll", EntryPoint="rf_read_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_read_hex(int icdev,int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("mwrf32.dll", EntryPoint="rf_write_hex",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_write_hex(int icdev,int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("mwrf32.dll", EntryPoint="rf_write",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

		[DllImport("mwrf32.dll", EntryPoint="rf_halt",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_halt(int icdev);

		[DllImport("mwrf32.dll", EntryPoint="rf_initval",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_initval(int icdev,int blocknr, uint val);

		[DllImport("mwrf32.dll", EntryPoint="rf_readval",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_readval(int icdev,int blocknr, out uint val);

		[DllImport("mwrf32.dll", EntryPoint="rf_increment",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_increment(int icdev,int blocknr, uint val);

		[DllImport("mwrf32.dll", EntryPoint="rf_decrement",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_decrement(int icdev,int blocknr,uint val);

		[DllImport("mwrf32.dll", EntryPoint="rf_restore",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_restore(int icdev,int blocknr);

		[DllImport("mwrf32.dll", EntryPoint="rf_transfer",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_transfer(int icdev,int blocknr);

		[DllImport("mwrf32.dll", EntryPoint="rf_reset",  SetLastError=true,
			 CharSet=CharSet.Auto, ExactSpelling=false,
			 CallingConvention=CallingConvention.StdCall)]
			//说明：     返回设备当前状态
		public static extern Int16 rf_reset(int icdev,int msec);

	}
}
