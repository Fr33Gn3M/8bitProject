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
using TH.ArcGISRest.ApiImports.FeatureService;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	internal class LayerDeleteResourceHandler : ResourceHandler
	{
		private const string ServiceResource = "deleteFeatures";
		
		public LayerDeleteResourceHandler()
		{
            this.ResourceMatcher = new RegexStringMatcher(string.Format("\\d+/{0}", ServiceResource), true);
            this.DefaultParamSerializer = new JsonValueSerializer();
			this.Method = HttpRequestMethod.Post;
			this.ResponseDeserializer = new DeleteFeaturesResultDeserializer();
			this.ParamSerializers.Add(DeleteFeaturesParameterNames.Geometry, new GeometrySerializer());
			this.ParamSerializers.Add(DeleteFeaturesParameterNames.InSR, new SpatialReferenceSerializer());
            this.ParamSerializers.Add(DeleteFeaturesParameterNames.ObjectIds, new PrimvateListSerializer());
		}
		
		private class DeleteFeaturesResultDeserializer : IDeserializer
		{
			
			public T DeserializeObject<T>(byte[] content)
			{
				var contentStr = Encoding.UTF8.GetString(content);
				if (string.IsNullOrWhiteSpace(RequestParams[DeleteFeaturesParameterNames.ObjectIds]))
				{
					object result = JsonConvert.DeserializeObject<DeleteResultsOnlyIdsNospecified>(contentStr);
					return (T)result;
				}
				else
				{
					object result = JsonConvert.DeserializeObject<DeleteResultsOnlyIdsSpecified>(contentStr);
                    return (T)result;
				}
			}
			
			public System.Collections.Generic.IDictionary<string, string> RequestParams {get; set;}
		}
		
		private class DeleteFeaturesParameterNames
		{
            public const string Deletes = "deletes";
            public const string ObjectIds = "objectIds";
			public const string Where = "where";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
		}
	}
	
}
