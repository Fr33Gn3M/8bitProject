using cn.jpush.api.push.mode;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TH.JpushManager
{
    public class PublicModel
    {
    }
    public enum PlatformType
    {
        android,
        ios,
        winphone,
        android_ios,
        android_winphone,
        ios_winphone,
        all
    }
    public class CusPushModel
    {
        public string MessContent { get; set; }
        public string PlatformType { get; set; }
        public string Alert { get; set; }
        public string Title { get; set; }
        public string[] Audience { get; set; }
    }
}