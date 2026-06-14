using ChatBot.Models;

namespace ChatBot;

public interface IDbRepository
{
    Task AddRoptanie(long? userId, string userName, long chatId);
    Task<int> ShowUserRoptania(long? userId);
    Task<bool> IsEnoughRoptaniaForMute(long? userId, int minutes);
    Task<bool> IsEnoughRoptaniaForUnMute(long? userId);
    Task<List<User>> ShowMostRoptaniaInCurrentChat(long chatId);
    Task<(bool canObtained, int roptania)> DailyRoptania(long? userId);
}