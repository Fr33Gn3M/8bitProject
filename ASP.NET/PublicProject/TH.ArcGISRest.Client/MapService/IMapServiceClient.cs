// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.MapServices;
using TH.ArcGISRest.ApiImports.FeatureBase;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public interface IMapServiceClient : ICommunicationObject
	{
		
		object ExportMap(AgsExportMapParams @params);
		object Find(AgsFindParams @params);
		IList<MapServiceLayerTableInfo> GetLayersDescription();
		MapServiceLayerTableInfo GetLayerDescription(int layerId);
		MapServiceInfo GetServiceDescription();
        object QueryLayer(int layerId, AgsQueryMapLayerParams @params);
		FeatureItem GetLayerFeature(int layerId, int featureId);
		byte[] GetMapTile(int level, int row, int column);
		string GetServiceName();
        object Identify(AgsIdentifyParams @params);
#region Asyn Pattern
		WebRequestContext BuildFindRequest(AgsFindParams @params);
		WebRequestContext BuildQueryLayerRequest(int layerId, AgsQueryMapLayerParams @params);
#endregion
	}
	
}
