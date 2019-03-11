using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Table
{
	[KnownType(typeof(CodedValueDomain))]
	[KnownType(typeof(RangeDomain))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public abstract class DomainBase
	{
		[DataMember()]
		public string Name { get; set; }
	}
}
