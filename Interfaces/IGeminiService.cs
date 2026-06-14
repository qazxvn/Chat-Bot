using Telegram.Bot.Types;

namespace ChatBot;

public interface IGeminiService
{
    Task<string> GetAiResponse(Message msg);
}