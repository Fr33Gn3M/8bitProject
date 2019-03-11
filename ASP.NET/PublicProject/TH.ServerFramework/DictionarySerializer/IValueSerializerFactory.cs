// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Reflection;


	namespace TH.ServerFramework.DictionarySerializer
	{
		public interface IValueSerializerFactory
		{
			bool CanHandle(PropertyInfo propertyInfo);
			IValueSerializer CreateValueSerializer(PropertyInfo propertyInfo);
		}
	}
