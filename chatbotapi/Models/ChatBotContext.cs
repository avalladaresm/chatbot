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

        public DbSet<Meeting> Meeting { get; set; }

        public DbSet<TrelloCard> TrelloCard { get; set; }

        public DbSet<TrelloBoard> TrelloBoard { get; set; }

        public DbSet<TrelloList> TrelloList { get; set; }
    }
}