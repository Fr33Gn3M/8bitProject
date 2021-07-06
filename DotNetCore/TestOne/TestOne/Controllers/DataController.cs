using System;
using System.Collections.Generic;
using CommonLog;
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
            return "running。。。";          
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        [HttpPost("query")]
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="objs">要更新的数据模型</param>
        /// <param name="tb">表名</param>
        /// <param name="pu">是否局部更新</param>
        /// <returns></returns>
        [HttpPost("update")]
        public JsonResult Update(string objs, string tb, string pu)
        {
            var result = new JsonResults() { };
            try
            {
                bool isPartialUpdates = false;
                if (!string.IsNullOrEmpty(pu) && pu == "1")
                    isPartialUpdates = true;
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(tb, dic, isPartialUpdates);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">要删除的id数组</param>
        /// <param name="tb">表名</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public JsonResult Delete(string ids, string tb)
        {
            var result = new JsonResults() { };
            try
            {
                var idInfos = JsonConvert.DeserializeObject<string[]>(ids);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(tb, idInfos);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="objs">更新模型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        [HttpPost("fupdate")]
        public JsonResult FilterUpdate(string objs, string filter)
        {
            var result = new JsonResults() { };

            try
            {
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(objs);
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                ServiceAppContext.Instance.DataBaseClassHelper.UpdateObjects(dic, filters);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        [HttpPost("fdelete")]
        public JsonResult FilterDelete(string filter)
        {
            var result = new JsonResults() { };

            try
            {
                var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
                ServiceAppContext.Instance.DataBaseClassHelper.DeleteObjects(filters);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="tb">表名</param>
        /// <param name="objs">模型</param>
        /// <returns></returns>
        public JsonResult batchInsert(string tb, string objs)
        {
            var result = new JsonResults() { };

            try
            {
                var dic = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(objs);
                ServiceAppContext.Instance.DataBaseClassHelper.BatchInsert(tb, dic);
            }
            catch (Exception ex)
            {
                result.setCode(ResultCode.CUSTOMEXCEPTION).setMsg(ex.Message);
            }
            return Utility.GetComResult(result);
        }
    }
}
