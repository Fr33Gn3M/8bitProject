using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using System.Text;
using PGK.Extensions;
using System.Linq;

namespace TH.ArcGISRest.AutoMapMappers
{
	public abstract class FlagEnumStringConverter<TEnum> : ITypeConverter<TEnum, string>
	{


		protected FlagEnumStringConverter()
		{
		}

		public string Convert(ResolutionContext context)
		{
			if (!context.SourceType.IsEnum) {
				throw new NotSupportedException();
			}
			if (!context.SourceType.IsDefined(typeof(FlagsAttribute), false)) {
				throw new NotSupportedException();
			}
			int i = (int)context.SourceValue;
			var  enumValues =context.SourceType.GetEnumNames();
			var  items = new List<string>();
			foreach (var enumValue in enumValues) {
                int value = int.Parse(enumValue);
                if (value == 0)
                {
					continue;
				}
                if ((value & i) == value)
                {
					var  enumName = Enum.GetName(typeof(TEnum), enumValue);
					items.Add(enumName);
				}
			}
			if (items.Count == 0) {
				return null;
			} else {
				return items.ToArray().ToString("", "", "", ",");
			}
		}
	}
}
