using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DespFeatureTemplate = TH.ArcGISRest.Description.Table.FeatureTemplate;
using AgsFeatureTemplate = TH.ArcGISRest.ApiImports.FeatureService.FeatureTemplate;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureTemplatesMapper : CollectionMapperBase<DespFeatureTemplate, AgsFeatureTemplate>
	{
	}
}
