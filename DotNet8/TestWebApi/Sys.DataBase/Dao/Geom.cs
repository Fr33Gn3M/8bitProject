namespace Sys.DataBase.Dao
{
    /// <summary>
    /// 空间过滤条件
    /// </summary>
    public class Geom
    {
        private SpatialQueryFilter geomFilter = new SpatialQueryFilter();

        public Geom(string field)
        {
            geomFilter.FieldName = field;
        }

        public SpatialQueryFilter Within(string value)
        {
            geomFilter.Sign = SpatialSign.STWithin;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotWithin(string value)
        {
            geomFilter.IsTrue = false;
            return Within(value);
        }

        public SpatialQueryFilter Touches(string value)
        {
            geomFilter.Sign = SpatialSign.STTouches;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotTouches(string value)
        {
            geomFilter.IsTrue = false;
            return Touches(value);
        }

        public SpatialQueryFilter Overlaps(string value)
        {
            geomFilter.Sign = SpatialSign.STOverlaps;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotOverlaps(string value)
        {
            geomFilter.IsTrue = false;
            return Overlaps(value);
        }

        public SpatialQueryFilter Intersects(string value)
        {
            geomFilter.Sign = SpatialSign.STIntersects;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotIntersects(string value)
        {
            geomFilter.IsTrue = false;
            return Intersects(value);
        }

        public SpatialQueryFilter Equals(string value)
        {
            geomFilter.Sign = SpatialSign.STEquals;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotEquals(string value)
        {
            geomFilter.IsTrue = false;
            return Equals(value);
        }

        public SpatialQueryFilter Disjoint(string value)
        {
            geomFilter.Sign = SpatialSign.STDisjoint;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotDisjoint(string value)
        {
            geomFilter.IsTrue = false;
            return Disjoint(value);
        }

        public SpatialQueryFilter Crosses(string value)
        {
            geomFilter.Sign = SpatialSign.STCrosses;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotCrosses(string value)
        {
            geomFilter.IsTrue = false;
            return Crosses(value);
        }

        public SpatialQueryFilter Contains(string value)
        {
            geomFilter.Sign = SpatialSign.STContains;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotContains(string value)
        {
            geomFilter.IsTrue = false;
            return Contains(value);
        }

        public SpatialQueryFilter Filter(string value)
        {
            geomFilter.Sign = SpatialSign.Filter;
            geomFilter.Geometry = THGeometry.ToTHGeometry(value, 0);
            return geomFilter;
        }

        public SpatialQueryFilter NotFilter(string value)
        {
            geomFilter.IsTrue = false;
            return Filter(value);
        }
    }
}
