using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace DemoNetCore8Api.Service.Serilog
{
    public static class AddSerilogModule
    {
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("NDRS");
            var columnOptions = new ColumnOptions
            {
                Store = new Collection<StandardColumn>
                {
                    StandardColumn.Id,
                    StandardColumn.TimeStamp,
                    StandardColumn.Level,
                    StandardColumn.Message,
                    StandardColumn.Exception 
                },
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("RequestPath", SqlDbType.NVarChar) { DataLength = 200 },
                    new SqlColumn("RequestBody", SqlDbType.NVarChar) { DataLength = -1 },
                    new SqlColumn("ResponseBody", SqlDbType.NVarChar) { DataLength = -1 },
                    new SqlColumn("ProcessingTime", SqlDbType.Float),
                    new SqlColumn("StatusCode", SqlDbType.Int) 
                }
            };

            // 配置 Serilog
                Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.MSSqlServer(
                   connectionString,
                   sinkOptions: new MSSqlServerSinkOptions
                   {
                       TableName = "ApiLogs",
                       AutoCreateSqlTable = true
                   },
                   columnOptions: columnOptions,
                   restrictedToMinimumLevel: LogEventLevel.Information
               ).Filter.ByIncludingOnly(logEvent =>
               {
                   if (logEvent.Properties.TryGetValue("IsCustomLog", out var isCustomLogValue))
                   {
                       return isCustomLogValue.ToString().Equals("true", StringComparison.OrdinalIgnoreCase);
                   }
                   return false;
               })
               .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
