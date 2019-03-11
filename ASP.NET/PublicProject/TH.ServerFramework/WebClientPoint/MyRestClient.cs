namespace TH.ServerFramework.WebClientPoint
{
    using TH.ServerFramework.Logs;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MyRestClient : IDisposable
    {
        private readonly Uri _baseUrl;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private IResourceHandler _DefaultResourceHandle;
        private readonly long _maxAllowedGetUrlLength;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HttpRequestMethod? _Method;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private Action<RestRequest> _OnBeforeRequest;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<string, object> _Params;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IList<IResourceHandler> _ResourceHandlers;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan _Timeout;
        private bool disposedValue;

        public MyRestClient(Uri baseUrl, long maxAllowedGetUrlLength = 0x7fffffffffffffffL)
        {
            this._baseUrl = baseUrl;
            _maxAllowedGetUrlLength = maxAllowedGetUrlLength;
            this.Params = new NameValueDictionary<object>();
        }

        public WebRequestContext BuildRequest(string resource)
        {
            string method;
            RestClient client = new RestClient(this._baseUrl.AbsoluteUri);
            ResourceOverrider resOverrider = new ResourceOverrider(this, resource);
            IDictionary<string, string> strParams = resOverrider.GetParamters();
            IDictionary<string, string> headers = resOverrider.GetHeaders();
            HttpRequestMethod myHttpMethod = resOverrider.GetMethod();
            RestRequest restRequest = this.GetRestRequest(client, resource, resOverrider);
            switch (((int) myHttpMethod))
            {
                case 0:
                    method = "GET";
                    break;

                case 1:
                    if (strParams.Any<KeyValuePair<string, string>>())
                    {
                        method = "POST";
                        break;
                    }
                    method = "GET";
                    break;

                case 2:
                    method = "GET";
                    if (client.BuildUri(restRequest).AbsoluteUri.Length > this._maxAllowedGetUrlLength)
                    {
                        method = "POST";
                    }
                    break;

                default:
                    throw new NotSupportedException(myHttpMethod.ToString());
            }
            return new WebRequestContext(client.BuildUri(restRequest), this._baseUrl, resource, headers, strParams, method);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue && disposing)
            {
            }
            this.disposedValue = true;
        }

        private RestRequest GetRestRequest(RestClient client, string resource, ResourceOverrider resOverrider)
        {
            RestRequest req = new RestRequest {
                Resource = resource
            };
            IDictionary<string, string> strParams = resOverrider.GetParamters();
            int reqLength = strParams.Sum(p => p.Key.Length + p.Value.Length);
            HttpRequestMethod myHttpMethod = resOverrider.GetMethod();
            switch (((int) myHttpMethod))
            {
                case 0:
                    req.Method = RestSharp.Method.GET;
                    break;

                case 1:
                    req.Method = RestSharp.Method.POST;
                    break;

                case 2:
                    if (reqLength <= this._maxAllowedGetUrlLength)
                    {
                        req.Method = RestSharp.Method.GET;
                        break;
                    }
                    req.Method = RestSharp.Method.POST;
                    break;

                default:
                    throw new NotSupportedException(myHttpMethod.ToString());
            }
            foreach (KeyValuePair<string, string> kv in strParams)
            {
                req.AddParameter(kv.Key, kv.Value);
            }
            IDictionary<string, string> headers = resOverrider.GetHeaders();
            foreach (KeyValuePair<string, string> kv in headers)
            {
                req.AddHeader(kv.Key, kv.Value);
            }
            Uri url = client.BuildUri(req);
            if (this.OnBeforeRequest != null)
            {
                this.OnBeforeRequest(req);
            }
            return req;
        }

        public byte[] Request(string resource)
        {
            ResourceOverrider resOverrider = new ResourceOverrider(this, resource);
            return this.Request(resource, resOverrider);
        }
        public T Request<T>(string resource)
        {
            ResourceOverrider resOverrider = new ResourceOverrider(this, resource);
            byte[] rawBytes = this.Request(resource, resOverrider);
            IDeserializer deserializer = resOverrider.GetDeserializer();
            if (deserializer == null)
            {
                throw new NotSupportedException();
            }
            deserializer.RequestParams = resOverrider.GetParamters();
            return deserializer.DeserializeObject<T>(rawBytes);
        }

        public byte[] RequestToStream(string resource)
        {
            ResourceOverrider resOverrider = new ResourceOverrider(this, resource);
            byte[] rawBytes = this.Request(resource, resOverrider);
            return rawBytes;
            //return Encoding.UTF8.GetString(rawBytes);
        }

        private byte[] Request(string resource, ResourceOverrider resOverrider)
        {
            RestClient client = new RestClient(this._baseUrl.AbsoluteUri);
            if (this.Timeout != TimeSpan.Zero)
            {
                client.Timeout = this.Timeout.Milliseconds;
            }
            RestRequest req = this.GetRestRequest(client, resource, resOverrider);
            IRestResponse resp = client.Execute(req);
           // LogManager.Logger.Log(LogManager.TestLog, LogLevel.Info, t.ElapsedMilliseconds);
            if (resp.ErrorException != null)
            {
                throw resp.ErrorException;
            }
            return resp.RawBytes;
        }

        public IResourceHandler DefaultResourceHandle
        {
            [DebuggerNonUserCode]
            get
            {
                return this._DefaultResourceHandle;
            }
            [DebuggerNonUserCode]
            set
            {
                this._DefaultResourceHandle = value;
            }
        }

        public HttpRequestMethod? Method
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

        public Action<RestRequest> OnBeforeRequest
        {
            [DebuggerNonUserCode]
            get
            {
                return this._OnBeforeRequest;
            }
            [DebuggerNonUserCode]
            set
            {
                this._OnBeforeRequest = value;
            }
        }

        public IDictionary<string, object> Params
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Params;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Params = value;
            }
        }

        public IList<IResourceHandler> ResourceHandlers
        {
            [DebuggerNonUserCode]
            get
            {
                return this._ResourceHandlers;
            }
            [DebuggerNonUserCode]
            set
            {
                this._ResourceHandlers = value;
            }
        }

        public TimeSpan Timeout
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Timeout;
            }
            [DebuggerNonUserCode]
            set
            {
                this._Timeout = value;
            }
        }
    }
}

