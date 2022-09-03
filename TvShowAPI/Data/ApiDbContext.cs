using Microsoft.EntityFrameworkCore;
using TvShowAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TvShowAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ActorSerie
            modelBuilder.Entity<ActorSerie>()
            .HasKey(t => new { t.SeriesId, t.ActorsId });

            modelBuilder.Entity<ActorSerie>()
                .HasOne(pt => pt.Serie)
                .WithMany(p => p.ActorSeries)
                .HasForeignKey(pt => pt.SeriesId);

            modelBuilder.Entity<ActorSerie>()
                .HasOne(pt => pt.Actor)
                .WithMany(t => t.ActorSeries)
                .HasForeignKey(pt => pt.ActorsId);
            #endregion

            #region ActorEpisode
            //modelBuilder.Entity<ActorEpisode>()
            //    .HasKey(t => new { t.EpisodeId, t.ActorID });

            //modelBuilder.Entity<ActorEpisode>()
            //    .HasOne(pt => pt.Episode)
            //    .WithMany(p => p.ActorEpisodes)
            //    .HasForeignKey(pt => pt.EpisodeId);

            //modelBuilder.Entity<ActorEpisode>()
            //    .HasOne(pt => pt.Actor)
            //    .WithMany(t => t.ActorEpisodes)
            //    .HasForeignKey(pt => pt.ActorID);
            #endregion

            #region SerieGenre
            modelBuilder.Entity<GenreSerie>()
           .HasKey(t => new { t.SerieId, t.GenreId });

            modelBuilder.Entity<GenreSerie>()
                .HasOne(pt => pt.Serie)
                .WithMany(p => p.GenreSeries)
                .HasForeignKey(pt => pt.SerieId);

            modelBuilder.Entity<GenreSerie>()
                .HasOne(pt => pt.Genre)
                .WithMany(t => t.GenreSeries)
                .HasForeignKey(pt => pt.GenreId);
            #endregion

            #region SerieUser
            modelBuilder.Entity<SerieUser>()
           .HasKey(t => new { t.UserID, t.SerieID });

            modelBuilder.Entity<SerieUser>()
                .HasOne(pt => pt.Serie)
                .WithMany(p => p.SerieUsers)
                .HasForeignKey(pt => pt.SerieID);

            modelBuilder.Entity<SerieUser>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.SerieUsers)
                .HasForeignKey(pt => pt.UserID);
            #endregion


        }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        public DbSet<SerieUser> SerieUser { get; set; }


    }
}
