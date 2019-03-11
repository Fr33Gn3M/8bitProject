using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [ServiceContract]
    public interface IArcGISPublishService
    {
        [OperationContract]
        int GetServiceCount();
        [OperationContract]
        ResultModel CreateServices(string ServerName, string ServerTitle, SdeConfigInfo sdeConfig, string featureLayerName, bool IsFeatureServer,SymbolBase symbols);
        [OperationContract]
        SymbolBase[] GetGeoSymobols(GeometryType geometryType);
        /// <summary>
        /// 创建SDE
        /// </summary>
        /// <param name="sdeLayer"></param>
        [OperationContract]
        void CreateSde(SdeLayerInfo sdeLayer);
        /// <summary>
        /// 得到SDE字段信息
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <returns></returns>
        [OperationContract]
        FieldModel[] GetFieldModels(SdeLayerInfo sdeLayerInfo);
        /// <summary>
        /// 查询SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        [OperationContract]
        SdeDataInfo[] GetSdeDataInfos(SdeLayerInfo sdeLayerInfo, string queryWhere);
        /// <summary>
        /// 编辑SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="sdeDataModels"></param>
        /// <returns></returns>
        [OperationContract]
        void EditSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels);
        /// <summary>
        /// 删除SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="sdeDataModels"></param>
        [OperationContract]
        void DeleteSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels);
        [OperationContract]
        bool IsExistLayer(SdeConfigInfo sdeConfig, string layerName);
    }
}
