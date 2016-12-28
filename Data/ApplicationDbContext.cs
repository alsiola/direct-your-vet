using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DYV.Models;
using DYV.Models.User;
using DYV.Models.PracticeViewModels;
using DYV.Models.SubscriberDashboard;
using DYV.Models.Purchase;
using DYV.Models.ClientRelations;

namespace DYV.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base (new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("<connec‌​tionstring>").Option‌​s) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Place>().Property(p => p.Latitude).HasColumnType("decimal(20,16)");
            builder.Entity<Place>().Property(p => p.Longitude).HasColumnType("decimal(20,16)");

            builder.Entity<ClientPractices>().HasKey(p => new { p.ClientUserId, p.PracticeId });
            builder.Entity<ClientPractices>().HasOne(p => p.ClientUser).WithMany(p => p.PermittedPractices).HasForeignKey(p => p.ClientUserId);
            builder.Entity<ClientPractices>().HasOne(p => p.Practice).WithMany(p => p.ClientPractices).HasForeignKey(p => p.PracticeId);

            builder.Entity<SubscriberPractice>().HasKey(p => new { p.PracticeId, p.SubscriberUserId });
            builder.Entity<SubscriberPractice>().HasOne(p => p.SubscriberUser).WithMany(p => p.SubscriberPractices).HasForeignKey(p => p.SubscriberUserId);
            builder.Entity<SubscriberPractice>().HasOne(p => p.Practice).WithMany(p => p.SubscriberPractices).HasForeignKey(p => p.PracticeId);

            builder.Entity<PlaceDayList>().HasKey(p => new { p.PlaceId, p.DayListId });
            builder.Entity<PlaceDayList>().HasOne(p => p.DayList).WithMany(p => p.PlaceDayLists).HasForeignKey(p => p.DayListId);
            builder.Entity<PlaceDayList>().HasOne(p => p.Place).WithMany().HasForeignKey(p => p.PlaceId);

            builder.Entity<ClientUser>().HasMany(p => p.Places).WithOne(p => p.ClientUser).HasForeignKey(p => p.ClientUserId);

            builder.Entity<SubscriberUser>().HasMany(p => p.DayLists).WithOne(p => p.SubscriberUser).HasForeignKey(p => p.SubscriberUserId);


            builder.Entity<EmailGroupSendResult>().HasBaseType<MessageGroupResult>();
            builder.Entity<SMSGroupSendResult>().HasBaseType<MessageGroupResult>();

            builder.Entity<MessageGroupResult>().HasOne(p => p.Practice).WithMany(p => p.MessageGroupSendResults).HasForeignKey(p => p.PracticeId);

            builder.Entity<SMSSendResult>().HasBaseType<MessageSendResult>();
            builder.Entity<EmailSendResult>().HasBaseType<MessageSendResult>();
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Practice> Practices { get; set; }

        public DbSet<DayList> DayLists { get; set; }

        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<SubscriberUser> SubscriberUsers { get; set; }

        public DbSet<ClientPractices> ClientPractices { get; set; }
        public DbSet<SubscriberPractice> SubscriberPractices { get; set; }
        public DbSet<PlaceDayList> PlaceDayLists { get; set; }

        public DbSet<UserInvite> UserInvites { get; set; }

        public DbSet<PurchaseCost> PurchaseCosts { get; set; }

        public DbSet<SMSGroupSendResult> SMSGroupSendResults { get; set; }
        public DbSet<SMSSendResult> SMSSendResults { get; set; }

        public DbSet<EmailGroupSendResult> EmailGroupSendResults { get; set; }
        public DbSet<EmailSendResult> EmailSendResults { get; set; }
    }
}
