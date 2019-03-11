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
		public class ValueSerializerRepo : IValueSerializerRepo
		{
			
			private readonly System.Collections.Generic.IList<IValueSerializerFactory> _factories;
			
			public ValueSerializerRepo()
			{
				_factories = new List<IValueSerializerFactory>();
			}
			
			public IValueSerializer CreateSerializer(PropertyInfo propertyInfo)
			{
				foreach (var factory in _factories)
				{
					if (factory.CanHandle(propertyInfo))
					{
						var valueSerializre = factory.CreateValueSerializer(propertyInfo);
						return valueSerializre;
					}
				}
				return null;
			}
			
			public void RegisterFactory(IValueSerializerFactory factory)
			{
				if (!_factories.Contains(factory))
				{
					_factories.Add(factory);
				}
			}
		}
	}
}
