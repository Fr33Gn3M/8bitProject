using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TH.ArcGISServiceManager
{
    public class SdeConnectFactory
    {
        private static Dictionary<string, SdeConnect> dic = new Dictionary<string, SdeConnect>();

        public static SdeConnect CreateSdeConnect(SdeConfigInfo sdeConfigInfo)
        {
            //SdeConnect.InitializeEngineLicense();
            if (dic.ContainsKey(sdeConfigInfo.SDEServer))
            {
                SdeEditClass.DeleteLock(sdeConfigInfo);
                return dic[sdeConfigInfo.SDEServer];
            }
            else
            {
                var m_SdeConnect = new SdeConnect(sdeConfigInfo);
                dic.Add(sdeConfigInfo.SDEServer, m_SdeConnect);
                return m_SdeConnect;
            }
        }

       
    }
}