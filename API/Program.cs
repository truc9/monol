using API.Endpoints;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Features.Common.Extensions;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft.AspnetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Extensions.Hosting", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
    .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
    .CreateLogger();

const string CORS_POLICY = "GRIDWISE_CORS_POLICY";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddLogging(b => b.AddSerilog(dispose: true));
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddBusinessServices();
builder.Services.AddPostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL")!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSerilogRequestLogging();
}

app.UseCors(CORS_POLICY);

app.UseHttpsRedirection();

app.UseAuthorization();

// Map endpoints
app.UseCustomerEndpoints();
app.UseWorkOrderEndpoints();

app.Run();