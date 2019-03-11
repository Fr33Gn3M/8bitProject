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
	internal class LayerEditResourceHandler : ResourceHandler
	{
        private const string ServiceResource = "applyEdits";

        public LayerEditResourceHandler()
		{
            this.ResourceMatcher = new RegexStringMatcher(string.Format("\\d+/{0}", ServiceResource), true);
            this.DefaultParamSerializer = new JsonValueSerializer();
			this.Method = HttpRequestMethod.Post;
            //this.ResponseDeserializer = new EditResultDeserializer();
            this.ParamSerializers.Add(DeleteFeaturesParameterNames.Deletes, new PrimvateListSerializer());
		}
		
        //private class EditResultDeserializer : IDeserializer
        //{
			
        //    public T DeserializeObject<T>(byte[] content)
        //    {
        //        var contentStr = Encoding.UTF8.GetString(content);
        //        if (string.IsNullOrWhiteSpace(RequestParams[DeleteFeaturesParameterNames.Deletes]))
        //        {
        //            var str = RequestParams[DeleteFeaturesParameterNames.Deletes];
        //            object result = JsonConvert.DeserializeObject<DeleteResultsOnlyIdsNospecified>(str);
        //            return (T)result;
        //        }
        //        else
        //        {
        //            var str = RequestParams[DeleteFeaturesParameterNames.Deletes];
        //            object result = JsonConvert.DeserializeObject<DeleteResultsOnlyIdsSpecified>(str);
        //            return (T)result;
        //        }
        //    }

        //    public System.Collections.Generic.IDictionary<string, string> RequestParams {get; set;}
        //}
		
		private class DeleteFeaturesParameterNames
		{
            public const string Deletes = "deletes";
		}
	}
	
}
