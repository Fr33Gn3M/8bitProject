using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ServerFramework.WebEndpoint.RESTServer.WebHandler;

namespace TH.ServerFramework.WebEndpoint.RESTServer
{
    public class GenericServiceProfile : IServiceProfile
    {
        public GenericServiceProfile()
        {
            _handlerTypeInstances = new Dictionary<Type, IWebHandler>();
            _handlerTypeParams = new Dictionary<Type, object[]>();
        }

        private IDictionary<Type, IWebHandler> _handlerTypeInstances = null;
        private IDictionary<Type, object[]> _handlerTypeParams = null;

        public IWebHandler CreateWebHandler(System.ServiceModel.Web.WebOperationContext context, System.IO.Stream postData)
        {
            IWebHandler webHandler= null;
            foreach (var item in _handlerTypeInstances)
            {
                if (item.Value.CanHandle(context, postData))
                {
                    webHandler = Activator.CreateInstance(item.Key, _handlerTypeParams[item.Key]) as IWebHandler;
                    webHandler.CanHandle(context, postData);
                    webHandler.SetMatchedUrlParamters(context);
                    break;
                }
            }
            return webHandler;
        }

        public void RegisterWebHandler(Type webHandlertype, params object[] objs)
        {
            if (_handlerTypeInstances.ContainsKey(webHandlertype))
                return;
            //var obj = Activator.CreateInstance(webHandlertype);
            var obj = Activator.CreateInstance(webHandlertype, objs);
            var webHandler = obj as IWebHandler;
            _handlerTypeInstances.Add(webHandlertype, webHandler);
            _handlerTypeParams.Add(webHandlertype, objs);
        }


        public string ServiceName
        {
            //get { return (SettingsSection.GetSection().ServiceProfiles[0] as ServiceProfileElem).ServiceProfileName; }
            get { return _serviceName; }
        }
        private string _serviceName;
        public void SetServiceName(string serviceName)
        {
            _serviceName = serviceName;
        }
    }
}
