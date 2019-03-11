using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.FeatureServer;
using TH.ArcGISRest.ApiImports.FeatureService;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class FeatureLayerInfosMapper : CollectionMapperBase<FeatureLayerInfo, FeatureServiceLayerInfo>
	{
	}
}
