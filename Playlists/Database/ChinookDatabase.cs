using System;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{
    public class ChinookDatabase : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            var path = AppDomain.CurrentDomain.BaseDirectory;

            optionsBuilder.UseSqlite($"Data Source={path}\\chinook.db");
        }
    }
}