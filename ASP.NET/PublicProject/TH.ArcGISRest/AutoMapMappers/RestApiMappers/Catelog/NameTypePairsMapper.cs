using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using TH.ArcGISRest.Description.Catelog;
using TH.ArcGISRest.ApiImports;

namespace TH.ArcGISRest.AutoMapMappers.RestApiMappers.RestApiMappers.Catelog
{
	[ReflectionProfileMapper()]
	public class NameTypePairsMapper : CollectionMapperBase<NameTypePair[], KeyValuePair<string, ServiceType>>
	{
		//Implements ITypeConverter(Of NameTypePair(), IDictionary(Of String, ServiceType))

		//Public Function Convert(context As ResolutionContext) As IDictionary(Of String, ServiceType) Implements ITypeConverter(Of NameTypePair(), IDictionary(Of String, ServiceType)).Convert
		//    Dim empty = New Dictionary(Of String, ServiceType)
		//    If context.IsSourceValueNull Then
		//        Return empty
		//    End If
		//    Dim sourceValue As NameTypePair() = context.SourceValue
		//    Dim targetValue = New Dictionary(Of String, ServiceType)
		//    For Each item In sourceValue
		//        Dim kv = Mapper.Map(Of KeyValuePair(Of String, ServiceType))(item)
		//        targetValue.Add(kv.Key, kv.Value)
		//    Next
		//    Return targetValue
		//End Function
	}
}
