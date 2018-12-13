using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ArcGIsLib.ApiImports.Geometry
{
	[Serializable()]
	[JsonObject()]
	public class AgsSpatialReference
	{
		[JsonProperty("wkid")]
		public int WKID { get; set; }
		[JsonProperty("wkt", NullValueHandling = NullValueHandling.Ignore)]
		public string WKT { get; set; }

		public static AgsSpatialReference FromString(string strSr)
		{
			throw new NotSupportedException();
		}
	}
}
