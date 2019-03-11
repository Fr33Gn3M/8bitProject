// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports.Geometry;
using PH.ServerFramework.WebClientPoint.Predefined;


namespace TH.ArcGISRest.Client
{
	public class GeometryServiceSerializer : JsonValueSerializer
	{
		public override string Serializer(object obj)
		{
			if (obj == null)
			{
				SetLastSerialized(true);
				return null;
			}
			if (obj is AgsSpatialReference)
			{
				var srSer = new SpatialReferenceSerializer();
				var str = srSer.Serializer(obj);
				SetLastSerialized(srSer.LastSerialized);
				return str;
			}
			return base.Serializer(obj);
		}
	}
	
}
