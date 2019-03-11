// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Web;
using RestSharp.Contrib;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class MapServiceClientProfile : IMyRestClientProfile
	{
		
		public const string ProfileName = "MapServiceProfile";
		
		private readonly IResourceHandler _defaultResourceHandle;
		private readonly IList<IResourceHandler> _resourceHandlers;
		
		public MapServiceClientProfile()
		{
			_defaultResourceHandle = new ResourceHandler();
			_defaultResourceHandle.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			_defaultResourceHandle.DefaultParamSerializer = new JsonValueSerializer();
			_defaultResourceHandle.Method = HttpRequestMethod.AutoGetPost;
			_defaultResourceHandle.ResponseDeserializer = new JsonDeserializer();
			_resourceHandlers = new List<IResourceHandler> (){new ExportMapResourceHandler (), new FindResourceHandler (), new QueryLayerResourceHandler()};
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
			if (request.Method != RestSharp.Method.GET)
			{
				return ;
			}
			var whereParam = request.Parameters.Find(p => p.Name == "where");
			if (whereParam != null)
			{
				whereParam.Value = HttpUtility.UrlEncode((string) whereParam.Value);
			}
		}
	}
	
}
