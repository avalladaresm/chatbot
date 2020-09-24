using Microsoft.EntityFrameworkCore;

namespace chatbotapi.Models
{
    public class ChatBotContext : DbContext
    {
        public ChatBotContext(DbContextOptions<ChatBotContext> options)
            : base(options)
        {
        }

        public DbSet<ChatItem> ChatItems { get; set; }
    }
}