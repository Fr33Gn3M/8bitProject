namespace TH.ServerFramework.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.IO.Compression;
    using System.Data;

    public static class StringExtensions
    {
        public static Stream ToStream(this string source, Encoding ec)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            if (ec == null)
            {
                ec = Encoding.UTF8;
            }
            var bytes = ec.GetBytes(source);
            bytes = GZipUtil.Compress(bytes);
            //var bb = GZipClassHelper.Compress(bytes);
            var ms = new MemoryStream(bytes);
            return ms;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (StreamWriter sw = new StreamWriter(ms, ec))
            //        sw.Write(source);
            //    return ms;
            //}
        }

        public static Stream ToStream2(this string source, Encoding ec)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }
            if (ec == null)
            {
                ec = Encoding.UTF8;
            }
            var bytes = ec.GetBytes(source);
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms, ec))
                    sw.Write(source);
                return ms;
            }
        }

    }

    public static class GZipUtil
    {

        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
     public  static byte[] Compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }


    }

}

