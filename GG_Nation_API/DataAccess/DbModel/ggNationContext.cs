using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.DbModel
{
    public partial class ggNationContext : DbContext
    {
        public ggNationContext()
        {
        }

        public ggNationContext(DbContextOptions<ggNationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Db_MessageDetail> MessageDetails { get; set; }
        public virtual DbSet<Db_PlayerCard> PlayerCards { get; set; }
        public virtual DbSet<Db_PlayerDetail> PlayerDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ggNation;Trusted_Connection=True; Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Db_MessageDetail>(entity =>
            {
                entity.HasKey(e => e.MessageDetailsId)
                    .HasName("PK__Message___41EE4DAFE13BAE49");

                entity.ToTable("Message_Details");

                entity.Property(e => e.MessageDetailsId).HasColumnName("MessageDetailsID");

                entity.Property(e => e.MessageDate).HasColumnType("datetime");

                entity.Property(e => e.MessageDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MessagesId).HasColumnName("MessagesID");
            });

            modelBuilder.Entity<Db_PlayerCard>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK__Player_C__4A4E74A865D3C180");

                entity.ToTable("Player_Card");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.PlayerCardUid).HasColumnName("PlayerCard_UID");

                entity.Property(e => e.PlayerDisplayName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Player_Display_Name");

                entity.Property(e => e.PlayerEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Player_Email");

                entity.Property(e => e.PlayerImage).HasColumnName("Player_image");

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Player_Name");

                entity.Property(e => e.PlayerPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Player_Password");
            });

            modelBuilder.Entity<Db_PlayerDetail>(entity =>
            {
                entity.HasKey(e => e.DetailsId)
                    .HasName("PK__Player_d__BAC862ACF72EB1D5");

                entity.ToTable("Player_details");

                entity.Property(e => e.DetailsId).HasColumnName("DetailsID");

                entity.Property(e => e.MessagesId).HasColumnName("MessagesID");

                entity.Property(e => e.PlayerCardUid).HasColumnName("PlayerCard_UID");

                entity.Property(e => e.PlayerPlatform)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Player_Platform");

                entity.Property(e => e.PlayerRating).HasColumnName("Player_Rating");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
