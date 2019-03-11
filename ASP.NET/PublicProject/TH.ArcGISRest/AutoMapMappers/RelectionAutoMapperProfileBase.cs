using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using System.Reflection;
using System.Linq;

namespace TH.ArcGISRest.AutoMapMappers
{
	public abstract class RelectionAutoMapperProfileBase : Profile
	{
		protected abstract IEnumerable<Assembly> GetAssemblies();

		private void CreateAssemblyMappers(Assembly asm)
		{
			var  mapperTypes = (from e in asm.GetTypes() where e.IsDefined(typeof(ReflectionProfileMapperAttribute), false) select e).ToList();
			foreach (var mapperType in mapperTypes) {
				var  genericTypeConveterType = typeof(ITypeConverter<, >);
				var  interfaceType = mapperType.GetInterface(genericTypeConveterType.FullName);
				var  genericArgs = interfaceType.GetGenericArguments();
				var  sourceType = genericArgs[0];
                var targetType = genericArgs[1];
				CreateMap(sourceType, targetType).ConvertUsing(mapperType);
			}
		}

		protected override sealed void Configure()
		{
			base.Configure();
			var  asms = GetAssemblies().ToArray();
			foreach (var asm in asms) {
				CreateAssemblyMappers(asm);
			}
		}
	}
}
