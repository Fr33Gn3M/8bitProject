namespace TH.ServerFramework.WebEndpoint
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IWebFileSystemService
    {
        [WebGet(UriTemplate = "getImg?token={strToken}&path={imagename}"), OperationContract]
        Stream GetFile(string strToken, string imagename);
        [WebInvoke(UriTemplate = "updateImages?token={strToken}&path={imagename}", Method = "POST")]
        void UpdateImages(Stream stream, string imagename, string strToken);
        [WebInvoke(UriTemplate = "setImg?token={strToken}&path={imagename}&img={image}", Method = "*")]
        void UpdateImg(string image, string strToken, string imagename);

    }
}

