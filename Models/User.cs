namespace ChatBot.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public long? UserId { get; set; }
    public string UserNameInDb { get; set; }
    public int Roptania { get; set; }
    public long ChatId { get; set; }
    public DateTime LastDailyRoptania { get; set; }
}