namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleValueDeserializer<T> : IQueryStringDeserializer<T>
    {
        public T Deserialize(string raw)
        {
            Type simpleType = typeof(T);
            if (simpleType.IsSubclassOf(typeof(Nullable)))
            {
                simpleType = Nullable.GetUnderlyingType(simpleType);
            }
            object simpleValue = null;
            if (typeof(string).Equals(simpleType))
            {
                simpleValue = raw;
            }
            else if (typeof(bool).Equals(simpleType))
            {
                simpleValue = bool.Parse(raw);
            }
            else if (typeof(int).Equals(simpleType))
            {
                simpleValue = int.Parse(raw);
            }
            else if (typeof(double).Equals(simpleType))
            {
                simpleValue = double.Parse(raw);
            }
            else
            {
                if (!typeof(DateTime).Equals(simpleType))
                {
                    throw new NotSupportedException(simpleType.FullName);
                }
                simpleValue = DateTime.Parse(raw);
            }
            return (T)simpleValue;
        }
    }
}

