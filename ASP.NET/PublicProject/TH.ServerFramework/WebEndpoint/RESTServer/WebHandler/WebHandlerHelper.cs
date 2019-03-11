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
using Newtonsoft.Json;
using TH.ServerFramework.Utility;


namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler
{

    public sealed class WebHandlerHelper
    {

        public class Format
        {
            public const string F = "f";
            public const string Json = "json";
            public const string Html = "html";
            public const string Image = "image";
            public const string Plain = "plain";
        }

        public const string CallbackName = "callback";

        static public string GetFormat(WebOperationContext context, Stream postData)
        {
            var queryParams = context.GetQueryParamters(postData);
            var format = queryParams["f"];
            return (string)(format);
        }

        static public Stream ApplyJsonResponse(WebOperationContext context, Stream postData, object response)
        {
            var queryParams = context.GetQueryParamters(postData);
            var _callback = queryParams[WebHandlerHelper.CallbackName];
            context.OutgoingResponse.ContentType = ContentTypeStrings.Text(Format.Json);
            if (!string.IsNullOrEmpty(_callback))
                context.OutgoingResponse.ContentType = ContentTypeStrings.Text(Format.Plain);
            if (response == null)
                return null;
            string jsonStr = string.Empty;
            if (response is byte[])
                jsonStr = System.Text.Encoding.UTF8.GetString(response as byte[]);
            else
                jsonStr = JsonConvert.SerializeObject(response);
            if (!string.IsNullOrEmpty(_callback))
                jsonStr = string.Format("{0}({1});", _callback, jsonStr);
            var stream = jsonStr.ToStream(null);
            return stream;
        }
        //static public Stream ApplyJsonResponse(WebOperationContext context, Stream postData, byte[] response)
        //{
        //    var queryParams = context.GetQueryParamters(postData);
        //    var _callback = queryParams[WebHandlerHelper.CallbackName];
        //    context.OutgoingResponse.ContentType = ContentTypeStrings.Text(ConstStrings.Format.Json);
        //    if (response == null)
        //        return null;
        //    var jsonStr = JsonConvert.SerializeObject(response);
        //    if (!string.IsNullOrEmpty(_callback))
        //    {
        //        context.OutgoingResponse.ContentType = ContentTypeStrings.Text(ConstStrings.Format.Html);
        //        jsonStr = string.Format("{0}({1});", _callback, jsonStr);
        //    }
        //    var stream = jsonStr.ToStream(null);
        //    return stream;
        //}
    }
}
