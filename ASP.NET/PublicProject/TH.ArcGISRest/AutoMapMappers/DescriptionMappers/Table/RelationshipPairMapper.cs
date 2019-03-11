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
	public class RelationshipPairMapper : ITypeConverter<RelationshipPair, FeatureRelationshipPair>
	{

		public FeatureRelationshipPair Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            RelationshipPair sourceValue = context.SourceValue as RelationshipPair;
			var  targetValue = new FeatureRelationshipPair();
			var _with1 = targetValue;
			_with1.ID = sourceValue.ID;
			_with1.Name = sourceValue.Name;
			_with1.RelatedTableId = sourceValue.RelatedTableId;
			return targetValue;
		}
	}
}
