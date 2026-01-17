namespace Sys.DataBase.Dao
{
    /// <summary>
    /// 属性过滤条件
    /// </summary>
    public class Props
    {
        private QueryFilter filter = new QueryFilter();

        public Props(string field)
        {
            filter.FieldName = field;
        }

        public QueryFilter IsNull()
        {
            filter.Sign = SQLSign.IsNuLL;
            filter.Value = "-";
            return filter;
        }

        public QueryFilter IsNotNull()
        {
            filter.Sign = SQLSign.IsNotNuLL;
            filter.Value = "-";
            return filter;
        }

        public QueryFilter Eq(object value)
        {
            filter.Sign = SQLSign.Equal;
            filter.Value = value;
            return filter;
        }

        public QueryFilter NotEq(object value)
        {
            filter.Sign = SQLSign.NoEqual;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Like(object value)
        {
            filter.Sign = SQLSign.Like;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Llike(object value)
        {
            filter.Sign = SQLSign.LeftLike;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Rlike(object value)
        {
            filter.Sign = SQLSign.RightLike;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Gt(object value)
        {
            filter.Sign = SQLSign.MoreThan;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Lt(object value)
        {
            filter.Sign = SQLSign.LessThan;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Ge(object value)
        {
            filter.Sign = SQLSign.MoreEqualThan;
            filter.Value = value;
            return filter;
        }

        public QueryFilter Le(object value)
        {
            filter.Sign = SQLSign.LessEqualThan;
            filter.Value = value;
            return filter;
        }


        public QueryFilter In(string value)
        {
            filter.Sign = SQLSign.In;
            filter.Value = value;
            return filter;
        }

        public QueryFilter In(string[] values)
        {
            string str = null;
            foreach (object item in values)
            {
                if (string.IsNullOrEmpty(str))
                    str = string.Format("'{0}'", item);
                else
                    str += string.Format(",'{0}'", item);
            }
            filter.Sign = SQLSign.In;
            filter.Value = str;
            return filter;
        }

        public QueryFilter NotIn(object value)
        {
            filter.Sign = SQLSign.NotIn;
            filter.Value = value;
            return filter;
        }

        public QueryFilter NotIn(string[] values)
        {
            this.In(values);
            filter.Sign = SQLSign.NotIn;
            return filter;
        }

        public string Max(string name = null)
        {
            return string.Format("1#{0}#{1}", filter.FieldName, name ?? filter.FieldName);
        }

        public string Min(string name = null)
        {
            return string.Format("2#{0}#{1}", filter.FieldName, name ?? filter.FieldName);
        }

        public string Count(string name = null)
        {
            return string.Format("3#{0}#{1}", filter.FieldName, name ?? filter.FieldName);
        }

        public string Sum(string name = null)
        {
            return string.Format("4#{0}#{1}", filter.FieldName, name ?? filter.FieldName);
        }

        public string Avg(string name = null)
        {
            return string.Format("5#{0}#{1}", filter.FieldName, name ?? filter.FieldName);
        }

        public string Left(int num, string name = null)
        {
            return string.Format("6#{0}#{1}#{2}", filter.FieldName, num, name ?? filter.FieldName);
        }
    }
}
