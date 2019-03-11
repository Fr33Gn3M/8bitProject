// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using TH.ArcGISRest.ApiImports;
using PH.ServerFramework.WebClientPoint;


namespace TH.ArcGISRest.Client
{
	public class FieldsFilterSerializer : ISerializer
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
            FieldsFilter fieldsFilter = obj as FieldsFilter;
			if (fieldsFilter.AllFields)
			{
				_lastSerialized = true;
				return "*";
			}
			else
			{
				var lsSer = new PrimvateListSerializer();
				var str = lsSer.Serializer(fieldsFilter.FieldNames);
				_lastSerialized = lsSer.LastSerialized;
				return str;
			}
		}
	}
	
}
