using Microsoft.Extensions.Logging;
using NBPExchangeRates.Application;
using NBPExchangeRates.Infrastructure;
using NBPExchangeRates.Persistence;
using NBPExchangeRates.WebApp.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});
    
builder.Services.AddControllersWithViews();

builder.Services.AddPersistence(configuration);

builder.Services.AddInfrastructure(configuration);

builder.Services.AddApplication();

var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddSerilog();

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();