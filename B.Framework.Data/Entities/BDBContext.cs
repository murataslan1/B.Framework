using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace B.Framework.Data.Entities
{
    public partial class BDBContext : DbContext
    {
        public BDBContext()
        {
        }

        public BDBContext(DbContextOptions<BDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=localTest;user=sa;password=aSd415263.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ColumnName).HasMaxLength(100);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Pk)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PK");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CUrl).HasColumnName("cURL");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UserName).HasColumnName("userName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
