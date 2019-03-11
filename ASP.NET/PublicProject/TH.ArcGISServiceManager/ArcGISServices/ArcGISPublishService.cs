using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ArcGISServiceManager
{
   public class ArcGISPublishService:IArcGISPublishService
    {
        public int GetServiceCount()
        {
            return CreateArcGISService.GetServiceCount();
        }

        public ResultModel CreateServices(string ServerName, string ServerTitle, SdeConfigInfo sdeConfig, string featureLayerName, bool IsFeatureServer, SymbolBase symbols)
        {
            string mxdPath = System.Configuration.ConfigurationSettings.AppSettings["MxdFilePath"].ToString();
            var model = CreateArcGISService.CreateMxd(mxdPath, ServerName, sdeConfig, featureLayerName, symbols);
            if (model.IsSuccessed==true &&System.IO.File.Exists(model.MxdPath))
                model = CreateArcGISService.CreateServices(model.MxdPath, ServerName, IsFeatureServer);
            return model;
        }

        public ResultModel CreateServices(string ServerName, string ServerTitle, SdeConfigInfo sdeConfig, bool IsFeatureServer, IList<FeatureSymbolInfo> featureList,ArcGISManagerInfo arcInfo)
        {
            string mxdPath = System.Configuration.ConfigurationSettings.AppSettings["MxdFilePath"].ToString();
            var model = CreateArcGISService.CreateMxd(mxdPath, ServerName, sdeConfig, featureList);
            if (model.IsSuccessed == true && System.IO.File.Exists(model.MxdPath))
                //model = CreateArcGISService.CreateServicesNew(model.MxdPath, ServerName, IsFeatureServer);
                model = CreateArcGISServiceNew.CreateService(model.MxdPath, ServerName, IsFeatureServer, arcInfo);
            return model;
        }


        public SymbolBase[] GetGeoSymobols(GeometryType geometryType)
        {
           return CreateArcGISService.GetSymbols(geometryType);
        }

        public void CreateSde(SdeLayerInfo sdeLayer)
        {
            SdeEditClass.CreateSde(sdeLayer);
        }

        public FieldModel[] GetFieldModels(SdeLayerInfo sdeLayerInfo)
        {
            var sharpType = string.Empty;
            return SdeEditClass.GetFieldModel(sdeLayerInfo, ref sharpType);
        }

        public SdeDataInfo[] GetSdeDataInfos(SdeLayerInfo sdeLayerInfo, string queryWhere)
        {
            return SdeEditClass.GetSdeDataInfos(sdeLayerInfo, queryWhere);
        }

        public void EditSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels)
        {
            SdeEditClass.EditSdeInfo(sdeLayerInfo, sdeDataModels, false);
        }

        public void DeleteSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels)
        {
            SdeEditClass.EditSdeInfo(sdeLayerInfo, sdeDataModels, true);
        }

        public bool IsExistLayer(SdeConfigInfo sdeConfig, string layerName)
        {
            return SdeEditClass.IsExistLayer(sdeConfig, layerName);
        }
    }
}
