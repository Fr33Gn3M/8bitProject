using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Time
{
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class ExportOptions
	{
		[DataMember()]
		public bool UseTime { get; set; }
		[DataMember()]
		public bool TimeDataCumulative { get; set; }
		[DataMember()]
		public double TimeOffset { get; set; }
		[DataMember()]
		public EsriTimeUnit TimeoffsetUnits { get; set; }
	}
}
