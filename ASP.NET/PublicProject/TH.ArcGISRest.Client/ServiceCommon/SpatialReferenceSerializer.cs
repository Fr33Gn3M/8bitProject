// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Geometry;
using PH.ServerFramework.WebClientPoint;



namespace TH.ArcGISRest.Client
{
	public class SpatialReferenceSerializer : ISerializer
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
			if (obj is AgsSpatialReference)
			{
                AgsSpatialReference agsSr = obj as AgsSpatialReference;
				if (string.IsNullOrEmpty(agsSr.WKT))
				{
					_lastSerialized = true;
					return agsSr.WKID.ToString();
				}
				else
				{
					var str = JsonConvert.SerializeObject(agsSr);
					_lastSerialized = true;
					return System.Convert.ToString( str);
				}
			}
			else
			{
				_lastSerialized = false;
				return null;
			}
		}
	}
	
}
