using ChatBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot;

public class StartBotWork : BackgroundService
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceProvider _serviceProvider;
    
    public StartBotWork(ITelegramBotClient bot, IServiceProvider serviceProvider)
    {
        _bot = bot;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bot.StartReceiving(
            updateHandler: OnUpdate,
            errorHandler: OnError,
            cancellationToken: stoppingToken
            );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public async Task OnError(ITelegramBotClient botClient, Exception ex, CancellationToken ct)
    {
        Console.WriteLine($"Ошибка api: {ex.Message}");
    }

    public async Task OnUpdate(ITelegramBotClient botClient, Update update, CancellationToken ct)
    {
        await (update switch
        {
            { Message: { } message } => OnMessage(botClient, message, ct),
            { EditedMessage: { } editedMessage } => OnMessage(botClient, editedMessage, ct),
        });
    }

    public async Task OnMessage(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        if(msg.Text is not {} messageText)
            return;

        var mess = messageText.Split(' ')[0].ToLower();
        
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IDbRepository>();
        var apiService = scope.ServiceProvider.GetRequiredService<IApiService>();
        var gemini = scope.ServiceProvider.GetRequiredService<IGeminiService>();

        IBotCommand command = mess switch
        {
            "/mute" => new MuteUser(repository),
            "/unmute" => new UnmuteUser(repository),
            "/start" => new StartBot(),
            "/роптать" => new Roptanie(repository),
            "/отчимы" => new Stepfathers(),
            "/спортики" => new Sportsmens(),
            "/роптания" => new ShowRoptanieBalance(repository),
            "/топроптаний" => new ShowMostRoptania(repository),
            "/фарм" => new RandomRoptania(repository),
            "/гнить" => new Roting(),
            "/список" => new CommandList(),
            "/место" => new GetRandomPlace(apiService),
            "/ии" => new AICall(gemini)
        };
        
        await command.ExecuteCommandAsync(botClient, msg, ct);
    }
}