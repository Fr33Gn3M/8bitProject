// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.Geometry;
using TH.ArcGISRest.ApiImports;
using TH.ArcGISRest;



namespace TH.ArcGISRest.Client
{
	public class GeometryTypeConverter
	{
		public static EsriGeometryType GetGeometryType(AgsGeometryBase geo)
		{
			if (geo is AgsPoint)
			{
				return EsriGeometryType.esriGeometryPoint;
			}
			else if (geo is AgsMultipoint)
			{
				return EsriGeometryType.esriGeometryMultipoint;
			}
			else if (geo is AgsEnvelope)
			{
				return EsriGeometryType.esriGeometryEnvelope;
			}
			else if (geo is AgsPolyline)
			{
				return EsriGeometryType.esriGeometryPolyline;
			}
			else if (geo is AgsPolygon)
			{
				return EsriGeometryType.esriGeometryPolygon;
			}
			else
			{
				throw (new NotSupportedException());
			}
		}
	}
	
}
