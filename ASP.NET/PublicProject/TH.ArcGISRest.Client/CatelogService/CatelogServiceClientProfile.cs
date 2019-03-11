// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;
using RestSharp;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class CatelogServiceClientProfile : IMyRestClientProfile
	{
		
		
		private readonly IResourceHandler _defaultResourceHandle;
		private readonly IList<IResourceHandler> _resourceHandlers;
		public const string ProfileName = "CatelogServiceProfile";
		
		public CatelogServiceClientProfile()
		{
			_defaultResourceHandle = new ResourceHandler();
			_defaultResourceHandle.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			_defaultResourceHandle.Method = HttpRequestMethod.Get;
			_defaultResourceHandle.ResponseDeserializer = new JsonDeserializer();
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
		
		public IList<IResourceHandler> ResourceHandlers
		{
			get
			{
				return _resourceHandlers;
			}
		}
		
		public void BeforeExecute(RestRequest request)
		{
			
		}
	}
	
}
