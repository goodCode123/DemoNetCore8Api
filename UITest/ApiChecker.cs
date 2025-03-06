using NUnit.Framework;

public static class ApiChecker
{
    private static bool _isApiConnected = false; // 靜態變數，記錄 API 是否已經連線成功

    /// <summary>
    /// 檢查 API 是否啟動。僅在未檢查成功時執行。
    /// </summary>
    /// <param name="url">API 測試 URL</param>
    /// <param name="retries">最大重試次數</param>
    /// <param name="retryInterval">重試間隔時間（毫秒）</param>
    public static void WaitForApiToStart(int retries = 5, int retryInterval = 2000)
    {
        if (_isApiConnected)
        {
            Console.WriteLine("API 已成功連線，跳過檢查");
            return;
        }

        var handler = new HttpClientHandler
        {
            // 忽略 HTTPS 證書驗證（僅適用於開發環境）
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };

        using var client = new HttpClient(handler);

        while (retries > 0)
        {
            try
            {
                var response = client.GetAsync("https://localhost:7062/swagger/index.html").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("API 已成功啟動");
                    _isApiConnected = true; // 設置成功標記
                    return;
                }
            }
            catch
            {
                Console.WriteLine($"API 尚未啟動，重試中...（剩餘次數: {retries - 1}）");
            }

            System.Threading.Thread.Sleep(retryInterval); // 等待重試
            retries--;
        }

        Assert.Fail("API 無法啟動或無法訪問");
    }
}