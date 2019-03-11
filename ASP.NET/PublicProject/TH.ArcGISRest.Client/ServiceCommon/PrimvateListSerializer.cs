// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Text;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public class PrimvateListSerializer : ISerializer
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
			string result = null;
			if (obj == null)
			{
				_lastSerialized = true;
			}
			else if (obj is IEnumerable)
			{
				var sb = new StringBuilder();
				foreach (var item in ((IEnumerable) obj))
				{
					sb.Append(item);
					sb.Append(",");
				}
				sb.Remove(sb.Length - 1, 1);
				result = sb.ToString();
				_lastSerialized = true;
			}
			else
			{
				_lastSerialized = false;
			}
			return result;
		}
	}
	
}
