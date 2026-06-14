using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class Roting : IBotCommand
{
    public string CommandName => "/гнить";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        if (msg.ReplyToMessage is { } replyToMessage)
        {
            if (replyToMessage.From?.Id == 6940002297)
            {
                await botClient.SendMessage(msg.Chat.Id, $"{msg.From?.Username} Пососал 👍", cancellationToken: ct);
                return;
            }
            
            await botClient.SendMessage(msg.Chat.Id, $"У матери {replyToMessage.From?.Username} сгнили ноги 👍", cancellationToken: ct);
        }
    }
}