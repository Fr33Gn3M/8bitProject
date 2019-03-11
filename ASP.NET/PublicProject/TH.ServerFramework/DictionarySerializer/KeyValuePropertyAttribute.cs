// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Reflection;
using Fasterflect;
using Microsoft.Practices.ServiceLocation;


namespace TH.ServerFramework.DictionarySerializer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyValuePropertyAttribute : Attribute
    {

        private readonly string _key;
        private readonly Type _valueSerializerType;
        private readonly string _valueSerializerFactoryMethodName;

        public KeyValuePropertyAttribute(Type valueSerializerType, string valueSerializerFactoryMethodName, string key)
        {
            _key = key;
            _valueSerializerFactoryMethodName = valueSerializerFactoryMethodName;
            _valueSerializerType = valueSerializerType;
        }

        internal static string GetKey(PropertyInfo propertyInfo, KeyValuePropertyAttribute attribute)
        {
            if (attribute._key != null)
            {
                return attribute._key;
            }
            else
            {
                var key = string.Format("{0}.{1}", propertyInfo.DeclaringType.FullName, propertyInfo.Name);
                return propertyInfo.Name;
            }
        }

        private static IValueSerializer CreateValueSerializer(KeyValuePropertyAttribute attribute, PropertyInfo propertyInfo)
        {
            IValueSerializer valueSerializer = null;
            if (attribute == null && (attribute._valueSerializerFactoryMethodName == null && attribute._valueSerializerType == null))
            {
                var valueSerializerRepo = ServiceLocator.Current.GetInstance<IValueSerializerRepo>();
                valueSerializer = valueSerializerRepo.CreateSerializer(propertyInfo);
            }
            else
            {
                if (attribute._valueSerializerType != null)
                {
                    valueSerializer = Activator.CreateInstance(attribute._valueSerializerType) as IValueSerializer;
                }
                else if (attribute._valueSerializerFactoryMethodName != null)
                {
                    var classType = propertyInfo.ReflectedType;
                    valueSerializer = classType.CallMethod(attribute._valueSerializerFactoryMethodName, Flags.StaticAnyVisibility) as IValueSerializer;
                }
                else
                {
                    var genType = typeof(PrimitiveTypeValueSerializer<>);
                    genType = genType.MakeGenericType(propertyInfo.PropertyType);
                    valueSerializer = Activator.CreateInstance(genType) as IValueSerializer;
                }
            }
            return valueSerializer;
        }

        internal static string SerializeValue(PropertyInfo propertyInfo, object propertyValue, KeyValuePropertyAttribute attribute)
        {
            var valueSerializer = CreateValueSerializer(attribute, propertyInfo);
            var serializedValue = valueSerializer.Serialize(propertyValue);
            return (string)(serializedValue);
        }

        internal static object DeserializeValue(PropertyInfo propertyInfo, string serializedValue, KeyValuePropertyAttribute attribute)
        {
            var valueSerializer = CreateValueSerializer(attribute, propertyInfo);
            var value = valueSerializer.Deserialize(serializedValue);
            return value;
        }
    }
}