// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.ServiceModel.Web;
using System.IO;
using System.Text;
using TH.ServerFramework.Utility;


namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler
{

    public abstract class WithFormatWebHandlerBase : WebHandlerBase
    {

        private readonly string[] _acceptFormats;
        private string _format;

        private static string GetFormat(WebOperationContext context, Stream postData)
        {
            var queryParams = context.GetQueryParamters(postData);
            var format = queryParams["f"];
            return (string)(format);
        }

        protected WithFormatWebHandlerBase(string serviceName, string urlPathParttern, string defaultFormat, string[] acceptFormats)
            : base(serviceName, urlPathParttern)
        {
            _acceptFormats = acceptFormats;
        }

        public override bool CanHandle(WebOperationContext context, Stream postData)
        {
            var result = base.CanHandle(context, postData);
            if (!result)
            {
                return false;
            }
            _format = GetFormat(context, postData);
            if (_acceptFormats != null && !_acceptFormats.Contains(_format, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }


        public abstract Stream Handle(string format, WebOperationContext context, Stream postData);

        public sealed override Stream Handle(WebOperationContext context, Stream postData)
        {
            return Handle(_format, context, postData);
        }
    }
}