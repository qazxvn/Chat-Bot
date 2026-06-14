using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class AICall(IGeminiService geminiService) : IBotCommand
{
    public string CommandName => "/ии";
    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var response = await geminiService.GetAiResponse(msg);

        await botClient.SendMessage(msg.Chat.Id, response, cancellationToken: ct);
    }
}