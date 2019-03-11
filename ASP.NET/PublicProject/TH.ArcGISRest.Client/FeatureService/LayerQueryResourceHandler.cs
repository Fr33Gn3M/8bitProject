// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Text;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.FeatureBase;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	internal class LayerQueryResourceHandler : ResourceHandler
	{
		
		private const string ServiceResource = "query";
		
		public LayerQueryResourceHandler()
		{
			this.Method = HttpRequestMethod.AutoGetPost;
			this.ResourceMatcher = new RegexStringMatcher(string.Format("\\d+/{0}", ServiceResource), true);
			this.ResponseDeserializer = new LayerQueryResultDeserializer();
			this.ParamSerializers.Add(QueryParameterNames.ObjectIds, new PrimvateListSerializer());
			this.ParamSerializers.Add(QueryParameterNames.Geometry, new GeometrySerializer());
			this.ParamSerializers.Add(QueryParameterNames.InSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(QueryParameterNames.OutSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(QueryParameterNames.OutFields, new FieldsFilterSerializer());
		}
		
		
		private class LayerQueryResultDeserializer : IDeserializer
		{
			public T DeserializeObject<T>(byte[] content)
			{
				var returnIdsOnly = false;
				if (RequestParams.ContainsKey(QueryParameterNames.ReturnIdsOnly))
				{
					returnIdsOnly = bool.Parse(RequestParams[QueryParameterNames.ReturnIdsOnly]);
				}
				var strContent = Encoding.UTF8.GetString(content);
				if (returnIdsOnly)
				{
					object result = JsonConvert.DeserializeObject<FeatureIdSet>(strContent);
					return (T)result;
				}
				else
				{
					object result = JsonConvert.DeserializeObject<FeatureSet>(strContent);
					return (T)result;
				}
			}
			
			public System.Collections.Generic.IDictionary<string, string> RequestParams {get; set;}
			
			
		}
		
		private class QueryParameterNames
		{
			public const string ObjectIds = "objectIds";
			public const string Where = "where";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
			public const string RelationParam = "relationParam";
			public const string Time = "time";
			public const string OutFields = "outFields";
			public const string ReturnGeometry = "returnGeometry";
			public const string OutSR = "outSR";
			public const string ReturnIdsOnly = "returnIdsOnly";
		}
	}
	
}
