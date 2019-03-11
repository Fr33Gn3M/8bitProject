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
	public class Field
	{
		[DataMember()]
		public string Name { get; set; }
		[DataMember()]
		public EsriFieldType Type { get; set; }
		[DataMember()]
		public string AliasName { get; set; }
		[DataMember()]
		public bool Editable { get; set; }
		[DataMember()]
		public int? Length { get; set; }
		[DataMember()]
		public DomainBase Domain { get; set; }
	}
}
