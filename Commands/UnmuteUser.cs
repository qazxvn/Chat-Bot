using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatBot.Commands;

public class UnmuteUser : IBotCommand
{
    private readonly IDbRepository _repository;

    public UnmuteUser(IDbRepository repository)
    {
        _repository = repository;
    }
    
    public string CommandName => "/unmute";

    public async Task ExecuteCommandAsync(ITelegramBotClient botClient, Message msg, CancellationToken ct)
    {
        if (msg.ReplyToMessage is { } replyToMessage)
        {
            var isUnMute = await _repository.IsEnoughRoptaniaForUnMute(msg.From?.Id);

            if (isUnMute)
            {
                await botClient.RestrictChatMember(
                    chatId: msg.Chat.Id,
                    userId: replyToMessage.From.Id,
                    new ChatPermissions
                    {
                        CanSendMessages = true,
                        CanSendAudios = true,
                        CanSendDocuments = true,
                        CanSendPhotos = true,
                        CanSendVideos = true,
                        CanSendVideoNotes = true,
                        CanSendVoiceNotes = true,
                        CanSendPolls = true,
                        CanSendOtherMessages = true,
                        CanAddWebPagePreviews = true
                    },
                    cancellationToken: ct
                );

                await botClient.SendMessage(msg.Chat.Id, $"{replyToMessage.From?.FirstName} был размучен",
                    cancellationToken: ct);
            }
            else
            {
                await botClient.SendMessage(msg.Chat.Id, $"У {msg.From?.FirstName} не хватает роптаний для размута",
                    cancellationToken: ct);
            }
        }
    }
}