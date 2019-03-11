using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using System.Linq;


namespace TH.ArcGISRest.AutoMapMappers
{
	public abstract class StringFlagEnumConverter<TEnum> : ITypeConverter<string, TEnum>
	{
		protected StringFlagEnumConverter()
		{
		}

		public TEnum Convert(ResolutionContext context)
		{
			if (context.SourceValue== null||string.IsNullOrEmpty(context.SourceValue.ToString())) {
				return default(TEnum);
			}
			var  targetType = typeof(TEnum);
			if (!targetType.IsEnum) {
				throw new NotSupportedException();
			}
			if (!targetType.IsDefined(typeof(FlagsAttribute), false)) {
				throw new NotSupportedException();
			}
			string sourceValue = context.SourceValue as string;
			int targetValue = 0;
			var  parts = (from e in sourceValue.Split(',') where !string.IsNullOrWhiteSpace(e) select e.Trim()).ToArray();
			var  enumNames = Enum.GetNames(targetType);
			foreach (var enumName in enumNames) {
				int enumValue = (int)Enum.Parse(targetType, enumName);
				if (enumValue == 0) {
					continue;
				}
				if (parts.Contains(enumName)) {
					targetValue = targetValue | enumValue;
				}
			}
			object objTargetValue = targetValue;
			return (TEnum)objTargetValue;
		}
	}
}
