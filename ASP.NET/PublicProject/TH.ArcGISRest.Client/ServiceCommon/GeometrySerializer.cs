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
	public class GeometrySerializer : ISerializer
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
				_lastSerialized = false;
				return null;
			}
			if (obj.GetType().FullName.Equals(typeof(AgsPoint).FullName))
			{
				AgsPoint agsPt = obj as AgsPoint;
				var str = string.Format("{0},{1}", agsPt.X, agsPt.Y);
				_lastSerialized = true;
				return System.Convert.ToString( str);
			}
			else if (obj.GetType().FullName.Equals(typeof(AgsEnvelope).FullName))
			{
                AgsEnvelope agsEnv = obj as AgsEnvelope;
				var str = string.Format("{0},{1},{2},{3}", agsEnv.XMin, agsEnv.YMin, agsEnv.XMax, agsEnv.YMax);
				_lastSerialized = true;
				return System.Convert.ToString( str);
			}
			else
			{
				var str = JsonConvert.SerializeObject(obj);
				_lastSerialized = true;
				return System.Convert.ToString( str);
			}
		}
	}
	
}
