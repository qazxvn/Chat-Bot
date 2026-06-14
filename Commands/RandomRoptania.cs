using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class RandomRoptania : IBotCommand
{
    private readonly IDbRepository _repository;

    public RandomRoptania(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/фарм";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var isDailyRoptania = await _repository.DailyRoptania(msg.From?.Id);

        if (isDailyRoptania.canObtained)
        {
            await botClient.SendMessage(msg.Chat.Id, $"{msg.From?.Username} получил {isDailyRoptania.roptania} роптаний", cancellationToken: ct);
        }
        else
        {
            await botClient.SendMessage(msg.Chat.Id, "Время еще не прошло", cancellationToken: ct);
        }
    }
}