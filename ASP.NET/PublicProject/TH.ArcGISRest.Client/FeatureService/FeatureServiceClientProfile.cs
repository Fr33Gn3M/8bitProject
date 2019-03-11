// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Web;
using TH.ArcGISRest.ApiImports;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class FeatureServiceClientProfile : IMyRestClientProfile
	{
		
		
		private readonly IResourceHandler _defaultResourceHandle;
		private readonly IList<IResourceHandler> _resourceHandlers;
		public const string ProfileName = "FeatureServiceProfile";

        public FeatureServiceClientProfile()
        {
            _defaultResourceHandle = new ResourceHandler();
            _defaultResourceHandle.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
            _defaultResourceHandle.Method = HttpRequestMethod.Post;
            _defaultResourceHandle.ResponseDeserializer = new JsonDeserializer();
            _defaultResourceHandle.DefaultParamSerializer = new JsonValueSerializer();
            _resourceHandlers = new List<IResourceHandler>();
            var serviceDescriptionHandle = new ResourceHandler { Method = HttpRequestMethod.Get, ResourceMatcher = new EqualsStringMatcher(string.Empty, true) };
            _resourceHandlers.Add(serviceDescriptionHandle);
            var queryResourceHandle = new LayerQueryResourceHandler();
            _resourceHandlers.Add(queryResourceHandle);
            var deleteResourceHandle = new LayerDeleteResourceHandler();
            _resourceHandlers.Add(deleteResourceHandle);
            var editResourceHandle = new LayerEditResourceHandler();
            _resourceHandlers.Add(editResourceHandle);
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
			//If request.Method <> RestSharp.Method.GET Then
			//    Return
			//End If
			//Dim whereParam = request.Parameters.Find(Function(p) p.Name = "where")
			//If whereParam IsNot Nothing Then
			//    whereParam.Value = HttpUtility.UrlEncode(whereParam.Value)
			//End If
		}
	}
	
}
