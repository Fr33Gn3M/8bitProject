using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.MapServer
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class MapLayerIndex
	{
		[DataMember()]
		public int ID { get; set; }
		[DataMember()]
		public string Name { get; set; }
		[DataMember()]
		public int? ParentLayerId { get; set; }
		[DataMember()]
		public bool DefaultVisibility { get; set; }
		[DataMember()]
		public int[] SubLayerIds { get; set; }
	}
}
