using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Table
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class FeatureTypeInfo
	{
		[DataMember()]
		public string Id { get; set; }
		[DataMember()]
		public string Name { get; set; }
		[DataMember()]
		public DomainBase[] Domains { get; set; }
		[DataMember()]
		public FeatureTemplate[] Templates { get; set; }
	}
}