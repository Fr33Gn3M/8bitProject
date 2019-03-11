using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	[JsonObject()]
	public class FeatureServiceInfo
	{
		public FeatureServiceInfo()
		{
			Layers =new IdNamePair[]  {
				
			};
            Tables = new IdNamePair[]{
				
			};
		}
		[JsonProperty("serviceDescription")]
		public string ServiceDescription { get; set; }
		[JsonProperty("layers")]
		public IdNamePair[] Layers { get; set; }
		[JsonProperty("tables")]
		public IdNamePair[] Tables { get; set; }
	}
}
