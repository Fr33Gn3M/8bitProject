// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Reflection;


namespace TH.ServerFramework
{
	namespace DictionarySerializer
	{
		internal class TimeSpanValueSerializerFactory : IValueSerializerFactory
		{
			
			public bool CanHandle(PropertyInfo propertyInfo)
			{
				var propValueType = propertyInfo.PropertyType;
				if (propertyInfo.Equals(typeof(TimeSpan)))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			public IValueSerializer CreateValueSerializer(PropertyInfo propertyInfo)
			{
				var serializer = new TimeSpanValueSerializer();
				return serializer;
			}
		}
	}
}
