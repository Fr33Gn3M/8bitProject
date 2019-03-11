using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ArcGISServiceManager.SDEEditServices
{
    public interface ISdeEditService
    {
        /// <summary>
        /// 创建SDE
        /// </summary>
        /// <param name="sdeLayer"></param>
        void CreateSde(SdeLayerInfo sdeLayer);
        /// <summary>
        /// 得到SDE字段信息
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <returns></returns>
        FieldModel[] GetFieldModels(SdeLayerInfo sdeLayerInfo);
        /// <summary>
        /// 查询SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        SdeDataInfo[] GetSdeDataInfos(SdeLayerInfo sdeLayerInfo, string queryWhere);
        /// <summary>
        /// 编辑SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="sdeDataModels"></param>
        /// <returns></returns>
        void EditSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels);
        /// <summary>
        /// 删除SDE数据
        /// </summary>
        /// <param name="sdeLayerInfo"></param>
        /// <param name="sdeDataModels"></param>
        void DeleteSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels);
        bool IsExistLayer(SdeConfigInfo sdeConfig, string layerName);
    }
}
