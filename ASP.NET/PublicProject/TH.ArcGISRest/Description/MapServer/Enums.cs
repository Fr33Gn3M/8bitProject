using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace TH.ArcGISRest.Description.MapServer
{
	[Flags()]
	public enum MapServiceCapability
	{
		None = 0,
		Map = 1,
        Query = 2,
        Data =4
	}
}
