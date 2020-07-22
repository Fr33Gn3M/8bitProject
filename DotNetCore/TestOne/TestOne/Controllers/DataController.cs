using System;
using System.Collections.Generic;
using Commons;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.OpenPgp;
using TestOne.Commons;

namespace TestOne.Controllers
{
    /// <summary>
    /// 基础数据操作控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {

        /// <summary>
        /// 判断服务是否正常
        /// </summary>
        /// <returns></returns>
        [HttpGet("Index")]
        public string Index()
        {
            return "服务正常使用。。。";          
        }

        /// <summary>
        /// query方法
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        [HttpPost("Query")]
        public JsonResult Query(string filter)
        {
            var result = new JsonResults() { };
            try
            {
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                var objs = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryResultN(filters);

                result.setData(objs).setTotal(objs.Length);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }
        /*
        public System.Web.Mvc.JsonResult Querys()
        {
            var result = new TH.DataBase.JsonResults() { };
            int count = 0;
            try
            {
                CheckUserInfo();
                string filters = TH.Commons.Encrypt.DecryptJson(Request["filters"]);
                var queryFilters = JsonConvert.DeserializeObject<QueryPageFilter[]>(filters);
                var info = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryFieldResults(queryFilters);
                foreach (var item in info)
                    Utility.GetGeometres(item.Result);
                result.Data = info;
                result.Total = count;
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        [ValidateInput(false)]
        public System.Web.Mvc.JsonResult Update()
        {
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };
            try
            {
                CheckUserInfo();
                string objs = TH.Commons.Encrypt.DecryptJson(queryParams["objs"]);
                string tableName = TH.Commons.Encrypt.DecryptJson(queryParams["tb"]);
                string part = TH.Commons.Encrypt.DecryptJson(queryParams["part"]);
                bool ispart = false;
                if (!string.IsNullOrEmpty(part) && part == "1")
                    ispart = true;
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                Utility.UpdateGeometry(dic);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(tableName, dic, ispart);
                if (ServiceAppContext.Instance.HistroyBaseManage != null)
                    ServiceAppContext.Instance.HistroyBaseManage.updateToHistory(tableName, dic);
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult FilterUpdate()
        {
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };

            try
            {
                CheckUserInfo();
                string objs = TH.Commons.Encrypt.DecryptJson(queryParams["objs"]);
                string filter = TH.Commons.Encrypt.DecryptJson(queryParams["filter"]);
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(dic, filters);
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        [ValidateInput(false)]
        public System.Web.Mvc.JsonResult Updates()
        {
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };
            try
            {
                CheckUserInfo();


                string objs = TH.Commons.Encrypt.DecryptJson(queryParams["objs"]);
                string part = TH.Commons.Encrypt.DecryptJson(queryParams["part"]);
                bool ispart = false;
                if (!string.IsNullOrEmpty(part) && part == "1")
                    ispart = true;
                var tableList = (JContainer)JsonConvert.DeserializeObject(objs);
                foreach (JToken tables in tableList)
                {
                    var tableInfos = (JProperty)tables;
                    var tableName = tableInfos.Name;
                    var TableFields = JavaScriptSerializer.Json.Deserialize<Dictionary<string, object>[]>(tableInfos.Value.ToString());
                    foreach (var item in TableFields)
                        Utility.UpdateGeometry(TableFields);
                    ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(tableName, TableFields, ispart);
                    if (ServiceAppContext.Instance.HistroyBaseManage != null)
                        ServiceAppContext.Instance.HistroyBaseManage.updateToHistory(tableInfos.Name, TableFields);
                }
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult Delete()
        {
            CheckUserInfo();
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };
            string filter = TH.Commons.Encrypt.DecryptJson(queryParams["filter"]);
            if (!string.IsNullOrEmpty(filter))
            {
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(filters);
            }
            else
            {
                string ids = TH.Commons.Encrypt.DecryptJson(queryParams["ids"]);
                string tableName = TH.Commons.Encrypt.DecryptJson(queryParams["tb"]);
                var idInfos = JsonConvert.DeserializeObject<string[]>(ids);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(tableName, idInfos);
            }
            result.Status = MessageTypeCode.Success;
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult batchInsert()
        {
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };
            try
            {
                string objs = TH.Commons.Encrypt.DecryptJson(queryParams["objs"]);
                string tableName = TH.Commons.Encrypt.DecryptJson(queryParams["tb"]);
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.BatchInsert(tableName, dic);
                result.Status = MessageTypeCode.Success;
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult Stat()
        {
            CheckUserInfo();
            var queryParams = Request;
            var result = new TH.DataBase.JsonResults() { };
            var type = TH.Commons.Encrypt.DecryptJson(queryParams["type"]);
            var condition = TH.Commons.Encrypt.DecryptJson(queryParams["params"]);
            var idInfos = JsonConvert.DeserializeObject<string[]>(condition);
            if (SqlDics.ContainsKey(type))
            {
                var info = ServiceAppContext.Instance.DataBaseClassHelper.GetQueryStatResultN(SqlDics[type], idInfos);
                result.Data = info;
                result.Total = info.Length;
                result.Status = MessageTypeCode.Success;
            }
            else
            {
                result.Status = 400;
                result.Msg = "非法查询！";
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult GetTableInfo()
        {
            ServiceAppContext.Instance.InitSJB();
            int count = 0;
            var result = new TH.DataBase.JsonResults() { };
            try
            {
                string ServiceName = TH.Commons.Encrypt.DecryptJson(Request["ServiceName"]);
                if (string.IsNullOrEmpty(ServiceName) == false)
                {
                    var helper = ServiceAppContext.Instance.GetSJDLDWBDataHelper(ServiceName);
                    if (helper != null)
                    {
                        count = helper.TableToTableNameFields.Count;
                        var list = new List<object>();
                        foreach (var item in helper.TableToTableNameFields)
                            list.Add(new TableInfo() {  TableName= item.Key, Columns= item.Value.ToArray()});
                        result.Data = list.ToArray();
                    }
                    else
                    {
 
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        public System.Web.Mvc.JsonResult CheckFWB()
        {
            ServiceAppContext.Instance.InitSJB();
            int count = 0;
            var result = new TH.DataBase.JsonResults() { };
            try
            {
                string fwTableInfo = TH.Commons.Encrypt.DecryptJson(Request["fwb"]);
                var model = JsonConvert.DeserializeObject<DB_SJFLFWB>(fwTableInfo);
                if (model == null || string.IsNullOrEmpty(model.FWPZ))
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = "数据库配置信息不对！";
                }
                try
                {
                    SqlPrividerType type = (SqlPrividerType)Enum.Parse(typeof(SqlPrividerType), model.FWLX);
                    var conn = DBClassHelper.OpenConnect(model.FWPZ, SqlHelperFactory.GetSqlPrividerTypeName(type));
                    conn.Close();
                    ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(new object[] { model });
                    if (ServiceAppContext.Instance.DataConnections.ContainsKey(model.FWMC))
                        ServiceAppContext.Instance.DataConnections[model.FWMC] = model;
                    if (ServiceAppContext.Instance.ServiceDataClassHelper.ContainsKey(model.FWMC) == false)
                    {
                        TH.DataModels.DBBase dbbase = new DataModels.DBBase();
                        var helper = SqlHelperFactory.GetSqlDataClassHelper(type, model.FWPZ, ServiceAppContext.GetNameSpaceName(dbbase.GetType()));
                        ServiceAppContext.Instance.ServiceDataClassHelper.Add(model.FWMC, helper);
                    }
                }
                catch (Exception ex)
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                if (ex is ServiceException)
                {
                    result.Status = (ex as ServiceException).Code;
                    result.Msg = (ex as ServiceException).Message;
                }
                else
                {
                    result.Status = MessageTypeCode.NotKnown;
                    result.Msg = ex.Message;
                }
            }
            return Utility.GetJsonResult(result);
        }

        private void CheckUserInfo(bool isWriteLog = false)
        {
            string opType = Request["op"];
            string token = Request["token"];
            if (isWriteLog == true)
            {
                var ipPort = Request.ServerVariables["Server_Port"] == null ? 0 : int.Parse(Request.ServerVariables["Server_Port"]);
                var ip = Request.ServerVariables["Remote_Addr"];
            }
        }
        */
    }
}
