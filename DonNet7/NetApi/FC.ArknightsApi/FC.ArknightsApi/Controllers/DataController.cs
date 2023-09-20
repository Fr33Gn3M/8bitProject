using FC.Core.Models;
using FC.Database.DataService;
using FC.Database.FilterModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FC.ArknightsApi.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService dataService;

        public DataController(IDataService _dataService)
        {
            dataService = _dataService;
        }

        public ApiResult Reload()
        {
            dataService.Reload();
            return ApiResult.Ok();
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{resource}/{id}")]
        public ApiResult Get(string resource, int id)
        {
            var result = dataService.Get(resource, id);
            return ApiResult.Ok(result, 1);
        }

        /// <summary>
        /// 根据复杂条件查询数据
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        //TODO object filter修改为条件对象
        [HttpPost("query/{resource}")]
        public ApiResult Query(string resource, PageQueryFilter filter)
        {
            var result = dataService.Query(resource, filter);
            return ApiResult.Ok(result);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("create/{resource}")]
        public ApiResult Create(string resource, JObject obj)
        {
            var result = dataService.Create(resource, obj);
            return ApiResult.Ok(result);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update/{resource}")]
        public ApiResult Update(string resource, JObject obj)
        {
            var result = dataService.Update(resource, obj);
            return ApiResult.Ok(result);
        }

        /// <summary>
        /// 删除数据（软删除）
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{resource}/{id}")]
        public ApiResult Delete(string resource, string id)
        {
            var result = dataService.Delete(resource, id);
            return ApiResult.Ok(result);
        }

        /// <summary>
        /// 删除数据（硬删除）
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("drop/{resource}/{id}")]
        public ApiResult Drop(string resource, string id)
        {
            var result = dataService.Drop(resource, id);
            return ApiResult.Ok(result);
        }
    }
}
