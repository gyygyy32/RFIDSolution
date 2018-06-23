using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Management;

namespace HFDesk.helpClass
{
    class MD5Handler
    {
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string hashOfInput)
        {
            // Hash the input.
            //string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            //use cpu id as hash
            if (0 == comparer.Compare(hashOfInput, getCpuIDhash()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static string getCpuIDhash()
        {
            string str = string.Empty;
            ManagementClass mcCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection mocCpu = mcCpu.GetInstances();
            foreach (ManagementObject m in mocCpu)
            {
                str = m["Processorid"].ToString().Trim();
                break;
            }

            return getMd5Hash("15" + str.Substring(str.Length - 5) + "0817");
        }
    }
}
