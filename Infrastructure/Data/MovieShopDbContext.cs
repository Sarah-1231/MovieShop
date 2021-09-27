using System;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet <User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        // Fluent API way of modeling the database 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Role>(ConfigureRole);

        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mg => new { mg.MovieId, mg.CastId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Casts).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(m => m.Cast).WithMany(g => g.Movies).HasForeignKey(mg => mg.CastId);
            builder.Property(t => t.Character).HasMaxLength(450);
        }


        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mg => new { mg.MovieId, mg.CrewId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Crews).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(m => m.Crew).WithMany(g => g.Movies).HasForeignKey(mg => mg.CrewId);
            builder.Property(t => t.Department).HasMaxLength(128);
            builder.Property(t => t.Job).HasMaxLength(128);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(m => m.Genre).WithMany(g => g.Movies).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // here we can model our Movie Table with our rules

            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            builder.Ignore(m => m.Rating);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);

            builder.Property(u => u.LockoutEndDate).HasDefaultValueSql("getdate()");
            builder.Property(u => u.LastLoginDateTime).HasDefaultValueSql("getdate()");

            builder.Property(u => u.TwoFactorEnabled).HasColumnType("bit");
            builder.Property(u => u.IsLocked).HasColumnType("bit");
            builder.Property(u => u.AccessFailedCount).HasColumnType("int");
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.Property(r => r.ReviewText).HasMaxLength(20000);
            builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");

        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            builder.HasIndex(p => new { p.UserId, p.MovieId }).IsUnique();
        }
        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20);
        }
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favoirte");
            builder.HasKey(f => new { f.MovieId, f.UserId });
        }

        // Func<int, int , decimal>
        // decimal method(int, int)
        // Action<int, int>
        // void method(int, int)
    }
}
