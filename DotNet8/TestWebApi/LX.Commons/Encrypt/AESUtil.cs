using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LX.Commons
{
    public class EncryptModel
    {
        public bool isSercet { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string sign { get; set; }
    }

    public abstract class AESUtil
    {
        private const string DEFAULT_AES_KEY = "Qztc&20230322###Qztc&20230322###";//32位 
        private const string DEFAULT_AES_IV = "2022031800000508";//16位 
        private const string Key = "@12345678912345!";
        private const string AES_IV = "@12345678912345!";


        private static Dictionary<string, EncryptModel> EncryptModelInfos = new Dictionary<string, EncryptModel>();

        public static void SetDecryptParam(string acticeName, string app_id, string timestamp, string sign, bool isSercet)
        {
            try
            {
                if (EncryptModelInfos.ContainsKey(acticeName) == false)
                    EncryptModelInfos[acticeName] = new EncryptModel() { appId = app_id, isSercet = isSercet, timestamp = timestamp, sign = sign };
                else
                    EncryptModelInfos.Add(acticeName, new EncryptModel() { appId = app_id, isSercet = isSercet, timestamp = timestamp, sign = sign });
            }
            catch (Exception)
            {

            }
        }

        public static string DecryptByAES2(string input, string acticeName)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            if (EncryptModelInfos.ContainsKey(acticeName) == false)
                return input;
            else
            {
                var enModel = EncryptModelInfos[acticeName];
                if (enModel.isSercet == true)
                {
                    return DecryptByAES(input);
                }
                else
                    return input;
            }
        }

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="input">明文字符串</param>  
        /// <returns>字符串</returns>  
        public static string EncryptByAES(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Mode = CipherMode.CBC;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.FeedbackSize = 128;

                rijndaelManaged.Key = Encoding.UTF8.GetBytes(Key);
                rijndaelManaged.IV = Encoding.UTF8.GetBytes(AES_IV);

                ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(bytes);
                    }
                }
            }
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="input">密文字节数组</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static string DecryptByAES(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input == "null")
            {
                return null;
            }
            var buffer = Convert.FromBase64String(input);
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Mode = CipherMode.CBC;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.FeedbackSize = 128;

                rijndaelManaged.Key = Encoding.UTF8.GetBytes(Key);
                rijndaelManaged.IV = Encoding.UTF8.GetBytes(AES_IV);

                ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream msEncrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="input">明文字符串</param>  
        /// <param name="key">密钥（32位）</param>  
        /// <returns>字符串</returns>  
        public static string AESEncrypt(string input, string iv)
        {
            if (input == null) return input;

            byte[] keyBytes = Encoding.UTF8.GetBytes(DEFAULT_AES_KEY);
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        //return Encoding.UTF8.GetString(bytes);
                        return ByteArrayToHexString(bytes);
                        //return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 使用系统默认的key和iv进行加密
        /// </summary>
        /// <param name="input">待加密的string</param>
        /// <returns>加密后的string</returns>
        public static string AESEncrypt(string input)
        {
            return AESEncrypt(input, DEFAULT_AES_IV.Substring(0, 16));
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="input">密文字节数组</param>  
        /// <param name="key">密钥（32位）</param>  
        /// <returns>返回解密后的字符串</returns>
        public static string AESDecrypt(string input, string iv)
        {
            if (input == null) return input;
            //byte[] inputBytes = Convert.FromBase64String(input);
            byte[] inputBytes = HexStringToByteArray(input);
            //byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(DEFAULT_AES_KEY);
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 使用系统默认的key和iv进行解密
        /// </summary>
        /// <param name="input">待解密的string</param>
        /// <returns>解密后的string</returns>
        public static string AESDecrypt(string input)
        {
            return AESDecrypt(input, DEFAULT_AES_IV.Substring(0, 16));
        }

        /// <summary>
        /// 将指定的16进制字符串转换为byte数组
        /// </summary>
        /// <param name="s">16进制字符串(如：“7F 2C 4A”或“7F2C4A”都可以)</param>
        /// <returns>16进制字符串对应的byte数组</returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 将一个byte数组转换成一个格式化的16进制字符串
        /// </summary>
        /// <param name="data">byte数组</param>
        /// <returns>格式化的16进制字符串</returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                //16进制数字
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
                //16进制数字之间以空格隔开
                //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();
        }
    }
}

