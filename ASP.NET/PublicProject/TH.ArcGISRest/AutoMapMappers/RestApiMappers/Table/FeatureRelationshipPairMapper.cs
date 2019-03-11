using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Table;
using TH.ArcGISRest.ApiImports.FeatureBase;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Table
{
	[ReflectionProfileMapper()]
	public class FeatureRelationshipPairMapper : ITypeConverter<FeatureRelationshipPair, RelationshipPair>
	{

		public RelationshipPair Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureRelationshipPair sourceValue = context.SourceValue as FeatureRelationshipPair; 
			var  targetValue = new RelationshipPair();
			var _with1 = targetValue;
			_with1.ID = sourceValue.ID;
			_with1.Name = sourceValue.Name;
			_with1.RelatedTableId = sourceValue.RelatedTableId;
			return targetValue;
		}
	}
}
