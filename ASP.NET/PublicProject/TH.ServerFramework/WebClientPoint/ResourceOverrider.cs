namespace TH.ServerFramework.WebClientPoint
{
    using TH.ServerFramework.WebClientPoint.Predefined;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class ResourceOverrider
    {
        private readonly IResourceHandler _resHandler;
        private readonly string _resource;
        private readonly MyRestClient _restClient;

        public ResourceOverrider(MyRestClient restClient, string resource)
        {
            this._restClient = restClient;
            this._resource = resource;
            this._resHandler = this.LoadResHandler();
        }

        public IDeserializer GetDeserializer()
        {
            if (this._resHandler != null)
            {
                return this._resHandler.ResponseDeserializer;
            }
            if (this._restClient.DefaultResourceHandle != null)
            {
                return this._restClient.DefaultResourceHandle.ResponseDeserializer;
            }
            return null;
        }

        public IDictionary<string, string> GetHeaders()
        {
            Dictionary<string, string> allHeaders = new Dictionary<string, string>();
            if (this._restClient.DefaultResourceHandle != null)
            {
                OverrideParams<string>(allHeaders, this._restClient.DefaultResourceHandle.RequestHeaders);
            }
            if (this._resHandler != null)
            {
                OverrideParams<string>(allHeaders, this._resHandler.RequestHeaders);
            }
            return allHeaders;
        }

        public HttpRequestMethod GetMethod()
        {
            if (this._restClient.Method.HasValue)
            {
                return this._restClient.Method.Value;
            }
            if (this._resHandler != null)
            {
                return this._resHandler.Method;
            }
            if (this._restClient.DefaultResourceHandle != null)
            {
                return this._restClient.DefaultResourceHandle.Method;
            }
            return HttpRequestMethod.AutoGetPost;
        }

        public IDictionary<string, string> GetParamters()
        {
            Dictionary<string, object> allParams = new Dictionary<string, object>();
            Dictionary<string, ISerializer> paramSers = new Dictionary<string, ISerializer>();
            if (this._restClient.DefaultResourceHandle != null)
            {
                OverrideParams<ISerializer>(paramSers, this._restClient.DefaultResourceHandle.ParamSerializers);
                OverrideParams<object>(allParams, this._restClient.DefaultResourceHandle.DefaultParams);
            }
            if (this._resHandler != null)
            {
                OverrideParams<object>(allParams, this._resHandler.DefaultParams);
                OverrideParams<ISerializer>(paramSers, this._resHandler.ParamSerializers);
            }
            OverrideParams<object>(allParams, this._restClient.Params);
            Dictionary<string, string> strParams = new Dictionary<string, string>();
            PrimvateValueSerializer paramSer = new PrimvateValueSerializer();
            foreach (KeyValuePair<string, object> kv in allParams)
            {
                string paramName = kv.Key;
                object paramValue = RuntimeHelpers.GetObjectValue(kv.Value);
                if (kv.Value != null)
                {
                    string strParamValue = string.Empty;
                    if (paramValue is string)
                    {
                        strParamValue = paramValue.ToString();
                    }
                    else
                    {
                        ISerializer ser = this.GetParamterSerializer(paramName);
                        strParamValue = ser.Serializer(RuntimeHelpers.GetObjectValue(paramValue));
                        if (!ser.LastSerialized)
                        {
                            throw new NotSupportedException();
                        }
                    }
                    strParams.Add(paramName, strParamValue);
                }
            }
            return strParams;
        }

        private ISerializer GetParamterSerializer(string paramName)
        {
            ISerializer serializer = new PrimvateValueSerializer();
            if (this._resHandler != null)
            {
                if (this._resHandler.DefaultParamSerializer != null)
                {
                    serializer = this._resHandler.DefaultParamSerializer;
                    if (this._resHandler.ParamSerializers.ContainsKey(paramName))
                    {
                        serializer = this._resHandler.ParamSerializers[paramName];
                    }
                }
                return serializer;
            }
            if (this._restClient.DefaultResourceHandle != null)
            {
                if (this._restClient.DefaultResourceHandle.ParamSerializers.ContainsKey(paramName))
                {
                    return this._restClient.DefaultResourceHandle.ParamSerializers[paramName];
                }
                if (this._restClient.DefaultResourceHandle.DefaultParamSerializer != null)
                {
                    serializer = this._restClient.DefaultResourceHandle.DefaultParamSerializer;
                }
            }
            return serializer;
        }

        private IResourceHandler LoadResHandler()
        {
            if (this._restClient.ResourceHandlers != null)
            {
                foreach (IResourceHandler resHandler in this._restClient.ResourceHandlers)
                {
                    if (resHandler.ResourceMatcher.IsMatch(this._resource))
                    {
                        return resHandler;
                    }
                }
            }
            return null;
        }

        private static void OverrideParams<TValue>(IDictionary<string, TValue> dic, IDictionary<string, TValue> otherDic)
        {
            if (dic == null)
            {
                dic = otherDic;
            }
            else if ((otherDic != null) && otherDic.Any<KeyValuePair<string, TValue>>())
            {
                foreach (KeyValuePair<string, TValue> kv in otherDic)
                {
                    if (dic.ContainsKey(kv.Key))
                    {
                        dic[kv.Key] = kv.Value;
                    }
                    else
                    {
                        dic.Add(kv.Key, kv.Value);
                    }
                }
            }
        }
    }
}

