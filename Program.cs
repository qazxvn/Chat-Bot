using ChatBot;
using ChatBot.Data;
using ChatBot.Repositories;
using ChatBot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<ITelegramBotClient>(provider => 
    new TelegramBotClient("8550020751:AAGRDL1q24yTi5dkkN-gj0UR-V6-e3agN5w"));

builder.Services.AddHostedService<StartBotWork>();
builder.Services.AddScoped<IDbRepository, DbRepository>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();


builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

using IHost host = builder.Build();
await host.RunAsync();