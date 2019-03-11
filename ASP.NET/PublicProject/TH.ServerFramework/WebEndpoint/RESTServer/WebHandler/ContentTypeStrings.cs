namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler
{
    using Microsoft.VisualBasic;
    using System;
    using System.Runtime.InteropServices;

    public class ContentTypeStrings
    {
        public static string Image(string detail)
        {
                if (string.IsNullOrEmpty(detail))
                {
                    return "image";
                }
                return string.Format("image/{0}", detail);
        }

        public static string Text(string detail)
        {
                if (string.IsNullOrEmpty(detail))
                {
                    return "text";
                }
                return string.Format("text/{0}", detail);
        }
    }
}

