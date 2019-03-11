using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TH.ArcGISRest.ApiImports.GeometryService
{
	[Serializable()]
    [JsonObject("AreaUnit")]
    public class AreaUnitClass
	{
		[JsonProperty("areaUnit")]
		[JsonConverter(typeof(StringEnumConverter))]
		public EsriAreaUnit AreaUnit { get; set; }
	}
}
