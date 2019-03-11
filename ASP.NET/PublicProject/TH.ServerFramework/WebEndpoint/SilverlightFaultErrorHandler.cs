namespace TH.ServerFramework.WebEndpoint
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;


    public class SilverlightFaultErrorHandler : Attribute, IErrorHandler, IServiceBehavior
    {

        public void AddBindingParameters(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<System.ServiceModel.Description.ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher item in serviceHostBase.ChannelDispatchers)
            {
                item.ErrorHandlers.Add(this);
            }
        }

        public void Validate(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }

        public bool HandleError(System.Exception error)
        {
            return true;
        }

        public void ProvideFault(System.Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            var fe = new FaultException(error.Message);
            var mf = fe.CreateMessageFault();
            fault = Message.CreateMessage(version, mf, "http://microsoft.wcf.documentation/default");
            var prop = new HttpResponseMessageProperty();
            prop.StatusCode = System.Net.HttpStatusCode.OK;
            fault.Properties[HttpResponseMessageProperty.Name] = prop;
        }
    }
}

