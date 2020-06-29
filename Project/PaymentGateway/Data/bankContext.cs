using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PaymentGateway.Data
{
    public partial class BankContext : DbContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public BankContext(string conn)
        {
            ConnectionString = conn;
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                var words = ConnectionString.Split('=');
                DatabaseName = words[words.Length - 1].Replace(";", string.Empty);
            }

        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=bank");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("card");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DayLimit).HasColumnType("decimal(7,2)");

                entity.Property(e => e.ExpiryDate)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionLimit).HasColumnType("decimal(7,2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("payment");

                entity.Property(e => e.Accepted).HasColumnType("tinyint(1)");

                entity.Property(e => e.Amount).HasColumnType("decimal(10,0)");

                entity.Property(e => e.CardId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
