using B1TestTask2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private const int CURRENCYPRECISION = 20;
        private const int CURRENCYSCALE = 2;

        public DbSet<Class> Classes { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<FileMetadata> Files { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<ClassTotals> ClassTotals { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired();
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("FileRecords");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.ActiveOpeningBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(r => r.PasiveOpeningBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(r => r.DebitTurnover)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(r => r.CreditTurnover)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(r => r.ActiveOutgoingBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(r => r.PassiveOutgoingBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.HasOne(r => r.File)
                .WithMany()
                .HasForeignKey(r => r.FileId)
                .HasPrincipalKey(f => f.Id);

                entity.HasOne(r => r.Class)
                 .WithMany()
                 .HasForeignKey(r => r.ClassId)
                 .HasPrincipalKey(c => c.Id);

            });

            modelBuilder.Entity<ClassTotals>(entity =>
            {
                entity.HasKey(ct => ct.Id);

                entity.Property(ct => ct.Subclass)
                .IsRequired(false);

                entity.Property(ct => ct.ActiveOpeningBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(ct => ct.PasiveOpeningBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(ct => ct.DebitTurnover)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(ct => ct.CreditTurnover)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(ct => ct.ActiveOutgoingBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.Property(ct => ct.PassiveOutgoingBalance)
                .HasPrecision(CURRENCYPRECISION, CURRENCYSCALE);

                entity.HasOne(ct => ct.File)
                .WithMany()
                .HasForeignKey(r => r.FileId)
                .HasPrincipalKey(f => f.Id);

                entity.HasOne(ct => ct.Class)
                .WithMany()
                .HasForeignKey(ct => ct.ClassId)
                .HasPrincipalKey(c => c.Id);
            });

            modelBuilder.Entity<FileMetadata>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.Property(f => f.FileName)
                .IsRequired();

                entity.Property(f => f.UploadDate)
                .HasDefaultValueSql("getdate()");

                entity.Property(f => f.Currency)
                .HasDefaultValue("руб.");

                entity.HasOne(f => f.Bank)
                .WithMany()
                .HasForeignKey(f => f.BankId)
                .HasPrincipalKey(b => b.Id);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Name)
                .IsRequired();
            });
        }
    }
}
