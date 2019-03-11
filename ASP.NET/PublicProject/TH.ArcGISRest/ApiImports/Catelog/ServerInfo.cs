using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports
{
	[Serializable()]
	[JsonObject()]
	public class ServerInfo
	{
		public ServerInfo()
		{
			AuthInfo = new AuthInfo { TokenBasedSecurity = false };
		}
		[JsonProperty("currentVersion")]
		public string CurrentVersionString { get; set; }
		[JsonProperty("soapURL")]
		public string SoapUrl { get; set; }
		[JsonProperty("secureSoapURL")]
		public string SecureSoapUrl { get; set; }
		[JsonProperty("authInfo")]
		public AuthInfo AuthInfo { get; set; }
        [JsonProperty("services")]
        public serviceObj[] Services { get; set; }

        public class serviceObj
        {
            public string name { get; set; }
            public string type { get; set; }
            public object serviceDetail { get; set; }
            public string layerList { get; set; }
        }
	}
    
}
