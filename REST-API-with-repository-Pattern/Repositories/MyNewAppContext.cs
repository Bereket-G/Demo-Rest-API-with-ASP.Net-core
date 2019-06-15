using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace REST_API_with_repository_Pattern.Repositories
{
    public partial class MyNewAppContext : DbContext
    {
        public MyNewAppContext()
        {
        }

        public MyNewAppContext(DbContextOptions<MyNewAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TaskList> TaskList { get; set; }
        public virtual DbSet<Todo> Todo { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=my_trello_db;Username=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('tasklist_id_seq'::regclass)");

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.LastUpdated).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('measures_measure_id_seq'::regclass)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.LastUpdated).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.TaskListId).HasColumnName("taskListId");

                entity.HasOne(d => d.TaskList)
                    .WithMany(p => p.Todo)
                    .HasForeignKey(d => d.TaskListId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("foreign_key_taskListId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.FirstName).HasColumnType("character varying");

                entity.Property(e => e.LastName).HasColumnType("character varying");

                entity.Property(e => e.LastUpdated).HasColumnType("date");

                entity.Property(e => e.UserName).HasColumnType("character varying");
            });

            modelBuilder.HasSequence("measures_measure_id_seq");

            modelBuilder.HasSequence("tasklist_id_seq");
        }
    }
}
