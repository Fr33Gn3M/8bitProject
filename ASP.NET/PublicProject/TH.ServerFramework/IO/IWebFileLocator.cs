namespace TH.ServerFramework.IO
{
    using System;

    public interface IWebFileLocator
    {
        string GetUrlPath(string instanceName, string path);
    }
}

