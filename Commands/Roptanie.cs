using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class Roptanie : IBotCommand
{
    private readonly IDbRepository _repository;

    public Roptanie(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/роптать";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        await _repository.AddRoptanie(msg.From?.Id, msg.From?.Username, msg.Chat.Id);

        await botClient.SendMessage(msg.Chat.Id, $"{msg.From?.Username} +1 роптание", cancellationToken: ct);
    }
}