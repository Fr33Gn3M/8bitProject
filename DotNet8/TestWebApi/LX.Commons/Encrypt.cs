using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace LX.Commons
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
            dCSP.Mode = CipherMode.ECB;
            dCSP.Padding = PaddingMode.PKCS7;
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

        public static string DecryptDES(string decryptString, string key)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            DCSP.Mode = CipherMode.ECB;
            DCSP.Padding = PaddingMode.PKCS7;
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
                return Encrypt.DecryptDES(json, LX.Commons.Encrypt.EncryptCode);
            else
                return json;
        }

        public static string EncryptJson(string json, bool isencrypt = false)
        {
            if (string.IsNullOrEmpty(json))
                return json;
            if (isencrypt == true)
                return Encrypt.EncryptDES(json, LX.Commons.Encrypt.EncryptCode);
            else
                return json;
        }
        /// <summary>
        /// 获取Json的Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }


        private const string Key = "@12345678912345!";
        private const string Vector = "@12345678912345!"; //弃用
        private const string AES_IV = "@12345678912345!";


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
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static byte[] AesEncrypt2(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;


            //KeyGenerator kgen = KeyGenerator.getInstance("AES");
            //SecureRandom secureRandom = SecureRandom.getInstance("SHA1PRNG");
            //secureRandom.setSeed(Encoding.ASCII.GetBytes(key));
            //kgen.init(128, secureRandom);
            //SecretKey secretKey = kgen.generateKey();
            //byte[] enCodeFormat = secretKey.getEncoded();


            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return resultArray;
        }

        public static byte[] AesDecrypt(byte[] toDecryptArray, string key)
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            return resultArray;
        }

        private static string Hex_2To16(Byte[] bytes)
        {
            String hexString = String.Empty;
            Int32 iLength = 65535;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                if (bytes.Length < iLength)
                {
                    iLength = bytes.Length;
                }

                for (int i = 0; i < iLength; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
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

        public static string DecryptByAES(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }




        /// <summary>
        /// 3des ecb模式加密
        /// </summary>
        /// <param name="aStrString">待加密的字符串</param>
        /// <param name="aStrKey">密钥</param>
        /// <param name="iv">加密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.CBC, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.GetEncoding("GB2312").GetBytes(aStrKey),
                    Mode = mode
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.GetEncoding("GB2312").GetBytes(iv);
                }
                var desEncrypt = des.CreateEncryptor();
                byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(aStrString);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }




        /// <summary>
        /// des 解密
        /// </summary>
        /// <param name="aStrString">加密的字符串</param>
        /// <param name="aStrKey">密钥</param>
        /// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
        /// <param name="mode">运算模式</param>
        /// <returns>解密的字符串</returns>
        public static string Decrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.GetEncoding("GB2312").GetBytes(aStrKey),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.GetEncoding("GB2312").GetBytes(iv);
                }
                var desDecrypt = des.CreateDecryptor(des.Key, des.IV);
                var result = "";
                byte[] buffer = Convert.FromBase64String(aStrString);
                result = Encoding.GetEncoding("GB2312").GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Md5Encrypt(string text, int codeLength)
        {

            if (!string.IsNullOrEmpty(text))
            {
                // 16位MD5加密（取32位加密的9~25字符）  
                if (codeLength == 16)
                {
                    return GetMD5_16(text).ToLower();
                }

                // 32位加密
                if (codeLength == 32)
                {
                    return GetMD5_32(text);
                }
            }
            return string.Empty;

        }

        public static string GetMD5_32(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        // 16位md5加密
        public static string GetMD5_16(string s)
        {
            MD5 md5 = MD5.Create();
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            string t2 = BitConverter.ToString(t, 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
        public static string Sha1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }



        /// <summary>
        /// 使用BouncyCastle进行AEAD_AES_256_GCM 解密
        /// </summary>
        /// <param name="key">key:32位字符</param>
        /// <param name="nonce">随机串12位</param>
        /// <param name="cipherData">密文(Base64字符)</param>
        /// <param name="associatedData">附加数据可能null</param>
        /// <returns></returns>
        public static string AesGcmDecryptByBouncyCastle(string key, string nonce, string cipherData, string associatedData)
        {
            var associatedBytes = associatedData == null ? null : Encoding.UTF8.GetBytes(associatedData);

            var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(key)),
                128,  //128 = 16 * 8 => (tag size * 8)
                Encoding.UTF8.GetBytes(nonce),
                associatedBytes);
            gcmBlockCipher.Init(false, parameters);

            var data = Convert.FromBase64String(cipherData);
            var plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];

            var length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }

    }
}


