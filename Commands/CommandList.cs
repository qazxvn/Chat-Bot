using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Commands;

public class CommandList : IBotCommand
{
    public string CommandName => "/список";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var commands = new List<string>
        {
            "/фарм",
            "/гнить",
            "/топроптаний",
            "/mute",
            "/роптать",
            "/роптания",
            "/спортики",
            "/start",
            "/отчимы",
            "/unmute",
            "/место",
            "/ии"
        };

        var rows = commands.Select(c => $"<code>{c}</code>");
        var message = "<b>Список команд:</b>\n\n" + string.Join("\n", rows);

        await botClient.SendMessage(
            msg.Chat.Id,
            message,
            parseMode: ParseMode.Html,
            cancellationToken: ct
        );
    }
}