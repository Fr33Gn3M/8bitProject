using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace OKXApi
{

    // 配置类
    public static class Config
    {
        // API配置（替换为你的真实信息）
        public const string ApiKey = "befcfddc-081d-4ef8-9f91-d009bcdd5e6b";
        public const string SecretKey = "210A9AC900C974E65CD6A33B969C4B70";
        public const string Passphrase = "Fzp17859763871!";
        // 测试网/实网切换（测试网：https://www.okx.com，实网：https://www.okx.com）
        public const string BaseUrl = "https://www.okx.com";
        // 策略参数
        public const string Symbol = "BTC-USDT-SWAP"; // OKX USDT本位BTC永续合约标识
        public const string Timeframe = "15m"; // K线周期
        public const int Leverage = 5; // 杠杆倍数
        public const double BaseRiskRatio = 0.01; // 基础风险比例：账户资金1%
        public const double StopLossRatio = 0.015; // 止损比例：3%
        public const double TakeProfitRatio = 0.03; // 止盈比例：5%
        public const int MaShort = 20; // 短期均线周期
        public const int MaLong = 60; // 长期均线周期
        public const double HighVolatilityThreshold = 0.05; // 高波动率阈值：5%
        public const double LowVolatilityMultiplier = 1.0; // 低波动率风险乘数
        public const double HighVolatilityMultiplier = 0.5; // 高波动率风险乘数
    }


    // OKX API客户端
    public class OKXApiClient
    {
        private readonly RestClient _client;
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _passphrase;

        public OKXApiClient(string baseUrl, string apiKey, string secretKey, string passphrase)
        {
            _client = new RestClient(baseUrl);
            _apiKey = apiKey;
            _secretKey = secretKey;
            _passphrase = passphrase;
        }

        // 生成OKX签名
        private string GenerateSignature(string timestamp, string method, string requestPath, string body = "")
        {
            var preHash = $"{timestamp}{method}{requestPath}{body}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(preHash));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        // 构建请求头
        private void AddAuthHeaders(RestRequest request, string requestPath, string body = "")
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var signature = GenerateSignature(timestamp, request.Method.ToString(), requestPath, body);

            request.AddHeader("OK-ACCESS-KEY", _apiKey);
            request.AddHeader("OK-ACCESS-SIGN", signature);
            request.AddHeader("OK-ACCESS-TIMESTAMP", timestamp);
            request.AddHeader("OK-ACCESS-PASSPHRASE", _passphrase);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
        }

        // GET请求
        public T Get<T>(string requestPath, Dictionary<string, string> parameters = null)
        {
            var request = new RestRequest(requestPath, Method.Get);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }

            AddAuthHeaders(request, requestPath);
            var response = _client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"GET请求失败: {response.ErrorMessage}");
                return default;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        // POST请求
        public T Post<T>(string requestPath, object body)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            var request = new RestRequest(requestPath, Method.Post);

            request.AddJsonBody(body);
            AddAuthHeaders(request, requestPath, jsonBody);

            var response = _client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"POST请求失败: {response.ErrorMessage}");
                return default;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }

    // 数据模型
    public class KlineData
    {
        public string instId { get; set; }
        public List<List<string>> data { get; set; }
    }

    public class BalanceData
    {
        public List<BalanceDetail> data { get; set; }
    }

    public class BalanceDetail
    {
        public string ccy { get; set; }
        public string availBal { get; set; } // 可用余额
    }

    public class PositionData
    {
        public List<PositionDetail> data { get; set; }
    }

    public class PositionDetail
    {
        public string instId { get; set; }
        public string posSide { get; set; } // 持仓方向 long/short
        public string pos { get; set; } // 持仓数量
        public string avgPx { get; set; } // 平均开仓价格
    }

    public class OrderResponse
    {
        public List<string> data { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
    }

    public class TickerData
    {
        public List<TickerDetail> data { get; set; }
    }

    public class TickerDetail
    {
        public string instId { get; set; }
        public string high24h { get; set; } // 24h最高价
        public string low24h { get; set; }  // 24h最低价
        public string last { get; set; }    // 当前最新价
    }

    // 策略核心逻辑
    public class PerpetualStrategy
    {
        private readonly OKXApiClient _apiClient;
        private double _currentPrice; // 最新行情价格

        public PerpetualStrategy()
        {
            _apiClient = new OKXApiClient(Config.BaseUrl, Config.ApiKey, Config.SecretKey, Config.Passphrase);
        }

        // 设置杠杆
        public bool SetLeverage()
        {
            var requestBody = new
            {
                instId = Config.Symbol,
                lever = Config.Leverage.ToString(),
                mgnMode = "isolated" // 逐仓模式
            };

            var response = _apiClient.Post<dynamic>("/api/v5/account/set-leverage", requestBody);

            if (response == null || response.code != "0")
            {
                Console.WriteLine($"设置杠杆失败: {response?.msg}");
                return false;
            }

            Console.WriteLine($"✅ 杠杆设置成功: {Config.Leverage}倍");
            return true;
        }

        // 获取K线数据
        public List<double> GetKlineData()
        {
            var parameters = new Dictionary<string, string>
            {
                { "instId", Config.Symbol },
                { "bar", Config.Timeframe },
                { "limit", "100" } // 获取最近100根K线
            };

            var klineResponse = _apiClient.Get<KlineData>("/api/v5/market/history-candles", parameters);

            if (klineResponse?.data == null || klineResponse.data.Count == 0)
            {
                Console.WriteLine("❌ 获取K线数据失败");
                return null;
            }

            // 转换收盘价为double列表（按时间正序排列）
            var closes = klineResponse.data
                .OrderBy(k => k[0])
                .Select(k => double.Parse(k[4]))
                .ToList();

            // 记录当前价格
            _currentPrice = closes.Last();
            Console.WriteLine($"📊 获取到{closes.Count}根K线，当前价格: {_currentPrice:F2} USDT");

            return closes;
        }

        // 计算均线
        public double CalculateMA(List<double> prices, int period)
        {
            if (prices.Count < period)
            {
                Console.WriteLine($"❌ K线数量不足，无法计算{period}均线");
                return 0;
            }

            return prices.Skip(prices.Count - period).Take(period).Average();
        }

        // 获取USDT可用余额
        public double GetUSDTBalance()
        {
            var parameters = new Dictionary<string, string>
            {
                { "ccy", "USDT" }
            };

            var balanceResponse = _apiClient.Get<BalanceData>("/api/v5/account/balance", parameters);

            if (balanceResponse?.data == null || balanceResponse.data.Count == 0)
            {
                Console.WriteLine("❌ 获取余额失败");
                return 0;
            }

            var usdtBalance = double.Parse(balanceResponse.data[0].availBal);
            Console.WriteLine($"💰 当前USDT可用余额: {usdtBalance:F2}");

            return usdtBalance;
        }

        // 获取当前持仓
        public (double longPos, double shortPos) GetPosition()
        {
            var parameters = new Dictionary<string, string>
            {
                { "instType", "SWAP" },
                { "instId", Config.Symbol }
            };

            var positionResponse = _apiClient.Get<PositionData>("/api/v5/position/positions", parameters);

            double longPosition = 0;
            double shortPosition = 0;

            if (positionResponse?.data != null && positionResponse.data.Count > 0)
            {
                foreach (var pos in positionResponse.data)
                {
                    if (pos.posSide == "long")
                    {
                        longPosition = double.Parse(pos.pos);
                    }
                    else if (pos.posSide == "short")
                    {
                        shortPosition = double.Parse(pos.pos);
                    }
                }
            }

            Console.WriteLine($"📦 当前持仓 - 多单: {longPosition:F8} BTC, 空单: {shortPosition:F8} BTC");
            return (longPosition, shortPosition);
        }

        // 获取24h波动率
        public double Get24hVolatility()
        {
            var parameters = new Dictionary<string, string>
            {
                { "instId", Config.Symbol }
            };

            var tickerResponse = _apiClient.Get<TickerData>("/api/v5/market/ticker", parameters);

            if (tickerResponse?.data == null || tickerResponse.data.Count == 0)
            {
                Console.WriteLine("❌ 获取24h行情失败");
                return 0;
            }

            var ticker = tickerResponse.data[0];
            double high = double.Parse(ticker.high24h);
            double low = double.Parse(ticker.low24h);
            _currentPrice = double.Parse(ticker.last); // 更新最新价格

            // 计算波动率 = (24h最高价 - 24h最低价) / 当前价格
            double volatility = (high - low) / _currentPrice;
            Console.WriteLine($"📊 24h波动率: {volatility:P2} (最高价: {high:F2}, 最低价: {low:F2})");

            return volatility;
        }

        // 下单函数（含动态止盈止损）
        public bool PlaceOrder(string side, double quantity)
        {
            // side: buy(开多)/sell(开空)/close_long(平多)/close_short(平空)
            string posSide = side.Contains("long") ? "long" : "short";
            string tdMode = "isolated"; // 逐仓
            string ordType = "market"; // 市价单
            string sz = quantity.ToString("0.00000000"); // 下单数量（保留8位小数）

            // 动态计算止盈止损价格（仅开仓单生效）
            double stopLossPrice = 0;
            double takeProfitPrice = 0;
            if (!side.Contains("close"))
            {
                if (side == "buy") // 开多：止损低于当前价，止盈高于当前价
                {
                    stopLossPrice = _currentPrice * (1 - Config.StopLossRatio);
                    takeProfitPrice = _currentPrice * (1 + Config.TakeProfitRatio);
                }
                else if (side == "sell") // 开空：止损高于当前价，止盈低于当前价
                {
                    stopLossPrice = _currentPrice * (1 + Config.StopLossRatio);
                    takeProfitPrice = _currentPrice * (1 - Config.TakeProfitRatio);
                }
            }

            var requestBody = new
            {
                instId = Config.Symbol,
                tdMode = tdMode,
                side = side.Contains("close") ? (side == "close_long" ? "sell" : "buy") : side,
                posSide = side.Contains("close") ? (side == "close_long" ? "long" : "short") : posSide,
                ordType = ordType,
                sz = sz,
                reduceOnly = side.Contains("close") ? true : false,
                slPx = stopLossPrice > 0 ? stopLossPrice.ToString("0.00") : "", // 止损价格
                tpPx = takeProfitPrice > 0 ? takeProfitPrice.ToString("0.00") : ""  // 止盈价格
            };

            var response = _apiClient.Post<OrderResponse>("/api/v5/trade/order", requestBody);

            if (response == null || response.code != "0")
            {
                Console.WriteLine($"❌ 下单失败: {response?.msg}");
                return false;
            }

            if (!side.Contains("close"))
            {
                Console.WriteLine($"✅ {side}下单成功，订单ID: {response.data[0]} | 止损: {stopLossPrice:F2} | 止盈: {takeProfitPrice:F2}");
            }
            else
            {
                Console.WriteLine($"✅ {side}下单成功，订单ID: {response.data[0]}");
            }
            return true;
        }

        // 核心策略执行
        public void RunStrategy()
        {
            try
            {
                Console.WriteLine($"\n========== 策略启动 {DateTime.Now:yyyy-MM-dd HH:mm:ss} ==========");

                // 1. 设置杠杆
                SetLeverage();

                // 2. 获取24h波动率（更新当前价格）
                double volatility = Get24hVolatility();
                if (volatility == 0)
                {
                    Console.WriteLine("❌ 波动率获取失败，策略终止");
                    return;
                }

                // 3. 获取K线数据
                var closes = GetKlineData();
                if (closes == null || closes.Count < Config.MaLong)
                {
                    Console.WriteLine("❌ K线数据不足，策略终止");
                    return;
                }

                // 4. 计算均线
                double maShort = CalculateMA(closes, Config.MaShort);
                double maLong = CalculateMA(closes, Config.MaLong);

                Console.WriteLine($"📈 均线数据 - {Config.MaShort}均线: {maShort:F2}, {Config.MaLong}均线: {maLong:F2}");

                // 5. 获取当前持仓
                var (longPos, shortPos) = GetPosition();

                // 6. 动态调整风险比例
                double dynamicRiskRatio = volatility > Config.HighVolatilityThreshold
                    ? Config.BaseRiskRatio * Config.HighVolatilityMultiplier
                    : Config.BaseRiskRatio * Config.LowVolatilityMultiplier;
                Console.WriteLine($"🎛️ 动态风险比例: {dynamicRiskRatio:P2} (基础: {Config.BaseRiskRatio:P2}, 波动率: {volatility:P2})");

                // 7. 计算下单数量
                double usdtBalance = GetUSDTBalance();
                double riskAmount = usdtBalance * dynamicRiskRatio;
                double orderQuantity = riskAmount / _currentPrice / Config.Leverage;

                // 最小下单数量限制（OKX BTCUSDT永续合约最小0.001 BTC）
                orderQuantity = Math.Max(orderQuantity, 0.001);
                Console.WriteLine($"📝 计划下单数量: {orderQuantity:F8} BTC (风险金额: {riskAmount:F2} USDT)");

                // 8. 策略逻辑判断（基于实时行情的动态信号）
                bool shouldOpenLong = maShort > maLong && longPos == 0 && shortPos == 0; // 金叉开多
                bool shouldOpenShort = maShort < maLong && longPos == 0 && shortPos == 0; // 死叉开空
                bool shouldCloseLong = maShort < maLong && longPos > 0; // 死叉平多
                bool shouldCloseShort = maShort > maLong && shortPos > 0; // 金叉平空

                // 执行交易
                if (shouldOpenLong)
                {
                    Console.WriteLine("📈 金叉信号+低波动率，执行开多");
                    PlaceOrder("buy", orderQuantity);
                }
                else if (shouldOpenShort)
                {
                    Console.WriteLine("📉 死叉信号+低波动率，执行开空");
                    PlaceOrder("sell", orderQuantity);
                }
                else if (shouldCloseLong)
                {
                    Console.WriteLine("📉 死叉信号，执行平多");
                    PlaceOrder("close_long", longPos);
                }
                else if (shouldCloseShort)
                {
                    Console.WriteLine("📈 金叉信号，执行平空");
                    PlaceOrder("close_short", shortPos);
                }
                else
                {
                    Console.WriteLine("ℹ️ 无交易信号，持仓不变");
                }

                Console.WriteLine("========== 策略执行完成 ==========\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 策略执行异常: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    // 主程序
    class Program
    {
        static void Main(string[] args)
        {
            var strategy = new PerpetualStrategy();

            // 循环运行策略（每15分钟执行一次，对应15m K线周期）
            while (true)
            {
                strategy.RunStrategy();
                // 休眠15分钟（900秒）
                System.Threading.Thread.Sleep(900 * 1000);
            }
        }
    }
}
