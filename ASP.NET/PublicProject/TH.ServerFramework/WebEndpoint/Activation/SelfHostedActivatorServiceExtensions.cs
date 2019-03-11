namespace TH.ServerFramework.WebEndpoint.Activation
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    public static class SelfHostedActivatorServiceExtensions
    {
        public static void Open<TServiceType>(this ISelfHostedActivatorService source)
        {
            var serviceType = typeof(TServiceType);
            var svcHost = new ServiceHost(serviceType);
            source.Open(() => svcHost);
        }

        public static void Close<TServiceType>( this ISelfHostedActivatorService source)
        {
            dynamic serviceType = typeof(TServiceType);
            source.Close(serviceType);
        }

        public static void OpenAsREST<TServiceType>(this ISelfHostedActivatorService source)
        {
            var serviceType = typeof(TServiceType);
            var webSvcHost = new WebServiceHost(serviceType);
            source.Open(() => webSvcHost);
        }

        public static void Open( this ISelfHostedActivatorService source, Type serviceType)
        {
            var svcHost = new ServiceHost(serviceType);
            source.Open(() => svcHost);
        }

        public static void OpenAsREST(this ISelfHostedActivatorService source, Type serviceType)
        {
            var webSvcHost = new WebServiceHost(serviceType);
            source.Open(() => webSvcHost);
        }
    }

}

