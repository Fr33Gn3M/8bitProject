using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Table
{
	[KnownType(typeof(FeatureDoubleValue))]
	[KnownType(typeof(FeatureStringValue))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public class FeatureValueBase
	{

		protected FeatureValueBase()
		{
		}
	}
}
