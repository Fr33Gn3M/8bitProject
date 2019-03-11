using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework
{
    public class ProfileBase : Profile
    {
        protected override void Configure()
        {
            base.Configure();
            var instanceType = this.GetType();
            var tcAtts = instanceType.GetCustomAttributes(typeof(TypeConverterAttribute), false);
            foreach (TypeConverterAttribute tcAtt in tcAtts)
            {
                var mapperType = tcAtt.TypeConverterType;
                dynamic genericTypeConveterType = typeof(ITypeConverter<,>);
                var interfaceTypes = mapperType.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    if (!IsMatch(interfaceType, genericTypeConveterType))
                        continue;
                    var genericArgs = interfaceType.GetGenericArguments();
                    var sourceType = genericArgs[0];
                    var targetType = genericArgs[1];
                    CreateMap(sourceType, targetType).ConvertUsing(mapperType);
                }
            }
        }

    //       Dim instanceType = Me.GetType
    //    Dim tcAtts = instanceType.GetCustomAttributes(GetType(TypeConverterAttribute), False)
    //    For Each tcAtt As TypeConverterAttribute In tcAtts
    //        Dim mapperType = tcAtt.TypeConverterType
    //        Dim genericTypeConveterType = GetType(ITypeConverter(Of ,))
    //        Dim interfaceTypes = mapperType.GetInterfaces
    //        For Each interfaceType In interfaceTypes
    //            If Not IsMatch(interfaceType, genericTypeConveterType) Then
    //                Continue For
    //            End If
    //            Dim genericArgs = interfaceType.GetGenericArguments
    //            Dim sourceType = genericArgs(0)
    //            Dim targetType = genericArgs(1)
    //            CreateMap(sourceType, targetType).ConvertUsing(mapperType)
    //        Next
    //    Next
    //End Sub
        private static bool IsMatch(Type interfaceType, Type genericInterfaceType)
        {
            return interfaceType.GetGenericTypeDefinition().Equals(genericInterfaceType);
        }

    }
}
