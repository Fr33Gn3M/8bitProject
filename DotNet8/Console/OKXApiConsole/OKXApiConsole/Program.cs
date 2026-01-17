// See https://aka.ms/new-console-template for more information
using OKXApi;
using System.Text;


// 关键：设置控制台输出编码为 UTF-8
Console.OutputEncoding = Encoding.UTF8;
// 可选：设置输入编码（如果需要读取 UTF-8 字符）
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("Hello, FZP!This OKX API Client 📈");
// 尝试打印 emoji
//Console.WriteLine("测试 emoji：📈");
//Console.WriteLine("Unicode 编码打印：\u1F4C8"); // 直接用 Unicode 编码

var strategy = new PerpetualStrategy();

// 循环运行策略（每15分钟执行一次，对应15m K线周期）
while (true)
{
    strategy.RunStrategy();
    // 休眠15分钟（900秒）
    System.Threading.Thread.Sleep(900 * 1000);
}