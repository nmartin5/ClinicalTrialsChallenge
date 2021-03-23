using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace ClinicalTrialsChallengeApi.Infrastructure.Persistence
{
    public class ClinicalTrialsDbContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public ClinicalTrialsDbContext(DbContextOptions<ClinicalTrialsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Email>()
                .HasDiscriminator<string>("email_type")
                .HasValue<ContactRequestEmail>(nameof(ContactRequestEmail))
                .HasValue<StudyRequestEmail>(nameof(StudyRequestEmail));

            modelBuilder.Entity<Email>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Email>()
            .Property(e => e.Subject).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Email>()
                .Property(e => e.Content).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Email>()
                .Property(e => e.Sent).HasConversion(dateTimeConverter).IsRequired();


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

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Base64EncodedContent).IsRequired();
            modelBuilder.Entity<Attachment>()
                .Property(e => e.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Recipient>()
                .HasKey(r => r.Address);

            modelBuilder.Entity<Recipient>()
                .Property(r => r.Address).IsRequired().HasMaxLength(75);

            modelBuilder.Entity<Recipient>()
                .Property(r => r.Name).HasMaxLength(100);
        }
    }
}
