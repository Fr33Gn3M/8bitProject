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
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.Geometry;
using PH.ServerFramework.WebClientPoint;
using System.Net;


namespace TH.ArcGISRest.Client
{
	internal class MapServiceClient : IMapServiceClient
	{
		
		private readonly Uri _mapServiceUrl;
		private readonly string _profileName;
		
		private string GetProfileName()
		{
			return _profileName;
		}
		
		public MapServiceClient(Uri mapServiceUrl, string profileName = null)
		{
			_mapServiceUrl = mapServiceUrl;
			_profileName = profileName;
		}

        public object ExportMap(AgsExportMapParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				if (@params.OutputAsImage)
				{
					restClient.Params.Add(ExportMapParameterNames.F, "image");
				}
				else
				{
					restClient.Params.Add(ExportMapParameterNames.F, "json");
				}
				restClient.Params.Add(ExportMapParameterNames.BBox, @params.Bbox);
				if (@params.Size != null)
				{
					restClient.Params.Add(ExportMapParameterNames.Size, @params.Size);
				}
				if (@params.Layers != null)
				{
					restClient.Params.Add(ExportMapParameterNames.Layers, @params.Layers);
				}
				if (@params.Transparent)
				{
					restClient.Params.Add(ExportMapParameterNames.Transparent, @params.Transparent);
				}
				var result = restClient.Request<object>(ServiceResources.ExportMap);
				return result;
			}
			
		}
		
		private void ApplyFindRestClient(MyRestClient restClient, AgsFindParams @params)
		{
			if (!string.IsNullOrEmpty(@params.SearchText))
			{
				restClient.Params.Add(FindParameterNames.SearchText, @params.SearchText);
			}
			if (!@params.Contains) //default is true
			{
				restClient.Params.Add(FindParameterNames.Contains, @params.Contains);
			}
			if (@params.Layers != null && @params.Layers.Any())
			{
				restClient.Params.Add(FindParameterNames.Layers, @params.Layers);
			}
			if (!@params.ReturnGeometry) //default is true
			{
				restClient.Params.Add(FindParameterNames.ReturnGeometry, @params.ReturnGeometry);
			}
			if (@params.MaxAllowableOffset != null)
			{
				restClient.Params.Add(FindParameterNames.MaxAllowableOffset, @params.MaxAllowableOffset.Value);
			}
			if (@params.Sr != null)
			{
				restClient.Params.Add(FindParameterNames.Sr, @params.Sr);
			}
		}
		
        //public FindFeatureResults Find(AgsFindParams @params)
        //{
        //    using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
        //    {
        //        ApplyFindRestClient(restClient, @params);
        //        var result = restClient.Request<FindFeatureResults>(ServiceResources.Find);
        //        return result;
        //    }
        //}
		
		public WebRequestContext BuildFindRequest(AgsFindParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				ApplyFindRestClient(restClient, @params);
				var wq = restClient.BuildRequest(ServiceResources.Find);
				return wq;
			}
			
		}
		
		public MapServiceLayerTableInfo GetLayerDescription(int layerId)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				var result = restClient.Request<MapServiceLayerTableInfo>(layerId.ToString());
				return result;
			}
			
		}
		
		public FeatureItem GetLayerFeature(int layerId, int featureId)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				var result = restClient.Request<FeatureItem>(string.Format("{0}/{1}", layerId, featureId));
				return result;
			}
			
		}
		
		public System.Collections.Generic.IList<MapServiceLayerTableInfo> GetLayersDescription()
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				var result = restClient.Request<AllLayersTablesInfo>(ServiceResources.AllLayersAndTables);
				var ls = new List<MapServiceLayerTableInfo>();
				ls.AddRange(result.Layers);
				ls.AddRange(result.Tables);
				return ls.AsReadOnly();
			}
			
		}
		
		public byte[] GetMapTile(int level, int row, int column)
		{
            using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
            {
                var webReqCtx = restClient.BuildRequest(string.Format("{0}/{1}/{2}/{3}", ServiceResources.MapTile, level, row, column));
                try
                {
                    using (var wc = new WebClient())
                        return wc.DownloadData(webReqCtx.BuiltUrl);
                }
                catch 
                {
                    return null;
                }
            }
		}
		
		public MapServiceInfo GetServiceDescription()
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				var result = restClient.Request<MapServiceInfo>(string.Empty);
				return result;
			}
			
		}
		
		public string GetServiceName()
		{
			var parts = _mapServiceUrl.ToString().Split('/');
			return parts[parts.Length - 2];
		}
		
		private void ApplyQueryLayerRequestResetClient(MyRestClient restClient, int layerId, AgsQueryMapLayerParams @params)
		{
			if (!string.IsNullOrEmpty(@params.Text))
			{
				restClient.Params.Add(QueryParameterNames.Text, @params.Text);
			}
			if (@params.Geometry != null)
			{
				var geoType = GeometryTypeConverter.GetGeometryType(@params.Geometry);
				var geo = @params.Geometry;
				geo.SpatialReference = null;
				restClient.Params.Add(QueryParameterNames.Geometry, geo);
				restClient.Params.Add(QueryParameterNames.GeometryType, geoType.ToString());
			}
			if (@params.SpatialRel != EsriSpatialRelationship.esriSpatialRelIntersects)
			{
				restClient.Params.Add(QueryParameterNames.SpatialRel, @params.SpatialRel);
			}
			if (!string.IsNullOrEmpty(@params.RelationParam))
			{
				restClient.Params.Add(QueryParameterNames.RelationParam, @params.RelationParam);
			}
			if (!string.IsNullOrEmpty(@params.Where))
			{
				restClient.Params.Add(QueryParameterNames.Where, @params.Where);
			}
			if (@params.ObjectIds != null && @params.ObjectIds.Any())
			{
				restClient.Params.Add(QueryParameterNames.ObjectIds, @params.ObjectIds);
			}
			if (@params.OutFields != null)
			{
				restClient.Params.Add(QueryParameterNames.OutFields, @params.OutFields);
			}
			restClient.Params.Add(QueryParameterNames.ReturnGeometry, @params.ReturnGeometry);
			if (@params.MaxAllowableOffset != null)
			{
				restClient.Params.Add(QueryParameterNames.MaxAllowableOffset, @params.MaxAllowableOffset.Value);
			}
			restClient.Params.Add(QueryParameterNames.ReturnIdsOnly, @params.ReturnIdsOnly);
		}
		
		public dynamic QueryLayer(int layerId, AgsQueryMapLayerParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				ApplyQueryLayerRequestResetClient(restClient, layerId, @params);
				object result = null;
				if (@params.ReturnIdsOnly)
				{
					result = restClient.Request<FeatureIdSet>(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				}
				else
				{
					result = restClient.Request<FeatureSet>(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				}
				return result;
			}
			
		}
		
		public WebRequestContext BuildQueryLayerRequest(int layerId, AgsQueryMapLayerParams @params)
		{
			using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
			{
				ApplyQueryLayerRequestResetClient(restClient, layerId, @params);
				var wq = restClient.BuildRequest(string.Format("{0}/{1}", layerId, ServiceResources.Query));
				return wq;
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
				return _mapServiceUrl;
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
		
		private class QueryParameterNames
		{
			public const string Text = "text";
			public const string Geometry = "geometry";
			public const string GeometryType = "geometryType";
			public const string InSR = "inSR";
			public const string SpatialRel = "spatialRel";
			public const string RelationParam = "relationParam";
			public const string Where = "where";
			public const string ObjectIds = "objectIds";
			public const string Time = "time";
			public const string OutFields = "outFields";
			public const string ReturnGeometry = "returnGeometry";
			public const string MaxAllowableOffset = "maxAllowableOffset";
			public const string OutSR = "outSR";
			public const string ReturnIdsOnly = "returnIdsOnly";
		}
		
		private class ExportMapParameterNames
		{
			public const string BBox = "bbox";
			public const string Size = "size";
			public const string Dpi = "dpi";
			public const string ImageSR = "imageSR";
			public const string BBoxSR = "bboxSR";
			public const string Format = "format";
			public const string LayerDefs = "layerDefs";
			public const string Layers = "layers";
			public const string Transparent = "transparent";
			public const string Time = "time";
			public const string LayerTimeOptions = "layerTimeOptions";
			public const string F = "f";
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
		
		private class ServiceResources
		{
			public const string ExportMap = "export";
			public const string Identify = "identify";
			public const string Find = "find";
			public const string GenerateKml = "generateKml";
			public const string MapTile = "tile";
			public const string Query = "query";
			public const string QueryRelatedRecords = "queryRelatedRecords";
			public const string Attachments = "attachments";
			public const string HtmlPopup = "htmlPopup";
			public const string Image = "images";
			public const string AllLayersAndTables = "layers";
			public const string KMLImage = "kml/mapImage.kmz";
			public const string ServiceExtension = "exts";
		}

        private class IdentifyParameterNames
        {
            public const string Geometry = "geometry";
            public const string GeometryType = "geometryType";
            public const string Sr = "sr";
            public const string LayerDefs = "layerDefs";
            public const string Time = "time";
            public const string Layers = "layers";
            public const string Tolerance = "tolerance";
            public const string MapExtent = "mapExtent";
            public const string ImageDisplay = "imageDisplay";
            public const string ReturnGeometry = "returnGeometry";
            public const string MaxAllowableOffset = "maxAllowableOffset";
        }

		public TimeSpan Timeout {get; set;}


        public object Identify(AgsIdentifyParams @params)
        {
            using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
            {
                if (@params.Time != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.Time, @params.Time);
                }
                if (@params.MapExtent != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.MapExtent, @params.MapExtent);
                }
                if (@params.SR != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.Sr, @params.SR);
                }
                if (@params.LayerDefs != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.LayerDefs, @params.LayerDefs);
                }
                if (@params.Layers != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.Layers, @params.Layers);
                }
                if (@params.Geometry != null)
                {
                    dynamic geoType = GeometryTypeConverter.GetGeometryType(@params.Geometry);
                    dynamic geo = @params.Geometry;
                    geo.SpatialReference = null;
                    restClient.Params.Add(QueryParameterNames.Geometry, geo);
                    restClient.Params.Add(QueryParameterNames.GeometryType, geoType.ToString);
                }
                if (@params.Tolerance != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.Tolerance, @params.Tolerance);
                }
                if (@params.ImageDisplay != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.ImageDisplay, @params.ImageDisplay);
                }
                if (@params.MaxAllowableOffset != null)
                {
                    restClient.Params.Add(IdentifyParameterNames.MaxAllowableOffset, @params.MaxAllowableOffset.Value);
                }

                restClient.Params.Add(IdentifyParameterNames.ReturnGeometry, @params.ReturnGeometry);
                dynamic result = restClient.RequestToStream(ServiceResources.Identify);
                return result;
            }
        }



        public object Find(AgsFindParams @params)
        {
            using (var restClient = MyRestClientFactory.Create(_mapServiceUrl, GetProfileName()))
            {
                ApplyFindRestClient(restClient, @params);
                dynamic result = restClient.RequestToStream(ServiceResources.Find);
                return result;
            }
        }
	}
	
}
