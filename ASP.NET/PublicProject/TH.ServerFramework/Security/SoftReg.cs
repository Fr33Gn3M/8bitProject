using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.IO;    //需要引用System.Management.dll

namespace TH.ServerFramework
{


    public class Regmutou
    {
        // Methods
        private static string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string str = "";
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    str = obj2["SerialNumber"].ToString().Trim();
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetComputerbit(string softname)
        {
            string cpuID = GetCpuID();
            string bIOSSerialNumber = GetBIOSSerialNumber();
            string hardDiskSerialNumber = GetHardDiskSerialNumber();
            string netCardMACAddress = GetNetCardMACAddress();
            StringBuilder builder = new StringBuilder();
            MD5 md = new MD5CryptoServiceProvider();
            var softname2 = BitConverter.ToString(md.ComputeHash(Encoding.Default.GetBytes(softname))).Replace("-", "").ToUpper().Substring(8, 0x10);
            builder.Append(softname2);
            if (cpuID != "")
            {
                cpuID = BitConverter.ToString(md.ComputeHash(Encoding.Default.GetBytes(cpuID))).Replace("-", "").ToUpper().Substring(8, 0x10);
                builder.Append("C" + cpuID);
                //return (softname + "C" + cpuID);
            }
            if (bIOSSerialNumber != "")
            {
                bIOSSerialNumber = BitConverter.ToString(md.ComputeHash(Encoding.Default.GetBytes(bIOSSerialNumber))).Replace("-", "").ToUpper().Substring(8, 0x10);
                builder.Append("B" + bIOSSerialNumber);
                //return (softname + "B" + bIOSSerialNumber);
            }

            if (hardDiskSerialNumber != "")
            {
                hardDiskSerialNumber = BitConverter.ToString(md.ComputeHash(Encoding.Default.GetBytes(hardDiskSerialNumber))).Replace("-", "").ToUpper().Substring(8, 0x10);
                builder.Append("H" + hardDiskSerialNumber);
                //return (softname + "H" + hardDiskSerialNumber);
            }
            //if (netCardMACAddress != "")
            //{
            //    netCardMACAddress = BitConverter.ToString(md.ComputeHash(Encoding.Default.GetBytes(netCardMACAddress))).Replace("-", "").ToUpper().Substring(8, 0x10);
            //    builder.Append("N" + netCardMACAddress);
            //    //return (softname + "N" + netCardMACAddress);
            //}
            return builder.ToString();
            //return (softname + "WF53A419DB238BBAD");
        }

        private static string GetCpuID()
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                string str = null;
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        private static string GetHardDiskSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string str = "";
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    str = obj2["SerialNumber"].ToString().Trim();
                    break;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        private static string GetNetCardMACAddress()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
                string str = "";
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    str = obj2["MACAddress"].ToString().Trim();
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        //private static bool regmutousoft(string computerbit, string softname, string filename)
        //{
        //    bool flag = true;
        //    string str = "";
        //    SHA1 sha = new SHA1CryptoServiceProvider();
        //    str = BitConverter.ToString(sha.ComputeHash(Encoding.Default.GetBytes(softname))).Replace("-", "").ToUpper();
        //    //if (!File.Exists(filename))
        //    //{
        //    //  flag = false;
        //    //  Stream stream = File.Open(filename, FileMode.OpenOrCreate);
        //    //  StreamWriter writer = new StreamWriter(stream);
        //    //  writer.WriteLine(computerbit);
        //    //  writer.Close();
        //    //  stream.Close();
        //    //  return flag;
        //    //}
        //    //Stream stream2 = File.Open(filename, FileMode.Open);
        //    //StreamReader reader = new StreamReader(stream2);
        //    //string str2 = "";
        //    string str3 = computerbit;
        //    //while ((str2 = reader.ReadLine()) != null)
        //    //{
        //    //  str3 = str2;
        //    //}
        //    //reader.Close();
        //    //stream2.Close();
        //    //if (str3.Length != 0x18)
        //    //{
        //    //  //return false;
        //    //}
        //    SHA1 sha2 = new SHA1CryptoServiceProvider();
        //    string str4 = BitConverter.ToString(sha2.ComputeHash(Encoding.Default.GetBytes(computerbit))).Replace("-", "").ToUpper();
        //    string str5 = "";
        //    for (int i = 0; i < str4.Length; i++)
        //    {
        //        if ((i % 2) == 1)
        //        {
        //            str5 = str5 + str4[i];
        //        }
        //    }
        //    string str6 = "";
        //    for (int j = 0; j < str.Length; j++)
        //    {
        //        if ((j % 2) == 0)
        //        {
        //            str6 = str6 + str[j];
        //        }
        //    }
        //    int[] numArray = new int[20];
        //    for (int k = 0; k < 20; k++)
        //    {
        //        numArray[k] = str6[k] + str5[k];
        //        numArray[k] = numArray[k] % 0x24;
        //    }
        //    string str7 = "";
        //    for (int m = 0; m < 20; m++)
        //    {
        //        if ((m > 0) && ((m % 4) == 0))
        //        {
        //            str7 = str7 + "-";
        //        }
        //        str7 = str7 + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[numArray[m]];
        //    }
        //    if (str3 == str7)
        //    {
        //        flag = true;
        //    }
        //    Console.WriteLine("机器码:" + str3);
        //    Console.WriteLine("注册码:" + str7);
        //    return flag;
        //}
    }





    public class SoftReg
    {
        ///<summary>
        /// 获取硬盘卷标号
        ///</summary>
        ///<returns></returns>
        private static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        ///<summary>
        /// 获取CPU序列号
        ///</summary>
        ///<returns></returns>
        private static string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuCollection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
            }
            return strCpu;
        }

        ///<summary>
        /// 生成机器码
        ///</summary>
        ///<returns></returns>
        public static string GetMNum()
        {
            string strMNum = null;
            string keyPath = Path.Combine(System.Environment.CurrentDirectory, CommonMembers.MachineCodeFileName);
            if (System.IO.File.Exists(keyPath))
            {
                var text = File.ReadAllText(keyPath);
                if (!string.IsNullOrWhiteSpace(text))
                    strMNum = text;
            }
            if (string.IsNullOrWhiteSpace(strMNum))
            {
                string strNum = GetCpu() + GetDiskVolumeSerialNumber();
                strMNum = strNum.Substring(0, 24);//截取前24位作为机器码
                using (StreamWriter sw = new StreamWriter(keyPath))
                {
                    sw.Write(strMNum);
                    sw.Close();
                }
            }
            return strMNum;
        }

        private static int[] intCode = new int[127];    //存储密钥
        private static char[] charCode = new char[25];  //存储ASCII码
        private static int[] intNumber = new int[25];   //存储ASCII码值

        //初始化密钥
        private static void SetIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        ///<summary>
        /// 生成注册码
        ///</summary>
        ///<returns></returns>
        public static string GetRNum()
        {
            SetIntCode();
            string strMNum = GetMNum();
            for (int i = 1; i < charCode.Length; i++)   //存储机器码
            {
                charCode[i] = Convert.ToChar(strMNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)  //改变ASCII码值
            {
                intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
            }
            string strAsciiName = "";   //注册码
            for (int k = 1; k < intNumber.Length; k++)  //生成注册码
            {

                if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k]
                    <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                }
                else if (intNumber[k] > 122)  //判断如果大于z
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                }
                else
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                }
            }
            return strAsciiName;
        }



    }
}