namespace FC.Database.Models
{
    public class PageQueryResult<T>
    {
        public PageQueryResult(List<T> dataList, int total) 
        {
            Total = total;
            DataList = dataList;
        }

        public int Total { get; set; }
        public List<T> DataList { get; set; }
    }
}
