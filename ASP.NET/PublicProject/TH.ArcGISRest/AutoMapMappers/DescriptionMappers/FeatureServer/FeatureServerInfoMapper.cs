using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.ApiImports.FeatureService;
using TH.ArcGISRest.Description.FeatureServer;
using AutoMapper;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.FeatureServer
{
	[ReflectionProfileMapper()]
	public class FeatureServerInfoMapper : ITypeConverter<FeatureServerInfo, FeatureServiceInfo>
	{

		public FeatureServiceInfo Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            FeatureServerInfo sourceValue = context.SourceValue as FeatureServerInfo;
			var  targetValue = new FeatureServiceInfo();
			var _with1 = targetValue;
			_with1.Layers = Mapper.Map<IdNamePair[]>(sourceValue.LayerIndexs);
			_with1.ServiceDescription = sourceValue.ServiceDescription;
			_with1.Tables = Mapper.Map<IdNamePair[]>(sourceValue.TableIndexs);
			return targetValue;
		}
	}
}
