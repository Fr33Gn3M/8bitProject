using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLabelClass = TH.ArcGISRest.ApiImports.Symbols.LabelClass;
using DespLabelClass = TH.ArcGISRest.Description.Drawing.Label.LabelClass;
using AutoMapper;
using TH.ArcGISRest.Description.Drawing.Symbol;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.Drawing.Label
{
	[ReflectionProfileMapper()]
	public class LabelClassMapper : ITypeConverter<AgsLabelClass, DespLabelClass>
	{

		public DespLabelClass Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            AgsLabelClass sourceValue = context.SourceValue as AgsLabelClass;
			var  targetValue = new DespLabelClass();
			var _with1 = targetValue;
			_with1.LabelExpression = sourceValue.LabelExpression;
            _with1.LabelPlacement = sourceValue.LabelPlacement;
			_with1.MaxScale = sourceValue.MaxScale;
			_with1.MinScale = sourceValue.MinScale;
			_with1.Symbol = Mapper.Map<TextSymbol>(sourceValue.Symbol);
			_with1.UseCodedValues = sourceValue.UseCodedValues;
			return targetValue;
		}
	}
}
