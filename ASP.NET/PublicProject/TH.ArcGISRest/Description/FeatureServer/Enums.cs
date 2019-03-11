using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace TH.ArcGISRest.Description.FeatureServer
{
	[Flags()]
	public enum FeatureServiceCapability
	{
		None = 0,
		Query = 1,
		Editing =2
	}
}
