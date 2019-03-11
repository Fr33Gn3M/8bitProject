using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace TH.ArcGISRest.ApiImports
{
	[Serializable()]
	public class FieldsFilter
	{
		public IList<string> FieldNames { get; set; }
		public bool AllFields { get; set; }
	}
}
