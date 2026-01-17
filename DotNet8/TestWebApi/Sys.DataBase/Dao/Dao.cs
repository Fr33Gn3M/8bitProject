using System;
using System.Collections.Generic;

namespace Sys.DataBase.Dao
{
    public class Dao
    {
        private QueryPageFilter tempVar = new QueryPageFilter();
        private IDataClassHelper helper;

        public Dao(IDataClassHelper helper, string name)
        {
            this.helper = helper;
            tempVar.TableName = name;
        }

        public Dao(IDataClassHelper helper, QueryPageFilter pageFilter)
        {
            this.helper = helper;
            tempVar = pageFilter;
        }

        public Dao QueryBuilder(params string[] fieldNames)
        {
            tempVar.ReturnFieldNames = fieldNames;
            return this;
        }

        public Dao Where(params QueryFilterBase[] filters)
        {
            UpdateFilters(filters);
            return this;
        }

        public Dao WhereOr(params QueryFilterBase[] filters)
        {
            tempVar.FilterAndOrType = SQLAndOr.Or;
            return Where(filters);
        }

        public Dao And(params QueryFilterBase[] filters)
        {
            AndOrQueryFilter tempVar = DB.CondAnd(filters);
            UpdateFilters(tempVar);
            return this;
        }

        public Dao Or(params QueryFilterBase[] filters)
        {
            AndOrQueryFilter tempVar = DB.CondOr(filters);
            UpdateFilters(tempVar);
            return this;
        }

        public Dao Limit(int index, int size)
        {
            tempVar.IsPage = true;
            tempVar.PageIndex = index;
            tempVar.PageSize = size;
            return this;
        }

        public Dao OrderAsc(params string[] orderFieldNames)
        {
            tempVar.OrderByType = SQLOrderBy.Asc;
            tempVar.OrderFieldNames = orderFieldNames;
            return this;
        }

        public Dao OrderDesc(params string[] orderFieldNames)
        {
            tempVar.OrderByType = SQLOrderBy.Desc;
            tempVar.OrderFieldNames = orderFieldNames;
            return this;
        }

        public Dao GroupBy(params string[] groupFieldNames)
        {
            tempVar.GroupByFieldNames = groupFieldNames;
            return this;
        }

        public QueryPageFilter Build()
        {
            return tempVar;
        }

        public Dictionary<string, object>[] List()
        {
            return helper.GetQueryResultN(tempVar);
        }
        public Dictionary<string, object>[] List(ref int count)
        {
            return helper.GetQueryResultN(tempVar, ref count);
        }

        public List<T> List<T>() where T : class, new()
        {
            return helper.GetObjList<T>(tempVar);
        }

        public Dictionary<string, object> FindFirst()
        {
            Limit(1, 1);
            return helper.GetQueryDic(tempVar);
        }

        public T FindFirst<T>() where T : class, new()
        {
            Limit(1, 1);
            return helper.GetObj<T>(tempVar);
        }

        public QueryFilterResult Result()
        {
            return helper.GetQueryResult(tempVar);
        }

        public QueryFilterResultDic ResultDic()
        {
            return helper.GetQueryResultDic(tempVar);
        }


        public int Count()
        {
            return helper.GetQueryResultNCount(tempVar);
        }

        public Dictionary<string, object>[] CallFunction(params object[] param)
        {
            var sqlModel = new SqlModel();
            sqlModel.ModelName = tempVar.TableName;
            return helper.GetQueryStatResult(sqlModel, param);
        }

        public bool Add(Dictionary<string, object> obj)
        {
            helper.AddObject(tempVar.TableName, obj);
            return true;
        }

        public bool AddBatch(params Dictionary<string, object>[] objs)
        {
            helper.AddObjects(tempVar.TableName, objs);
            return true;
        }

        /// <summary>
        /// 使用sql server的BulkCopy来批量快速新增
        /// (注意：类的字段顺序要跟数据库的字段顺序一致)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool InsertBatch<T>(List<T> objs)
        {
            helper.BatchInsert(tempVar.TableName, objs);
            return true;
        }

        /// <summary>
        /// 使用sql server的BulkCopy来批量快速新增
        /// (注意：dic的字段顺序要跟数据库的字段顺序一致)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool InsertBatch(Dictionary<string, object>[] dics)
        {
            helper.BatchInsert(tempVar.TableName, dics);
            return true;
        }

        public bool DeleteBatch(params string[] ids)
        {
            helper.DeleteObjects(tempVar.TableName, ids);
            return true;
        }

        public bool OnlyUpdate(Dictionary<string, object> dic)
        {
            helper.UpdateObject(tempVar.TableName, dic);
            return true;
        }

        //public bool OnlyUpdate(object model)
        //{
        //    var dic = helper.ConvertDict2(model);
        //    helper.UpdateObject(tempVar.TableName, dic);
        //    return true;
        //}

        public bool UpdateBatch(params Dictionary<string, object>[] list)
        {
            helper.UpdateObjects(tempVar.TableName, list);
            return true;
        }

        public bool UpdateBatch(params object[] models)
        {
            helper.UpdateObjects(models);
            return true;
        }

        public bool Update(Dictionary<string, object> dic)
        {
            helper.UpdateObjects(tempVar.TableName, new Dictionary<string, object>[] { dic });
            return true;
        }

        public bool Update(object model)
        {
            helper.UpdateObjects(new object[] { model });
            return true;
        }

        public bool BuildUpdate(Dictionary<string, object> objs)
        {
            if (tempVar.Filters == null || tempVar.Filters.Length == 0) throw new Exception("条件不能为空！");
            helper.UpdateObjects(objs, tempVar);
            return true;
        }

        public bool BuildDelete()
        {
            if (tempVar.Filters == null || tempVar.Filters.Length == 0) throw new Exception("条件不能为空！");
            helper.DeleteObjects(tempVar);
            return true;
        }

        private void UpdateFilters(params QueryFilterBase[] filters)
        {
            List<QueryFilterBase> list;
            if (tempVar.Filters == null)
            {
                list = new List<QueryFilterBase>();
            }
            else
            {
                list = new List<QueryFilterBase>(tempVar.Filters);
            }
            foreach (QueryFilterBase item in filters)
            {
                if (item is QueryFilter)
                {
                    QueryFilter filter = (QueryFilter)item;
                    if (filter.Value != null) list.Add(filter);
                }
                if (item is SpatialQueryFilter)
                {
                    SpatialQueryFilter filter = (SpatialQueryFilter)item;
                    if (filter.Geometry != null) list.Add(filter);
                }
                if (item is AndOrQueryFilter)
                {
                    AndOrQueryFilter filter = (AndOrQueryFilter)item;
                    if (filter.Filters != null && filter.Filters.Length > 0)
                        list.Add(filter);
                }
            }
            tempVar.Filters = list.ToArray();
        }
    }
}
