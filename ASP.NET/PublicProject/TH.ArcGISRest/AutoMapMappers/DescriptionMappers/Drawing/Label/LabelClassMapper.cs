using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AgsLabelClass = TH.ArcGISRest.ApiImports.Symbols.LabelClass;
using DespLabelClass = TH.ArcGISRest.Description.Drawing.Label.LabelClass;
using TH.ArcGISRest.ApiImports.Symbols;
using AutoMapper;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class LabelClassMapper : ITypeConverter<DespLabelClass, AgsLabelClass>
	{

		public AgsLabelClass Convert(ResolutionContext context)
		{
			if (context.IsSourceValueNull) {
				return null;
			}
            DespLabelClass sourceValue = context.SourceValue as DespLabelClass;
			var  targetValue = new AgsLabelClass();
			var _with1 = targetValue;
			_with1.LabelExpression = sourceValue.LabelExpression;
			_with1.LabelPlacement = sourceValue.LabelPlacement;
			_with1.MaxScale = sourceValue.MaxScale;
			_with1.MinScale = sourceValue.MinScale;
            _with1.Symbol = Mapper.Map<ISymbol>(sourceValue.Symbol) as TextSymbol;
			_with1.UseCodedValues = sourceValue.UseCodedValues;
			return targetValue;
		}
	}
}
