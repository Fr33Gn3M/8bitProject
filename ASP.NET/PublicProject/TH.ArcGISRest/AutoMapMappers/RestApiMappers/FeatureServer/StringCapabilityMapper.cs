using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.FeatureServer;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class StringCapabilityMapper : StringFlagEnumConverter<FeatureServiceCapability>
	{

	}
}
