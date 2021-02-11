namespace TaskManager.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext()
            : base("name=DbContext")
        {
        }

        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Map>()
                .Property(e => e.Title)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<Task>()
                .Property(e => e.Title)
                .IsFixedLength();
        }
    }
}
