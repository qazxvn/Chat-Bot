using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class Stepfathers : IBotCommand
{
    public string CommandName => "/отчимы";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        if (msg.ReplyToMessage is { } reply)
        {
            if (reply.From?.Id == 6940002297)
            {
                await botClient.SendMessage(msg.Chat.Id, $"{msg.From?.Username} Пососал 👍", cancellationToken: ct);
                return;
            }
            
            var user = reply.From?.FirstName;

            await botClient.SendMessage(msg.Chat.Id, $"отчимы выебали {user}", cancellationToken: ct);
        }
    }
}