namespace TH.ServerFramework.WebClientPoint
{
    using TH.ServerFramework.WebClientPoint.Predefined;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ResourceHandler : IResourceHandler
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<string, object> _DefaultParams;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ISerializer _DefaultParamSerializer;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private HttpRequestMethod _Method;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<string, ISerializer> _ParamSerializers;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IDictionary<string, string> _RequestHeaders;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IStringMatcher _ResourceMatcher;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IDeserializer _ResponseDeserializer;

        public ResourceHandler()
        {
            this.DefaultParams = new NameValueDictionary<object>();
            this.Method = HttpRequestMethod.Get;
            this.ParamSerializers = new NameValueDictionary<ISerializer>();
            this.RequestHeaders = new NameValueDictionary<string>();
            this.ResponseDeserializer = new JsonDeserializer(new JsonConverter[0]);
            this.DefaultParamSerializer = new JsonValueSerializer();
        }

        public IDictionary<string, object> DefaultParams
        {
            [DebuggerNonUserCode]
            get
            {
                return this._DefaultParams;
            }
            [DebuggerNonUserCode]
            set
            {
                this._DefaultParams = value;
            }
        }

        public ISerializer DefaultParamSerializer
        {
            [DebuggerNonUserCode]
            get
            {
                return this._DefaultParamSerializer;
            }
            [DebuggerNonUserCode]
            set
            {
                this._DefaultParamSerializer = value;
            }
        }

        public HttpRequestMethod Method
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Method;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Method = value;
            }
        }

        public IDictionary<string, ISerializer> ParamSerializers
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ParamSerializers;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ParamSerializers = value;
            }
        }

        public IDictionary<string, string> RequestHeaders
        {
            [DebuggerNonUserCode]
            get
            {
                return this._RequestHeaders;
            }
            [DebuggerNonUserCode]
            set
            {
                this._RequestHeaders = value;
            }
        }

        public IStringMatcher ResourceMatcher
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ResourceMatcher;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ResourceMatcher = value;
            }
        }

        public IDeserializer ResponseDeserializer
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ResponseDeserializer;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ResponseDeserializer = value;
            }
        }
    }
}

