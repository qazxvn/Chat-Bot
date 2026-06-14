using System.Net;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatBot.Commands;

public class GetRandomPlace(IApiService apiService) : IBotCommand
{
    public string CommandName => "/место";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        var place = await apiService.RandomPlacePick();
        
        var message =
            $"📍 <b>{Sanitize(place.title)}</b>\n" +
            $"<i>{Sanitize(place.subways)}</i>\n\n" +

            $"🏠 {Sanitize(place.adress)}\n\n" +

            $"📝 {Sanitize(place.description)}";
        
        await botClient.SendMessage(msg.Chat.Id, message, parseMode: ParseMode.Html, cancellationToken: ct);
    }

    public string Sanitize(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        
        var noTags = Regex.Replace(input, "<.*?>", string.Empty);
        
        return WebUtility.HtmlDecode(noTags);
    }
}