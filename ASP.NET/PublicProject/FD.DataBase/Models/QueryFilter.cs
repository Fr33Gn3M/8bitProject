using FD.DataBase.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;

namespace FD.DataBase
{
    public enum SQLSign
    {
        IsNuLL,
        IsNotNuLL,
        Like,//like
        Equal,//相等
        MoreThan,//>
        LessThan,//<
        MoreEqualThan,//>=
        LessEqualThan,//<=
        NoEqual,//<>
        In,//包含
        NotIn,//不包含
    }

    public enum SQLOrderBy
    {
        Desc,
        Asc
    }

    public enum SQLAndOr
    {
        And,
        Or
    }

    [Serializable()]
    [JsonObject("FilterBase")]
    public  class QueryFilterBase
    {
            
    }

    [Serializable()]
    [JsonObject("AndOrFilter")]
    public class AndOrQueryFilter : QueryFilterBase
    {
        [JsonProperty("Filters")]
        [JsonConverter(typeof(QueryFilterCollectionConverter))]
        public QueryFilterBase[] Filters { get; set; }
        [JsonProperty("FilterAndOrType")]
        public SQLAndOr FilterAndOrType { get; set; }
    }

    [JsonObject("Filter")]
    public class QueryFilter : QueryFilterBase
    {
        [JsonProperty("FieldName")]
        public string FieldName { get; set; }
        [JsonProperty("Value")]
        public object Value { get; set; }//如果是in和notIn用;隔开
        [JsonProperty("Sign")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SQLSign Sign { get; set; }
    }

    [JsonObject("SpatialFilter")]
    public class SpatialQueryFilter : QueryFilterBase
    {
        public SpatialQueryFilter()
        {
            IsTrue = true;
        }
        [JsonProperty("Geometry")]
        public FDGeometry Geometry { get; set; }
        [JsonProperty("FieldName")]
        public string FieldName { get; set; }
        [JsonProperty("Sign")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpatialSign Sign { get; set; }
        [JsonProperty("IsTrue")]
        public bool IsTrue { get; set; }
    }

    public enum SpatialSign
    {
        Filter,//是否相交
        STContains,
        STCrosses,
        STDisjoint,
        STEquals,
        STIntersects,
        STOverlaps,
        STTouches,
        STWithin
    }

    [JsonObject("QueryPageFilter")]
    public class QueryPageFilter
    {
        public QueryPageFilter()
        {
            IsPage = false;
            IsReturnCount = false;
            IsDelete = false;
            IsConvertPart = false;
        }
        [JsonProperty("TableName")]
        public string TableName { get; set; }//ViewName视图名称或表名
        [JsonProperty("IsPage")]
        public bool IsPage { get; set; }
        [JsonProperty("PageSize")]
        public int PageSize { get; set; }
        [JsonProperty("PageIndex")]
        public int PageIndex { get; set; }
        [JsonProperty("Filters")]
        [JsonConverter(typeof(QueryFilterCollectionConverter))]
        public QueryFilterBase[] Filters { get; set; }
        [JsonProperty("FilterAndOrType")]
        public SQLAndOr FilterAndOrType { get; set; }
        [JsonProperty("OrderFieldNames")]
        public string[] OrderFieldNames { get; set; }
        [JsonProperty("GroupByFieldNames")]
        public string[] GroupByFieldNames { get; set; }
        [JsonProperty("OrderByType")]
        public SQLOrderBy OrderByType { get; set; }
        [JsonProperty("ReturnFieldNames")]
        public string[] ReturnFieldNames { get; set; }
        [JsonProperty("SqlFunctionCondition")]
        public SqlFunctionCondition SqlFunctionCondition { get; set; }
        [JsonProperty("IsDelete")]
        public bool IsDelete { get; set; }
        [JsonProperty("IsReturnCount")]
        public bool IsReturnCount { get; set; }
        [JsonProperty("IsConvertPart")]
        public bool IsConvertPart { get; set; }

        public static QueryPageFilter Create(string tableName)
        {
            return new QueryPageFilter() { TableName = tableName };
        }
        //public SpatialQueryFilter[] SpatialFilters { get; set; }
        public static QueryPageFilter Create<T>()
        {
            var t = typeof(T).Name;
            return new QueryPageFilter() { TableName = t };
        }

        public QueryFilterBase ToFilters()
        {
            var fitler = new AndOrQueryFilter() { FilterAndOrType = FilterAndOrType, Filters = Filters };
            return fitler;
        }

        internal static bool IsSqlFilter(object obj)
        {
            if (obj == null)
                return false;
            var InText = obj.ToString();
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join";
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }
    }

    [JsonObject("SqlFunctionCondition")]
    public class SqlFunctionCondition 
    {
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SqlFunctionType Type { get; set; }
        [JsonProperty("Params")]
        public string[] Params { get; set; }
    }

    public enum SqlFunctionType
    {
        Max,
        Min,
        Count,
        Sum
    }
    public class FilterQueryResult
    {
        public int TotalCount { get; set; }
        public DataTable Result { get; set; }
    }
}
