using LibraryAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.EfCore
{
    public class EF_DataContext : IdentityDbContext<ApplicationUser>
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options)
        {

        }



        public DbSet<Question> Questions { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Question>().HasKey(x => x.Id);
            modelBuilder.Entity<Folder>().HasKey(x => x.Id);

            modelBuilder.Entity<Folder>(e =>
            {
                e.Property(en => en.Name).IsRequired();
            });

            modelBuilder.Entity<Question>(e =>
            {
                e.Property(en => en.Description).IsRequired();
                e.Property(en => en.Answer).IsRequired();
            });

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Folder)
                .WithMany(f => f.Questions)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Folder)
                .WithMany(f => f.Questions)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
