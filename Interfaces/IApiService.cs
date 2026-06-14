namespace ChatBot;

public interface IApiService
{
    Task<(string title, string adress, string description, string subways)> RandomPlacePick();
}