namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler
{
    using TH.ServerFramework.Utility;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.ServiceModel.Web;
    using System.Text.RegularExpressions;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections;

    public abstract class WebHandlerBase : IWebHandler
    {
        private  Regex _regex;
        private  string _serviceName;
        protected WebHandlerBase()
        {

        }

        protected WebHandlerBase(string serviceName, string urlPathPattern)
        {
            this._regex = new Regex(urlPathPattern, RegexOptions.None);
            this._serviceName = serviceName;
        }

        public virtual bool CanHandle(WebOperationContext context, Stream postData)
        {
            string urlPath = context.GetWildcardPath();
            bool result = this._regex.IsMatch(urlPath);
            //if (result == true)
            //    SetMatchedUrlParamters(urlPath);
            return result;
        }

        public virtual void SetMatchedUrlParamters(WebOperationContext context)
        {
            string urlPath = context.GetWildcardPath();
            SetMatchedUrlParamters(urlPath);
        }
        private void SetMatchedUrlParamters(string urlPath)
        {
            var m = _regex.Match(urlPath);
            var groupNames = _regex.GetGroupNames();
            _MatchedUrlParamters = new NameValueCollection();
            foreach (var item in groupNames)
            {
                var value = m.Groups[item].Value;
                _MatchedUrlParamters.Set(item, value);
                //_MatchedUrlParamters[item] = value;
            }
        }

        public abstract Stream Handle(WebOperationContext context, Stream postData);

        private NameValueCollection _MatchedUrlParamters;
        protected NameValueCollection MatchedUrlParamters
        {
            get
            {
                return _MatchedUrlParamters;
            }
           //private set 
           // {
           //     _MatchedUrlParamters = value; 
           // }
        }

        public string ServiceName
        {
            get
            {
                return this._serviceName;
            }
        }

        public void InitWebHandler(string serviceName, string urlPathPattern)
        {
            _regex = new Regex(urlPathPattern, RegexOptions.None);
            _serviceName = serviceName;
        }
    }
}

