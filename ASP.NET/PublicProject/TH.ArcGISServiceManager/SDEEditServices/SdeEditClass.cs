using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.ServiceLocation;
using PH.ServerFramework.Logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TH.ArcGISServiceManager
{
    public class SdeEditClass
    {

        public static void DeleteLock(SdeConfigInfo sdeConfig)
        {
            OleDbDataBase.ConnectionString = "provider=SQLOLEDB;data source =" + sdeConfig.SDEServer + ";initial catalog = " + sdeConfig.DataBase + "; user id = " + sdeConfig.SDEUser + ";password = " + sdeConfig.SDEPassWord + ";";
            Exception ex = null;
            OleDbDataBase.ExecuteNonQuery("Delete from  sde_state_locks", out ex);
            OleDbDataBase.ExecuteNonQuery("Delete from  sde_table_locks", out ex);
            OleDbDataBase.ExecuteNonQuery("Delete from  sde_layer_locks", out ex);
            OleDbDataBase.ExecuteNonQuery("Delete from  sde_object_locks", out ex);
        }

        public static string[] GetSdeLayerNames(SdeConfigInfo sdeConfig)
        {
            OleDbDataBase.ConnectionString = "provider=SQLOLEDB;data source =" + sdeConfig.SDEServer + ";initial catalog = " + sdeConfig.DataBase + "; user id = " + sdeConfig.SDEUser + ";password = " + sdeConfig.SDEPassWord + ";";
            Exception ex = null;
            var table = OleDbDataBase.GetDataTable("select table_name from sde.SDE_layers where layer_id>1", out ex);
            var list = new List<string>();
            foreach (DataRow item in table.Rows)
                list.Add(item[0].ToString());
            return list.ToArray();
        }

        public static bool IsExistLayer(SdeConfigInfo sdeConfig,string layerName)
        {
            var list = GetSdeLayerNames(sdeConfig);
            bool isExist = false;
            var existList = list.Where(m => m.ToLower()==layerName.ToLower()).FirstOrDefault();
            if (existList != null)
                isExist = true;
            return isExist;
        }


        public static void CreateSde(SdeLayerInfo sdeLayer)
        {
            try
            {
                CreateSde(sdeLayer.SdeLayerName, sdeLayer.SdeTitleName, sdeLayer.SdeConfig, sdeLayer.FieldModels, sdeLayer.SpatialReference, sdeLayer.GeoType);
            }
            catch 
            {
            }
        }

        private  static void CreateSde(string sdeName, string sdeTitle, SdeConfigInfo sdeConfig, FieldModel[] fields, string spatialReference, GeometryType geometryType)
        {
            if (string.IsNullOrEmpty(spatialReference))
            {
                throw new Exception("没有空间参考");
            }
            var wsp = CreateArcGISService.OpenSdeFeatureWsp(sdeConfig);
            try
            {
                IFeatureWorkspace pFeatureWspEx = wsp as IFeatureWorkspace;
                DeleteLock(sdeConfig);
                IVersionedWorkspace pVerWorkspaceEdit = wsp as IVersionedWorkspace;
                IVersionEdit pVersionEdit = pVerWorkspaceEdit as IVersionEdit;

                IWorkspaceEdit pWorkspaceEdit = wsp as IWorkspaceEdit;
                pWorkspaceEdit.StartEditing(false);
                //打开工作空间进行编辑
                pWorkspaceEdit.StartEditOperation();
                //Data.ClsEditMethod.StartEditing();//创建编辑版


                IField oField = new FieldClass();
                IFields oFields = new FieldsClass();
                IFieldsEdit FieldsEdit = null;
                IFieldEdit oFieldEdit = null;
                IFieldsEdit oFieldsEdit = new FieldsClass();

                FieldsEdit = oFields as IFieldsEdit;
                oFieldEdit = oField as IFieldEdit;
                oFieldEdit.Name_2 = "OBJECTID";
                oFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                oFieldEdit.IsNullable_2 = false;
                oFieldEdit.Required_2 = false;
                oFieldsEdit.AddField(oFieldEdit);
                FieldsEdit = oFields as IFieldsEdit;
                foreach (var item in fields)
                {
                    if (item.FieldName == "OBJECTID" || item.FieldName == "OBJECTID_1")
                        continue;
                    oField = new FieldClass();
                    oFieldEdit = oField as IFieldEdit;
                    oFieldEdit.Name_2 = item.FieldName;
                    oFieldEdit.AliasName_2 = item.FieldAlias;
                    oFieldEdit.Type_2 = GetFieldType(item.FieldType);// esriFieldType.esriFieldTypeString;
                    oFieldEdit.IsNullable_2 = true;
                    oFieldEdit.Length_2 = item.FieldLength == null ? 255 : item.FieldLength.Value;
                    oFieldsEdit.AddField(oFieldEdit);
                }

                oField = new FieldClass();
                oFieldEdit = oField as IFieldEdit;
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.AvgNumPoints_2 = 5;
                if (geometryType == GeometryType.Point)
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                else if (geometryType == GeometryType.Polygon)
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                else if (geometryType == GeometryType.PolyLine)
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                pGeoDefEdit.GridCount_2 = 1;
                pGeoDefEdit.HasM_2 = false;
                pGeoDefEdit.HasZ_2 = false;
                ISpatialReferenceFactory2 pSpatRefFact = new SpatialReferenceEnvironmentClass();
                ISpatialReference pSR1 = null;
                int rd;
                pSpatRefFact.CreateESRISpatialReference(spatialReference, out pSR1, out rd);
                // var pSRR = pSpatRefFact.CreateESRISpatialReferenceFromPRJ(spatialReference);
                // ISpatialReference pSR = (ISpatialReference)pSRR;
                pGeoDefEdit.SpatialReference_2 = pSR1; //pSpatRefFact.CreateGeographicCoordinateSystem(4326);
                //pSR1;// Common.CommonClass.axMap.SpatialReference;
                oFieldEdit.Name_2 = "SHAPE";
                oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                oFieldEdit.GeometryDef_2 = pGeoDef;
                oFieldEdit.IsNullable_2 = true;
                oFieldEdit.Required_2 = true;
                oFieldsEdit.AddField(oFieldEdit);
                oFields = oFieldsEdit;

                var aClass = pFeatureWspEx.CreateFeatureClass(sdeName, oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", null);
                if (!string.IsNullOrEmpty(sdeTitle))
                {
                    IClassSchemaEdit aClassEdit = aClass as IClassSchemaEdit;
                    if (aClassEdit != null) aClassEdit.AlterAliasName(sdeTitle);
                }
                System.Threading.Thread.Sleep(2000);
                IVersionedObject2 pVersionedObject = aClass as IVersionedObject2;
                //try
                //{
                //    if (pVersionedObject != null)
                //    {
                //        if (pVersionedObject.HasUncompressedEdits)
                //        {
                //            IVersionedWorkspace pVerWorkspaceEdit2 = wsp as IVersionedWorkspace;
                //            pVerWorkspaceEdit.Compress();
                //            pVerWorkspaceEdit = null;
                //        }
                //        pVersionedObject.RegisterAsVersioned(false);
                //    }
                //}
                //catch
                //{ }
                try
                {
                    pVersionedObject.RegisterAsVersioned(true);
                    pVersionedObject = null;
                    aClass = null;
                }
                catch (Exception ex2)
                {
                }

                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);

                //版本注册

                pWorkspaceEdit = null;
                wsp = null;

            }
            catch (Exception e)
            {
                wsp = null;
                //var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                //log.Log(LogLevel.Debug, e.Message);
                //throw new Exception("SDE");
            }

        }

        public static esriFieldType GetFieldType(string type)
        {
            if (type.Contains("esriFieldType"))
            {
                var dbType1 = (esriFieldType)Enum.Parse(typeof(esriFieldType), type);
                return dbType1;
            }
            esriFieldType fieldType = esriFieldType.esriFieldTypeString;
            try
            {
                var dbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), type);
                switch (dbType)
                {
                    case SqlDbType.Bit:
                    case SqlDbType.Int:
                        fieldType = esriFieldType.esriFieldTypeInteger;
                        break;
                    case SqlDbType.UniqueIdentifier:
                        fieldType = esriFieldType.esriFieldTypeGUID;
                        break;
                    case SqlDbType.DateTime:
                    case SqlDbType.Date:
                    case SqlDbType.DateTime2:
                        fieldType = esriFieldType.esriFieldTypeDate;
                        break;
                    case SqlDbType.Float:
                    case SqlDbType.Decimal:
                        fieldType = esriFieldType.esriFieldTypeDouble;
                        break;

                    case SqlDbType.NVarChar:
                    case SqlDbType.VarChar:
                    case SqlDbType.Text:
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                    default:
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                }
            }
            catch
            {
            }
            return fieldType;
        }

        public static string GetFieldTypeStr(string type)
        {
            if (type.Contains("esriFieldType"))
            {
                var dbType1 = (esriFieldType)Enum.Parse(typeof(esriFieldType), type);
                return dbType1.ToString();
            }
            esriFieldType fieldType = esriFieldType.esriFieldTypeString;
            try
            {
                var dbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), type);
                switch (dbType)
                {
                    case SqlDbType.Bit:
                    case SqlDbType.Int:
                        fieldType = esriFieldType.esriFieldTypeInteger;
                        break;
                    case SqlDbType.UniqueIdentifier:
                        fieldType = esriFieldType.esriFieldTypeGUID;
                        break;
                    case SqlDbType.DateTime:
                    case SqlDbType.Date:
                    case SqlDbType.DateTime2:
                        fieldType = esriFieldType.esriFieldTypeDate;
                        break;
                    case SqlDbType.Float:
                    case SqlDbType.Decimal:
                        fieldType = esriFieldType.esriFieldTypeDouble;
                        break;

                    case SqlDbType.NVarChar:
                    case SqlDbType.VarChar:
                    case SqlDbType.Text:
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                    default:
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                }
            }
            catch
            {
            }
            return fieldType.ToString();
        }



        private static string GetFieldType(esriFieldType type)
        {
            var fieldType = SqlDbType.VarChar;
            try
            {
                switch (type)
                {
                    case esriFieldType.esriFieldTypeInteger:
                        fieldType = SqlDbType.Int;
                        break;
                    case esriFieldType.esriFieldTypeGUID:
                        fieldType = SqlDbType.UniqueIdentifier;
                        break;
                    case esriFieldType.esriFieldTypeDate:
                        fieldType = SqlDbType.DateTime;
                        break;

                    case esriFieldType.esriFieldTypeDouble:
                        fieldType = SqlDbType.Decimal;
                        break;
                    case esriFieldType.esriFieldTypeString:
                        fieldType = SqlDbType.VarChar;
                        break;
                    case esriFieldType.esriFieldTypeBlob:
                        fieldType = SqlDbType.VarBinary;
                        break;
                
                    case esriFieldType.esriFieldTypeOID:
                        fieldType = SqlDbType.Int;
                        break;
                    case esriFieldType.esriFieldTypeXML:
                        fieldType =SqlDbType.Xml;
                        break;
           
                    case esriFieldType.esriFieldTypeGeometry:
                        break;
                    default:
                        fieldType = SqlDbType.VarChar;
                        break;
                }
            }
            catch
            {
            }
            return fieldType.ToString();
        }

        public static string GetFieldTypeStr2(string type)
        {
            //if (type.Contains("esriFieldType"))
            //{
            //    var dbType1 = (esriFieldType)Enum.Parse(typeof(esriFieldType), type);
            //    return dbType1.ToString();
            //}
            esriFieldType fieldType = esriFieldType.esriFieldTypeString;
            try
            {
                var dbType = (SqlDbType)Enum.Parse(typeof(System.Data.OleDb.OleDbType), type);
                switch (type.ToLower())
                {
                    case "int":
                        fieldType = esriFieldType.esriFieldTypeInteger;
                        break;
                    case "uniqueIdentifier":
                        fieldType = esriFieldType.esriFieldTypeGUID;
                        break;
                    case "datetime":
                    case "date":
                    case "datetime2":
                        fieldType = esriFieldType.esriFieldTypeDate;
                        break;
                    case "float":
                    case "decimal":
                        fieldType = esriFieldType.esriFieldTypeDouble;
                        break;

                    case "varchar":
                    case "char":
                    case "nvarchar":
                    case "text":
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                    case "varbin":
                    case "varbinary":
                        fieldType = esriFieldType.esriFieldTypeBlob;
                        break;
                    default:
                        fieldType = esriFieldType.esriFieldTypeString;
                        break;
                }
            }
            catch
            {
            }
            return fieldType.ToString();
        }


        private static int GetNumIndex(string fieldName, IFeatureClass featureClass)
        {
            return featureClass.Fields.FindField(fieldName);
        }

        private static FieldModel[] GetFieldModel(IFeatureClass featureClass)
        {
            var list = new List<FieldModel>();
            for (int i = 0; i < featureClass.Fields.FieldCount; i++)
            {
                var field = featureClass.Fields.get_Field(i);
                var model = new FieldModel();
                model.FieldAlias = field.AliasName;
                model.FieldName = field.Name;
                model.FieldType = field.Type.ToString();   //GetFieldType(field.Type);
                //model.IsPrimaryKey = field.IsIdentical(field.Clone());
                model.IsPrimaryKey = field.Required;
                model.FieldLength = field.Length;
                model.IsNull = field.IsNullable;
                model.Editable = field.Editable;
                if (model.FieldName.ToLower() == "shape" || model.FieldName.ToLower() == "shape.area" || model.FieldName.ToLower() == "shape.len")
                    continue;
                list.Add(model);
            }
            return list.ToArray();
        }

        public static FieldModel[] GetFieldModel(SdeLayerInfo sdeLayerInfo, ref string shapeType)
        {
            var sdeConn = SdeConnectFactory.CreateSdeConnect(sdeLayerInfo.SdeConfig);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)sdeConn.Workspace;
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(sdeLayerInfo.SdeLayerName);
            var feilds = GetFieldModel(featureClass);
            shapeType = featureClass.ShapeType.ToString();
            featureWorkspace = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
            featureClass = null;
            return feilds;
        }

        public static SdeDataInfo[] GetSdeDataInfos(SdeLayerInfo sdeLayerInfo,string queryWhere)
        {
            var list = new List<SdeDataInfo>();
            var sdeConn = SdeConnectFactory.CreateSdeConnect(sdeLayerInfo.SdeConfig);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)sdeConn.Workspace;
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(sdeLayerInfo.SdeLayerName);
            int count = 0;
            IFeatureCursor pFCurson = null;

            if (string.IsNullOrEmpty(queryWhere))
            {
                count = featureClass.FeatureCount(null);
                pFCurson = featureClass.Search(null, false);
            }
            else
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = queryWhere;
                count = featureClass.FeatureCount(queryFilter);
                pFCurson = featureClass.Search(queryFilter, false);
            }
            IFeature pFea = null;
           // IFields fields = featureClass.Fields as FieldsClass;
            pFea = pFCurson.NextFeature();
            while (pFea != null)
            {
                SdeDataInfo sdeData = new SdeDataInfo();
                //sdeData.CoordName = "";
                sdeData.IsSdeData = true;
                sdeData.Data = new Dictionary<string, object>();
                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    IField field = featureClass.Fields.get_Field(i);
                    var obj = pFea.get_Value(i);
                    sdeData.Data.Add(field.Name, obj);
                }
                sdeData.Geom = pFea.ShapeCopy;
               // var Igeom = pFea.ShapeCopy as  IGeometryCollection;
                pFea = pFCurson.NextFeature();
                list.Add(sdeData);
            }

            pFCurson = null;
            featureWorkspace = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
            featureClass = null;
            return list.ToArray();
        }

        //private static bool IsGeoSimlpe(IGeometry geom)
        //{
        //    if (geom.GeometryType == esriGeometryType.esriGeometryLine || geom.GeometryType == esriGeometryType.esriGeometryPolyline)
        //    {
        //        var line = geom as PolylineClass;
        //        var line2 = line.Clone() as PolylineClass;
        //        line2.Simplify();
        //        if (line2.PointCount < 2)
        //            return false;
        //    }
        //    if (geom.GeometryType == esriGeometryType.esriGeometryPolygon)
        //    {
        //        var line = geom as PolygonClass;
        //        var line2 = line.Clone() as PolygonClass;
        //        line2.Simplify();
        //        if (line2.PointCount < 3)
        //            return false;
        //    }
        //    return true;
        //}

        public static void EditSdeInfo(SdeLayerInfo sdeLayerInfo, SdeDataInfo[] sdeDataModels, bool IsDeleted)
        {
            try
            {
                var sdeConn = SdeConnectFactory.CreateSdeConnect(sdeLayerInfo.SdeConfig);
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)sdeConn.Workspace;
                IFeatureClass featureClass = featureWorkspace.OpenFeatureClass("sde." + sdeLayerInfo.SdeLayerName);
                IDataset ds = (IDataset)featureClass;
                IWorkspaceEdit pws = (IWorkspaceEdit)ds.Workspace;
                //IVersionedWorkspace pVersion = featureWorkspace as IVersionedWorkspace;
                //IVersionEdit pVersionEdit = pVersion as IVersionEdit;
                pws.StartEditing(false);
                pws.StartEditOperation();
                //bool success = pVersionEdit.Reconcile(sdeLayerInfo.SdeConfig.SDEVersion);
                IFeatureCursor pFCurson = null;
                foreach (var sdeDataModel in sdeDataModels)
                {
                    //var geo = sdeDataModel.Geom as IGeometry;
                    //bool result = IsGeoSimlpe(geo);
                    //if (result == false)
                    //    continue;
                    IFeature pFea = null;
                    IQueryFilter queryFilter = new QueryFilterClass();
                    var model = sdeLayerInfo.FieldModels.Where(m => m.IsPrimaryKey == true).FirstOrDefault();
                    if (model!=null&&model.FieldName != "OBJECTID_1")
                    {
                        if (sdeDataModel.Data.ContainsKey(model.FieldName))
                        {
                            // string queryStr = string.Format("{0} = '{1}'", "O_OBJECTID", sdeDataModel.Data[model.FieldName]);
                             string queryStr = string.Format("{0} = '{1}'", model.FieldName, sdeDataModel.Data[model.FieldName]);
                            queryFilter.WhereClause = queryStr;
                            pFCurson = featureClass.Search(queryFilter, false);
                            pFea = pFCurson.NextFeature();
                        }
                    }
                    if (pFea != null && IsDeleted == true)
                        pFea.Delete();
                    else
                    {
                        if (pFea == null)
                            pFea = featureClass.CreateFeature();
                        SaveFeature(sdeLayerInfo, pFea, featureClass, sdeDataModel);
                    }
                    if (pFea != null)
                        pFea.Store();
                }
                if (pFCurson != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCurson);
                pFCurson = null;
                pws.StopEditing(true);
                pws.StopEditOperation();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ds);
                ds = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                featureClass = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveFeature(SdeLayerInfo sdeLayerInfo, IFeature pFea, IFeatureClass featureClass, SdeDataInfo sdeDataModel)
        {
            foreach (var item in sdeLayerInfo.FieldModels)
            {
                if (item.FieldName.ToLower() != "objectid" && item.FieldName.ToLower() != "objectid_1" && sdeDataModel.Data.ContainsKey(item.FieldName))
                    pFea.set_Value(GetNumIndex(item.FieldName, featureClass), sdeDataModel.Data[item.FieldName]);
            }
            var geo = sdeDataModel.Geom as IGeometry;
            pFea.Shape = geo;

        }

        public static double[] Mercator2LonLat(double X, double Y)
        {
            double[] d = new double[2];
            double x = X / 20037508.34 * 180;
            double y = Y / 20037508.34 * 180;
            y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2);
            d[0] = x;
            d[1] = y;
            return d;
        }

        public static double[] LonLat2Mercator(double X, double Y)
        {
            double[] d = new double[2];
            double x = X * 20037508.34 / 180;
            double y = Math.Log(Math.Tan((90 + Y) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
            d[0] = x;
            d[1] = y;
            return d;
        }
    }
}