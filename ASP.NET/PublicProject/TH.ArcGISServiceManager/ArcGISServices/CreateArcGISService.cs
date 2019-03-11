using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Server;
using Microsoft.Practices.ServiceLocation;
using PH.ServerFramework.Logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace TH.ArcGISServiceManager
{
    public class CreateArcGISService
    {
        public static void Init()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            if (!InitializeEngineLicense())
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "没有GIS许可，系统将无法显示地图！");
               // MessageBox.Show("没有GIS许可，系统将无法显示地图");
            }
        }

        private static bool InitializeEngineLicense()
        {
            //AoInitialize aoi = new AoInitialize();
            //esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeArcInfo;
            //esriLicenseExtensionCode extensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst;

            //if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable && aoi.IsExtensionCodeAvailable(productCode, extensionCode) == esriLicenseStatus.esriLicenseAvailable)
            //{
            //    aoi.Initialize(productCode);
            //    aoi.CheckOutExtension(extensionCode);
            //    return true;
            //}
            //else
            //    return false;


            IAoInitialize m_AoInitialize = new AoInitializeClass();
            esriLicenseStatus pLicesestatus = (esriLicenseStatus)m_AoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            if (pLicesestatus == esriLicenseStatus.esriLicenseAvailable)
            {
                if (pLicesestatus != esriLicenseStatus.esriLicenseCheckedOut)
                {
                    pLicesestatus = (esriLicenseStatus)m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
                }
                else
                {
                    return false;
                   // System.Windows.Forms.MessageBox.Show("程序初始化失败");
                }
            }
            else
            {
                return false;
               // System.Windows.Forms.MessageBox.Show("没有程序运行许可");
            }
            return true;

        }

        
        static string HostName = System.Configuration.ConfigurationSettings.AppSettings["HostName"].ToString();
        static string MapServerUserName = System.Configuration.ConfigurationSettings.AppSettings["MapServerUserName"].ToString();
        static string MapserverPwd = System.Configuration.ConfigurationSettings.AppSettings["MapserverPwd"].ToString();
        static string serviceCachePath = System.Configuration.ConfigurationSettings.AppSettings["serviceCachePath"].ToString();
        //tmj 20150104
        static string StyleFilePath = System.Configuration.ConfigurationSettings.AppSettings["StyleFilePath"].ToString();
        static string StyleFieldName = System.Configuration.ConfigurationSettings.AppSettings["StyleFieldName"].ToString();
        static ISymbol DefPointSymbol = null;
        static ISymbol DefLineSymbol = null;
        static ISymbol DefFillSymbol = null;

        public static ResultModel CreateMxd(string MxdPath, string MxdName, SdeConfigInfo sdeConfig, string featureLayerName, SymbolBase symbols)
        {
            if (!System.IO.Directory.Exists(MxdPath))
                System.IO.Directory.CreateDirectory(MxdPath);

            IMapDocument pMapDocument = new MapDocumentClass();
            if (MxdPath.Substring(MxdPath.Length - 1) != @"\")
                MxdPath += @"\";
            string path = MxdPath + MxdName + ".mxd";
            if (File.Exists(path))
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "mxd文件已存在！");
                pMapDocument = null;
                var model1 = new ResultModel("mxd文件已存在！", true);
                model1.MxdPath = path;
                return model1;
                // return "mxd文件已存在！";
            }
            pMapDocument.New(path);
            pMapDocument.Open(path,"");
            AddLayerToMxd(pMapDocument, MxdName, sdeConfig, featureLayerName,symbols);
            if (pMapDocument == null)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "无法创建!");
                return new ResultModel("无法创建！",false);
            }
            // return "无法创建";
            if (pMapDocument.get_IsReadOnly(MxdPath + MxdName + ".mxd") == true)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "mxd文件为只读文件!");
                return new ResultModel("mxd文件为只读文件！",false);
            }
            try
            {
                pMapDocument.Save(true, false);
            }
            catch (Exception ex)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, ex.Message);
            }
            pMapDocument.Close();
            pMapDocument = null;
            var model = new ResultModel();
            model.MxdPath = path;
            return model;
        }

        public static ResultModel CreateMxd(string MxdPath, string MxdName, SdeConfigInfo sdeConfig, IList<FeatureSymbolInfo> featureList)
        {
            if (!System.IO.Directory.Exists(MxdPath))
                System.IO.Directory.CreateDirectory(MxdPath);

            IMapDocument pMapDocument = new MapDocumentClass();
            if (MxdPath.Substring(MxdPath.Length - 1) != @"\")
                MxdPath += @"\";
            string path = MxdPath + MxdName + ".mxd";
            if (File.Exists(path))
            {
                
                //var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                //log.Log(LogLevel.Info, "mxd文件已存在！");
                //pMapDocument = null;
                //var model1 = new ResultModel("mxd文件已存在！", true);
                //model1.MxdPath = path;
                //return model1;
                // return "mxd文件已存在！";
                File.Delete(path);
                string msdpath = MxdPath + MxdName + ".msd";
                File.Delete(msdpath);
            }
            pMapDocument.New(path);
            pMapDocument.Open(path, "");
            foreach (var item in featureList)
            {
                AddRangeLayerToMxd(pMapDocument, MxdName, sdeConfig, item);
            }
            if (pMapDocument == null)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "无法创建!");
                return new ResultModel("无法创建！", false);
            }
            // return "无法创建";
            if (pMapDocument.get_IsReadOnly(MxdPath + MxdName + ".mxd") == true)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "mxd文件为只读文件!");
                return new ResultModel("mxd文件为只读文件！", false);
            }
            try
            {
                pMapDocument.Save(true, false);
            }
            catch (Exception ex)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, ex.Message);
            }
            pMapDocument.Close();
            pMapDocument = null;
            var model = new ResultModel();
            model.MxdPath = path;
            return model;
        }

        /******************* 2014-12-31 tmj Start **********************************/
        /// <summary>
        /// 添加图层到MXD,并符号化图层
        /// </summary>
        /// <param name="pMapDocument"></param>
        /// <param name="MxdName"></param>
        /// <param name="sdeConfig"></param>
        /// <param name="featureLayerName"></param>
        /// <param name="symbols"></param>
        /// <returns></returns>
        private static bool AddLayerToMxd(IMapDocument pMapDocument, string MxdName, SdeConfigInfo sdeConfig, string featureLayerName, SymbolBase symbols)
        {
            IMap pMap = pMapDocument.get_Map(0);
            //IMap pMap = new Map() ;
            IMapLayers mapLayer = pMap as IMapLayers;
            mapLayer.ClearLayers();
            IFeatureClass pFeatureClass = GetFeatureClass(sdeConfig,featureLayerName);
            IFeatureLayer pFLayer = new FeatureLayerClass();
            pFLayer.FeatureClass = pFeatureClass;
            pFLayer.Name = featureLayerName;

            ILayer layer = pFLayer as ILayer;
            if (symbols != null && symbols.Styles != null && symbols.Styles.Count() > 0)//无符号的话，则系统随机符号
            {
                //symbols对象里无pSymbol信息，故需要获取符号列表，获取真正有符号的列表信息
                List<GeoStyle> geoStyles = new List<GeoStyle>();
                SymbolBase[] sbs = null;
                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    sbs = GetSymbols(GeometryType.Point);
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    sbs = GetSymbols(GeometryType.PolyLine);
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    sbs = GetSymbols(GeometryType.Polygon);
                }
                if (sbs != null && sbs.Length > 0 && sbs[0].Styles.Length > 0)
                {
                    foreach (var gs in symbols.Styles)
                    {
                        foreach (var gs2 in sbs[0].Styles)
                        {
                            if (gs.Name == gs2.Name && gs.Category == gs2.Category)
                            {
                                geoStyles.Add(gs2);
                                break;
                            }
                        }
                    }

                    DrawLayerStyle(layer, StyleFieldName,geoStyles);
                }
            }
            mapLayer.AddLayer(layer);

            IMxdContents pMxdC = pMap as IMxdContents;
            pMapDocument.ReplaceContents(pMxdC);
            //pMapDocument.SetActiveView(pMxdC.ActiveView);
            pFeatureClass = null;
            return true;
        }

        /// <summary>
        /// 添加图层到MXD,并符号化图层,不清空之前的图层
        /// </summary>
        /// <param name="pMapDocument"></param>
        /// <param name="MxdName"></param>
        /// <param name="sdeConfig"></param>
        /// <param name="featureLayerName"></param>
        /// <param name="symbols"></param>
        /// <returns></returns>
        private static bool AddRangeLayerToMxd(IMapDocument pMapDocument, string MxdName, SdeConfigInfo sdeConfig, FeatureSymbolInfo fe)
        {
            IMap pMap = pMapDocument.get_Map(0);
            //IMap pMap = new Map() ;
            pMap.Name = "Layers";
            IMapLayers mapLayer = pMap as IMapLayers;
            //mapLayer.ClearLayers();
            IFeatureClass pFeatureClass = GetFeatureClass(sdeConfig, fe.featureLayerName);

            //QI到IGeoDataset
            IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
            //QI到IGeoDatasetSchemaEdit
            IGeoDatasetSchemaEdit pGeoDatasetSchemaEdit = pGeoDataset as IGeoDatasetSchemaEdit;
            if (pGeoDatasetSchemaEdit.CanAlterSpatialReference == true)
            {
                //创建SpatialReferenceEnvironmentClass对象
                ISpatialReferenceFactory2 pSpaRefFactory = new SpatialReferenceEnvironmentClass();
                //创建地理坐标系对象
                IGeographicCoordinateSystem pNewGeoSys = pSpaRefFactory.CreateGeographicCoordinateSystem(4490);//4214代表Beijing1954
                pGeoDatasetSchemaEdit.AlterSpatialReference(pNewGeoSys);
            }

            IFeatureLayer pFLayer = new FeatureLayerClass();
            pFLayer.FeatureClass = pFeatureClass;
            pFLayer.Name = fe.featureLayerName;

            ILayer layer = pFLayer as ILayer;
            if (fe.symbols != null && fe.symbols.Styles != null && fe.symbols.Styles.Count() > 0)//无符号的话，则系统随机符号
            {
                //symbols对象里无pSymbol信息，故需要获取符号列表，获取真正有符号的列表信息
                List<GeoStyle> geoStyles = new List<GeoStyle>();
                SymbolBase[] sbs = null;
                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    sbs = GetSymbols(GeometryType.Point);
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    sbs = GetSymbols(GeometryType.PolyLine);
                }
                else if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    sbs = GetSymbols(GeometryType.Polygon);
                }
                if (sbs != null && sbs.Length > 0 && sbs[0].Styles.Length > 0)
                {
                    foreach (var gs in fe.symbols.Styles)
                    {
                        foreach (var gs2 in sbs[0].Styles)
                        {
                            if (gs.Name == gs2.Name && gs.Category == gs2.Category)
                            {
                                geoStyles.Add(gs2);
                                break;
                            }
                        }
                    }
                    if(fe.renderer!=null)
                        DrawLayerStyle(layer, fe.renderer);
                    else
                        DrawLayerStyle(layer, StyleFieldName, geoStyles);
                }
            }

            //ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            //ISpatialReference iSpa = pSpatialReferenceFactory.CreateGeographicCoordinateSystem(4490);
            //layer.SpatialReference = iSpa;
            mapLayer.AddLayer(layer);

            IMxdContents pMxdC = pMap as IMxdContents;
            pMapDocument.ReplaceContents(pMxdC);
            //pMapDocument.SetActiveView(pMxdC.ActiveView);
            pFeatureClass = null;
            return true;
        }

        public static SymbolBase GetSymbol(GeometryType type, string styleName)
        {
            var symbols = GetSymbols(type);
            var symbol = new SymbolBase();
            foreach (var gs2 in symbols[0].Styles)
            {
                if (styleName == gs2.Name)
                {
                    symbol.Styles = new GeoStyle[] { gs2 };
                    break;
                }
            }
            return symbol;
        }

        public static SymbolBase GetSymbol(string typeName, string styleName)
        {
            GeometryType type = (GeometryType)Enum.Parse(typeof(GeometryType), typeName);
            return GetSymbol(type, styleName);
        }

        /// <summary>
        /// 获取符号文件列表
        /// </summary>
        /// <returns></returns>
        public static SymbolBase[] GetSymbols(GeometryType geometryType)
        {
            IStyleGallery pStyleGallery = new ServerStyleGallery();
            IStyleGalleryStorage pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
            pStyleGalleryStorage.AddFile(StyleFilePath);
            string[] strClassName = null;
            if (geometryType == GeometryType.Point)
            {
                strClassName = new String[] { "Marker Symbols" };   //读取点样式
            }
            else if (geometryType == GeometryType.PolyLine)
            {
                strClassName = new String[] {  "Line Symbols"};    //读取线样式
            }
            else if (geometryType == GeometryType.Polygon)
            {
                strClassName = new String[] {  "Fill Symbols" };    //读取面样式
            }
            string className = "";
            IEnumStyleGalleryItem pEnumStyleGalleryItem = null;
            IStyleGalleryItem pStyleGalleryItem = null;
            List<SymbolBase> symbolList = new List<SymbolBase>();
            for (int i = 0; i < strClassName.Length; i++)
            {
                try
                {
                    SymbolBase s = new SymbolBase();
                    List<GeoStyle> geoList = new List<GeoStyle>();
                    className = strClassName[i];
                    pEnumStyleGalleryItem = pStyleGallery.get_Items(className, StyleFilePath, "");

                    pEnumStyleGalleryItem.Reset();
                    pStyleGalleryItem = pEnumStyleGalleryItem.Next();
                    while (pStyleGalleryItem != null)
                    {
                        GeoStyle gs = new GeoStyle();
                        gs.ClassName = className;
                        gs.Name = pStyleGalleryItem.Name;
                        gs.Category = pStyleGalleryItem.Category;
                        gs.pSymbol = pStyleGalleryItem.Item as ISymbol;
                        Image img = StyleToImage(gs.pSymbol, 32, 32);
                        if (img != null)
                        {
                            gs.Image = ImageToBytes(img);
                        }
                        geoList.Add(gs);
                        pStyleGalleryItem = pEnumStyleGalleryItem.Next();
                    }
                    if (geoList.Count != 0)
                    {
                        s.Styles = geoList.ToArray();
                        symbolList.Add(s);
                    }
                }
                catch
                { }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
            pEnumStyleGalleryItem = null;
            if (symbolList.Count > 0)
            {
                SymbolBase[] sbs = symbolList.ToArray();
                return sbs;
            }
            else
            {
                return new SymbolBase[0];
            }
        }

        /// <summary>
        /// 符号化
        /// </summary>
        /// <param name="layer"></param>
        public static void DrawLayerStyle(ILayer pLayer, string FieldName, List<GeoStyle> geoStyles)
        {
            if (geoStyles.Count == 1)
                DrawLayerStyle(pLayer, geoStyles[0]);
            else
            {
                IUniqueValueRenderer pUVR = null;
                IGeoFeatureLayer pGFL = null;
                pGFL = pLayer as IGeoFeatureLayer;

                int iCode = pGFL.FeatureClass.FindField(FieldName);
                if (iCode >= 0)
                {
                    pUVR = new UniqueValueRenderer();
                    pUVR.UseDefaultSymbol = false;
                    pUVR.FieldCount = 1;
                    pUVR.set_Field(0, FieldName);
                    if (geoStyles.Count > 0)
                    {
                        foreach (var gs in geoStyles)
                        {
                            pUVR.AddValue(gs.Name, FieldName, gs.pSymbol);
                            pUVR.set_Symbol(gs.Name, gs.pSymbol);
                        }
                    }
                }
                if (pUVR != null)
                {
                    pGFL.Renderer = pUVR as IFeatureRenderer;
                    pGFL.DisplayField = FieldName;
                }
            }
        }

        /// <summary>
        /// 根据配置的渲染样式进行渲染
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="renderer"></param>
        public static void DrawLayerStyle(ILayer pLayer, Dictionary<string, object> renderer)
        {
            //简单渲染
            if (renderer["type"].ToString() == "simple")
            {
                IGeoFeatureLayer pGFL = pLayer as IGeoFeatureLayer;
                ISimpleRenderer pUVR = null;
                pUVR = new SimpleRenderer();
                pUVR.Symbol = renderer["symbol"] as ISymbol;
                if (pUVR != null)
                {
                    pGFL.Renderer = pUVR as IFeatureRenderer;
                }
            }
            //唯一值渲染
            else
            {
                IUniqueValueRenderer pUVR = null;
                IGeoFeatureLayer pGFL = null;
                pGFL = pLayer as IGeoFeatureLayer;

                string FieldName = renderer["field1"].ToString();
                int iCode = pGFL.FeatureClass.FindField(FieldName);
                if (iCode >= 0)
                {
                    pUVR = new UniqueValueRenderer();
                    pUVR.DefaultLabel = renderer.ContainsKey("defaultLabel") && renderer["defaultLabel"] != null ? renderer["defaultLabel"].ToString() : "";
                    pUVR.DefaultSymbol = renderer["defaultSymbol"] as ISymbol;
                    pUVR.UseDefaultSymbol = true;
                    pUVR.FieldCount = 1;
                    pUVR.set_Field(0, FieldName);
                    

                    var uniqueValueInfos = renderer["uniqueValueInfos"] as Dictionary<string,object>[];

                    foreach (var item in uniqueValueInfos)
                    {
                        pUVR.AddValue(item["value"].ToString(), FieldName, item["symbol"] as ISymbol);
                        pUVR.set_Symbol(item["value"].ToString(), item["symbol"] as ISymbol);
                    }
                }
                if (pUVR != null)
                {
                    pGFL.Renderer = pUVR as IFeatureRenderer;
                    pGFL.DisplayField = FieldName;
                }
            }
        }

        /// <summary>
        /// 图层字段符号化
        /// </summary>
        /// <param name="layer"></param>
        public static void DrawLayerStyle(ILayer pLayer,GeoStyle geoStyles)
        {
            IGeoFeatureLayer pGFL  = pLayer as IGeoFeatureLayer;
            ISimpleRenderer pUVR = null;
            pUVR = new SimpleRenderer();
            pUVR.Symbol = geoStyles.pSymbol;
            if (pUVR != null)
            {
                pGFL.Renderer = pUVR as IFeatureRenderer;
            }
        }

        /// <summary>
        /// 创建默认点符号
        /// </summary>
        /// <returns></returns>
        public static ISymbol CreatePointSymbol()
        {
            if (DefPointSymbol == null)
            {
                ISimpleMarkerSymbol pStartSymbol = null;
                pStartSymbol = new SimpleMarkerSymbolClass();
                pStartSymbol.Angle = 0;//角度
                pStartSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;//形状

                IRgbColor pStartColor = new RgbColorClass();
                pStartColor.Red = 255;
                pStartSymbol.Color = pStartColor;

                pStartSymbol.Size = 4;
                DefPointSymbol = pStartSymbol as ISymbol;
            }
            return DefPointSymbol;
        }
        /// <summary>
        /// 创建默认线符号
        /// </summary>
        /// <returns></returns>
        public static ISymbol CreateLineSymbol()
        {
            if (DefLineSymbol == null)
            {
                // 获取IRGBColor接口
                IRgbColor color = new RgbColor();
                // 设置颜色属性
                color.Red = 255;
                color.Green = 0;
                color.Blue = 0;

                // 获取ILine符号接口
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
                // 设置线符号属性
                simpleLineSymbol.Width = 2.0;
                simpleLineSymbol.Color = color;
                //simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                (simpleLineSymbol as ISymbol).ROP2 = esriRasterOpCode.esriROPXOrPen;
                DefLineSymbol = simpleLineSymbol as ISymbol;
            }
            return DefLineSymbol;
        }
        /// <summary>
        /// 创建默认面符号
        /// </summary>
        /// <returns></returns>
        public static ISymbol CreateFillSymbol()
        {
            if (DefFillSymbol == null)
            {
                // 获取IRGBColor接口
                IRgbColor color = new RgbColor();
                // 设置颜色属性
                color.Red = 255;
                color.Green = 0;
                color.Blue = 0;

                SimpleFillSymbol class2 = new SimpleFillSymbol();
                class2.Style = esriSimpleFillStyle.esriSFSNull;
                ISimpleLineSymbol symbol = new SimpleLineSymbol();
                symbol.Width = 2.0;
                symbol.Color = color;
                //simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                (symbol as ISymbol).ROP2 = esriRasterOpCode.esriROPXOrPen;
                class2.Outline = symbol;
                DefFillSymbol = class2 as ISymbol;
            }
            return DefFillSymbol;
        }
        /// <summary>
        /// 符号转图片
        /// </summary>
        /// <param name="sym"></param>
        /// <returns></returns>
        public static Image StyleToImage(ISymbol sym)
        {
            return StyleToImage(sym, 0x10, 0x10);
        }
        /// <summary>
        /// 符号转图片
        /// </summary>
        /// <param name="sym"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image StyleToImage(ISymbol sym, int width, int height)
        {
            if (sym == null)
            {
                return null;
            }
            try
            {
                Image image = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = graphics.GetHdc();
                IEnvelope env = new EnvelopeClass();
                env.XMin = 1.0;
                env.XMax = width - 1;
                env.YMin = 1.0;
                env.YMax = height - 1;
                IGeometry geometry = CreateGeometryFromSymbol(sym, env);
                if (geometry != null)
                {

                    ITransformation transformation = CreateTransformationFromHDC(hdc, env, width, height);
                    sym.SetupDC((int)hdc, transformation);
                    sym.Draw(geometry);
                    sym.ResetDC();
                }
                graphics.ReleaseHdc(hdc);
               
                return image;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 图片转字节组
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image img)
        {
            System.IO.MemoryStream Ms = new MemoryStream();
            img.Save(Ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imgBytes = new byte[Ms.Length];
            Ms.Position = 0;
            Ms.Read(imgBytes, 0, (int)Ms.Length);
            Ms.Close();
            return imgBytes;
        }


        /// <summary>
        /// 这个方法使用HDC、图形的范围和要输出的图片的大小来构造一个 ITransformation.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="geomEnv"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <returns></returns>
        public static ITransformation CreateTransformationFromHDC(IntPtr hdc, IEnvelope geomEnv, int imageWidth, int imageHeight)
        {
            tagRECT frame = new tagRECT();
            frame.left = 0;
            frame.top = 0;
            frame.right = imageWidth - 1;
            frame.bottom = imageHeight - 1;
            double dpi = Graphics.FromHdc(hdc).DpiY;
            long lDpi = (long)dpi;
            if (lDpi == 0)
            {
                return null;
            }
            IDisplayTransformation dispTrans = new DisplayTransformationClass();
            dispTrans.Bounds = geomEnv;
            dispTrans.VisibleBounds = geomEnv;
            dispTrans.set_DeviceFrame(ref frame);
            dispTrans.Resolution = dpi;
            return dispTrans;
        }

        private static IGeometry CreateGeometryFromSymbol(ISymbol sym, IEnvelope env)
        {
            IPoint point;
            if (sym is IMarkerSymbol)
            {
                IArea area = (IArea)env;
                return area.Centroid;
            }
            if ((sym is ILineSymbol) || (sym is ITextSymbol))
            {
                IPolyline polyline = new ESRI.ArcGIS.Geometry.PolylineClass();
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(env.LowerLeft.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.FromPoint = point;
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(env.UpperRight.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.ToPoint = point;
                if (sym is ITextSymbol)
                {
                    (sym as ITextSymbol).Text = "样本字符";
                }
                return polyline;
            }
            if (sym is IFillSymbol)
            {
                IPolygon polygon = new ESRI.ArcGIS.Geometry.PolygonClass();
                IPointCollection points = (IPointCollection)polygon;
                point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperLeft.X, env.UpperLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperRight.X, env.UpperRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerRight.X, env.LowerRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                return polygon;
            }
            return null;
        }

        /*************** 2014-12-31 tmj End **************************************/

        private static IColor GetRGBColor(int R, int G, int B)
        {
            IColor pColor = CreateObject("esriDisplay.RgbColor") as IColor;
            pColor.RGB = B * 65536 + G * 256 + R;
            return pColor;
        }

        #region 判断服务、文档是否存在
        /// <summary>
        /// 判断是否存在该服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool isExistService(string serviceName)
        {
            return false;
            /*
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("url", HostName);
            propertySet.SetProperty("ConnectionMode", esriAGSConnectionMode.esriAGSConnectionModeAdmin);
            //propertySet.SetProperty("ServerType", esriAGSServerType.esriAGSServerTypeDiscovery);
            propertySet.SetProperty("user", MapServerUserName);
            propertySet.SetProperty("password", MapserverPwd);
            propertySet.SetProperty("ALLOWINSECURETOKENURL", true); //设置为false会弹出一个警告对话框

            IAGSServerConnectionName3 pConnectName = new AGSServerConnectionNameClass() as IAGSServerConnectionName3;//10.1新增接口
            pConnectName.ConnectionProperties = propertySet;
            IAGSServerConnectionAdmin pAGSAdmin = ((IName)pConnectName).Open() as IAGSServerConnectionAdmin;
            IAGSServerConnectionAdmin pAGSServerConnectionAdmin = pAGSAdmin as IAGSServerConnectionAdmin;
            try
            {
                IServerObjectManager som = pAGSServerConnectionAdmin.ServerObjectManager;
                IServerObjectConfigurationInfo pConfig = som.GetConfigurationInfo(serviceName, "MapServer");
                return true;
            }
            catch (Exception e)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Debug, "服务器出错!");
                return false;
            }
            */
            //ESRI.ArcGIS.ADF.Identity identity = new ESRI.ArcGIS.ADF.Identity(MapServerUserName, MapserverPwd, "");
            //ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection agsConnection = new ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection(HostName, identity);
            //agsConnection.Connect();
            //if (agsConnection.IsConnected)
            //{
            //    try
            //    {
            //        IServerObjectManager som = agsConnection.ServerObjectManager;
            //        IServerObjectConfigurationInfo pConfig = som.GetConfigurationInfo(serviceName, "MapServer");
            //        return true;
            //    }
            //    catch (Exception e)
            //    {
            //        var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
            //        log.Log(LogLevel.Debug, "服务器出错!");
            //        return false;
            //    }
            //}
            //return false;
        }

        private static bool isExistMxd(string MxdPath, string MapName)
        {
            if (MxdPath.Substring(MxdPath.Length - 1) != @"/")
                MxdPath += @"/";
            if (File.Exists(MxdPath + MapName + ".mxd"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region SDE连接以及数据获取

        private static IPropertySet GetProPerty(SdeConfigInfo sdeConfig)
        {
            IPropertySet propertySet = new PropertySet();
            propertySet.SetProperty("SERVER", sdeConfig.SDEServer);
            propertySet.SetProperty("INSTANCE", sdeConfig.Instance);
            propertySet.SetProperty("DATABASE", sdeConfig.DataBase);
            propertySet.SetProperty("USER", sdeConfig.SDEUser);
            propertySet.SetProperty("PASSWORD", sdeConfig.SDEPassWord);
            propertySet.SetProperty("VERSION", sdeConfig.SDEVersion);
            return propertySet;
        }

        public static IWorkspace OpenSDEWorkSpace(IPropertySet pPropSet)
        {
            IWorkspace pWorkSpace = null;
            //IWorkspaceFactory pSdeFact = new SdeWorkspaceFactoryClass();
            IWorkspaceFactory pSdeFact = (IWorkspaceFactory)CreateObject("esriDataSourcesGDB.SdeWorkspaceFactory");
            try
            {
                pWorkSpace = pSdeFact.Open(pPropSet, 0);
            }
            catch (Exception e)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "OpenSDEWorkSpace出错!");
                pSdeFact = null;
            }
            return pWorkSpace;
        }

        public static IRasterWorkspaceEx OpenSdeRasterWsp(SdeConfigInfo sdeConfig)
        {
            try
            {
                IPropertySet pProPerty = GetProPerty(sdeConfig);
                IRasterWorkspaceEx pRasterWsp = OpenSDEWorkSpace(pProPerty) as IRasterWorkspaceEx;
                return pRasterWsp;
            }
            catch (Exception e)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, "OpenSdeRasterWsp 出错!");
                return null;
            }
        }

        public static IWorkspace OpenSdeFeatureWsp(SdeConfigInfo sdeConfig)
        {
            try
            {
                IPropertySet pProPerty = GetProPerty(sdeConfig);
                //IFeatureWorkspace pRasterWsp = OpenSDEWorkSpace(pProPerty) as IFeatureWorkspace;
                IWorkspaceFactory factory = new SdeWorkspaceFactory();
                IWorkspace pRasterWsp = factory.Open(pProPerty, 0) as IWorkspace;
                return pRasterWsp;
            }
            catch (Exception e)
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Info, " OpenSdeFeatureWsp 出错!");
                return null;
            }
        }

        private static IFeatureClass GetFeatureClass(SdeConfigInfo sdeConfig, string FeatureName)
        {
            IWorkspace pWspEx = OpenSdeFeatureWsp(sdeConfig);
            IFeatureWorkspace pFeatureWspEx = pWspEx as IFeatureWorkspace;
            if (pFeatureWspEx == null) return null;
            IFeatureClass pFeatureClass = pFeatureWspEx.OpenFeatureClass(FeatureName);
            pFeatureWspEx = null;
            return pFeatureClass;
        }

        #endregion

        public static ResultModel CreateServices(string MapPath, string ServerName, bool IsFeature)
        {
            var model = new ResultModel();
            if (isExistService(ServerName))
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Debug, "服务名已存在!");
                model = new ResultModel("服务名已存在!", true);
            }
            else
            {
                ESRI.ArcGIS.ADF.Identity identity = new ESRI.ArcGIS.ADF.Identity(MapServerUserName, MapserverPwd, "WORKGROUP");
                ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection agsConnection = new ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection(HostName, identity);
                agsConnection.Connect();
                IServerObjectAdmin pServerObjectAdmin;
                pServerObjectAdmin = agsConnection.ServerObjectAdmin;
                IServerObjectConfiguration2 configuration = (IServerObjectConfiguration2)pServerObjectAdmin.CreateConfiguration();
                configuration.Name = ServerName;//发布Service的名称，必填    
                configuration.TypeName = "MapServer";//发布服务的类型，如：MapServer，GeocodeServer    
                IPropertySet props = configuration.Properties;
                props.SetProperty("FilePath", MapPath);//设置MXD的路径

                #region 一下的property并非必须，只要一个filepath就可以发布
                //props.SetProperty("OutputDir", System.IO.Path.Combine(serviceCachePath, "arcgisoutput"));//图片的输出目录    
                //props.SetProperty("VirtualOutPutDir", "http://" + HostName + "/arcgisoutput");//图片输出的虚拟路径    
                //props.SetProperty("SupportedImageReturnTypes", "URL");//支持的图片类型    
                //props.SetProperty("MaxImageHeight", "2048");//图片的最大高度    
                //props.SetProperty("MaxRecordCount", "500");//返回记录的最大条数    
                //props.SetProperty("MaxBufferCount", "100");//缓冲区分析的最大数目    
                //props.SetProperty("MaxImageWidth", "2048");//图片的最大宽度    
                //props.SetProperty("IsCached", "false");//是否切片    
                //props.SetProperty("CacheOnDemand", "false");//是否主动切片    
                //props.SetProperty("IgnoreCache", "false");//是否忽略切片    
                //props.SetProperty("ClientCachingAllowed", "true");//是否允许客户端缓冲    
                //props.SetProperty("CacheDir", System.IO.Path.Combine(serviceCachePath, "arcgiscache", ServerName));//切片的输出路径    
                //props.SetProperty("SOMCacheDir", System.IO.Path.Combine(serviceCachePath, "arcgiscache"));//som的切片输出路径    

                //configuration.Description = "NewService";//Service的描述    
                configuration.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh;//或者esriServerIsolationLow,esriServerIsolationAny    
                configuration.IsPooled = true;//是否池化    
                configuration.MaxInstances = 2;//最多的实例数    
                configuration.MinInstances = 1;//最少的实例数    

                ////设置刷新    
                //IPropertySet recycleProp = configuration.RecycleProperties;
                //recycleProp.SetProperty("StartTime", "00:00");//刷新开始时间    
                //recycleProp.SetProperty("Interval", "3600");//刷新间隔    

                ////设置是否开启REST服务    
                IPropertySet infoProp = configuration.Info;
                infoProp.SetProperty("WebEnabled", "true");//是否提供REST服务    
                infoProp.SetProperty("WebCapabilities", "Map,Query,Data");//提供何种服务    

                if (IsFeature == true)
                {
                    configuration.set_ExtensionEnabled("FeatureServer", true);
                    var featureServer = configuration.get_ExtensionInfo("FeatureServer");
                    featureServer.SetProperty("WebEnabled", "true");//是否提供REST服务    
                    featureServer.SetProperty("WebCapabilities", "Query,Editing");//提供何种服务    
                    configuration.set_ExtensionInfo("FeatureServer", featureServer);
                }


                //configuration.StartupType = esriStartupType.esriSTAutomatic;//或者esriSTManual    
                //configuration.UsageTimeout = 120;//客户端占用一个服务的最长时间    
                //configuration.WaitTimeout = 120;//客户端申请一个服务的最长等待时间    
                #endregion
                //添加服务到Server    
                pServerObjectAdmin.AddConfiguration(configuration);
                //启动服务    
                pServerObjectAdmin.StartConfiguration(ServerName, "MapServer");
            }
            model.ServiceUrl = "http://" + HostName + "/ArcGIS/rest/services/" + ServerName + "/MapServer";
            if (IsFeature == true)
                model.FeatureUrl = "http://" + HostName + "/ArcGIS/rest/services/" + ServerName + "/FeatureServer/0";
            return model;
        }

        public static ResultModel CreateServicesNew(string MapPath, string ServerName, bool IsFeature)
        {
            var model = new ResultModel();
            if (isExistService(ServerName))
            {
                var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                log.Log(LogLevel.Debug, "服务名已存在!");
                model = new ResultModel("服务名已存在!", true);
            }
            else
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("url", HostName);
                propertySet.SetProperty("ConnectionMode", esriAGSConnectionMode.esriAGSConnectionModeAdmin);
                //propertySet.SetProperty("ServerType", esriAGSServerType.esriAGSServerTypeDiscovery);
                propertySet.SetProperty("user", MapServerUserName);
                propertySet.SetProperty("password", MapserverPwd);
                propertySet.SetProperty("ALLOWINSECURETOKENURL", false); //设置为false会弹出一个警告对话框

                IAGSServerConnectionName3 pConnectName = new AGSServerConnectionNameClass() as IAGSServerConnectionName3;//10.1新增接口
                pConnectName.ConnectionProperties = propertySet;
                IAGSServerConnectionAdmin pAGSServerConnectionAdmin = ((IName)pConnectName).Open() as IAGSServerConnectionAdmin;
                IServerObjectAdmin pServerObjectAdmin = pAGSServerConnectionAdmin.ServerObjectAdmin;
                IServerObjectConfiguration5 configuration = (IServerObjectConfiguration5)pServerObjectAdmin.CreateConfiguration();



                //Set the general configuration settings

                //pConfiguration.Name = ServerName;
                //pConfiguration.TypeName = "MapServer";
                //pConfiguration.TargetCluster = "default";

                //pConfiguration.StartupType = esriStartupType.esriSTAutomatic;
                //pConfiguration.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh;
                //pConfiguration.IsPooled = true;
                //pConfiguration.Description = "Modsim Map Output";
                //IPropertySet props = pConfiguration.Properties;
                //props.SetProperty("FilePath", MapPath);
            

                /*
                ESRI.ArcGIS.ADF.Identity identity = new ESRI.ArcGIS.ADF.Identity(MapServerUserName, MapserverPwd, "WORKGROUP");
                ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection agsConnection = new ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection(HostName, identity);
                agsConnection.Connect();
                IServerObjectAdmin pServerObjectAdmin;
                pServerObjectAdmin = agsConnection.ServerObjectAdmin;
                IServerObjectConfiguration2 configuration = (IServerObjectConfiguration2)pServerObjectAdmin.CreateConfiguration();
                */
                configuration.Name = ServerName;//发布Service的名称，必填    
                configuration.TypeName = "MapServer";//发布服务的类型，如：MapServer，GeocodeServer    
                IPropertySet props = configuration.Properties;
                props.SetProperty("FilePath", MapPath);//设置MXD的路径

                #region 一下的property并非必须，只要一个filepath就可以发布
                //props.SetProperty("OutputDir", System.IO.Path.Combine(serviceCachePath, "arcgisoutput"));//图片的输出目录    
                //props.SetProperty("VirtualOutPutDir", "http://" + HostName + "/arcgisoutput");//图片输出的虚拟路径    
                //props.SetProperty("SupportedImageReturnTypes", "URL");//支持的图片类型    
                //props.SetProperty("MaxImageHeight", "2048");//图片的最大高度    
                //props.SetProperty("MaxRecordCount", "500");//返回记录的最大条数    
                //props.SetProperty("MaxBufferCount", "100");//缓冲区分析的最大数目    
                //props.SetProperty("MaxImageWidth", "2048");//图片的最大宽度    
                //props.SetProperty("IsCached", "false");//是否切片    
                //props.SetProperty("CacheOnDemand", "false");//是否主动切片    
                //props.SetProperty("IgnoreCache", "false");//是否忽略切片    
                //props.SetProperty("ClientCachingAllowed", "true");//是否允许客户端缓冲    
                //props.SetProperty("CacheDir", System.IO.Path.Combine(serviceCachePath, "arcgiscache", ServerName));//切片的输出路径    
                //props.SetProperty("SOMCacheDir", System.IO.Path.Combine(serviceCachePath, "arcgiscache"));//som的切片输出路径    

                //configuration.Description = "NewService";//Service的描述    
                configuration.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh;//或者esriServerIsolationLow,esriServerIsolationAny    
                configuration.IsPooled = true;//是否池化    
                configuration.MaxInstances = 2;//最多的实例数    
                configuration.MinInstances = 1;//最少的实例数    

                ////设置刷新    
                //IPropertySet recycleProp = configuration.RecycleProperties;
                //recycleProp.SetProperty("StartTime", "00:00");//刷新开始时间    
                //recycleProp.SetProperty("Interval", "3600");//刷新间隔    

                ////设置是否开启REST服务    
                IPropertySet infoProp = configuration.Info;
                infoProp.SetProperty("WebEnabled", "true");//是否提供REST服务    
                infoProp.SetProperty("WebCapabilities", "Map,Query,Data");//提供何种服务    

                if (IsFeature == true)
                {
                    configuration.set_ExtensionEnabled("FeatureServer", true);
                    var featureServer = configuration.get_ExtensionInfo("FeatureServer");
                    featureServer.SetProperty("WebEnabled", "true");//是否提供REST服务    
                    featureServer.SetProperty("WebCapabilities", "Query,Editing");//提供何种服务    
                    configuration.set_ExtensionInfo("FeatureServer", featureServer);
                }


                //configuration.StartupType = esriStartupType.esriSTAutomatic;//或者esriSTManual    
                //configuration.UsageTimeout = 120;//客户端占用一个服务的最长时间    
                //configuration.WaitTimeout = 120;//客户端申请一个服务的最长等待时间    
                #endregion
                //添加服务到Server    
                pServerObjectAdmin.AddConfiguration(configuration);
                //启动服务    
                pServerObjectAdmin.StartConfiguration(ServerName, "MapServer");
            }
            model.ServiceUrl = "http://" + HostName + "/ArcGIS/rest/services/" + ServerName + "/MapServer";
            if (IsFeature == true)
                model.FeatureUrl = "http://" + HostName + "/ArcGIS/rest/services/" + ServerName + "/FeatureServer/0";
                 
            return model;
        }

        private static IServerContext CreateServerContext()
        {
            ESRI.ArcGIS.ADF.Identity identity = new ESRI.ArcGIS.ADF.Identity(MapServerUserName, MapserverPwd, "");
            ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection agsConnection = new ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection(HostName, identity);
            agsConnection.Connect();
            if (agsConnection.IsConnected)
            {
                try
                {
                    IServerObjectManager som = agsConnection.ServerObjectManager;
                    IServerContext pServerContext = som.CreateServerContext("Geometry", "GeometryServer");
                    return pServerContext;
                }
                catch (Exception e)
                {
                    var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                    log.Log(LogLevel.Info, " CreateServerContext 出错!");
                    return null;
                }
            }
            return null;
        }

        #region  ServerContext CreateObject函数

        private static object CreateObject(string ObjectCLSID)
        {
            IServerContext pServerContext = CreateServerContext();
            if (pServerContext == null) return null;
            try
            {
                return pServerContext.CreateObject(ObjectCLSID);
            }
            catch
            {
                return null;
            }
            finally
            {
                pServerContext.ReleaseContext();
            }
        }
        #endregion

        public static int GetServiceCount()
        {
            ESRI.ArcGIS.ADF.Identity identity = new ESRI.ArcGIS.ADF.Identity(MapServerUserName, MapserverPwd, "");
            ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection agsConnection = new ESRI.ArcGIS.ADF.Connection.AGS.AGSServerConnection(HostName, identity);
            agsConnection.Connect();
            if (agsConnection.IsConnected)
            {
                try
                {
                    IServerObjectManager som = agsConnection.ServerObjectManager;
                    var serviceDic = som.GetServerDirectoryInfos();
                    return serviceDic.Count;
                }
                catch (Exception e)
                {
                    var log = ServiceLocator.Current.GetInstance<ILogManagerRepo>();
                    log.Log(LogLevel.Info, " CreateServerContext 出错!");
                    return 0;
                }
            }
            return 0;
        }



    }
}
