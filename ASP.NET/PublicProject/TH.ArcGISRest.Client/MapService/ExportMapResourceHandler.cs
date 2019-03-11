// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest.ApiImports.MapServices;
using PH.ServerFramework.WebClientPoint;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	internal class ExportMapResourceHandler : ResourceHandler
	{
		
		private const string ServiceResource = "export";
		
		public ExportMapResourceHandler()
		{
			this.DefaultParams.Add(AgsGlobalParams.JsonOutput.Key, AgsGlobalParams.JsonOutput.Value);
			this.DefaultParamSerializer = new JsonValueSerializer();
			this.Method = HttpRequestMethod.AutoGetPost;
			this.ParamSerializers.Add(ExportMapParameterNames.BBox, new GeometrySerializer());
			this.ParamSerializers.Add(ExportMapParameterNames.Size, new SizeSerializer());
			this.ParamSerializers.Add(ExportMapParameterNames.BBoxSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(ExportMapParameterNames.ImageSR, new SpatialReferenceSerializer());
			this.ParamSerializers.Add(ExportMapParameterNames.Layers, new LayerDisplaySerializer());
			this.ResourceMatcher = new EqualsStringMatcher(ServiceResource, true);
			this.ResponseDeserializer = new ExportMapResponseDeserializer();
		}
		
		
		private class SizeSerializer : ISerializer
		{
			
			private bool _lastSerialized;
			
			public bool LastSerialized
			{
				get
				{
					return _lastSerialized;
				}
			}
			
			public string Serializer(object obj)
			{
				if (obj == null)
				{
					_lastSerialized = true;
					return null;
				}
				if (obj is ImageSize)
				{
                    ImageSize imgSize = obj as ImageSize;
					var str = string.Format("{0},{1}", imgSize.Width, imgSize.Height);
					_lastSerialized = true;
					return System.Convert.ToString( str);
				}
				else
				{
					_lastSerialized = false;
					return null;
				}
			}
		}
		
		private class LayerDisplaySerializer : ISerializer
		{
			
			private bool _lastSerialized;
			
			public bool LastSerialized
			{
				get
				{
					return _lastSerialized;
				}
			}
			
			public string Serializer(object obj)
			{
				if (obj == null)
				{
					_lastSerialized = true;
					return null;
				}
				if (obj is LayerDisplay)
				{
					LayerDisplay ld = obj as LayerDisplay;
					var lsStr = new PrimvateListSerializer();
					var str = string.Format("{0}:{1}", ld.DisplayOption.ToString(), lsStr.Serializer(ld.LayerIds));
					_lastSerialized = true;
					return System.Convert.ToString( str);
				}
				else
				{
					_lastSerialized = false;
					return null;
				}
			}
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
	}
	
}
