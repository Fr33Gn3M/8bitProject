using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsFeatureTemplate = TH.ArcGISRest.ApiImports.FeatureService.FeatureTemplate;
using DespFeatureTemplate = TH.ArcGISRest.Description.Table.FeatureTemplate;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTemplatesMapper : CollectionMapperBase<AgsFeatureTemplate, DespFeatureTemplate>
	{
	}
}