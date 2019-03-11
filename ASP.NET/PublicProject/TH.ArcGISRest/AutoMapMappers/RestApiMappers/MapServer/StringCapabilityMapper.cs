using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.MapServer;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.MapServer
{
	[ReflectionProfileMapper()]
	public class StringCapabilityMapper : StringFlagEnumConverter<MapServiceCapability>
	{
	}
}
