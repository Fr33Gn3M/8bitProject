namespace FD.DataBase.Models
{
    public class DataResult
    {
        public DataResult()
        {
            Status = 1000;
        }
        public int Status { get; set; }//1000为正常，9999为异常
        public string Error { get; set; } //错误提示信息
        public object[] Rows { get; set; }//结果集
        public object Result { get; set; }//结果对象
        public int Total { get; set; }//总数
    }
}
