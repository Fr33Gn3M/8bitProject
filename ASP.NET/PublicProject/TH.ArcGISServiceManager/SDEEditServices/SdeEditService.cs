using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ArcGISServiceManager.SDEEditServices
{
   public class SdeEditService:ISdeEditService
    {

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
            SdeEditClass.EditSdeInfo(sdeLayerInfo, sdeDataModels,false);
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
