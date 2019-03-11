// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace TH.ServerFramework
{
	namespace DictionarySerializer
	{
		public class TimeSpanValueSerializer : IValueSerializer
		{
			
			public object Deserialize(string serializedValue)
			{
				if (serializedValue == null)
				{
					return null;
				}
				return TimeSpan.Parse(serializedValue);
			}
			
			public string Serialize(object value)
			{
				if (value == null)
				{
					return null;
				}
                TimeSpan ts = (TimeSpan)value;
				return ts.ToString();
			}
		}
	}
}
