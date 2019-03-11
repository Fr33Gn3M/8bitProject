using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Configuration;

namespace TH.ArcGISServiceManager
{
    public class SdeConnect
    {
       public SdeConnect(SdeConfigInfo configInfo)
       {
           Init(configInfo);
       }

       private IWorkspace m_Workspace;
       public IWorkspace Workspace
       {
           get 
           {
               return m_Workspace;
           }
       }

        public string ConnectSde(string strServer, string strInstance, string strDatabase, string strUserName, string strPwd, string strVersion)
        {
            if (strVersion == "NULL")
            {
                return "版本信息丢失，请与系统管理员联系";
            }
            try
            {
                IPropertySet propertySet = new PropertySetClass();
                try
                {
                    propertySet.SetProperty("SERVER", strServer);
                    propertySet.SetProperty("INSTANCE", strInstance);
                    propertySet.SetProperty("DATABASE", strDatabase);
                    propertySet.SetProperty("USER", strUserName);
                    propertySet.SetProperty("PASSWORD", strPwd);
                    propertySet.SetProperty("VERSION", strVersion);
                }
                catch (Exception err)
                {
                    propertySet = null;
                    return err.Message;
                }

                IWorkspace workspace;
                IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
                if (propertySet == null)
                {
                    workspace = null;
                    return "属性集没有初始化。";
                }
                else
                {
                    workspace = factory.Open(propertySet, 0);
                    if (workspace != null)
                    {
                        m_Workspace = workspace;
                        workspace = null;
                        factory = null;
                        return "";
                    }
                    else
                    {
                        return "连接没有成功";
                    }
                }
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        private static bool IsInitialized = false;
        public static bool InitializeEngineLicense()
        {
            if (IsInitialized == false)
            {
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
                AoInitialize aoi = new AoInitialize();
                esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB;
                esriLicenseExtensionCode extensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst;
                if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable && aoi.IsExtensionCodeAvailable(productCode, extensionCode) == esriLicenseStatus.esriLicenseAvailable)
                {
                    aoi.Initialize(productCode);
                    aoi.CheckOutExtension(extensionCode);
                    IsInitialized = true;
                }
                else
                    IsInitialized = false;
            }
            return IsInitialized;
        }

        public void Init(SdeConfigInfo configInfo)
        { 

            string strReturn = ConnectSde(configInfo.SDEServer, configInfo.Instance, configInfo.DataBase, configInfo.SDEUser, configInfo.SDEPassWord, configInfo.SDEVersion);
            if (strReturn != "")
            {
                if (strReturn.IndexOf("Server machine not found") >= 0)
                    strReturn = "没有找到指定的SDE服务器";
                else if (strReturn.IndexOf("Bad login user") >= 0)
                    strReturn = "SDE服务器连接错误";
                else if (strReturn.IndexOf("SDE not running on server") >= 0)
                    strReturn = "请检查指定的SDE服务是否存在并正常运行";
                else if (strReturn.IndexOf("Entry for SDE instance not found in services file") >= 0)
                    strReturn = "SDE服务连接错误";
                else if (strReturn.IndexOf("Network I/O error") >= 0)
                    strReturn = "SDE服务连接错误";
                throw new Exception(strReturn);
            }
        } 

    }
}
