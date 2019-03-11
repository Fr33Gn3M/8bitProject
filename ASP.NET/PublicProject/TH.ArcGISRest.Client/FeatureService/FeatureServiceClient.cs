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
using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	internal class FeatureServiceClient : IFeatureServiceClient
	{
		
		private readonly Uri _baseUrl;
		private readonly string _profileName;
		
		public FeatureServiceClient(Uri baseUrl, string profileName = null)
		{
			_baseUrl = baseUrl;
			_profileName = profileName;
		}
		
		private string GetProfileName()
		{
			return _profileName;
		}
		
		public AddResults AddFeatures(int layerId, EsriFeature[] features)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(AddUpdateParameterNames.Features, features);
				var result = restClient.Request<AddResults>(string.Format("{0}/{1}", layerId, ServiceResources.AddFeatures));
				return result;
			}
		}
		
		public EditResults ApplyEdits(int layerId, AgsApplyEditsParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				if (@params.Adds != null && @params.Adds.Any())
				{
					restClient.Params.Add(ApplyEditsParameterNames.Adds, @params.Adds);
				}
				if (@params.Updates != null && @params.Updates.Any())
				{
					restClient.Params.Add(ApplyEditsParameterNames.Updates, @params.Updates);
				}
				if (@params.Deletes != null && @params.Deletes.Any())
				{
					restClient.Params.Add(ApplyEditsParameterNames.Deletes, @params.Deletes);
				}
				var result = restClient.Request<EditResults>(string.Format("{0}/{1}", layerId, ServiceResources.ApplyEdits));
				return result;
			}
			
		}
		
		public DeleteResultsBase DeleteFeatures(int layerId, AgsDeleteFeaturesParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				if (@params.Geometry != null)
				{
					var geoType = GeometryTypeConverter.GetGeometryType(@params.Geometry);
					@params.Geometry.SpatialReference = null;
					restClient.Params.Add(DeleteFeaturesParameterNames.Geometry, @params.Geometry);
					restClient.Params.Add(DeleteFeaturesParameterNames.GeometryType, geoType.ToString());
					if (@params.SpatialRel != EsriSpatialRelationship.esriSpatialRelWithin)
					{
						restClient.Params.Add(DeleteFeaturesParameterNames.SpatialRel, @params.SpatialRel.ToString());
					}
				}
				if (!string.IsNullOrEmpty(@params.Where))
				{
					restClient.Params.Add(DeleteFeaturesParameterNames.Where, @params.Where);
				}
				if (@params.ObjectIds != null && @params.ObjectIds.Any())
				{
					restClient.Params.Add(DeleteFeaturesParameterNames.ObjectIds, @params.ObjectIds);
				}
				var result = restClient.Request<DeleteResultsBase>(string.Format("{0}/{1}", layerId, ServiceResources.DeleteFeatures));
				return result;
			}
		}
		
		public FeatureItem GetFeature(int layerId, int featureId)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				var result = restClient.Request<FeatureItem>(string.Format("{0}/{1}", layerId, featureId));
				return result;
			}
			
		}
		
		public FeatureServiceLayerInfo GetLayerDescription(int layerId)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				var result = restClient.Request<FeatureServiceLayerInfo>(layerId.ToString());
				return result;
			}
			
		}

        public List<FeatureServiceLayerInfo> GetLayersDescription()
        {
            using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
            {
                var layerList = new List<FeatureServiceLayerInfo>();
                var service = restClient.Request<FeatureServiceInfo>(string.Empty);
                foreach (var item in service.Layers)
                {
                    var result = restClient.Request<FeatureServiceLayerInfo>(item.ID.ToString());
                    layerList.Add(result);
                }
                return layerList;
            }
        }
		
		public FeatureServiceInfo GetServiceDescription()
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				var result = restClient.Request<FeatureServiceInfo>(string.Empty);
				return result;
			}
			
		}
		
		public string GetServiceName()
		{
			var parts = _baseUrl.ToString().Split('/');
			return parts[parts.Length - 2];
		}

        private void ApplyQueryRestClient(MyRestClient restClient, int layerId, AgsQueryFeatureLayerParams @params)
        {
            if (@params.ObjectIds != null && @params.ObjectIds.Any())
            {
                restClient.Params.Add(QueryParameterNames.ObjectIds, @params.ObjectIds);
            }
            if (@params.Geometry != null)
            {
                var geo = @params.Geometry;
                var geoType = GeometryTypeConverter.GetGeometryType(geo);
                restClient.Params.Add(QueryParameterNames.Geometry, geo);
                restClient.Params.Add(QueryParameterNames.GeometryType, geoType.ToString());
            }
            if (!string.IsNullOrEmpty(@params.Where))
            {
                restClient.Params.Add(QueryParameterNames.Where, @params.Where);
            }
            if (@params.SpatialRel != EsriSpatialRelationship.esriSpatialRelWithin)
            {
                restClient.Params.Add(QueryParameterNames.SpatialRel, @params.SpatialRel.ToString());
            }
            if (!string.IsNullOrEmpty(@params.RelationParam))
            {
                restClient.Params.Add(QueryParameterNames.RelationParam, @params.RelationParam);
            }
            if (@params.OutSR != null)
            {
                restClient.Params.Add(QueryParameterNames.OutSR, @params.OutSR);
            }
            if (@params.InSR != null)
            {
                restClient.Params.Add(QueryParameterNames.InSR, @params.InSR);
            }
            if (@params.OutFields != null)
            {
                restClient.Params.Add(QueryParameterNames.OutFields, @params.OutFields);
            }
            restClient.Params.Add(QueryParameterNames.ReturnGeometry, @params.ReturnGeometry);
            if (@params.ReturnIdsOnly == true)
                restClient.Params.Add(QueryParameterNames.ReturnIdsOnly, @params.ReturnIdsOnly);
        }
		
		public object Query(int layerId, AgsQueryFeatureLayerParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				ApplyQueryRestClient(restClient, layerId, @params);
                var result = restClient.Request(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				//var result2 = restClient.Request<object>(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				return result;
			}
		}
		
		public WebRequestContext BuildQueryRequest(int layerId, AgsQueryFeatureLayerParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				ApplyQueryRestClient(restClient, layerId, @params);
				var wq = restClient.BuildRequest(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				return wq;
			}
			
		}
		
		public UpdateResults UpdateFeatures(int layerId, EsriFeature[] features)
		{
			using (var restClient = MyRestClientFactory.Create(_baseUrl, GetProfileName()))
			{
				restClient.Params.Add(AddUpdateParameterNames.Features, features);
				var result = restClient.Request<UpdateResults>(string.Format("{0}/{1}", layerId, ServiceResources.UpdateFeatures));
				return result;
			}
			
		}
		
		public void Close()
		{
		}
		
		public void Open()
		{
		}
		
		public Uri Via
		{
			get
			{
				return _baseUrl;
			}
			set
			{
				throw (new NotSupportedException());
			}
		}
		
#region IDisposable Support
		private bool disposedValue; // To detect redundant calls
		
		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}
				
				// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
				// TODO: set large fields to null.
			}
			this.disposedValue = true;
		}
		
		// TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
		//Protected Overrides Sub Finalize()
		//    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		//    Dispose(False)
		//    MyBase.Finalize()
		//End Sub
		
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
#endregion
		
		private class ServiceResources
		{
			public const string Query = "query";
			public const string QueryRelatedRecords = "queryRelatedRecords";
			public const string AddFeatures = "addFeatures";
			public const string UpdateFeatures = "updateFeatures";
			public const string DeleteFeatures = "deleteFeatures";
			public const string ApplyEdits = "applyEdits";
			public const string AddAttachment = "addAttachment";
			public const string UpdateAttachment = "updateAttachment";
			public const string DeleteAttachments = "deleteAttachments";
			public const string Attachments = "attachments";
			public const string HtmlPopup = "htmlPopup";
			public const string Images = "images";
		}
		
		private class DeleteFeaturesParameterNames
		{
			public const string ObjectIds = "objectIds";
			public const string Where = "where";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
		}
		
		private class AddUpdateParameterNames
		{
			public const string Features = "features";
		}
		
		private class ApplyEditsParameterNames
		{
			public const string Adds = "adds";
			public const string Updates = "updates";
			public const string Deletes = "deletes";
		}
		
		private class QueryParameterNames
		{
			public const string ObjectIds = "objectIds";
			public const string Where = "where";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
			public const string RelationParam = "relationParam";
			public const string Time = "time";
			public const string OutFields = "outFields";
			public const string ReturnGeometry = "returnGeometry";
			public const string OutSR = "outSR";
			public const string ReturnIdsOnly = "returnIdsOnly";
		}
		
		
		public TimeSpan Timeout {get; set;}
	}
	
}
