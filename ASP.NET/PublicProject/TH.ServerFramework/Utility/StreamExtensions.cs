namespace TH.ServerFramework.Utility
{
    using Microsoft.VisualBasic;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class StreamExtensions
    {
        public static byte[] ToBytes(this Stream source)
        {
            if (source is MemoryStream)
            {
                return ((MemoryStream) source).ToArray();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                source.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}

