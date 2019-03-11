using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TH.Commons
{
    public class Encrypt
    {
        public static string EncryptCode = "THTH1212";
        private static string _EncryptKey = "BBA991A6-9A98-4708-AF96-FF1101AD3143";
        //默认密钥向量 
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //private static string DCBKey = "thth1212";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString)
        {
            //byte[] rgbKey = Encoding.UTF8.GetBytes(_EncryptKey.Substring(0, 8));
            //byte[] rgbIV = Keys;
            //byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            //DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            //cStream.Write(inputByteArray, 0, inputByteArray.Length);
            //cStream.FlushFinalBlock();
            //return Convert.ToBase64String(mStream.ToArray());
            return EncryptDES(encryptString, _EncryptKey.Substring(0, 8));
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string key)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString)
        {
           return DecryptDES(decryptString, _EncryptKey.Substring(0, 8));
        }

        public static string DecryptDES(string decryptString,string key)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        public static byte[] DecryptDES(byte[] inputByteArray, string key)
        {
           byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
           byte[] rgbIV = Keys;
           DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
           using (MemoryStream mStream = new MemoryStream())
           {
               CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
               cStream.Write(inputByteArray, 0, inputByteArray.Length);
               cStream.FlushFinalBlock();
               return mStream.ToArray();
           }
        }

        public static byte[] EncryptDES(byte[] inputByteArray, string key)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = Keys;
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            {
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return mStream.ToArray();
            }
        }

        public static string DecryptJson(string json, bool isencrypt = false)
        {
            if (string.IsNullOrEmpty(json))
                return json;
            if (isencrypt == true)
                return Encrypt.DecryptDES(json, TH.Commons.Encrypt.EncryptCode);
            else
                return json;
        }

        public static string EncryptJson(string json, bool isencrypt = false)
        {
            if (string.IsNullOrEmpty(json))
                return json;
            if (isencrypt == true)
                return Encrypt.EncryptDES(json, TH.Commons.Encrypt.EncryptCode);
            else
                return json;
        }
    }

}
