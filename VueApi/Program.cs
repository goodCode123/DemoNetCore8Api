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
        policy.AllowAnyOrigin()  // ���\����ӷ�
              .AllowAnyHeader()  // ���\������Y
              .AllowAnyMethod(); // ���\���� HTTP ��k
    });
});

var env = builder.Environment;

// �[�����P���t�m���
IConfigurationBuilder configBuilder = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()) // �]�w���ε{���ڥؿ�
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // �򥻳]�w
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) // �ھ����ҥ[�����P���]�w
    .AddEnvironmentVariables(); // �[�������ܶq

if (env.IsProduction())
{
    builder.Configuration.AddJsonFile("/dotnet/appsettings/", optional: true, reloadOnChange: true);
}


IConfiguration _config = configBuilder.Build();
_config.Bind(new AppSettings(_config));

// �]�w Newtonsoft.Json ���q�{�欰
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // �T�� camelCase
});

builder.AddSerilog();
builder.AddJwt();
builder.AddAutofac();

var app = builder.Build();

app.UseSerilogRequestLogging(); // �ҥ� Request Logging
app.UseCustomSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// �ϥ� CORS ��h
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
