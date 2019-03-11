// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.JsonConverters;
using Newtonsoft.Json;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	internal class QueryLayerResourceHandler : ResourceHandler
	{
		
		private const string ServiceResource = "query";
		
		public QueryLayerResourceHandler()
		{
			this.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			this.DefaultParamSerializer = new JsonValueSerializer();
			this.Method = HttpRequestMethod.AutoGetPost;
			this.ParamSerializers.Add(QueryParameterNames.Geometry, new GeometrySerializer());
			this.ParamSerializers.Add(QueryParameterNames.InSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(QueryParameterNames.ObjectIds, new PrimvateListSerializer());
			this.ParamSerializers.Add(QueryParameterNames.OutFields, new FieldsFilterSerializer());
			this.ParamSerializers.Add(QueryParameterNames.OutSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(QueryParameterNames.SpatialRel, new EnumSerializer());
			this.ResourceMatcher = new RegexStringMatcher(string.Format("\\d+/{0}", ServiceResource), true);
			this.ResponseDeserializer = new JsonDeserializer(ConverterFactory);
		}
		
		private static JsonConverter[] ConverterFactory(JsonDeserializer deserializer)
		{
			var ls = new List<JsonConverter>();
			var returnIdsOnly = false;
			if (deserializer.RequestParams.ContainsKey(QueryParameterNames.ReturnIdsOnly))
			{
				var strReturnIdsOnly = deserializer.RequestParams[QueryParameterNames.ReturnIdsOnly];
				returnIdsOnly = bool.Parse(strReturnIdsOnly);
			}
			if (!returnIdsOnly)
			{
				ls.Add(new FeatureSetConverter());
			}
			return ls.ToArray();
		}
		
		private class QueryParameterNames
		{
			public const string Text = "text";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
			public const string RelationParam = "relationParam";
			public const string Where = "where";
			public const string ObjectIds = "objectIds";
			public const string Time = "time";
			public const string OutFields = "outFields";
			public const string ReturnGeometry = "returnGeometry";
			public const string MaxAllowableOffset = "maxAllowableOffset";
			public const string OutSR = "outSR";
			public const string ReturnIdsOnly = "returnIdsOnly";
		}
	}
	
}
