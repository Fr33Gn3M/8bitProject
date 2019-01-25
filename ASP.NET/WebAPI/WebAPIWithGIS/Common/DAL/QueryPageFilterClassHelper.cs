using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public static class QueryPageFilterClassHelper
    {
        #region 属性查询
        
        public static QueryPageFilter Equal(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.Equal, value);
        }

        public static QueryPageFilter MoreThan(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.MoreThan, value);
        }

        public static QueryPageFilter LessThan(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.LessThan, value);
        }

        public static QueryPageFilter Like(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.Like, value);
        }

        public static QueryPageFilter MoreEqualThan(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.MoreEqualThan, value);
        }

        public static QueryPageFilter LessEqualThan(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.LessEqualThan, value);
        }

        public static QueryPageFilter NoEqual(this QueryPageFilter collection, string FieldName, object value)
        {
            return UpdateQuery(collection, FieldName, SQLSign.NoEqual, value);
        }

        public static QueryPageFilter In(this QueryPageFilter collection, string FieldName, string[] value)
        {
            var str = GetSqlIn(value);
            return UpdateQuery(collection, FieldName, SQLSign.In, str);
        }

        public static QueryPageFilter NotIn(this QueryPageFilter collection, string FieldName, string[] value)
        {
            var str = GetSqlIn(value);
            return UpdateQuery(collection, FieldName, SQLSign.NotIn, str);
        }

        public static QueryPageFilter Page(this QueryPageFilter collection, int PageSize, int PageIndex)
        {
            collection.IsPage = true;
            collection.PageIndex = PageIndex;
            collection.PageSize = PageSize;
            return collection;
        }

        public static QueryPageFilter IsNuLL(this QueryPageFilter collection, string FieldName)
        {
            return UpdateQuery(collection, FieldName, SQLSign.IsNuLL, null);
        }

        public static QueryPageFilter IsNotNuLL(this QueryPageFilter collection, string FieldName)
        {
            return UpdateQuery(collection, FieldName, SQLSign.IsNotNuLL, null);
        }

        public static QueryPageFilter AndFilters(this QueryPageFilter collection, QueryFilterBase[] queryFilter)
        {
            var fitler = new AndOrQueryFilter() { FilterAndOrType = SQLAndOr.And };
            var filters = new List<QueryFilterBase>(queryFilter);
            fitler.Filters = filters.ToArray();
            return UpdateQuery(collection, fitler);
        }

        public static QueryPageFilter OrFilters(this QueryPageFilter collection, QueryFilterBase[] queryFilter)
        {
            var fitler = new AndOrQueryFilter() { FilterAndOrType = SQLAndOr.Or };
            var filters = new List<QueryFilterBase>(queryFilter);
            fitler.Filters = filters.ToArray();
            return UpdateQuery(collection, fitler);
            // return UpdateQuery(collection, FieldName, SQLSign.IsNotNuLL, null);
        }

        public static QueryPageFilter AndOrFilters(this QueryPageFilter collection, QueryFilterBase queryFilter)
        {
            return UpdateQuery(collection, queryFilter);
        }

        private static QueryPageFilter UpdateQuery(QueryPageFilter collection, QueryFilterBase filter)
        {
            List<QueryFilterBase> list;
            if (collection.Filters == null)
                list = new List<QueryFilterBase>();
            else
                list = new List<QueryFilterBase>(collection.Filters);
            list.Add(filter);
            collection.Filters = list.ToArray();
            return collection;
        }


        //public static QueryPageFilter And(this QueryPageFilter collection)
        //{
        //    collection.FilterAndOrType =  SQLAndOr.And;
        //    return collection;
        //}

        //public static QueryPageFilter Or(this QueryPageFilter collection)
        //{
        //    collection.FilterAndOrType =  SQLAndOr.Or;
        //    return collection;
        //}

        public static QueryPageFilter OrderBy(this QueryPageFilter collection,  string[] OrderFieldNames)
        {
            collection.OrderByType = SQLOrderBy.Asc;
            collection.OrderFieldNames = OrderFieldNames;
            return collection;
        }

        public static QueryPageFilter OrderByDesc(this QueryPageFilter collection, string[] OrderFieldNames)
        {
            collection.OrderByType = SQLOrderBy.Desc;
            collection.OrderFieldNames = OrderFieldNames;
            return collection;
        }

        public static QueryPageFilter ReturnFields(this QueryPageFilter collection, string[] ReturnFieldNames)
        {
            collection.ReturnFieldNames = ReturnFieldNames;
            return collection;
        }

        public static QueryPageFilter Funtion(this QueryPageFilter collection,SqlFunctionCondition sqlFuntion)
        {
            collection.SqlFunctionCondition = sqlFuntion;
            return collection;
        }

        public static string GetSqlIn(string[] listIds)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in listIds)
            {
                if (builder.Length != 0)
                    builder.Append(",");
                builder.Append("'" + item + "'");
            }
            return builder.ToString();
        }

        private static QueryPageFilter UpdateQuery(QueryPageFilter collection, string FieldName, SQLSign sign, object value)
        {
            List<QueryFilterBase> list;
            if (collection.Filters == null)
                list = new List<QueryFilterBase>();
            else
                list = new List<QueryFilterBase>(collection.Filters);
            var filter = GetQueryFilter(FieldName, sign, value);
            list.Add(filter);
            collection.Filters = list.ToArray();
            return collection;
        }

        //private static QueryPageFilter UpdateQuery(QueryPageFilter collection, string FieldName, SQLSign sign, object value)
        //{
        //    List<QueryFilterBase> list;
        //    if (collection.Filters == null)
        //        list = new List<QueryFilterBase>();
        //    else
        //        list = new List<QueryFilterBase>(collection.Filters);
        //    var filter = GetQueryFilter(FieldName, sign, value);
        //    list.Add(filter);
        //    collection.Filters = list.ToArray();
        //    return collection;
        //}

        public static string GetSqlIn(object[] listIds)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in listIds)
            {
                if (builder.Length != 0)
                    builder.Append(",");
                builder.Append("'" + item + "'");
            }
            return builder.ToString();
        }

        public static QueryFilter GetQueryFilter(string fieldName, SQLSign sign, object value)
        {
            QueryFilter qf = new QueryFilter();
            qf.FieldName = fieldName;
            qf.Sign = sign;
            qf.Value = value;
            return qf;
        }

        #endregion

        #region 要素查询
        public static QueryPageFilter STWithin(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STWithin, value, true);
        }

        public static QueryPageFilter NotSTWithin(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STWithin, value, false);
        }

        public static QueryPageFilter STTouches(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STTouches, value, true);
        }

        public static QueryPageFilter NotSTTouches(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STTouches, value, false);
        }

        public static QueryPageFilter STOverlaps(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STOverlaps, value, true);
        }

        public static QueryPageFilter NotSTOverlaps(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STOverlaps, value, false);
        }

        public static QueryPageFilter STIntersects(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STIntersects, value, true);
        }

        public static QueryPageFilter NotSTIntersects(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STIntersects, value, false);
        }


        public static QueryPageFilter STEquals(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STEquals, value, true);
        }

        public static QueryPageFilter NotSTEquals(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STEquals, value, false);
        }

        public static QueryPageFilter STDisjoint(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STDisjoint, value, true);
        }

        public static QueryPageFilter NotSTDisjoint(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STDisjoint, value, false);
        }

        public static QueryPageFilter STCrosses(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STCrosses, value, true);
        }

        public static QueryPageFilter NotSTCrosses(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STCrosses, value, false);
        }
        public static QueryPageFilter STContains(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STContains, value, true);
        }

        public static QueryPageFilter NotSTContains(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.STContains, value, false);
        }

        public static QueryPageFilter Filter(this QueryPageFilter collection, string FieldName, THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.Filter, value, true);
        }
            
        public static QueryPageFilter NotFilter(this QueryPageFilter collection, string FieldName,THGeometry value)
        {
            return UpdateSpatialQuery(collection, FieldName, SpatialSign.Filter, value,false);
        }

        private static QueryPageFilter UpdateSpatialQuery(QueryPageFilter collection, string FieldName, SpatialSign sign, THGeometry value, bool isTrue)
        {
            List<QueryFilterBase> list;
            if (collection.Filters == null)
                list = new List<QueryFilterBase>();
            else
                list = new List<QueryFilterBase>(collection.Filters);
            var filter = GetQueryFilter(FieldName, sign, value,isTrue);
            list.Add(filter);
            collection.Filters = list.ToArray();
            return collection;
        }

        public static SpatialQueryFilter GetQueryFilter(string fieldName, SpatialSign sign, THGeometry value,bool isTrue)
        {
            var qf = new SpatialQueryFilter();
            qf.FieldName = fieldName;
            qf.Sign = sign;
            qf.Geometry = value;
            qf.IsTrue = isTrue;
            return qf;
        }
        #endregion

    }
}
