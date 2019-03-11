using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureBase;
using TH.ArcGISRest.Description.Table;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Table
{
	[ReflectionProfileMapper()]
	public class RelationshipPairsMapper : CollectionMapperBase<RelationshipPair, FeatureRelationshipPair>
	{
	}
}
