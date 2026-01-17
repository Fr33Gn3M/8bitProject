using System;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace LX.Commons.BizUtils.SM3
{
    /// <summary>
    /// SM3工具类
    /// </summary>
    public static class SM3Util
    {
        public static string Sign(string data)
        {
            byte[] msg1 = Encoding.UTF8.GetBytes(data);
            //byte[] key1 = Encoding.Default.GetBytes(secretKey);

            //var keyParameter = new KeyParameter(key1);
            var sm3 = new SM3Digest();

            //HMac mac = new HMac(sm3); // 带密钥的杂凑算法
            //mac.Init(keyParameter);
            sm3.BlockUpdate(msg1, 0, msg1.Length);
            // byte[] result = new byte[sm3.GetMacSize()];
            byte[] result = new byte[sm3.GetDigestSize()];
            sm3.DoFinal(result, 0);
            return BitConverter.ToString(result, 0).Replace("-", string.Empty).ToUpper();
            //return Encoding.ASCII.GetString(Hex.Encode(result));
        }
    }
}
