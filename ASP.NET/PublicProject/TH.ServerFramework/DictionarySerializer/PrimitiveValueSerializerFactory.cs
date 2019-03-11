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
		internal class PrimitiveValueSerializerFactory : IValueSerializerFactory
		{
			
			public bool CanHandle(PropertyInfo propertyInfo)
			{
				var propValueType = propertyInfo.PropertyType;
				if (PrimitiveTypeValueSerializer<int>.GetPrimitiveTypes().Contains(propValueType))
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
				var genType = typeof(PrimitiveTypeValueSerializer<>);
				var t = genType.MakeGenericType(propertyInfo.PropertyType);
                var valueSerializer = Activator.CreateInstance(t) as IValueSerializer;
				return valueSerializer;
			}
		}
	}
}
