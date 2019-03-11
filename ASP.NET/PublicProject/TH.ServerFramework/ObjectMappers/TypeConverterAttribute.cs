using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TypeConverterAttribute : Attribute
    {
        private readonly Type _typeConverterType;
        public TypeConverterAttribute(Type typeConverterType)
        {
            _typeConverterType = typeConverterType;
        }

        public Type TypeConverterType
        {
            get { return _typeConverterType; }
        }
    }
}
