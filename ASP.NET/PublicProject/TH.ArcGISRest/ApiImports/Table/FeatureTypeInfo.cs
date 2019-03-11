using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.JsonConverters;
using TH.ArcGISRest.ApiImports.Domains;

namespace TH.ArcGISRest.ApiImports.FeatureService
{
	[Serializable()]
	[JsonObject()]
	public class FeatureTypeInfo
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("domains")]
		[JsonConverter(typeof(DomainCollectionConverter))]
		public DomainBase[] Domains { get; set; }
		[JsonProperty("templates")]
		public FeatureTemplate[] Templates { get; set; }
	}
}
