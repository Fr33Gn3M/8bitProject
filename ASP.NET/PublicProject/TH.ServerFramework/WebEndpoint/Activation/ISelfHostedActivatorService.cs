namespace TH.ServerFramework.WebEndpoint.Activation
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;

    public delegate void ServiceHostClosedEventHandler(object sender, EventArgs e);
    public delegate void ServiceHostOpenEventHandler(object sender, EventArgs e);
    public interface ISelfHostedActivatorService
    {
        event ServiceHostClosedEventHandler ServiceHostClosed;
        event ServiceHostOpenEventHandler ServiceHostOpen;

        void Close(Type serviceType);
        void Open(Func<ServiceHostBase> factory);


    }
}

