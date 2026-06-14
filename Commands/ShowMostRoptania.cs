using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Commands;

public class ShowMostRoptania : IBotCommand
{
    private readonly IDbRepository _repository;

    public ShowMostRoptania(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/топроптаний";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var usersRoptaniaList = await _repository.ShowMostRoptaniaInCurrentChat(msg.Chat.Id);

        var rows = usersRoptaniaList.Select(u => $" <b>{u.UserNameInDb}</b> — <code>{u.Roptania} роптаний </code>");
        var message = "<b>Список роптаний:</b>\n\n" + string.Join("\n", rows);

        await botClient.SendMessage(msg.Chat.Id, message, parseMode: ParseMode.Html ,cancellationToken: ct);
    }
}