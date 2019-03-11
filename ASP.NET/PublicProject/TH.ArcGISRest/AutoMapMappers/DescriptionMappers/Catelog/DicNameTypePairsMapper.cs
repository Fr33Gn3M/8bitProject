using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TH.ArcGISRest.Description.Catelog;
using AutoMapper;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.DescriptionMappers.Drawing
{
	[ReflectionProfileMapper()]
	public class DicNameTypePairsMapper : CollectionMapperBase<KeyValuePair<string, ServiceType>, NameTypePair[]>
	{
		//Implements ITypeConverter(Of IDictionary(Of String, ServiceType), NameTypePair())

		//Public Function Convert(context As ResolutionContext) As NameTypePair() Implements ITypeConverter(Of IDictionary(Of String, ServiceType), NameTypePair()).Convert
		//    Dim lsTargetValue = New List(Of NameTypePair)
		//    If context.IsSourceValueNull Then
		//        Return lsTargetValue.ToArray
		//    End If
		//    Dim sourceValue As IDictionary(Of String, ServiceType) = context.SourceValue
		//    For Each kv In sourceValue
		//        Dim item = Mapper.Map(Of NameTypePair)(kv)
		//        If item IsNot Nothing Then
		//            lsTargetValue.Add(item)
		//        End If
		//    Next
		//    Return lsTargetValue.ToArray
		//End Function
	}
}
