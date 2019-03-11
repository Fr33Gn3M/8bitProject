// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace TH.ArcGISRest.Client
{
	public class ServiceClientFactory
	{
		public static IMapServiceClient CreateMapServiceClient(Uri url)
		{
			var client = new MapServiceClient(url, MapServiceClientProfile.ProfileName);
			return client;
		}
		
		public static IFeatureServiceClient CreateFeatureServiceClient(Uri url)
		{
			var client = new FeatureServiceClient(url, FeatureServiceClientProfile.ProfileName);
			return client;
		}
		
		public static IGeometryServiceClient CreateGeometryServiceClient(Uri url)
		{
			var client = new GeometryServiceClient(url, GeometryServiceClientProfile.ProfileName);
			return client;
		}
		
		public static ICatelogServiceClient CreateCatelogServiceClient(Uri url)
		{
			var client = new CatelogServiceClient(url, CatelogServiceClientProfile.ProfileName);
			return client;
		}
	}
	
}
