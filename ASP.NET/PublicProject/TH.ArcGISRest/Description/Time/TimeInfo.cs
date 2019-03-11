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
	public class TimeInfo
	{
		[DataMember()]
		public string StartTimeField { get; set; }
		[DataMember()]
		public string EndTimeField { get; set; }
		[DataMember()]
		public string TrackIdField { get; set; }
		[DataMember()]
		public TimeExtent TimeExtent { get; set; }
		[DataMember()]
		public TimeReference TimeReference { get; set; }
		[DataMember()]
		public double TimeInterval { get; set; }
		[DataMember()]
		public EsriTimeUnit TimeIntervalUnits { get; set; }
		[DataMember()]
		public ExportOptions ExportOptions { get; set; }
	}
}
