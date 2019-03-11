using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TH.ArcGISServiceManager
{
    using System.Collections;
    using System.IO;
    using System.Net;
    using ESRI.ArcGIS.SOESupport;
    using ESRI.ArcGIS.esriSystem;
    using System.Reflection;
    using ESRI.ArcGIS.Geoprocessing;

    /// <summary>
    /// 服务类型
    /// </summary>
    public enum ServiceType
    {
        MapServer,
        GeocodeServer,
        SearchServer,
        IndexingLauncher,
        IndexGenerator,
        GeometryServer,
        GeoDataServer,
        GPServer,
        GlobeServer,
        ImageServer
    }

    /// <summary>
    /// 负载平衡
    /// </summary>
    public enum LoadBalancing
    {
        ROUND_ROBIN,
        FAIL_OVER
    }

    /// <summary>
    /// isolation level
    /// 
    /// </summary>
    public enum IsolationLevel
    {
        LOW,
        HIGH
    }

    public class CreateArcGISServiceNew
    {
        //private static string HostName = System.Configuration.ConfigurationSettings.AppSettings["HostName"].ToString();
        private static string ServiceCachePath = System.Configuration.ConfigurationSettings.AppSettings["serviceCachePath"].ToString();
        //private static string username = System.Configuration.ConfigurationSettings.AppSettings["MapServerUserName"].ToString();
        //private static string password = System.Configuration.ConfigurationSettings.AppSettings["MapserverPwd"].ToString();
        //private static string urlRestAdmin = HostName + "/admin";
        //private static string urlRestServer = HostName + "/server";

        /// <summary>
        /// Create arcgis server folder
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="description">Description of the folder</param>
        /// <returns>True if successfully created</returns>
        public static bool CreateServerFolder(string folderName, string description, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string folderUrl = arcInfo.HostName + "/admin" +"/services/" + folderName + "?f=json&token=" + token;
                string resultExistsFolder = GetResult(folderUrl);
                if (!HasError(resultExistsFolder))
                {
                    return true; // exists
                }
                else
                {
                    string createFolderUrl = arcInfo.HostName + "/admin" + "/services/createFolder";
                    string postContent = string.Format("folderName={0}&description={1}&f=json&token={2}", folderName, description, token);
                    string result = GetResult(createFolderUrl, postContent);
                    return HasSuccess(result);
                }
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// Get physical Path and virtual Path from directory ags
        /// </summary>
        /// <param name="directory">directory ags</param>
        /// <param name="physicalPath">physical Path</param>
        /// <param name="virtualPath">virtual Path</param>
        /// <returns>True if successfully return path</returns>
        public static bool GetServerDirectory(string directory, out string physicalPath, out string virtualPath, ArcGISManagerInfo arcInfo)
        {
            physicalPath = null;
            virtualPath = null;
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string directoryUrl = arcInfo.HostName + "/admin" + "/system/directories/" + directory + "?f=json&token=" + token;
 
                string result = GetResult(directoryUrl);
 
                JsonObject jsonObject = new JsonObject(result);
                if (!jsonObject.Exists("physicalPath") || !jsonObject.TryGetString("physicalPath", out physicalPath))
                {
                    throw new Exception();
                }
 
                jsonObject = new JsonObject(result);
                if (!jsonObject.Exists("virtualPath") || !jsonObject.TryGetString("virtualPath", out virtualPath))
                {
                    throw new Exception();
                }
 
                return true;
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// Delete Service
        /// </summary>
        /// <param name="serviceName">Service Name</param>
        /// <param name="serviceType">Server Type</param>
        /// <returns>True if successfully deleted</returns>
        public static bool DeleteService(string serviceName, ServiceType serviceType, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string serviceUrl = arcInfo.HostName + "/admin" + "/services/" + serviceName + "." + Enum.GetName(typeof(ServiceType), serviceType) + "/delete";
                string result = GetResult(serviceUrl, "f=json&token=" + token);
                return HasSuccess(result);
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// Start Service
        /// </summary>
        /// <param name="serviceName">Service Name</param>
        /// <param name="serviceType">Server Type</param>
        /// <returns>True if successfully started</returns>
        public static bool StartService(string serviceName, ServiceType serviceType, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string serviceUrl = arcInfo.HostName + "/admin" + "/services/" + serviceName + "." + Enum.GetName(typeof(ServiceType), serviceType) + "/start";
                string result = GetResult(serviceUrl, "f=json&token=" + token);
                return HasSuccess(result);
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName">Service Name</param>
        /// <param name="serviceType">Server Type</param>
        /// <returns>True if successfully stopped</returns>
        public static bool StopService(string serviceName, ServiceType serviceType, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string serviceUrl = arcInfo.HostName + "/admin" + "/services/" + serviceName + "." + Enum.GetName(typeof(ServiceType), serviceType) + "/stop";
                string result = GetResult(serviceUrl, "f=json&token=" + token);
                return HasSuccess(result);
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// 列举服务
        /// </summary>
        public static void ListServices(ArcGISManagerInfo arcInfo)
        {
            ListServices(null,arcInfo);
        }
 
        /// <summary>
        /// list of services in folder
        /// </summary>
        /// <param name="folder">name of folder</param>
        public static void ListServices(string folder, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string serviceUrl = arcInfo.HostName + "/admin" + "/services/" + folder;
                string postcontent = "f=json&token=" + token;
                string result = GetResult(serviceUrl, postcontent);
 
                JsonObject jsonObject = new JsonObject(result);
                object[] folders = null;
                if (jsonObject.Exists("folders") && jsonObject.TryGetArray("folders", out folders))
                {
                    foreach (string subfolder in folders)
                    {
                        ListServices(subfolder, arcInfo);
                    }
                }
 
                object[] services = null;
                if (jsonObject.Exists("services") && jsonObject.TryGetArray("services", out services))
                {
                    IEnumerable<JsonObject> jsonObjectService = services.Cast<JsonObject>();
                    jsonObjectService.ToList().ForEach(jo =>
                    {
                        string serviceName;
                        jo.TryGetString("serviceName", out serviceName);
                        string folderName;
                        jo.TryGetString("folderName", out folderName);
                        Console.WriteLine(folderName + "/" + serviceName);
                    });
                }
            }
            catch
            {
                throw;
            }
        }
 
        /// <summary>
        /// create service type MapServer
        /// </summary>
        /// <returns>>True if successfully created</returns>
        public static ResultModel CreateService(string MapPath, string ServerName, bool IsFeature, ArcGISManagerInfo arcInfo)
        {
            try
            {
                string token = GenerateAGSToken(arcInfo);
                string serviceUrl = arcInfo.HostName + "/admin" +"/services/createService";
 
                JsonObject jsonObject = new JsonObject();
                jsonObject.AddString("serviceName", ServerName);
                //服务类型
                jsonObject.AddString("type", Enum.GetName(typeof(ServiceType), ServiceType.MapServer));
                jsonObject.AddString("description", "This is an example");
                //不同的服务类型，其capabilities是不同的，地图服务的为Map，query和data
                jsonObject.AddString("capabilities", "Map,Query,Data");
 
                //jsonObject.AddString("capabilities","Uploads");//gp 服务的capabilities
                jsonObject.AddString("clusterName", "default");
                jsonObject.AddLong("minInstancesPerNode", 1);
                jsonObject.AddLong("maxInstancesPerNode", 2);
                jsonObject.AddLong("maxWaitTime", 60);
                jsonObject.AddLong("maxStartupTime", 300);
                jsonObject.AddLong("maxIdleTime", 1800);
                jsonObject.AddLong("maxUsageTime", 600);
                jsonObject.AddLong("recycleInterval", 24);
                jsonObject.AddString("loadBalancing", Enum.GetName(typeof(LoadBalancing), LoadBalancing.ROUND_ROBIN));
                jsonObject.AddString("isolationLevel", Enum.GetName(typeof(IsolationLevel), IsolationLevel.HIGH));
 
                JsonObject jsonObjectProperties = new JsonObject();
 
                // see for a list complete http://resources.arcgis.com/en/help/server-admin-api/serviceTypes.html
                jsonObjectProperties.AddLong("maxBufferCount", 100); // optional 100
                jsonObjectProperties.AddString("virtualCacheDir", arcInfo.HostName + "/server" + "/arcgiscache"); // optional
                jsonObjectProperties.AddLong("maxImageHeight", 2048); // optional 2048
                jsonObjectProperties.AddLong("maxRecordCount", 1000); // optional 500
 
               //10.1中服务是通过msd的形式发布的，所以创建地图服务时候将mxd转换成msd的形式，创建msd的形式而其他服务的数据发布形式，参考上面的链接
                IGeoProcessor2 gp = new GeoProcessorClass();
                // Add the BestPath toolbox.
                var path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "Mxd2Msd.tbx");
                gp.AddToolbox(path);
                // Generate the array of parameters.
                IVariantArray parameters = new VarArrayClass();
                parameters.Add(MapPath);
                string msdPath = MapPath.Replace(".mxd", ".msd");
                parameters.Add(msdPath);
                object sev = null;
                try
                {
                    // Execute the model tool by name.
                    gp.Execute("Mxd2Msd", parameters, null);
                    Console.WriteLine(gp.GetMessages(ref sev));
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    // Print geoprocessing execution error messages.
                    Console.WriteLine(gp.GetMessages(ref sev));
                    for (int i = 0; i < gp.MessageCount; i++)
                        sb.AppendLine(gp.GetMessage(i));
                }

                jsonObjectProperties.AddString("filePath", msdPath); //地图服务 required
                //jsonObjectProperties.AddString( "toolbox",@"d:\Buffer.tbx");//gp服务使用的是路径创建gp服务的路径
                jsonObjectProperties.AddLong("maxImageWidth", 2048); // optional 2048
                jsonObjectProperties.AddBoolean("cacheOnDemand", false); // optional false
                jsonObjectProperties.AddString("virtualOutputDir", arcInfo.HostName + "/server" + "/arcgisoutput");
                jsonObjectProperties.AddString("outputDir", Path.Combine(ServiceCachePath , "directories/arcgisoutput"));
                jsonObjectProperties.AddString("jobsDirectory",Path.Combine(ServiceCachePath , "directories/arcgisjobs"));      // required
                jsonObjectProperties.AddString("supportedImageReturnTypes", "MIME+URL"); // optional MIME+URL
                jsonObjectProperties.AddBoolean("isCached", false); // optional false
                jsonObjectProperties.AddBoolean("ignoreCache", false); // optional false 
                jsonObjectProperties.AddBoolean("clientCachingAllowed", false); // optional true 
                jsonObjectProperties.AddString("cacheDir", Path.Combine(ServiceCachePath , "directories/arcgiscache")); // optional
 
                jsonObject.AddJsonObject("properties", jsonObjectProperties);
 
                string result = GetResult(serviceUrl, "service=" + HttpUtility.UrlEncode(jsonObject.ToJson()) + "&f=json&token=" + token);
                
                if (HasSuccess(result))
                {
                    StartService(ServerName, ServiceType.MapServer, arcInfo);
                    ResultModel model = new ResultModel();
                    model.IsSuccessed = true;
                    model.ServiceUrl = arcInfo.HostName + "/rest/services/" + ServerName + "/" + Enum.GetName(typeof(ServiceType), ServiceType.MapServer);
                    return model;
                }
                    
                else return new ResultModel("error",false);
               
            }
            catch
            {
                return new ResultModel("error", false);
            }
        }
 
        /// <summary>
        /// check is status is equal success
        /// </summary>
        /// <param name="result">result of request</param>
        /// <returns>True if status is equal success</returns>
        private static bool HasSuccess(string result)
        {
            JsonObject jsonObject = new JsonObject(result);
            string status = null;
            if (!jsonObject.Exists("status") || !jsonObject.TryGetString("status", out status))
            {
                return false;
            }
 
            return status == "success";
        }
 
        /// <summary>
        ///错误信息判断
        /// </summary>
        /// <param name="result">result of request</param>
        /// <returns>True if status is equal error</returns>
        private static bool HasError(string result)
        {
            JsonObject jsonObject = new JsonObject(result);
            string status = null;
            if (!jsonObject.Exists("status") || !jsonObject.TryGetString("status", out status))
            {
                return false;
            }
 
            return status == "error";
        }
 
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">get 请求url</param>
        /// <returns>return response</returns>
        private static string GetResult(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
 
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="postContent">post content</param>
        /// <returns></returns>
        private static string GetResult(string url, string postContent)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                byte[] content = Encoding.UTF8.GetBytes(postContent);
                request.ContentLength = content.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = WebRequestMethods.Http.Post;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(content, 0, content.Length);
                    requestStream.Close();
                    WebResponse response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
 
        /// <summary>
        /// 产生token
        /// </summary>
        /// <returns>返回一个toke，采用默认的过期时间令牌</returns>
        private static string GenerateAGSToken(ArcGISManagerInfo arcInfo)
        {
            try
            {
                string urlGenerateToken = string.Format("{0}/generateToken", arcInfo.HostName + "/admin");
                string credential = string.Format("username={0}&password={1}&client=requestip&expiration=&f=json", arcInfo.MapServerUserName, arcInfo.MapserverPwd);
                string result = GetResult(urlGenerateToken, credential);
 
                JsonObject jsonObject = new JsonObject(result);
                string token = null;
                if (!jsonObject.Exists("token") || !jsonObject.TryGetString("token", out token))
                {
                    throw new Exception("Token not found!");
                }
 
                return token;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
