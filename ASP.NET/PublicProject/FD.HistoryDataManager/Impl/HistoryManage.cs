using FD.DataBase;
using FD.HistoryDataManager.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FD.HistoryDataManager.Impl
{
    public class HistoryManage : IHistoryManage
    {
        #region 初始化
        private HistoryManage()
        {
        }
        private static HistoryManage instant;
        public static HistoryManage Instant
        {
            get
            {
                if(instant==null)
                    instant = new HistoryManage(); 
                return instant;
            }
        }
        
        #endregion

        public void updateToHistory(string tableName, Dictionary<string, object>[] dic)
        {
            //当历史表dic存在这张表名时，需要加入历史
            if (HisServiceAppContext.Instance.CorrelationTableDic.ContainsKey(tableName))
            {
                var tabledic = HisServiceAppContext.Instance.DataBaseClassHelper.DataBaseKyFieldTableDic;
                var keyfield = tabledic[tableName];
                var ids = (from item in dic
                           select item[keyfield].ToString()).ToArray();
                var filter = QueryPageFilter.Create(tableName).In(keyfield, ids);
                var newDic = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(filter);
                updateHistoryObject(tableName, newDic);
            }
        }

        public void updateHistoryObject(string tableName, Dictionary<string, object>[] dics)
        {
            var historyTableName = HisServiceAppContext.Instance.CorrelationTableDic[tableName];
            var hisDic = HisServiceAppContext.Instance.HistoryTableDic[historyTableName];
            foreach (var data in dics)
            {
                if (data.ContainsKey("SHAPE"))
                {
                    var obj = (FD.DataBase.THGeometry)data["SHAPE"];
                    if (obj.WKT == "Null")
                    {
                        data["SHAPE"] = null;
                    }
                }
                data.Add(hisDic["LSBZJ"], Guid.NewGuid().ToString());
                data.Add(hisDic["LSBSJC"], Convert.ToDateTime(data[hisDic["GXSJZDMC"]]).ToFileTimeUtc().ToString());
            }
            HisServiceAppContext.Instance.HisDataBaseClassHelper.UpdateObjects(historyTableName, dics);
        }

        /// <summary>
        /// 获取历史差异主方法
        /// </summary>
        /// <param name="resultDic"></param>
        /// <returns></returns>
        public HistoryDiffModel[] GetHistoryDifference(QueryPageFilter[] queryFilters)
        {
            QueryFilterResultDic[] resultDic = HisServiceAppContext.Instance.HisDataBaseClassHelper.GetQueryFieldResults(queryFilters);

            if (resultDic.Count() <= 0) return null;
            List<HistoryDiffModel> historyDiffList = new List<HistoryDiffModel>();
            var mainTableInfo = resultDic[0];
            var hisDic = (Dictionary<string, string>)HisServiceAppContext.Instance.HistoryTableDic[mainTableInfo.TableName];
            for (int i = 0; i < mainTableInfo.Result.Count() - 1; i++)
            {
                HistoryDiffModel historyDiffModel = new HistoryDiffModel();
                List<SubTableDiffModel> cbcyList = new List<SubTableDiffModel>();
                #region 主表信息
                //修改字段
                Dictionary<string, string> replaceDic = getReplaceDic(mainTableInfo.TableName);
                Dictionary<string, object> xgzd = new Dictionary<string, object>();
                xgzd = GetTwoDataHistory(mainTableInfo.Result[i], mainTableInfo.Result[i + 1], replaceDic, "编辑");
                //JSON字段判断
                if (hisDic.ContainsKey("JSONZDMC"))
                {
                    Dictionary<string, object> jsonxgzd = new Dictionary<string, object>();
                    jsonxgzd = GetJsonDiff(mainTableInfo.Result[i][hisDic["JSONZDMC"]].ToString(), mainTableInfo.Result[i + 1][hisDic["JSONZDMC"]].ToString());
                    Dictionary<string, object> wzdic = xgzd.Union(jsonxgzd).ToDictionary(k=>k.Key,k=>k.Value);
                    historyDiffModel.XGZD = new Dictionary<string, object>(wzdic);
                }
                else
                    historyDiffModel.XGZD = new Dictionary<string, object>(xgzd);
                if (historyDiffModel.XGZD.Count <= 0) historyDiffModel.XGZD = null;
                //更新时间
                historyDiffModel.GXSJ = mainTableInfo.Result[i][hisDic["GXSJZDMC"]].ToString();
                //修改人
                var caozuoid = mainTableInfo.Result[i][hisDic["CZRIDZDMC"]].ToString();
                //暂时先用ID，后面要改回UserNmae
                var queryFilter = QueryPageFilter.Create("SYS_YHB").Equal("ID", caozuoid);
                var result = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter);
                if (result.Count() > 0)
                {
                    historyDiffModel.XGR = result[0]["YHXM"].ToString();
                    historyDiffModel.BM = result[0]["SSBMID"].ToString();
                }
                //修改人所在部门
                var queryFilter2 = QueryPageFilter.Create("SYS_DWBMB").Equal("ID", string.IsNullOrEmpty(historyDiffModel.BM) ? "" : historyDiffModel.BM);
                var result2 = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter2);
                if (result2.Count() > 0)
                {
                    historyDiffModel.BM = result2[0]["BMMC"].ToString();
                }
                //主表名
                historyDiffModel.ZBM = hisDic["BMZWXS"];
                #endregion

                #region 从表信息

                var newtimeid = mainTableInfo.Result[i][hisDic["LSBSJC"]].ToString();
                var oldtimeid = mainTableInfo.Result[i + 1][hisDic["LSBSJC"]].ToString();
                for (int j = 1; j < resultDic.Count(); j++)
                {
                    SubTableDiffModel subTableDiffModel = new SubTableDiffModel();
                    var subTableList = resultDic[j];
                    var subTableHisDic = (Dictionary<string, string>)HisServiceAppContext.Instance.HistoryTableDic[subTableList.TableName];
                    var oldSubTableArray = (from item in subTableList.Result
                                            where item[subTableHisDic["LSBSJC"]].Equals(oldtimeid)
                                            select item).ToArray();
                    var newSubTableArray = (from item in subTableList.Result
                                            where item[subTableHisDic["LSBSJC"]].Equals(newtimeid)
                                            select item).ToArray();
                    subTableDiffModel.CBZDCYList = getSubTableFieldDiffList(oldSubTableArray, newSubTableArray, subTableHisDic, subTableList.TableName);
                    subTableDiffModel.XSBM = subTableHisDic["BMZWXS"];
                    if (subTableDiffModel.CBZDCYList.Count == 0) continue;
                    cbcyList.Add(subTableDiffModel);
                    historyDiffModel.CBCYList = cbcyList;
                }

                #endregion
                if (historyDiffModel.XGZD == null && (historyDiffModel.CBCYList == null || historyDiffModel.CBCYList.Count <= 0)) continue;
                historyDiffList.Add(historyDiffModel);
            }
            #region 获取第一条是谁新增的
            HistoryDiffModel first = new HistoryDiffModel();
            first = getFirstAdd(mainTableInfo.TableName, mainTableInfo.Result[mainTableInfo.Result.Length - 1]);
            historyDiffList.Add(first);
            #endregion
            return historyDiffList.ToArray();
        }

        /// <summary>
        /// 根据两个值dic查出差异
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="oldData"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetTwoDataHistory(Dictionary<string, object> newData, Dictionary<string, object> oldData, Dictionary<string, string> fieldDic,string caozuo)
        {
            var xgzd = new Dictionary<string, object>();
            switch (caozuo)
            {
                case "新增":
                    foreach (var item in newData)
                    {
                        if (!fieldDic.ContainsKey(item.Key) || string.IsNullOrEmpty(item.Value.ToString())) continue;
                        xgzd.Add(fieldDic[item.Key], new string[] { item.Value.ToString(), "" });
                    }
                    break;
                case "删除":
                    foreach (var item in oldData)
                    {
                        if (!fieldDic.ContainsKey(item.Key) || string.IsNullOrEmpty(item.Value.ToString())) continue;
                        xgzd.Add(fieldDic[item.Key], new string[] { item.Value.ToString(), "" });
                    }
                    break;
                case "编辑":
                    foreach (var col in newData)
                    {
                        if (!fieldDic.ContainsKey(col.Key)) continue;
                        if (!ComparerTwoValue(col.Value, oldData[col.Key]))
                        {
                            var xgqValue = oldData[col.Key] == null || string.IsNullOrEmpty(oldData[col.Key].ToString()) ? "无" : oldData[col.Key].ToString();
                            var xghValue = col.Value == null || string.IsNullOrEmpty(col.Value.ToString()) ? "无" : col.Value.ToString();
                            xgzd.Add(fieldDic[col.Key], new string[] { xgqValue, xghValue });
                        }
                    }
                    break;
            }
            return xgzd;
        }

        private Dictionary<string, object> GetJsonDiff(string newStr, string oldStr)
        {
            if (string.IsNullOrEmpty(newStr) || string.IsNullOrEmpty(oldStr)) return new Dictionary<string,object>();
            var oldJsonStr = System.Web.HttpUtility.HtmlDecode(oldStr);
            var newJsonStr = System.Web.HttpUtility.HtmlDecode(newStr);
            var oldDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(oldJsonStr);
            var newDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(newJsonStr);

            var result = (from newData in newDic
                          where !oldDic.Contains(newData)
                          where oldDic.ContainsKey(newData.Key) || !string.IsNullOrEmpty(newData.Value)
                          let newKey = newData.Key
                          let xgq = ((oldDic.ContainsKey(newKey) && string.IsNullOrEmpty(oldDic[newKey])) || !oldDic.ContainsKey(newKey)) ? "无" : oldDic[newKey]
                          let xgh = string.IsNullOrEmpty(newData.Value) ? "无" : newData.Value
                          select new { fieldName = newKey, diffArr = new string[] { xgq, xgh } }).ToDictionary(k => k.fieldName, k => (object)k.diffArr);
            return result;
        }

        private List<SubTableFieldDiffModel> getSubTableFieldDiffList(Dictionary<string, object>[] oldArray, Dictionary<string, object>[] newArray, Dictionary<string, string> hisDic, string subTableName)
        {
            List<SubTableFieldDiffModel> subTableFieldDiffList = new List<SubTableFieldDiffModel>();
            Dictionary<string, string> replaceDic = getReplaceDic(subTableName);
            foreach (var newinfo in newArray)
            {
                var oldlist = from item in oldArray
                              where item[hisDic["YBZJ"]].Equals(newinfo[hisDic["YBZJ"]])
                              select item;
                SubTableFieldDiffModel subTableFieldDiffModel = new SubTableFieldDiffModel();
                if (oldlist.Count() <= 0)
                    subTableFieldDiffModel = HandleFieldDiff(null, newinfo, replaceDic, hisDic);
                else
                    subTableFieldDiffModel = HandleFieldDiff(oldlist.First(), newinfo, replaceDic, hisDic);
                if (subTableFieldDiffModel.XGZD.Count == 0) continue;
                subTableFieldDiffList.Add(subTableFieldDiffModel);
            }
            foreach (var oldinfo in oldArray)
            {
                var newlist = from item in newArray
                              where item[hisDic["YBZJ"]].Equals(oldinfo[hisDic["YBZJ"]])
                              select item;
                SubTableFieldDiffModel subTableFieldDiffModel = new SubTableFieldDiffModel();
                if (newlist.Count() <= 0)
                    subTableFieldDiffModel = HandleFieldDiff(oldinfo, null, replaceDic, hisDic);
                if (subTableFieldDiffModel.XGZD == null||subTableFieldDiffModel.XGZD.Count == 0) continue;
                subTableFieldDiffList.Add(subTableFieldDiffModel);
            }

            return subTableFieldDiffList;
        }

        private SubTableFieldDiffModel HandleFieldDiff(Dictionary<string, object> oldinfo, Dictionary<string, object> newinfo, Dictionary<string, string> replaceDic, Dictionary<string, string> hisDic)
        {
            SubTableFieldDiffModel subTableFieldDiffModel = new SubTableFieldDiffModel();
            string caozuo = string.Empty;
            Dictionary<string, object> xgzd = new Dictionary<string, object>();
            Dictionary<string, object> objectInfo = new Dictionary<string, object>();
            
            if (oldinfo == null)
            {
                //新增
                caozuo = "新增";
                objectInfo = newinfo;
            }
            else if (newinfo == null)
            {
                //无删除状态的删除
                caozuo = "删除";
                objectInfo = oldinfo;
            }
            else if (hisDic["SFYSCZT"] == "True" && !Convert.ToBoolean(oldinfo[hisDic["SCZTZDMC"]]) && oldinfo[hisDic["SCZTZDMC"]] != null && Convert.ToBoolean(newinfo[hisDic["SCZTZDMC"]]) && newinfo[hisDic["SCZTZDMC"]] != null)
            {
                //有删除状态的删除
                caozuo = "删除";
                objectInfo = oldinfo;
            }
            else
            {
                //编辑
                caozuo = "编辑";
                objectInfo = oldinfo;
                
            }

            xgzd = GetTwoDataHistory(newinfo, oldinfo, replaceDic,caozuo);

            subTableFieldDiffModel.CAOZUO = caozuo;
            if (hisDic.ContainsKey("SJLXZD"))
                subTableFieldDiffModel.LX = objectInfo[hisDic["SJLXZD"]].ToString();
            subTableFieldDiffModel.XGZD = xgzd;
            return subTableFieldDiffModel;
        }

        private HistoryDiffModel getFirstAdd(string tableName, Dictionary<string, object> dic)
        {
            HistoryDiffModel hisDiffModel = new HistoryDiffModel();
            var hisDic = (Dictionary<string, string>)HisServiceAppContext.Instance.HistoryTableDic[tableName];
            hisDiffModel.GXSJ = dic[hisDic["GXSJZDMC"]].ToString();
            //修改人
            var caozuoid = dic[hisDic["CZRIDZDMC"]].ToString();
            //暂时用ID，后续要改回UserNmae
            var queryFilter = QueryPageFilter.Create("SYS_YHB").Equal("ID", caozuoid);
            var result = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter);
            if (result.Count() > 0)
            {
                hisDiffModel.XGR = result[0]["YHXM"].ToString();
                hisDiffModel.BM = result[0]["SSBMID"].ToString();
            }
            //修改人所在部门
            var queryFilter2 = QueryPageFilter.Create("SYS_DWBMB").Equal("ID", string.IsNullOrEmpty(hisDiffModel.BM) ? "" : hisDiffModel.BM);
            var result2 = HisServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(queryFilter2);
            if (result2.Count() > 0)
            {
                hisDiffModel.BM = result2[0]["BMMC"].ToString();
            }
            //主表名
            hisDiffModel.ZBM = hisDic["BMZWXS"];

            return hisDiffModel;
        }

        public Dictionary<string, string> getReplaceDic(string tableName)
        {
            var redic = HisServiceAppContext.Instance.ShowFieldDic;
            if (redic == null || !redic.ContainsKey(tableName)) return defaultDic;
            return HisServiceAppContext.Instance.ShowFieldDic[tableName];
        }

        /// <summary>
        /// 字段对比方法
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static bool ComparerTwoValue(Object object1, Object object2)
        {

            if (object1 == null && object2 == null)
            {
                return true;
            }
            if (object1 == null && object2.ToString().Trim() == "")
            {
                return true;
            }
            if (object2 == null && object1.ToString().Trim() == "")
            {
                return true;
            }
            if (object1 != null && object2 == null)
            {
                return false;
            }
            if (object1 == null && object2 != null)
            {
                return false;
            }
            if (object1.ToString().Equals(object2.ToString()))
            {
                return true;
            }
            return false;
        }

        public static Dictionary<string, string> defaultDic = new Dictionary<string, string>() 
        { 
            {"MC","名称"},
            {"DZ","地址"}
        };
    }

    
}
