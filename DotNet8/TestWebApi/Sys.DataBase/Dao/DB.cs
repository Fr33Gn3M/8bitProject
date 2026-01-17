using System.Collections.Generic;

namespace Sys.DataBase.Dao
{
    /// <summary>
    /// 数据库操作工具类
    /// </summary>
    public class DB
    {
        private IDataClassHelper helper;

        public DB(IDataClassHelper helper)
        {
            this.helper = helper;
        }

        public Dao Dao(string name)
        {
            return new Dao(helper, name);
        }

        public Dao Dao(QueryPageFilter pageFilter)
        {
            return new Dao(helper, pageFilter);
        }

        public IDataClassHelper GetHelper()
        {
            return helper;
        }

        public static Props Props(string field)
        {
            return new Props(field);
        }

        public static Geom Geom(string field)
        {
            return new Geom(field);
        }

        public static AndOrQueryFilter CondOr(params QueryFilterBase[] filters)
        {
            AndOrQueryFilter queryFilter = new AndOrQueryFilter();
            List<QueryFilterBase> list = new List<QueryFilterBase>();
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
            queryFilter.FilterAndOrType = SQLAndOr.Or;
            queryFilter.Filters = list.ToArray();
            return queryFilter;
        }

        public static AndOrQueryFilter CondAnd(params QueryFilterBase[] filters)
        {
            AndOrQueryFilter queryFilter = CondOr(filters);
            queryFilter.FilterAndOrType = SQLAndOr.And;
            return queryFilter;
        }
    }
}
