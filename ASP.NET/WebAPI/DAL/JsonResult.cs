using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace DAL
{
    public class JsonResult
    {
        public JsonResult()
        {
            Status = 1;
        }
        public int Status { get; set; }//1为正常，-1为异常,0为登录异常
        public string Error { get; set; } //错误提示信息
        public object[] Rows { get; set; }//得到结果
        public object Result { get; set; }//得到结果
        //public int current { get; set; }
        //public int rowCount { get; set; }
        public int Total { get; set; }

        //public static QueryPageFilter GetJsonResult(NameValueCollection queryParams)
        //{
        //    string filter = queryParams["filter"];
        //    var filters = JsonConvert.DeserializeObject<QueryPageFilter>(filter);
        //    if (filters.IsPage == true)
        //    {
        //        //int pagesize = int.Parse(queryParams["rowCount"]);
        //        //int pageIndex = int.Parse(queryParams["current"]);
        //        //filters.PageIndex = filters.PageIndex;
        //       // filters.PageSize = fil;
        //        //if (pagesize == -1)
        //        //    filters.IsPage = false;
        //    }
        //    return filters;
        //}

    }

    public class JsonResults
    {
        public JsonResults()
        {
            status = 1;
        }
        public int status { get; set; }//1为正常，-1为异常,0为登录异常
        public string message { get; set; } //错误提示信息
        //public object[] Rows { get; set; }//得到结果
        public object result { get; set; }//得到结果
        
        public int Total { get; set; }

       

    }

    public class JsonResultDic
    {
        public JsonResultDic()
        {
            Status = 1;
        }
        public int Status { get; set; }//1为正常，-1为异常,0为登录异常
        public string Error { get; set; } //错误提示信息
        public Dictionary<string,object>[] Rows { get; set; }//得到结果
        public object Result { get; set; }//得到结果
        public int Total { get; set; }
    }

   
}
