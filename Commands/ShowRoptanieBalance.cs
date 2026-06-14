using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class ShowRoptanieBalance : IBotCommand
{
    private readonly IDbRepository _repository;

    public ShowRoptanieBalance(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/роптания";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var userBalance = await _repository.ShowUserRoptania(msg.From?.Id);
        
        await botClient.SendMessage(msg.Chat.Id, $"У {msg.From?.Username} {userBalance} роптаний", cancellationToken: ct);
    }
}