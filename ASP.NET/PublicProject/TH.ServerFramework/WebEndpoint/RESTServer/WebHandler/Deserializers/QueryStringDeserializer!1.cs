namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class QueryStringDeserializer<T>
    {
        private readonly object _defualtValue;
        private readonly IQueryStringDeserializer<T> _deserializer;
        private readonly bool _isRequired;
        private readonly string _parameterName;
        private readonly NameValueCollection _paramters;

        public QueryStringDeserializer(NameValueCollection paramters, string parameterName, IQueryStringDeserializer<T> deserializer)
        {
            this._paramters = paramters;
            this._isRequired = true;
            this._parameterName = parameterName;
            this._deserializer = deserializer;
        }

        public QueryStringDeserializer(NameValueCollection paramters, string parameterName, IQueryStringDeserializer<T> deserializer, object defaultValue)
        {
            this._paramters = paramters;
            this._parameterName = parameterName;
            this._isRequired = false;
            this._defualtValue = RuntimeHelpers.GetObjectValue(defaultValue);
            this._deserializer = deserializer;
        }

        public T Deserialize()
        {
            string paramValue = this._paramters[this._parameterName];
            if (paramValue == null)
                return default(T);
            if (string.IsNullOrEmpty(paramValue))
            {
                if (this._isRequired)
                {
                    throw new ArgumentException(this._parameterName);
                }
                return (T)this._defualtValue;
            }
            return this._deserializer.Deserialize(paramValue);
        }
    }
}

