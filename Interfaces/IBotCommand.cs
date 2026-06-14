using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot;

public interface IBotCommand
{
    string CommandName { get; }
    Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct);
}