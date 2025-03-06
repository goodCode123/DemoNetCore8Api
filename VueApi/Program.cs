using Repository;
using Serilog;
using DemoNetCore8Api.Service.Ioc;
using DemoNetCore8Api.Service.Jwt;
using DemoNetCore8Api.Service.Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // 允許任何來源
              .AllowAnyHeader()  // 允許任何標頭
              .AllowAnyMethod(); // 允許任何 HTTP 方法
    });
});

var env = builder.Environment;

// 加載不同的配置文件
IConfigurationBuilder configBuilder = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()) // 設定應用程式根目錄
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // 基本設定
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) // 根據環境加載不同的設定
    .AddEnvironmentVariables(); // 加載環境變量

if (env.IsProduction())
{
    builder.Configuration.AddJsonFile("/dotnet/appsettings/", optional: true, reloadOnChange: true);
}


IConfiguration _config = configBuilder.Build();
_config.Bind(new AppSettings(_config));

// 設定 Newtonsoft.Json 的默認行為
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // 禁用 camelCase
});

builder.AddSerilog();
builder.AddJwt();
builder.AddAutofac();

var app = builder.Build();

app.UseSerilogRequestLogging(); // 啟用 Request Logging
app.UseCustomSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// 使用 CORS 原則
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
