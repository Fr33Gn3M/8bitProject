using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureService;
using TH.ArcGISRest.Description.FeatureServer;
using AutoMapper;
using TH.ArcGISRest.Description;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class FeatureServiceInfoMapper : ITypeConverter<FeatureServiceInfo, FeatureServerInfo>
	{

		public FeatureServerInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureServiceInfo sourceValue = context.SourceValue as FeatureServiceInfo;
			var  targetValue = new FeatureServerInfo();
			var _with1 = targetValue;
			_with1.ServiceDescription = sourceValue.ServiceDescription;
			_with1.LayerIndexs = Mapper.Map<IdNamePair[]>(sourceValue.Layers);
			_with1.TableIndexs = Mapper.Map<IdNamePair[]>(sourceValue.Tables);
			return targetValue;
		}
	}
}
