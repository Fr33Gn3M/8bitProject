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
	public class CatelogFolderItem
	{
		[JsonProperty("currentVersion")]
		public string CurrentVersionString { get; set; }
		[JsonProperty("folders")]
		public string[] FolderNames { get; set; }
		[JsonProperty("services")]
		public NameTypePair[] ServiceIndexs { get; set; }
	}
}
