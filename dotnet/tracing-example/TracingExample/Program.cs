using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TracingExample.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
                  .AddSource(DiagnosticsConfig.ActivitySource.Name)
                  .AddSource(DiagnosticsConfig.BrokerServiceActivitySource.Name)
                  .ConfigureResource(resource => resource
                      .AddService("TracingExampleService"))
                  .AddAspNetCoreInstrumentation()
                  .AddConsoleExporter()
                  .AddJaegerExporter()
                  .AddHttpClientInstrumentation()
                  ;
        tracerProviderBuilder.ConfigureServices(services => services.Configure<JaegerExporterOptions>(builder.Configuration.GetSection("Jaeger")));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
