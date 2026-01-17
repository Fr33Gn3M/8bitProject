namespace FC.Database.Models
{
    public class PageQueryDicResult
    {
        public PageQueryDicResult(List<Dictionary<string, object>> dataList, int total)
        {
            Total = total;
            DataList = dataList;
        }

        public int Total { get; set; }
        public List<Dictionary<string, object>> DataList { get; set; }
    }
}
