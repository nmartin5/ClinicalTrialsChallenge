using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrialsChallengeApi.Infrastructure.Persistence
{
    public class ClinicalTrialsDbContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public ClinicalTrialsDbContext(DbContextOptions<ClinicalTrialsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasDiscriminator<string>("email_type")
                .HasValue<ContactRequestEmail>(nameof(ContactRequestEmail))
                .HasValue<StudyRequestEmail>(nameof(StudyRequestEmail));

            modelBuilder.Entity<Email>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Email>()
                .Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<Email>()
                .HasMany(e => e.Attachments)
                .WithOne();

            modelBuilder.Entity<Email>()
                .HasOne(e => e.Recipient)
                .WithMany();

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<Attachment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Recipient>()
                .HasKey(r => r.Address);
        }
    }
}
