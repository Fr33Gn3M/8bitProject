// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class GeometryServiceClientProfile : IMyRestClientProfile
	{
		
		
		public const string ProfileName = "GeometryServiceProfile";
		
		private IResourceHandler _defaultResourceHandle;
		private IList<IResourceHandler> _resourceHandlers;
		
		
		public GeometryServiceClientProfile()
		{
			_defaultResourceHandle = new ResourceHandler();
			_defaultResourceHandle.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			_defaultResourceHandle.Method = HttpRequestMethod.AutoGetPost;
			_defaultResourceHandle.ResponseDeserializer = new JsonDeserializer();
			_defaultResourceHandle.DefaultParamSerializer = new GeometryServiceSerializer();
			_resourceHandlers = new List<IResourceHandler>();
		}
		
		public IResourceHandler DefaultResourceHandle
		{
			get
			{
				return _defaultResourceHandle;
			}
		}
		
		public string Name
		{
			get
			{
				return ProfileName;
			}
		}
		
		public System.Collections.Generic.IList<IResourceHandler> ResourceHandlers
		{
			get
			{
				return _resourceHandlers;
			}
		}
		
		public void BeforeExecute(RestSharp.RestRequest request)
		{
			
		}
	}
	
}
