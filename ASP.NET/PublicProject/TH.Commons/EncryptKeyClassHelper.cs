using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TH.Commons
{
    public class EncryptKeyClassHelper
    {

        public static string IsVerifySuccess(string code, string path,ref DateTime? effectiveTime)
        {
            if (!File.Exists(path))
                return "无密钥，不可用！";
            var arr = File.ReadAllBytes(path);
            var str = ASCIIEncoding.ASCII.GetString(arr);
            //定义这个byte[]数组的长度 为文件的length          
            //把fs文件读入到arrFile数组中，0是指偏移量，从0开始读，arrFile.length是指需要读的长度，也就是整个文件的长度  
            var reslut = Decrypt(code, str, CommonMembers.PublicKey,ref effectiveTime);
            if (string.IsNullOrEmpty(reslut))
                UpdateEncryptInfo(DateTime.Now, path);
            return reslut;
        }

        public static void GenerateKey(int p_currentBitStrength, out string publicAndPrivateKeys, out string justPublicKey)
        {
            using (RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider(p_currentBitStrength))
            {
                string privatekey = oRSA.ToXmlString(true);//私钥 
                string publickey = oRSA.ToXmlString(false);//公钥 
                publicAndPrivateKeys = "<BitStrength>" + p_currentBitStrength.ToString() + "</BitStrength>" + privatekey;
                justPublicKey = "<BitStrength>" + p_currentBitStrength.ToString() + "</BitStrength>" + publickey;
            }
        }
 
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="p_inputString">需要加密的字符串信息</param>
        /// <param name="p_strKeyPath">加密用的密钥所在的路径(*.cyh_publickey)</param>
        /// <returns>加密以后的字符串信息</returns>
        public static string Encrypt(DateTime? effectiveTime,string p_inputString, string fileString)
        {
            string outString = null;
            if (fileString != null)
            {
                string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                fileString = fileString.Replace(bitStrengthString, "");
                int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
                try
                {
                    outString = EncryptString(p_inputString, bitStrength, fileString);
                    if (effectiveTime != null)
                    {
                        string time = effectiveTime.Value.ToString();
                        string time1 = DateTime.Now.ToString();
                        var encryptStr =TH.Commons.Encrypt.EncryptDES(time);
                        var encryptStr1 =TH.Commons.Encrypt.EncryptDES(time1);
                        outString = encryptStr + "&&" + outString + "&&" + encryptStr1;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }

            return outString;
        }

        public static void UpdateEncryptInfo(DateTime currTime, string path)
        {
            var arr1 = File.ReadAllBytes(path);
            var p_inputString = ASCIIEncoding.ASCII.GetString(arr1);
            var arr = p_inputString.Split("&&");
            if (arr.Length == 3)
            {
                string time1 = currTime.ToString();
                var encryptStr1 =TH.Commons.Encrypt.EncryptDES(time1);
                string outString = arr[0] + "&&" + arr[1] + "&&" + encryptStr1;
                arr1 = ASCIIEncoding.ASCII.GetBytes(outString);
                File.WriteAllBytes(path, arr1);
            }
        }

        ///// <summary>
        ///// 解密
        ///// </summary>
        ///// <param name="p_inputString">需要解密的字符串信息</param>
        ///// <param name="p_strKeyPath">解密用的密钥所在的路径(*.cyh_primarykey)</param>
        ///// <returns>解密以后的字符串信息</returns>
        //public static string Decrypt(string p_inputString, string fileString)
        //{
        //    string outString = null;
        //    if (fileString != null)
        //    {
        //        string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
        //        fileString = fileString.Replace(bitStrengthString, "");
        //        int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
        //        try
        //        {
        //            outString = DecryptString(p_inputString, bitStrength, fileString);
        //        }
        //        catch (Exception Ex)
        //        {
        //            throw Ex;
        //        }

        //    }

        //    return outString;

        //}

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="code">字符串信息</param>
        /// <param name="p_inputString">需要解密的字符串信息</param>
        /// <param name="p_strKeyPath">解密用的密钥所在的路径(*.cyh_primarykey)</param>
        /// <returns>解密以后的字符串信息</returns>
        public static string Decrypt(string mathineNo, string p_inputString, string fileString,ref DateTime? effectiveTime)
        {
           string  str = "亲，无密钥！请联系管理员！";
            if (fileString != null)
            {
                str = string.Empty;
                string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                fileString = fileString.Replace(bitStrengthString, "");
                int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
                try
                {
                    var arr = p_inputString.Split("&&");
                    if (arr.Length == 1)
                    {
                        bool result = DecryptString(mathineNo, p_inputString, bitStrength, fileString);
                        if (result == false)
                            str = "亲，密钥不可用！请联系管理员！";
                    }
                    else
                    {
                        if (arr.Length == 3)
                        {
                            var time1Str =TH.Commons.Encrypt.DecryptDES(arr[0]);
                            var time2Str =TH.Commons.Encrypt.DecryptDES(arr[2]);
                             effectiveTime = DateTime.Parse(time1Str);
                            var activeTime = DateTime.Parse(time2Str);
                            var inputStr = arr[1];
                            if (DateTime.Now > effectiveTime) 
                                str = "亲，您的密钥过期！请联系管理员！";
                            if (DateTime.Now < activeTime)
                                str = "亲，您的系统已经崩溃！请联系管理员！";
                            if (string.IsNullOrEmpty(str))
                            {
                                bool result = DecryptString(mathineNo, inputStr, bitStrength, fileString);
                                if (result == false)
                                    str = "亲，密钥不可用！请联系管理员！";
                            }
                        }
                        else
                            str = "亲，密钥不可用！请联系管理员！";
                    }

                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            return str;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="p_inputString">需要加密的字符串</param>
        /// <param name="p_dwKeySize">密钥的大小</param>
        /// <param name="p_xmlString">包含密钥的XML文本信息</param>
        /// <returns>加密后的文本信息</returns>
        private static string EncryptString(string p_inputString, int p_dwKeySize, string p_xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(p_dwKeySize))
            {
                rsa.FromXmlString(p_xmlString);
                // 加密对象 
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                //hash后的数据只能通过密钥解密(为了保证数据的安全，可以用对称加密加密一下数据)
                byte[] source = System.Text.ASCIIEncoding.ASCII.GetBytes(p_inputString);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                string s = Convert.ToBase64String(result);
                byte[] b = f.CreateSignature(result);
                var str = Convert.ToBase64String(b);
                stringBuilder.Append(str);
            }
            return stringBuilder.ToString();
        }

        ///// <summary>
        ///// 解密
        ///// </summary>
        ///// <param name="p_inputString">需要解密的字符串信息</param>
        ///// <param name="p_dwKeySize">密钥的大小</param>
        ///// <param name="p_xmlString">包含密钥的文本信息</param>
        ///// <returns>解密后的文本信息</returns>
        //private static string DecryptString(string inputString, int dwKeySize, string xmlString)
        //{
        //    RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
        //    rsaCryptoServiceProvider.FromXmlString(xmlString);
        //    int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
        //    int iterations = inputString.Length / base64BlockSize;
        //    ArrayList arrayList = new ArrayList();
        //    for (int i = 0; i < iterations; i++)
        //    {
        //        byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
        //        Array.Reverse(encryptedBytes);
        //        var arr = rsaCryptoServiceProvider.Decrypt(encryptedBytes, true);
        //        arrayList.AddRange(arr);
        //    }
        //    return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        //}
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="p_inputString">需要解密的字符串信息</param>
        /// <param name="p_dwKeySize">密钥的大小</param>
        /// <param name="p_xmlString">包含密钥的文本信息</param>
        /// <returns>解密后的文本信息</returns>
        private static bool DecryptString(string mathineNo, string inputString, int dwKeySize, string xmlString)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(dwKeySize))
            {
                rsa.FromXmlString(xmlString);
                RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] key = Convert.FromBase64String(inputString);
                SHA1Managed sha = new SHA1Managed();
                byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(mathineNo));
                string s = Convert.ToBase64String(name);
                if (f.VerifySignature(name, key))
                    return true;
                else
                    return false;
            }
        }

      

    }
}
