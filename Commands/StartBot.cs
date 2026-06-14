using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class StartBot : IBotCommand
{
    public string CommandName => "/start";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        await botClient.SendMessage(msg.Chat.Id, "Бот для роптания", cancellationToken: ct);
    }
}