using ChatBot.Data;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Repositories;

public class DbRepository : IDbRepository
{
    private readonly AppDbContext _context;

    public DbRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddRoptanie(long? userId, string userName, long chatId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null)
        {
            var newUser = new User
            {
                UserId = userId,
                Roptania = 1,
                UserNameInDb = userName,
                ChatId = chatId
            };

            await _context.Users.AddAsync(newUser);
        }
        else
        {
            user.Roptania += 1;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> ShowUserRoptania(long? userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        return user?.Roptania ?? 0;
    }

    public async Task<bool> IsEnoughRoptaniaForMute(long? userId, int minutes)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null || user.Roptania < minutes) return false;

        user.Roptania -= minutes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsEnoughRoptaniaForUnMute(long? userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null || user.Roptania < 50) return false;

        user.Roptania -= 50;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> ShowMostRoptaniaInCurrentChat(long chatId)
    {
        var users = await _context.Users.Where(x => x.ChatId == chatId)
            .OrderByDescending(x => x.Roptania)
            .ToListAsync();
        
        return users;
    }

    public async Task<(bool canObtained, int roptania)> DailyRoptania(long? userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null) return (false, 0);

        if (user.LastDailyRoptania == null || user.LastDailyRoptania.AddDays(1) <= DateTime.UtcNow)
        {
            var random = new Random();

            var roptania = random.Next(1, 100);
            
            user.Roptania += roptania;
            user.LastDailyRoptania = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return (true, roptania);
        }

        return (false, 0);
    }
}