using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace LX.Commons.BizUtils
{
    public class SignUtils
    {
        /// <summary>
        /// 通过SHA256_HMAC生成加密串
        /// </summary>
        /// <param name="paramDic">参数字典</param>
        /// <param name="key">secret_key</param>
        /// <returns></returns>
        public static string GenerateSignBySHA256HMAC(Dictionary<string, object> paramDic, string key)
        {
            return GenerateSignatureBySHA256HMAC(paramDic, key);
        }
        /// <summary>
        /// 通过SHA256_HMAC生成加密串
        /// </summary>
        /// <param name="paramDic">参数字典</param>
        /// <param name="key">secret_key</param>
        /// <returns></returns>
        private static string GenerateSignatureBySHA256HMAC(Dictionary<string, object> paramDic, string key)
        {
            var paramSortedDic = new SortedDictionary<string, object>(paramDic);
            var paramString = string.Join("&", paramSortedDic.Select(pattern => pattern.Key + "=" + pattern.Value));


            return SHA256_HMAC(paramString, key);
        }
        /// <summary>
        /// SHA256_HMAC加密算法
        /// </summary>
        /// <param name="message">加密串</param>
        /// <param name="secret">secret_key</param>
        /// <returns></returns>
        public static string SHA256_HMAC(string message, string secret, string type = null)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return ByteArrayToHexString(hashmessage);
            }
        }

        public static string SHA256_HMAC_Base64(string message, string secret, string type = null)
        {
            secret = secret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        /// <summary>
        /// 通过SHA256生成加密串
        /// </summary>
        /// <param name="paramDic">参数字典</param>
        /// <returns></returns>
        public static string GenerateSignBySHA256(Dictionary<string, object> paramDic)
        {
            var paramSortedDic = new SortedDictionary<string, object>(paramDic);
            var paramString = string.Join("&", paramSortedDic.Select(pattern => pattern.Key + "=" + pattern.Value));

            return SHA256EncryptString(paramString);
        }

        /// <summary>
        /// SHA256_HMAC加密算法算法
        /// </summary>
        /// <param name="message">加密串</param>
        /// <param name="secret">secret_key</param>
        /// <returns></returns>
        public static string SHA256EncryptString(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        //    public static byte[] hmacSHA256(byte[] data, byte[] key) throws NoSuchAlgorithmException, InvalidKeyException {
        //    String algorithm = "HmacSHA256";
        //    Mac mac = Mac.getInstance(algorithm);
        //    mac.init(new SecretKeySpec(key, algorithm));
        //    return mac.doFinal(data);
        //}


        /// <summary>
        /// 字节数组转16进制小写字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                //{0:X2} 大写
                sb.AppendFormat("{0:x2}", b);
            }
            var hex = sb.ToString().ToLower();
            return hex;
        }

        /// <summary>
        /// SHA256WithRSA加签算法
        /// </summary>
        /// <param name="data">待加签字符串</param>
        /// <param name="privatekey">私钥</param>
        /// <returns></returns>
        public static string RsaSign(string data, string privatekey)
        {
            var netKey = RSAPrivateKeyJava2DotNet(privatekey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(netKey);
            //创建一个空对象
            var rsaClear = new RSACryptoServiceProvider();
            var paras = rsa.ExportParameters(true);
            rsaClear.ImportParameters(paras);
            //签名返回
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var signData = rsa.SignData(Encoding.UTF8.GetBytes(data), sha256);
                return Convert.ToBase64String(signData);
            }
        }

        /// <summary>  
        /// RSA私钥格式转换，java->.net  
        /// </summary>  
        /// <param name="privateKey">java生成的RSA私钥</param>  
        /// <returns></returns>  
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        }
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 生成32位小写md5，跟上面那个方法加密结果有偏差
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string Gen32byteMD5(string txt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(txt);//
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String = byte2String + targetData[i].ToString("x2");
            }

            return byte2String;
        }

        /// <summary>
        /// SHA256WithRSA验签算法
        /// </summary>
        /// <param name="data">待验签字符串</param>
        /// <param name="signData">sign</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool RsaVerifySign(string data, string signData, string publicKey)
        {
            var netKey = RSAPublicKeyJava2DotNet(publicKey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(netKey);
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                return rsa.VerifyData(Encoding.UTF8.GetBytes(data), sha256, Convert.FromBase64String(signData));
            }
        }

        /// <summary>  
        /// RSA公钥格式转换，java->.net  
        /// </summary>  
        /// <param name="publicKey">java生成的公钥</param>  
        /// <returns></returns>  
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }




        public static string GetHmacMd5Str(string data, string sigSecret)
        {
            string result = "";
            try
            {
                byte[] keyByte = Encoding.UTF8.GetBytes(sigSecret);
                byte[] dataByte = Encoding.UTF8.GetBytes(data);
                byte[] hmacMd5Byte = GetHmacMd5Bytes(keyByte, dataByte);
                StringBuilder md5StrBuff = new StringBuilder();
                for (int i = 0; i < hmacMd5Byte.Length; i++)
                {
                    if ((0xFF & hmacMd5Byte[i]).ToString("X").Length == 1)
                        md5StrBuff.Append("0").Append((0xFF & hmacMd5Byte[i]).ToString("X"));
                    else
                        md5StrBuff.Append((0xFF & hmacMd5Byte[i]).ToString("X"));
                }
                result = md5StrBuff.ToString().ToUpper();
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        private static byte[] GetHmacMd5Bytes(byte[] key, byte[] data)
        {
            /*
            * HmacMd5 calculation formula: H(K XOR opad, H(K XOR ipad, text))
            * HmacMd5 计算公式：H(K XOR opad, H(K XOR ipad, text))
            * H代表hash算法，本类中使⽤MD5算法，K代表密钥，text代表要加密的数据 ipad为0x36，opad为0x5C。
            */
            int length = 64;
            byte[] ipad = new byte[length];
            byte[] opad = new byte[length];
            for (int i = 0; i < 64; i++)
            {
                ipad[i] = 0x36;
                opad[i] = 0x5C;
            }
            byte[] actualKey = key; // Actual key.
            byte[] keyArr = new byte[length]; // Key bytes of 64 bytes length
            /*
            * If key's length is longer than 64,then use hash to digest it and
           use
            * the result as actual key. 如果密钥⻓度，⼤于64字节，就使⽤哈希算法，计算其
           摘要，作为真正的密钥。
            */
            if (key.Length > length)
            {
                actualKey = MD5Byte(key);
            }
            for (int i = 0; i < actualKey.Length; i++)
            {
                keyArr[i] = actualKey[i];
            }
            /*
            * append zeros to K 如果密钥⻓度不⾜64字节，就使⽤0x00补⻬到64字节。
            */
            if (actualKey.Length < length)
            {
                for (int i = actualKey.Length; i < keyArr.Length; i++)
                    keyArr[i] = 0x00;
            }
            /*
            * calc K XOR ipad 使⽤密钥和ipad进⾏异或运算。
            */
            byte[] kIpadXorResult = new byte[length];
            for (int i = 0; i < length; i++)
            {
                kIpadXorResult[i] = (byte)(keyArr[i] ^ ipad[i]);
            }
            /*
            * append "text" to the end of "K XOR ipad" 将待加密数据追加到K XOR ipad计
           算结果后⾯。
            */
            byte[] firstAppendResult = new byte[kIpadXorResult.Length +
           data.Length];
            for (int i = 0; i < kIpadXorResult.Length; i++)
            {
                firstAppendResult[i] = kIpadXorResult[i];
            }
            for (int i = 0; i < data.Length; i++)
            {
                firstAppendResult[i + keyArr.Length] = data[i];
            }



            /*
            * calc H(K XOR ipad, text) 使⽤哈希算法计算上⾯结果的摘要。
            */
            byte[] firstHashResult = MD5Byte(firstAppendResult);
            /*
            * calc K XOR opad 使⽤密钥和opad进⾏异或运算。
            */
            byte[] kOpadXorResult = new byte[length];
            for (int i = 0; i < length; i++)
            {
                kOpadXorResult[i] = (byte)(keyArr[i] ^ opad[i]);
            }
            /*
            * append "H(K XOR ipad, text)" to the end of "K XOR opad" 将H(K XOR
            * ipad, text)结果追加到K XOR opad结果后⾯
            */
            byte[] secondAppendResult = new byte[kOpadXorResult.Length
            + firstHashResult.Length];
            for (int i = 0; i < kOpadXorResult.Length; i++)
            {
                secondAppendResult[i] = kOpadXorResult[i];
            }
            for (int i = 0; i < firstHashResult.Length; i++)
            {
                secondAppendResult[i + keyArr.Length] = firstHashResult[i];
            }
            /*
            * H(K XOR opad, H(K XOR ipad, text)) 对上⾯的数据进⾏哈希运算。
            */
            byte[] hmacMd5Bytes = MD5Byte(secondAppendResult);
            return hmacMd5Bytes;
        }

        private static byte[] MD5Byte(byte[] str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(str);
            return output;
        }




        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="value">明⽂</param>
        /// <param name="key">数据秘钥</param>
        /// <param name="iv">初始向量</param>
        /// <param name="mode">运算模式（eg:CipherMode.CBC,CipherMode.ECB等）</param>
        /// <param name="padding">填充模式（eg:PaddingMode.PKCS7）</param>
        /// <returns></returns>
        public static string AesEncrypt(string value, string key, string iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            int feedbackSize = 0;
            if (key == null)
                return null;
            if (key.Length == 16)
            {
                feedbackSize = 128;

            }
            else if (key.Length == 24)
            {
                feedbackSize = 192;
            }
            else if (key.Length == 32)
            {
                feedbackSize = 256;

            }
            else
                return null;

            try
            {
                var _keyByte = Encoding.UTF8.GetBytes(key);
                var _valueByte = Encoding.UTF8.GetBytes(value);
                using (var aes = new RijndaelManaged())
                {
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    aes.FeedbackSize = feedbackSize;
                    aes.Key = _keyByte;
                    aes.Mode = mode;
                    aes.Padding = padding;
                    var cryptoTransform = aes.CreateEncryptor();
                    var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                    var result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                    result = result.Replace("\r", "");
                    result = result.Replace("\n", "");
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <param name="key">数据秘钥</param>
        /// <param name="iv">初始向量</param>
        /// <param name="mode">运算模式（eg:CipherMode.CBC,CipherMode.ECB等）</param>
        /// <param name="padding">填充模式（eg:PaddingMode.PKCS7）</param>
        /// <returns></returns>
        public static string AesDecrypt(string value, string key, string iv, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            if (key == null || key.Length != 16)
                return null;

            try
            {
                var _keyByte = Encoding.UTF8.GetBytes(key);
                var _valueByte = Convert.FromBase64String(value);
                using (var aes = new RijndaelManaged())
                {
                    aes.IV = Encoding.UTF8.GetBytes(iv);
                    aes.Key = _keyByte;
                    aes.Mode = mode;
                    aes.Padding = padding;
                    var cryptoTransform = aes.CreateDecryptor();
                    var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                    return Encoding.UTF8.GetString(resultArray);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="encodeType"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
    }
}
