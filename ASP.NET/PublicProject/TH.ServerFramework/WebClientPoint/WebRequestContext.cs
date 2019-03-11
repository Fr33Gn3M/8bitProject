namespace TH.ServerFramework.WebClientPoint
{
    using System;
    using System.Collections.Generic;

    public class WebRequestContext
    {

        private readonly IDictionary<string, string> _headers;
        private readonly IDictionary<string, string> _params;
        private readonly string _method;
        private readonly string _resource;
        private readonly Uri _baseUrl;

        private readonly Uri _builtUrl;
        internal WebRequestContext(Uri builtUrl, Uri baseUrl, string resource, IDictionary<string, string> headers, IDictionary<string, string> @params, string method)
        {
            _headers = headers;
            _params = @params;
            _method = method;
            _resource = resource;
            _baseUrl = baseUrl;
            _builtUrl = builtUrl;
        }

        public Uri BuiltUrl
        {
            get { return _builtUrl; }
        }

        public Uri BaseUrl
        {
            get { return _baseUrl; }
        }

        public string Resource
        {
            get { return _resource; }
        }

        public IDictionary<string, string> Headers
        {
            get { return _headers; }
        }

        public IDictionary<string, string> Params
        {
            get { return _params; }
        }

        public string Method
        {
            get { return _method; }
        }
    }

}

