using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace TH.ArcGISRest.AutoMapMappers
{
	public class RelectionAutoMapperProfile : RelectionAutoMapperProfileBase
	{

		protected override IEnumerable<Assembly> GetAssemblies()
		{
            return new List<Assembly>() { this.GetType().Assembly };
		}
	}
}
