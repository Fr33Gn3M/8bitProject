// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	internal class FindResourceHandler : ResourceHandler
	{
		
		private const string ServiceResource = "find";
		
		public FindResourceHandler()
		{
			this.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			this.DefaultParamSerializer = new PrimvateValueSerializer();
			this.Method = HttpRequestMethod.Get;
			this.ParamSerializers.Add(FindParameterNames.Sr, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(FindParameterNames.Layers, new PrimvateListSerializer());
			this.ResourceMatcher = new EqualsStringMatcher(ServiceResource, true);
			this.ResponseDeserializer = new JsonDeserializer();
		}
		
		private class FindParameterNames
		{
			public const string SearchText = "searchText";
			public const string Contains = "contains";
			public const string SearchFields = "searchFields";
			public const string Sr = "sr";
			public const string LayerDefs = "layerDefs";
			public const string Layers = "layers";
			public const string ReturnGeometry = "returnGeometry";
			public const string MaxAllowableOffset = "maxAllowableOffset";
		}
	}
	
}
