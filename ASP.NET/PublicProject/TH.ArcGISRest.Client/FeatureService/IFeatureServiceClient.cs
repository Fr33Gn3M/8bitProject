// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.FeatureService;
using TH.ArcGISRest.ApiImports.FeatureBase;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public interface IFeatureServiceClient : ICommunicationObject
	{
		object Query(int layerId, AgsQueryFeatureLayerParams @params);
		AddResults AddFeatures(int layerId, EsriFeature[] features);
		UpdateResults UpdateFeatures(int layerId, EsriFeature[] features);
		EditResults ApplyEdits(int layerId, AgsApplyEditsParams @params);
		DeleteResultsBase DeleteFeatures(int layerId, AgsDeleteFeaturesParams @params);
		FeatureItem GetFeature(int layerId, int featureId);
		FeatureServiceLayerInfo GetLayerDescription(int layerId);
		FeatureServiceInfo GetServiceDescription();
		string GetServiceName();
        List<FeatureServiceLayerInfo> GetLayersDescription();
		WebRequestContext BuildQueryRequest(int layerId, AgsQueryFeatureLayerParams @params);
	}
	
}
