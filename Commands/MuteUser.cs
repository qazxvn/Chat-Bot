using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class MuteUser : IBotCommand
{
    private readonly IDbRepository _repository;

    public MuteUser(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/mute";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        if (msg.ReplyToMessage is { } replyToMessage)
        {
            if (replyToMessage.From?.Id == 6940002297)
            {
                await botClient.SendMessage(msg.Chat.Id, $"{msg.From?.Username} Пососал 👍", cancellationToken: ct);
                return;
            }
            
            var isMute = await _repository.IsEnoughRoptaniaForMute(msg.From?.Id, 50);

            if (isMute)
            {
                await botClient.RestrictChatMember(
                    chatId: msg.Chat.Id, 
                    userId: replyToMessage.From.Id,
                    new ChatPermissions
                    {
                        CanSendMessages = false
                    },
                    untilDate: DateTime.UtcNow.AddMinutes(10),
                    cancellationToken: ct
                );
                
                await botClient.SendMessage(msg.Chat.Id, $"{replyToMessage.From?.FirstName} был замучен на 10 мин",
                    cancellationToken: ct);
            }
            else
            {
                await botClient.SendMessage(msg.Chat.Id, $"У {msg.From.FirstName} не хватает роптаний", cancellationToken: ct);
            }
        }
    }
}