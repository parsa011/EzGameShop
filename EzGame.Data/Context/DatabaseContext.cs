using EzGame.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EzGame.Data.Context
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext()
        {
            
        }

        public DatabaseContext(DbContextOptions options) :base(options)
        {
            
        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;Database=;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
        public new virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DollarPrice> DollarPrice { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameDiscount> GameDiscounts { get; set; }
        public virtual DbSet<GameEdition> GameEditions  { get; set; }
        public virtual DbSet<GameGenre> GameGenres { get; set; }
        public virtual DbSet<GamePlatform> GamePlatforms  { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms  { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}