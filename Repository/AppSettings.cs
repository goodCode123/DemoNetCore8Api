using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class AppSettings
    {
        /// <summary>
        /// 參數設定
        /// </summary>
        /// <param name="configuration"></param>
        public AppSettings(IConfiguration configuration)
        {
            //https://blog.darkthread.net/blog/read-aspnetcore-appsetting-from-classlib/
            configuration.Bind(this);
        }

        /// <summary>
        /// DB連線字串
        /// </summary>
        public static DB ConnectionStrings { get; set; }

        public static Jwt JwtSettings { get; set;}
    }

    public class DB
    {
        /// <summary>
        /// Wms Edi DB
        /// </summary>
        public string? NDRS { get; set; }

        /// <summary>
        /// Hangfire DB
        /// </summary>
        public string? HangfireDB { get; set; }

        /// <summary>
        /// 金財通 DB
        /// </summary>
        public string? DbWms { get; set; }

        /// <summary>
        /// Tms DB
        /// </summary>
        public string? DbTms { get; set; }

        /// <summary>
        /// Next DB
        /// </summary>
        public string? Next { get; set; }
    }

    public class Jwt
    {
        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public required string SigningKey { get; set; }

        public required string Expires { get; set; }
    }
}
